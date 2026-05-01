# Ways of Working Workshop — April 23rd 2026
## Test Automation Group — Preparation Notes

**Group sponsor:** Mark Cunningham
**Members:** Radha Krishna Kopparthi, Shanmugham Balamurugan, Rudrakshi Wangu
**Problem statements:** 4.2.1 · 4.2.2 · 4.2.3

---

## Execution Environment

| Property | Detail |
|---|---|
| **Infrastructure** | 5 VMs, 2 users per VM (10 users total) |
| **Access method** | Remote Desktop (RDP) from local laptops |
| **Test distribution** | Test cases split manually between 10 users |
| **Environments** | SIT, UAT, FAT, PPE, MO, TRN — all isolated and independently configurable |
| **CI Pipeline** | Azure DevOps pipeline already exists — agent pool `VOA-TESTTEAM-AUTOMATION` |
| **Credentials** | Variable groups `D365_SIT_Regression`, `D365_UAT_Regression` etc. already provisioned |

---

## CI Pipeline — Current State Assessment

An Azure DevOps pipeline (`ymal`) already exists and is partially functional.
This resolves the majority of blockers anticipated before reviewing the pipeline file.

### What is already working

| Capability | Evidence in pipeline |
|---|---|
| Agent pool provisioned | `pool: name: VOA-TESTTEAM-AUTOMATION` |
| Multi-environment support | Dropdown parameter: FAT, SIT, UAT, PPE, MO, TRN |
| Environment credentials secured | Variable groups `D365_${{ parameters.Environment }}_Regression` |
| Service account for pipeline login | `az login --username '$(username)' --password '$(password)'` from variable group |
| Environment tag filtering | `testFiltercriteria: '${{ parameters.Environment_val }}'` — maps to `@SIT_Smoke` etc. |
| NuGet restore and build | `NuGetCommand@2` and `VSBuild@1` tasks wired up |
| Test result publishing intent | `PublishTestResults@2` and `CopyFiles@2` present |

### Issues found in the pipeline — must fix before enabling PR gate

| # | Issue | Severity | Detail |
|---|---|---|---|
| 1 | `runInParallel: true` will crash the framework | Critical | `VSTest` spins up multiple threads; `DriverHelper.Driver` is a static field — two threads overwrite each other's driver mid-test causing `NullReferenceException` or `NoSuchSessionException` |
| 2 | `PublishTestResults` format mismatch | High | Format is set to `NUnit` (expects `.xml`) but path points to `Report.html` (ExtentReports HTML) — results never appear in Azure DevOps |
| 3 | `rerunFailedTests: true` masks flakiness | Medium | Automatically retrying failed tests hides instability — a broken scenario can pass on retry and the PR merges with broken code |
| 4 | `App.config` replacement is fragile | Medium | Regex only matches `value="SIT"` — silently fails if the committed value is anything else, running all tests against the wrong environment |
| 5 | No PR trigger defined | Medium | Pipeline is manually triggered only — no `pr:` section exists to auto-fire on pull request raise |
| 6 | `CopyFiles` without `PublishBuildArtifacts` | Low | HTML report is copied to staging directory but never published — disappears after the run |
| 7 | `testRunTitle` parameter defined but never used | Low | Parameter declared at top but not referenced in any task |
| 8 | Branch hardcoded in checkout | Low | `@CT_ReqNRoll_Migration` hardcoded — must be updated when work merges to `main` |

---

## 4.2.1 — What criteria must be met to gate pull requests on test automation results alone?

### Proposed PR gate definition

> A pull request may only merge when the `@SIT_Smoke` scenario subset passes 100%
> in the CI pipeline against the SIT environment. Any failure blocks the merge.
> The run must complete within 30 minutes.

### Six criteria that must all be true before the gate is credible

| # | Criterion | Current state | Action required |
|---|---|---|---|
| 1 | **Stable smoke subset exists** | `@SIT_Smoke` tag exists and is wired into pipeline filter | Formally agree which scenarios are in scope — P1_A smoke scenarios — and baseline execution time |
| 2 | **Suite is not flaky** | `rerunFailedTests: true` in pipeline confirms known flakiness | Complete framework optimisation (Issues 2 and 5 in `TEST_CODE_ANALYSIS.md`) then remove the rerun setting |
| 3 | **CI pipeline triggers on PR raise** | Pipeline exists but is manually triggered only | Add `pr:` trigger section to YAML targeting `main` and `develop` branches |
| 4 | **Results visible on the PR** | `PublishTestResults` points at wrong file format — results not appearing | Fix `testResultsFiles` to point at NUnit XML output, not ExtentReports HTML |
| 5 | **Parallel execution disabled** | `runInParallel: true` — will crash static Driver | Set `runInParallel: false` immediately |
| 6 | **Pass/fail threshold defined** | `continueOnError: true` on VSTest + `failTaskOnFailedTests: true` on PublishTestResults — inconsistent | Remove `continueOnError: true` from VSTest so a test failure fails the pipeline at source |

