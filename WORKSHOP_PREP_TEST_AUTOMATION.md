# Ways of Working Workshop — April 23rd 2026
## Test Automation Group — Preparation Notes

**Group sponsor:** Mark Cunningham
**Members:** Radha Krishna Kopparthi, Shanmugham Balamurugan, Rudrakshi Wangu
**Problem statements:** 4.2.1 · 4.2.2 · 4.2.3

---

## Environment Context

The QA team already operates across three dedicated, isolated environments:

| Environment | Purpose | Existing smoke tag |
|---|---|---|
| SIT | System Integration Testing — primary automation target | `@SIT_Smoke` |
| UAT | User Acceptance Testing | `@UAT_Smoke` |
| FAT | Factory Acceptance Testing | `@FAT_Smoke` |

Environment URLs are already parameterised in `App.config` (`SITBaseUrl`, `UATBaseUrl`) and
switched at runtime via `Environment.properties`. Feature file scenarios are already tagged
per environment. This infrastructure is workshop-ready — it does not need to be built.

---

## 4.2.1 — What criteria must be met to gate pull requests on test automation results alone?

### Current state

| Area | State |
|---|---|
| Test execution | Manual — 10 users split across 5 VMs, running subsets of the suite via RDP |
| PR gate | None — no automated trigger exists on pull request raise |
| Environments | SIT, UAT, FAT available and isolated |
| Test tagging | Environment-specific tags already in place (`@SIT_Smoke`, `@P1_A`, `@Regression`) |
| Suite stability | Known flakiness — hard sleeps, inconsistent wait step spellings, process-killing (being addressed) |
| Reporting | ExtentReports HTML saved locally per run — not published back to a PR |

### Proposed PR gate definition

> A pull request may only merge when the `@SIT_Smoke` scenario subset passes 100%
> in the CI pipeline against the SIT environment. Any failure blocks the merge.
> The run must complete within 30 minutes.

### Six criteria that must all be true before the gate is credible

| # | Criterion | Current state | Action required |
|---|---|---|---|
| 1 | **A stable smoke subset exists** | `@SIT_Smoke` tag exists but scope needs formal agreement | Agree which scenarios constitute the gate — P1_A smoke scenarios only |
| 2 | **Suite is not flaky** | Hard sleeps, inconsistent wait spellings causing intermittent failures | Complete framework optimisation (Issues 2 and 5 in `TEST_CODE_ANALYSIS.md`) before trusting the gate |
| 3 | **A CI/CD pipeline triggers tests on PR raise** | No pipeline trigger — execution is fully manual | Azure DevOps pipeline wired to trigger `@SIT_Smoke` run when PR raised to target branch |
| 4 | **Results reported back to the PR** | ExtentReports saved locally only | Pipeline publishes NUnit XML results to Azure DevOps Test Results tab — pass/fail visible on the PR |
| 5 | **A D365 service account exists for pipeline use** | Currently each user logs in manually with personal credentials | A dedicated non-personal D365 service account with appropriate roles — **likely HMRC-side blocker** |
| 6 | **Pass/fail threshold is formally defined** | No threshold — judged by manual inspection | Define: 100% pass on smoke suite = gate passes. Any failure = PR blocked, no exceptions |

### Proposed smoke suite scope

| Tag | Scenarios included | Estimated run time |
|---|---|---|
| `@SIT_Smoke` | Existing SIT smoke scenarios | To be baselined |
| `@P1_A` | Priority 1 critical path scenarios | To be reviewed — may overlap with above |

> **Recommendation:** Run a timed baseline of `@SIT_Smoke` against SIT before the workshop
> to establish actual execution time. If it exceeds 30 minutes, a smaller `@PRGate` subset
> should be carved out explicitly.

---

## 4.2.2 — How do we reach a world where tests submitted alongside PRs introducing new functionality?

This requires a change to **process and Definition of Done**, not just tooling.

### The shift required

| Today | Target |
|---|---|
| Feature built → QA writes tests separately → tests added later | Feature file written at refinement → shipped in same PR as D365 change |
| Tests are a QA-only responsibility | Developers contribute to or review feature files as part of their PR |
| No test requirement to merge | PR blocked until scenario passes in CI |

### Proposed Definition of Done additions

| DoD Item | Owner |
|---|---|
| Gherkin scenario written and peer-reviewed before sprint start | QA + BA |
| Step definitions implemented or reused from `CommonSteps.cs` | QA |
| Page object updated for any new UI elements introduced | QA |
| Test data added to Excel file and committed | QA |
| Scenario tagged correctly (`@Regression`, `@SIT_Smoke`, priority tag) | QA |
| Scenario passes in CI pipeline against SIT | Pipeline (automated) |
| No regression in `@SIT_Smoke` suite | Pipeline (automated) |

### Proposed PR template checklist

To be added to the Azure DevOps PR template:

```
## Test Automation Checklist
- [ ] Gherkin scenario included for new functionality
- [ ] Scenario tagged with correct environment and priority tags
- [ ] Step definitions added or reused — no duplicate bindings introduced
- [ ] Test data added to TestData.xlsx and committed
- [ ] CI pipeline passed on SIT_Smoke suite
```

### HMRC-side blockers for 4.2.2

