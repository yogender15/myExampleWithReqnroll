# Azure DevOps Pipeline — Fix Instructions for DevOps Team
## File: FAT Regression Pipeline (ymal)

All changes are listed in priority order. Each entry shows the exact
existing line(s) to find, what to change them to, and why.

---

## Fix 1 — Pipeline name hardcoded to FAT
**Priority:** Medium | **Line:** 1

**Existing code:**
```yaml
name: $(Date:yyyyMMdd)$(Rev:.r)_FATRegression_Tests
```

**Replace with:**
```yaml
name: $(Date:yyyyMMdd)$(Rev:.r)_${{ parameters.Environment }}Regression_Tests
```

**Why:** The pipeline name appears in the Azure DevOps build history. Currently
every run is labelled FATRegression even when you select SIT or UAT from the
dropdown. The fix makes the name reflect the environment actually being tested.

---

## Fix 2 — Agent pinned to one specific VM
**Priority:** High | **Lines:** 42–43

**Existing code:**
```yaml
    pool:
      name: VOA-TESTTEAM-AUTOMATION
      demands:
      - Agent.Name -equals C190VOAsvrTEST2
```

**Replace with:**
```yaml
    pool:
      name: VOA-TESTTEAM-AUTOMATION
```

**Why:** The `demands` block locks every pipeline run to a single machine
(`C190VOAsvrTEST2`). If that VM is busy, rebooting, or unavailable the
pipeline queues and waits indefinitely. Removing the demand allows any
available agent in the `VOA-TESTTEAM-AUTOMATION` pool to pick up the run.
Only add a demand back if there is a specific software or hardware dependency
on that one machine.

---

## Fix 3 — Variable group hardcoded to FAT credentials
**Priority:** Critical | **Line:** 46

**Existing code:**
```yaml
    variables:
    - group: D365_FAT_Regression
```

**Replace with:**
```yaml
    variables:
    - group: D365_${{ parameters.Environment }}_Regression
```

**Why:** The variable group contains the username, password and environment
URLs used to log into Dynamics 365. This line always loads FAT credentials
regardless of which environment is selected in the pipeline dropdown.
If someone runs the pipeline with Environment = SIT, it still logs into
FAT and runs tests against FAT data. The fix loads the correct credentials
group for whichever environment is selected (e.g. `D365_SIT_Regression`,
`D365_UAT_Regression`).

---

## Fix 4 — App.config environment replacement only works when value is "SIT"
**Priority:** Medium | **Line:** 66

**Existing code:**
```yaml
          ((Get-Content -path $sourceARMPath -Raw) -replace '<add key="EnvironmentVal" value="SIT"/>' ,'<add key="EnvironmentVal" value="$(Environment)"/>') | Set-Content -Path $targetARMPath
```

**Replace with:**
```yaml
          (Get-Content -path $sourceARMPath -Raw) -replace '(<add key="EnvironmentVal" value=")[^"]*("/>)', "`$1$(Environment)`$2" | Set-Content -Path $targetARMPath
```

**Why:** The existing line searches for the exact text `value="SIT"` and
replaces it. If a developer commits the file with `value="UAT"` or any
other value, the search finds nothing and the replacement silently does
nothing — tests then run against the wrong environment with no error or
warning. The fix uses a pattern that matches any current value between
the quotes and replaces it with the selected environment, making it
reliable regardless of what value is currently in the file.

---

## Fix 5 — Test filter hardcoded to FAT_Smoke — ignores the dropdown selection
**Priority:** Critical | **Line:** 83

**Existing code:**
```yaml
        testFiltercriteria: Category=FAT_Smoke
```

**Replace with:**
```yaml
        testFiltercriteria: '${{ parameters.Environment_val }}'
```

**Why:** The pipeline has a dropdown parameter (`Environment_val`) with
options for FAT_Smoke, SIT_Smoke, UAT_Smoke etc. However the VSTest task
ignores this parameter and always runs `Category=FAT_Smoke` no matter
what the user selects. Selecting SIT_Smoke from the dropdown has no effect.
The fix wires the task up to the parameter so the correct test suite runs
for the selected environment.

---

## Fix 6 — runInParallel: true crashes the test framework
**Priority:** Critical | **Line:** 85

**Existing code:**
```yaml
        runInParallel: true
```

**Replace with:**
```yaml
        runInParallel: false
```

**Why:** Setting `runInParallel: true` tells VSTest to run multiple tests
simultaneously using multiple threads. The test framework has a single shared
browser instance (`DriverHelper.Driver` — a static field). When two threads
run at the same time they both try to control the same browser — one thread
launches a page while the other closes it, causing crashes and
`NullReferenceException` errors. This is also why `rerunFailedTests: true`
exists — to mask the resulting failures. Setting this to `false` means one
test runs at a time, which matches how the framework is designed.

---

## Fix 7 — Remove automatic test retry that hides failures
**Priority:** Medium | **Lines:** 88–89

**Existing code:**
```yaml
        rerunFailedTests: true
        rerunType: 'basedOnTestFailureCount'
```

**Replace with:** *(delete both lines entirely)*

**Why:** When a test fails, these settings automatically retry it without
telling anyone. A broken test can pass on the second attempt and the
pipeline shows green — giving a false sense of confidence. This is
especially dangerous for a PR gate where a genuinely broken scenario
should block the merge, not silently retry until it passes. The
underlying cause of failures should be fixed in the framework, not
hidden by retrying. Note: Fix 6 (disabling parallel) will resolve most
of the failures that currently require this retry.

---

## Fix 8 — continueOnError allows a failed test run to appear successful
**Priority:** Medium | **Line:** 91

**Existing code:**
```yaml
      continueOnError: true
```