### Proposed smoke suite scope

| Pipeline parameter | Tag filter | Target environment | Purpose |
|---|---|---|---|
| PR gate | `Category=SIT_Smoke` | SIT | Blocks merge on failure |
| Release to UAT | `Category=UAT_Smoke` | UAT | Gates UAT deployment |
| Release to FAT | `Category=FAT_Smoke` | FAT | Gates FAT deployment |
| Full regression | `Category=Regression` | SIT or UAT | Scheduled nightly run |

---

## 4.2.2 — How do we reach a world where tests are submitted alongside PRs introducing new functionality?

### The shift required

| Today | Target |
|---|---|
| Feature built → QA writes tests separately | Feature file written at refinement → ships in same PR as D365 change |
| Tests are a QA-only responsibility | Developers contribute to or review feature files as part of their PR |
| No automated test requirement to merge | PR blocked until `@SIT_Smoke` passes in CI |
| Pipeline triggered manually | Pipeline triggers automatically on PR raise |

### Proposed Definition of Done additions

| DoD Item | Owner |
|---|---|
| Gherkin scenario written and peer-reviewed before sprint start | QA + BA |
| Step definitions implemented or reused from `CommonSteps.cs` | QA |
| Page object updated for any new UI elements introduced | QA |
| Test data added to `TestData.xlsx` and committed | QA |
| Scenario tagged correctly (`@Regression`, `@SIT_Smoke`, priority tag) | QA |
| No duplicate step bindings introduced | QA |
| Scenario passes in CI pipeline against SIT | Pipeline (automated) |
| No regression in `@SIT_Smoke` suite | Pipeline (automated) |

### Proposed Azure DevOps PR template checklist

```
## Test Automation Checklist
- [ ] Gherkin scenario included for new functionality
- [ ] Scenario tagged with correct environment and priority tags
- [ ] Step definitions added or reused — no duplicate bindings introduced
- [ ] Test data added to TestData.xlsx and committed
- [ ] CI pipeline passed on SIT_Smoke suite
```

### HMRC-side blockers for 4.2.2

| Blocker | Revised status |
|---|---|
| D365 service account for pipeline | Already resolved — credentials in variable groups |
| Azure DevOps pipeline permissions | Already resolved — pipeline and agent pool exist |
| D365 browser session policy | Already resolved — pipeline executing successfully |
| Test data governance for pipeline | Open — `TestData.xlsx` contains reference data; synthetic data policy may be needed |

---

## 4.2.3 — Radha's Suggestions

### Suggestion 1 — Fix the three critical pipeline issues before enabling the PR gate

The pipeline exists and is close to production-ready. Three fixes are blocking it from being
used as a PR gate:

**Fix 1 — Disable parallel execution (Critical)**
```yaml
# Before
runInParallel: true

# After
runInParallel: false
```
`DriverHelper.Driver` is a static field. Parallel VSTest threads share it and crash each other.

**Fix 2 — Correct the PublishTestResults path (High)**
```yaml
# Before
testResultsFormat: 'NUnit'
testResultsFiles: '...Report.html'   # HTML is not NUnit format

# After
testResultsFormat: 'NUnit'
testResultsFiles: '**\TestResults\*.xml'
searchFolder: '$(System.DefaultWorkingDirectory)'
```

**Fix 3 — Add PR trigger (Medium)**
```yaml
# Add at top of pipeline file
pr:
  branches:
    include:
      - main
      - develop
```

**Fix 4 — Robust App.config environment replacement (Medium)**
```yaml
# Before — only works if current value is exactly "SIT"
-replace '<add key="EnvironmentVal" value="SIT"/>' ,'<add key="EnvironmentVal" value="$(Environment)"/>'

# After — works regardless of current value
(Get-Content $path -Raw) -replace '(<add key="EnvironmentVal" value=")[^"]*("/>)', "`$1${{ parameters.Environment }}`$2" | Set-Content $path
```

### Suggestion 2 — Migrate from Selenium to Playwright on .NET 8

The current Selenium framework has structural constraints that will limit CI/CD ambition:
- Static `DriverHelper.Driver` — requires `runInParallel: false` in the pipeline
- Manual wait management — flaky results across environments with different latency
- `PDF_Utility` screenshot code duplicated hundreds of times in step definitions