| Blocker | Impact | Owner |
|---|---|---|
| No D365 service account for pipeline | Pipeline cannot authenticate to Dynamics — tests cannot run in CI without it | HMRC security / Active Directory team |
| Azure DevOps pipeline permissions | Team needs permission to create and configure pipeline triggers for test execution | HMRC DevOps / platform team |
| D365 browser session policy | Some Dynamics orgs restrict non-interactive or headless browser sessions | HMRC D365 admin |
| Test data governance | `TestData.xlsx` contains real reference data — a synthetic data policy may be needed for pipeline use | HMRC data governance team |

---

## 4.2.3 — Radha's Suggestions (proposed content)

### Suggestion 1 — Migrate from Selenium to Playwright on .NET 8

The current Selenium framework has structural constraints that will limit any CI/CD ambition:
- Static `DriverHelper.Driver` is not thread-safe
- Manual wait management causes flaky results across environments with different latency
- `PDF_Utility` screenshot code duplicated hundreds of times in step definitions

Playwright eliminates these by design:
- Each scenario gets its own scoped `IPage` — isolated, clean, no static state
- Auto-wait on every action — hard sleeps become unnecessary
- Built-in trace viewer, screenshots and video recording — no step-level evidence code needed
- Browser contexts are isolated by default — no shared profile conflicts

A full migration recommendation and phased plan has been prepared: see `MIGRATION_RECOMMENDATION.md`.

**Proposed ask for R1.11:** Approve the Playwright migration as a programme objective.
The BDD layer (all 23 feature files and Gherkin scenarios) survives unchanged — only the
browser automation layer and page objects change.

### Suggestion 2 — Framework optimisation as a prerequisite to the CI gate

The existing framework has known quality issues that will cause the PR gate to produce
false failures if not resolved first. A full analysis is documented in `TEST_CODE_ANALYSIS.md`.

Highest-priority items before enabling the gate:

| Issue | Risk to gate | Status |
|---|---|---|
| Three spellings of the same wait step — three different bindings | Silent misbinding — wrong wait method called silently | In progress (pilot fix done on BorderlineNDRToCT.feature) |
| Hard sleeps (`waits for 'N' secs`) across all feature files | Different VM/network latency means same sleep fails on some environments | Open |
| Commented-out steps accumulating in feature files | Noise — obscures intent during review | Open |
| Duplicate `Given`/`When` step definitions | Two methods to maintain in sync | Open |

### Suggestion 3 — Formal test ownership model

Currently all 10 users run tests manually with no single owner of the suite health.
For a CI gate to work, ownership must be defined:

| Role | Responsibility |
|---|---|
| Test Automation Lead | Owns `@SIT_Smoke` suite health — investigates CI failures within 24hrs |
| Feature QA | Writes and owns scenarios for their feature area |
| Pipeline owner | Maintains Azure DevOps pipeline configuration |
| Test data owner | Maintains `TestData.xlsx` and DB query accuracy |

### Suggestion 4 — Leverage existing environment tagging

The framework already has environment-specific tags on scenarios. This means a
single codebase can target different environments by tag at runtime — no separate
test projects needed.

| Pipeline trigger | Tag filter | Target environment |
|---|---|---|
| PR raised to `main` | `@SIT_Smoke` | SIT |
| Release to UAT | `@UAT_Smoke` | UAT |
| Release to FAT | `@FAT_Smoke` | FAT |
| Full regression | `@Regression` | SIT or UAT |

This tag-to-environment mapping should be formally documented and agreed as a programme
standard at the workshop.

---

## HMRC-Side Blockers — Consolidated List

*To be presented to the VOA Assurance lead for resolution heading into R1.11.*

| # | Blocker | Problem statements affected | Lead time estimate |
|---|---|---|---|
| 1 | D365 non-personal service account for CI pipeline execution | 4.2.1, 4.2.2 | High — security approval process |
| 2 | Azure DevOps pipeline creation and trigger permissions | 4.2.1, 4.2.2 | Medium — DevOps team request |
| 3 | D365 browser/session policy for automated login | 4.2.1, 4.2.2 | Medium — D365 admin config |
| 4 | Test data governance policy for pipeline-executed tests | 4.2.2, 4.2.3 | High — data governance approval |

---

## How Current Work Feeds Into the Workshop

The test automation team is not starting from zero. Work already completed or underway
provides concrete evidence and proposals to bring to the session:

| Workshop objective | Evidence / artefact ready |
|---|---|
| Framework stability for PR gate | `TEST_CODE_ANALYSIS.md` — 12 issues identified, 2 resolved, pilot fix underway |
| Migration recommendation | `MIGRATION_RECOMMENDATION.md` — full Playwright + .NET 8 proposal with phased plan |
| Optimisation summary | `README.md` — Phase 1, 2, 3 changes documented with before/after |
| Environment separation | Already in place — SIT, UAT, FAT with corresponding smoke tags |
| Parallel execution resolved | `reqnroll.json` corrected — in-process threading removed, aligned to 10-user model |

---

## Confidence Vote Preparation

The session closes with a 1–5 confidence vote: *"How confident do you feel in being able
to implement the presented ideal solutions by end of R1.9?"*

| If blockers 1–4 above are resolved by HMRC | Confidence |
|---|---|
| All four resolved | 4 / 5 — framework work already underway, pipeline wiring is the remaining piece |
| Blockers 1 and 3 unresolved (service account + session policy) | 2 / 5 — CI gate cannot run without D365 login working in pipeline |
| All four unresolved | 1 / 5 — manual execution only, no PR gate achievable |

**The single most important blocker to surface on April 23rd is Blocker 1 —
the D365 service account for pipeline execution.** Everything else can be built
around it; without it, automated CI is not possible regardless of framework quality.