**Replace with:** *(delete this line entirely)*

**Why:** `continueOnError: true` on the VSTest task means the pipeline
carries on even if tests fail. This combined with `failTaskOnFailedTests: true`
on the later PublishTestResults task creates confusing behaviour — the test
step itself shows as passed (yellow warning) even when tests failed, and
the pipeline only fails at the publish step. Removing this line makes the
test step fail immediately and clearly when tests fail, giving an accurate
signal to the team.

---

## Fix 9 — PublishTestResults pointing at HTML file instead of NUnit XML
**Priority:** High | **Line:** 96

**Existing code:**
```yaml
        testResultsFiles: '$(Pipeline.Workspace)/s/CTAzureAutomationRepo/BSTVOAQAAutomation/BSTVOAQAAutomation/TestResults/Report.html'
```

**Replace with:**
```yaml
        testResultsFiles: '**\TestResults\*.xml'
```

**Also update line 97 — searchFolder:**

**Existing code:**
```yaml
        searchFolder: '$(Pipeline.Workspace)/s/CTAzureAutomationRepo/BSTVOAQAAutomation/BSTVOAQAAutomation/TestResults'
```

**Replace with:**
```yaml
        searchFolder: '$(System.DefaultWorkingDirectory)'
```

**Why:** The `testResultsFormat` is set to `NUnit` which expects XML files
produced by the NUnit test runner. The path currently points at `Report.html`
which is the ExtentReports visual HTML report — a completely different format.
Azure DevOps cannot read HTML as NUnit XML so the Test Results tab in the
pipeline remains empty after every run. The fix points at the NUnit XML files
that the test runner actually produces in the TestResults folder.

---

## Fix 10 — HTML report is packed but never published as a build artifact
**Priority:** Low | **After line 105 — add new task**

**Existing code ends at:**
```yaml
    - task: CopyFiles@2
      displayName: 'Copy Files'
      inputs:
        SourceFolder: '$(Pipeline.Workspace)/s/CTAzureAutomationRepo/BSTVOAQAAutomation/BSTVOAQAAutomation/TestResults/'
        Contents: '*Report.html'
        TargetFolder: '$(Build.ArtifactStagingDirectory)'
```

**Add the following task immediately after:**
```yaml
    - task: PublishBuildArtifacts@1
      displayName: 'Publish HTML Report Artifact'
      inputs:
        PathtoPublish: '$(Build.ArtifactStagingDirectory)'
        ArtifactName: 'TestReport_${{ parameters.Environment }}'
        publishLocation: 'Container'
```

**Why:** The `CopyFiles` task moves the ExtentReports HTML file into a
staging area, but without a `PublishBuildArtifacts` step nothing is actually
saved. The file sits in a temporary folder and is deleted when the pipeline
agent cleans up after the run. Adding this task means the HTML report is
saved as a named artifact (`TestReport_SIT`, `TestReport_FAT` etc.) and
is accessible from the pipeline run page in Azure DevOps for as long as
the build is retained.

---

## Fix 11 — Add PR trigger so pipeline fires automatically on pull requests
**Priority:** Medium | **Add at top of file, before `parameters:`**

**Existing code has no trigger section.**

**Add the following block before the `parameters:` section:**
```yaml
pr:
  branches:
    include:
      - main
      - develop
```

**Why:** Currently the pipeline can only be run manually by pressing the
Run button in Azure DevOps. For it to act as a PR quality gate — blocking
a merge until tests pass — it must trigger automatically whenever a pull
request is raised against `main` or `develop`. This block adds that trigger.
Adjust the branch names to match your actual target branches if different.

---

## Fix 12 — Wire up unused testRunTitle parameter
**Priority:** Low | **Line:** 75 (VSTest task — add one line inside inputs)**

**Existing VSTest task inputs do not include testRunTitle.**

**Add inside the VSTest@2 `inputs:` block:**
```yaml
        testRunTitle: '${{ parameters.testRunTitle }}'
```

**Also add inside the PublishTestResults@2 `inputs:` block:**
```yaml
        testRunTitle: '${{ parameters.testRunTitle }}'
```

**Why:** The `testRunTitle` parameter is defined at the top of the pipeline
with a default value of `'Acceptance Test Run'` but is never used anywhere.
Wiring it into the VSTest and PublishTestResults tasks means the run title
appears correctly in the Azure DevOps Test Results tab, making it easier to
identify which run is which in the history.

---

## Summary of all changes for DevOps team

| # | Line(s) | Priority | Change type | One-line description |
|---|---|---|---|---|
| 1 | 1 | Medium | Replace | Parameterise pipeline name — remove hardcoded FAT |
| 2 | 42–43 | High | Delete | Remove agent demand — allow any agent in pool |
| 3 | 46 | Critical | Replace | Parameterise variable group — use selected environment |
| 4 | 66 | Medium | Replace | Robust App.config regex — works for any current value |
| 5 | 83 | Critical | Replace | Wire test filter to parameter — stop hardcoding FAT_Smoke |
| 6 | 85 | Critical | Replace | Set runInParallel to false — static Driver cannot run parallel |
| 7 | 88–89 | Medium | Delete | Remove rerunFailedTests — stop hiding flaky test failures |
| 8 | 91 | Medium | Delete | Remove continueOnError — fail the step when tests fail |
| 9 | 96–97 | High | Replace | Fix PublishTestResults path — point at NUnit XML not HTML |
| 10 | After 105 | Low | Add | Add PublishBuildArtifacts task — actually save the HTML report |
| 11 | Top of file | Medium | Add | Add pr: trigger — auto-fire pipeline on pull request |
| 12 | VSTest + PublishTestResults | Low | Add | Wire up unused testRunTitle parameter |