Playwright eliminates these by design — each scenario gets its own scoped `IPage`,
auto-wait on every action, built-in tracing. The full recommendation and phased plan
is in `MIGRATION_RECOMMENDATION.md`. **Proposed ask for R1.11:** approve migration as
a programme objective. All 23 feature files and Gherkin scenarios survive unchanged.

### Suggestion 3 — Framework optimisation as a prerequisite to removing test rerun

`rerunFailedTests: true` in the pipeline is a sign of known test flakiness.
Before this can be removed (which it must be for a trustworthy PR gate), the
framework issues causing instability must be resolved. A full analysis is in
`TEST_CODE_ANALYSIS.md`. Highest priority items:

| Issue | Risk to gate | Status |
|---|---|---|
| Three spellings of the same wait step | Wrong wait method called silently — intermittent failures | In progress — pilot fix done on BorderlineNDRToCT.feature |
| Hard sleeps across all feature files | Different VM/network latency per environment — same sleep unreliable | Open |
| `runInParallel: true` in pipeline | Crashes static Driver — must fix immediately | Identified — fix is one line |
| `PublishTestResults` format mismatch | Test results never appear in Azure DevOps | Identified — fix is a path change |

### Suggestion 4 — Leverage existing environment tagging

The tag-to-environment mapping is already built — formalise it as a programme standard:

| Pipeline trigger | Tag filter | Target environment |
|---|---|---|
| PR raised to `main` | `Category=SIT_Smoke` | SIT |
| Release to UAT | `Category=UAT_Smoke` | UAT |
| Release to FAT | `Category=FAT_Smoke` | FAT |
| Full regression (nightly) | `Category=Regression` | SIT |

### Suggestion 5 — Formal test ownership model

| Role | Responsibility |
|---|---|
| Test Automation Lead | Owns `@SIT_Smoke` suite health — investigates CI failures within 24hrs |
| Feature QA | Writes and owns scenarios for their feature area |
| Pipeline owner | Maintains Azure DevOps YAML — applies fixes from this document |
| Test data owner | Maintains `TestData.xlsx` and DB query accuracy |

---

## HMRC-Side Blockers — Consolidated List

*To be presented to the VOA Assurance lead for resolution heading into R1.11.*

| # | Blocker | Revised status | Remaining action |
|---|---|---|---|
| 1 | D365 service account for CI pipeline execution | **Resolved** — credentials in variable groups | None |
| 2 | Azure DevOps pipeline creation and trigger permissions | **Resolved** — pipeline and agent pool exist | Add `pr:` trigger to YAML |
| 3 | D365 browser/session policy for automated login | **Resolved** — pipeline executing successfully | None |
| 4 | Test data governance policy for pipeline-executed tests | **Open** | Agree synthetic/reference data policy for CI runs |

---

## How Current Work Feeds Into the Workshop

| Workshop objective | Evidence / artefact ready |
|---|---|
| Framework stability for PR gate | `TEST_CODE_ANALYSIS.md` — 12 issues identified, 2 resolved, pilot fix underway |
| Migration recommendation ready | `MIGRATION_RECOMMENDATION.md` — full Playwright + .NET 8 proposal with phased plan |
| Optimisation summary | `README.md` — Phase 1, 2 and 3 changes documented with before/after |
| Environment separation | Already in place — SIT, UAT, FAT, PPE, MO, TRN with corresponding smoke tags |
| Parallel execution resolved | `reqnroll.json` corrected — in-process threading removed |
| CI pipeline exists | Azure DevOps pipeline with agent pool, variable groups and tag filter in place |
| Pipeline issues documented | 8 issues identified — 3 critical fixes needed before PR gate can be enabled |

---

## Confidence Vote Preparation

The session closes with a 1–5 vote: *"How confident do you feel in being able to implement
the presented ideal solutions by end of R1.9?"*

### Before reviewing the pipeline — estimated confidence: 2 / 5
*(Service account, pipeline, environments all assumed to be HMRC blockers)*

### After reviewing the pipeline — revised confidence: 4 / 5

| What is already done | What remains |
|---|---|
| Agent pool provisioned | `runInParallel: false` — one line change |
| Service account credentials secured in variable groups | `PublishTestResults` path fix — one line change |
| Multi-environment support built | Add `pr:` trigger — three lines |
| Tag-based test filtering working | App.config replacement made robust |
| NuGet, build, test execution all wired | Framework flakiness resolved (in progress) |
| | `rerunFailedTests` removed once suite is stable |

> **The single most important action before April 23rd:**
> Apply the three critical pipeline fixes (`runInParallel`, `PublishTestResults` path,
> `pr:` trigger) and run a timed baseline of `@SIT_Smoke` to establish the execution
> time. That single number — how long the smoke suite takes — is what the group
> needs to answer 4.2.1 with confidence.
