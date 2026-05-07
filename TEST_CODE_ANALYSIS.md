# Test Code Quality Analysis
## Features · Steps · Pages

---

## Execution Environment

| Property | Detail |
|---|---|
| **Infrastructure** | 5 VMs, 2 users per VM (10 users total) |
| **Access method** | Remote Desktop (RDP) from local laptops — browser and VS are visible on the VM |
| **Test distribution** | Test cases split manually between the 10 users for execution |
| **Parallelism model** | Cross-user split across VMs — no in-process parallel threading required |
| **Display** | RDP sessions provide a visible display — headless mode is not needed |

---

## Summary Table

| # | Area | Issue | Files Affected | Priority | Status |
|---|---|---|---|---|---|
| 1 | Steps | `PDF_Utility` try-catch block duplicated in every step method — PDF is the primary report tool, consolidation needed not removal | All step files | High | Open |
| 2 | Steps | Three different spellings of the same wait step create three separate bindings | `CommonSteps.cs` + all `.feature` files | High | Open |
| 3 | Features | No `Background:` blocks — identical setup steps repeated in every scenario | All `.feature` files | High | Open |
| 4 | Steps | Duplicate step definitions for identical actions (`Given`/`When` same text, same body) | `CommonSteps.cs` | Medium | Open |
| 5 | Features | Hard `waits for 'N' secs` mixed with dynamic waits | All `.feature` files | **High** *(escalated — see VM note)* | Open |
| 6 | Steps | Micro step files (2–3 methods each) add file noise with no organisational value | `CompositePropertyChangeSteps.cs`, `PartDemolitionSteps.cs`, `PADEntrySteps.cs`, `CTInformalChallengeSteps.cs` | Medium | Open |
| 7 | Features | Commented-out steps accumulating throughout feature files | All `.feature` files | Low | Open |
| 8 | Features | Scenario names embed ticket IDs instead of describing what is verified | All `.feature` files | Low | Open |
| 9 | Steps | `CommonPage` instantiated inside every step body instead of once as a field | `CommonSteps.cs` | Low | Open |
| 10 | Features | Assertions written as `Given`/`And` instead of `Then` | All `.feature` files | Low | Open |
| 11 | Config | `testThreadCount: 2` in `reqnroll.json` conflicted with the 10-user execution model | `reqnroll.json` | High | **Resolved** |
| 12 | Hooks | Process-killing in `BeforeScenario` targeted all sessions on the VM — would kill second user's browser | `Hooks.cs` | High | **Resolved** |
| 13 | Hooks | ExtentReports infrastructure generates `Report.html` that the team does not use — PDF files are the only report consumed. Confirmed: `Report.html` does not exist on VMs after test runs | `Hooks.cs` | Medium | **Confirmed — Ready to Remove** |

---

## Detailed Findings

---

### Issue 1 — `PDF_Utility` try-catch block duplicated in every step method
**Area:** Steps &nbsp;|&nbsp; **Priority:** High &nbsp;|&nbsp; **Status:** Open

| | Detail |
|---|---|
| **What** | Every step method wraps its body in an identical try-catch block that instantiates `PDF_Utility` twice — once on success, once on failure |
| **Where** | `CommonSteps.cs`, `LoginSteps.cs`, `RequestSteps.cs`, and all other step files — hundreds of occurrences |
| **Important context** | PDF is the team's **primary evidence and reporting tool** — the team views PDF files in the TestResults folder, not the ExtentReports HTML. `PDF_Utility` must be kept and working. The problem is duplication, not the tool itself. |
| **Why it matters** | ~8 identical lines per method duplicated hundreds of times; `takeScreenshot()` on success and on failure serve different purposes but both belong in a central hook, not per step |
| **Recommendation** | Consolidate into `[AfterStep]` hook — one call covers all steps automatically. Keep `exceptionPdFLogger` in catch blocks only where specific exception detail adds diagnostic value beyond the screenshot |

**What each PDF_Utility call does — and where it should live:**

| Call | Purpose | Where it should live |
|---|---|---|
| `pdf_Util.takeScreenshot()` in try block | Captures page state after successful step — builds the evidence PDF | Move to `[AfterStep]` hook — fires automatically after every step |
| `pdf_Util.takeScreenshot()` in catch block | Captures page state at point of failure | Move to `[AfterStep]` hook — fires on failure too |
| `pdf_Util.exceptionPdFLogger(e)` in catch block | Logs the exception detail into the PDF | Keep in catch blocks where specific exception context is needed |

**Before — 8 lines duplicated in every step method:**
```csharp
[Given(@"User navigates to '(.*)' under '(.*)'")]
public void GivenUserNavigatesTo(string menuItem, string section)
{
    try
    {
        NavigateToMenuItem(section, menuItem);
        var pdf_Util = new PDF_Utility();
        pdf_Util.takeScreenshot();              // success screenshot
    }
    catch (Exception e)
    {
        var pdf_Util = new PDF_Utility();
        pdf_Util.takeScreenshot();              // failure screenshot
        pdf_Util.exceptionPdFLogger(e);         // exception detail
        throw new Exception("Exception details : " + e.Message);
    }
}
```

**After — step method contains only its action:**
```csharp
[Given(@"User navigates to '(.*)' under '(.*)'")]
public void GivenUserNavigatesTo(string menuItem, string section)
{
    NavigateToMenuItem(section, menuItem);
}
```

**AfterStep hook handles screenshot for all steps centrally:**
```csharp
[AfterStep]
public void AfterStep()
{
    var pdf_Util = new PDF_Utility();
    pdf_Util.takeScreenshot();   // fires after every step — success and failure

    if (_scenarioContext.TestError != null)
    {
        pdf_Util.exceptionPdFLogger(_scenarioContext.TestError);
    }
}
```

---

### Issue 2 — Three spellings of the same wait step create three separate bindings
**Area:** Steps &nbsp;|&nbsp; **Priority:** High &nbsp;|&nbsp; **Status:** Open

| | Detail |
|---|---|
| **What** | The saving wait step has three different forms in the feature files, each resolving to a different step definition method doing the same thing |
| **Where** | All `.feature` files + `CommonSteps.cs` |
| **Why it matters** | Creates three separate methods with identical implementations; a typo in a feature file silently binds to a different method |

| Variant found in feature files | Binding method in CommonSteps |
|---|---|
| `user waits till 'Saving...' 'progressbar' disappears` | Method A |
| `User waits till 'Saving...' 'progressbar' disappears` | Method B (different regex, uppercase) |
| `user wait till 'Saving...' 'progressbar' disappears` | Method C (missing 's') |

**Recommendation:** Pick one canonical form, update all feature files to use it, and delete the two redundant step definitions.

---

### Issue 3 — No `Background:` blocks — identical setup repeated in every scenario
**Area:** Features &nbsp;|&nbsp; **Priority:** High &nbsp;|&nbsp; **Status:** Open

| | Detail |
|---|---|
| **What** | Every scenario in every feature file starts with the same 2–3 setup steps |
| **Where** | All `.feature` files |
| **Why it matters** | Setup intent is buried inside every scenario; adding a new scenario means copy-pasting the setup block; a change to the login step must be made in every scenario |

**Rule:** A step belongs in `Background:` only if it is **identical across every scenario in the feature**.
Steps whose values vary per scenario must stay inside each scenario.

---

**Case A — All three steps identical across every scenario → all three go to `Background:`**

```gherkin
# Before
Scenario: NP01_...
    Given User uses test data with ID 'NP_024' from 'NewProperty' sheet
    And User is logged in to Dynamics application to work for "NewProp_E2E_Flow" case
    And User collapse if site map navigation expanded on left pane
    ...

Scenario: NP02_...
    Given User uses test data with ID 'NP_024' from 'NewProperty' sheet
    And User is logged in to Dynamics application to work for "NewProp_E2E_Flow" case
    And User collapse if site map navigation expanded on left pane
    ...
```

```gherkin
# After — all three steps are identical so all three move to Background:
Background:
    Given User uses test data with ID 'NP_024' from 'NewProperty' sheet
    And User is logged in to Dynamics application to work for "NewProp_E2E_Flow" case
    And User collapse if site map navigation expanded on left pane

Scenario: NP01_...
    ...   # starts directly with the first step unique to this scenario

Scenario: NP02_...
    ...   # starts directly with the first step unique to this scenario
```

---

**Case B — Test data ID or case name varies per scenario → only the truly shared step goes to `Background:`**

```gherkin
# Before
Scenario: NP01_...
    Given User uses test data with ID 'NP_024' from 'NewProperty' sheet
    And User is logged in to Dynamics application to work for "NewProp_CTBT_Integration" case
    And User collapse if site map navigation expanded on left pane
    ...

Scenario: NP06_...
    Given User uses test data with ID 'TD_003' from 'ReEntryNewProperty' sheet
    And User is logged in to Dynamics application to work for "NewPropReEntryDeletion_E2E_Process" case
    And User collapse if site map navigation expanded on left pane
    ...
```

```gherkin
# After — only the step that is truly the same for every scenario goes to Background:
Background:
    And User collapse if site map navigation expanded on left pane

Scenario: NP01_...
    Given User uses test data with ID 'NP_024' from 'NewProperty' sheet
    And User is logged in to Dynamics application to work for "NewProp_CTBT_Integration" case
    ...

Scenario: NP06_...
    Given User uses test data with ID 'TD_003' from 'ReEntryNewProperty' sheet
    And User is logged in to Dynamics application to work for "NewPropReEntryDeletion_E2E_Process" case
    ...
```

> **Note:** Where multiple scenarios share the same test data ID and case name, consider
> consolidating them into a `Scenario Outline` with an `Examples:` table — this removes
> the per-scenario data setup entirely.

---

### Issue 4 — Duplicate step definitions for identical actions
**Area:** Steps &nbsp;|&nbsp; **Priority:** Medium &nbsp;|&nbsp; **Status:** Open

| | Detail |
|---|---|
| **What** | The same action is bound twice — once as `[Given]` and once as `[When]` — with identical method bodies |
| **Where** | `CommonSteps.cs` (multiple occurrences) |
| **Why it matters** | Two methods must be maintained in sync; Reqnroll supports stacking multiple keyword attributes on one method |

**Before — two methods, identical bodies:**
```csharp
[Given(@"User clicks on '(.*)' on the commandbar")]
public void GivenUserClicksOnOnTheCommandbar(string option)
    => ClickCommandBarOption(option);

[When(@"User clicks on '(.*)' on the commandbar")]
public void GivenTheUserClicksOnOnTheCommandbar(string option)
    => ClickCommandBarOption(option);
```

**After — one method, two keyword attributes:**
```csharp
[Given(@"User clicks on '(.*)' on the commandbar")]
[When(@"User clicks on '(.*)' on the commandbar")]
public void UserClicksOnCommandbar(string option)
    => ClickCommandBarOption(option);
```

---

### Issue 5 — Hard `waits for 'N' secs` mixed with dynamic waits
**Area:** Features &nbsp;|&nbsp; **Priority:** High *(escalated from Medium)* &nbsp;|&nbsp; **Status:** Open

| | Detail |
|---|---|
| **What** | Fixed-duration sleeps are used alongside proper condition-based waits |
| **Where** | All `.feature` files |
| **Why it matters** | Hard sleeps add fixed time to every run regardless of application state; they slow the suite on fast machines and cause flakiness on slow ones |
| **VM context** | RDP introduces network latency between the user's laptop and the VM, and the application itself runs over the network from the VM. Both layers add timing variability that hard sleeps cannot account for — a 5-second wait that works reliably on VM1 with a fast connection may consistently time out on VM3 with a slower one. This is why priority has been escalated from Medium to High. |

| Hard sleep (bad) | Dynamic wait (good) |
|---|---|
| `And user waits for '10' secs` | `And user waits till progress indicator disappears` |
| `And user waits for '5' secs` | `And user waits till 'Saving...' progressbar disappears` |
| `And user waits for '2' secs` | `And User waits till ProgressRing disappears` |

**Recommendation:** Replace every `waits for 'N' secs` with a polling wait on a visible UI condition. Where no visible condition exists, investigate why the test needs to pause — it usually signals a missing wait condition in the page object.

---

### Issue 6 — Micro step files with 2–3 methods each
**Area:** Steps &nbsp;|&nbsp; **Priority:** Medium &nbsp;|&nbsp; **Status:** Open

| File | Methods | Lines | Recommendation |
|---|---|---|---|
| `CompositePropertyChangeSteps.cs` | 2 | 38 | Move to `CommonSteps.cs` or a domain-specific file |
| `PartDemolitionSteps.cs` | 2 | 37 | Move to `CommonSteps.cs` or `FullDemolitionSteps.cs` |
| `PADEntrySteps.cs` | 1 | 26 | Move to `CommonSteps.cs` |
| `CTInformalChallengeSteps.cs` | ~4 | 45 | Merge into `InformalChallengeSteps.cs` if it exists, else `CommonSteps.cs` |

Files with genuine domain cohesion and meaningful size (e.g., `EstateFileSteps.cs`, `LAPortal.cs`, `RequestSteps.cs`) are fine as separate files.

---

### Issue 7 — Commented-out steps accumulating in feature files
**Area:** Features &nbsp;|&nbsp; **Priority:** Low &nbsp;|&nbsp; **Status:** Open

| | Detail |
|---|---|
| **What** | Blocks of commented-out Gherkin steps left in feature files |
| **Where** | `NewPropertyIndividual.feature`, `NewPropertyEstate.feature`, and others |
| **Why it matters** | Adds noise that obscures the actual scenario flow; the removed steps are already preserved in git history |

**Example from `NewPropertyIndividual.feature`:**
```gherkin
#And User click on 'Documents' tab from 'Request Form'
#And user waits till progress indicator disappears
#And User clicks on 'Upload' button and selects 'This Request'
#And User clicks on 'Choose Files' and uploads the 'DummyUploadLarge' document
#Given User click on 'Queues' under 'Service' section
#And User filter 'Title' coloumn filter by 'equals' with 'Job Name' and 'pick'
```

**Recommendation:** Delete all commented-out steps. If a step was removed intentionally, the reason belongs in a git commit message or ticket — not as dead code in the feature file.

---

### Issue 8 — Scenario names embed ticket IDs
**Area:** Features &nbsp;|&nbsp; **Priority:** Low &nbsp;|&nbsp; **Status:** Open

| | Detail |
|---|---|
| **What** | Scenario names contain internal ticket references as prefixes |
| **Where** | All `.feature` files |
| **Why it matters** | Makes names long and hard to read in test reports; the ticket reference becomes misleading if a scenario spans multiple tickets or the ticket is closed |

**Before:**
```gherkin
Scenario: NP01_CTPRELMGMT-1637_CTPRELMGMT-1521_CTPRELMGMT-1512_CTBT Integration For New Property Individual and large file upload
```

**After:**
```gherkin
# CTPRELMGMT-1637, CTPRELMGMT-1521, CTPRELMGMT-1512
Scenario: CTBT integration for new property individual with large file upload
```

---

### Issue 9 — `CommonPage` instantiated inside every step body
**Area:** Steps &nbsp;|&nbsp; **Priority:** Low &nbsp;|&nbsp; **Status:** Open

| | Detail |
|---|---|
| **What** | A new `CommonPage` instance is created at the start of every step method that needs it |
| **Where** | `CommonSteps.cs` |
| **Why it matters** | Repetitive; `CommonPage` is stateless so one instance per step class is sufficient |

**Before — new instance per method:**
```csharp
[Given(@"user validates the correspondence link and clicks on it")]
public void GivenUserValidatesCorrespondenceLink()
{
    CommonPage commonpage = new CommonPage();
    commonpage.ValidateCorrespondenceLinkAndClick();
}
```

**After — single field, instantiated once:**
```csharp
public class CommonSteps : BasePage
{
    private readonly CommonPage _commonPage = new CommonPage();

    [Given(@"user validates the correspondence link and clicks on it")]
    public void GivenUserValidatesCorrespondenceLink()
        => _commonPage.ValidateCorrespondenceLinkAndClick();
}
```

---

### Issue 10 — Assertions written as `Given`/`And` instead of `Then`
**Area:** Features &nbsp;|&nbsp; **Priority:** Low &nbsp;|&nbsp; **Status:** Open

| | Detail |
|---|---|
| **What** | Steps that verify or assert outcomes are written using the `Given`/`And` keyword instead of `Then` |
| **Where** | All `.feature` files |
| **Why it matters** | `Given`/`When`/`Then` communicate setup / action / assertion to a reader; using `Given` for assertions makes the intent ambiguous in test reports and reviews |

| Step | Current keyword | Should be |
|---|---|---|
| `User validate value 'Pass' for 'PAD Validation Outcome' field` | `And` (after Given) | `Then` |
| `User asserts below 'Created Assessments' details` | `And` (after Given) | `Then` |
| `user validated 'Noncompliant' text` | `And` (after Given) | `Then` |
| `User asserts 'Linked Assessment' is linked with assement 'Yes'` | `And` (after Given) | `Then` |

---

### Issue 11 — `testThreadCount: 2` conflicted with the 10-user execution model
**Area:** Config &nbsp;|&nbsp; **Priority:** High &nbsp;|&nbsp; **Status:** Resolved

| | Detail |
|---|---|
| **What** | `reqnroll.json` had `testThreadCount: 2`, spinning up two test threads within a single user session |
| **Where** | `reqnroll.json` |
| **Why it mattered** | With two threads on the same user account, both point to the same browser profile directory — Chromium-based browsers lock the profile on launch, so the second thread crashes or silently falls back to a temp profile |
| **VM context** | The team's parallelism is already handled by splitting tests across 10 users on 5 VMs. In-process threading adds no benefit and introduces profile-locking conflicts |
| **Resolution** | `parallelExecution` block removed from `reqnroll.json` — Reqnroll defaults to single-threaded execution |

**Before:**
```json
{
  "parallelExecution": {
    "testThreadCount": 2
  }
}
```

**After:**
```json
{
}
```

---

### Issue 12 — Process-killing in `BeforeScenario` targeted all user sessions on the VM
**Area:** Hooks &nbsp;|&nbsp; **Priority:** High &nbsp;|&nbsp; **Status:** Resolved

| | Detail |
|---|---|
| **What** | `CloseExistingChromeInstances()` and `CloseExistingEdgeInstances()` called `Process.GetProcessesByName()` before every scenario |
| **Where** | `Hooks.cs` — `BeforeScenario` |
| **Why it mattered** | `Process.GetProcessesByName()` on Windows returns all matching processes on the machine regardless of which user session owns them. With 2 users on the same VM, User A starting a test run would kill User B's browser that is actively mid-test in their own RDP session |
| **Resolution** | Both methods and all calls removed from `Hooks.cs`. `DriverDispose()` in `AfterScenario` already handles cleanup of the current session's browser cleanly |

**Before:**
```csharp
[BeforeScenario("UI")]
public void BeforeScenario()
{
    CloseExistingEdgeInstances();   // kills ALL msedge on the machine
    Driver = DriverInitiation();
    ...
}
```

**After:**
```csharp
[BeforeScenario("UI")]
public void BeforeScenario()
{
    DriverHelper.Driver = DriverHelper.DriverInitiation();
    scenarioTest = extentTest.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
}
```

---

### Issue 13 — ExtentReports infrastructure generates an unused HTML report
**Area:** Hooks &nbsp;|&nbsp; **Priority:** Medium &nbsp;|&nbsp; **Status:** Confirmed — pending team sign-off to remove

| | Detail |
|---|---|
| **What** | The entire ExtentReports infrastructure in `Hooks.cs` is intended to generate `TestResults/Report.html` on every run |
| **Where** | `Hooks.cs` — `BeforeTestRun`, `BeforeFeature`, `BeforeScenario`, `AfterStep`, `AfterTestRun` |
| **Confirmed finding** | `Report.html` does **not exist** on VMs after test runs — ExtentReports is neither generating nor being used in practice |
| **Root cause** | Either `extent.Flush()` is not executing when a test run ends abnormally, or the path `AppDomain.CurrentDomain.BaseDirectory + @"..\..\TestResults"` resolves incorrectly on the VM. Either way the output is lost. |
| **Team reporting** | The team exclusively views PDF files generated by `PDF_Utility` in the TestResults folder. `Report.html` is not viewed by anyone including stakeholders or the pipeline. |
| **Why it matters** | Dead code runs on every single step — the entire `AfterStep` switch statement (40 lines) writes to an HTML report that is never produced. The static fields `extent`, `extentTest`, `scenarioTest` add shared-state risk for no benefit. The ExtentReports NuGet package adds dependency weight for zero return. |
| **Recommendation** | Once the team confirms no one depends on `Report.html`, remove all ExtentReports code from `Hooks.cs` and uninstall the `ExtentReports` NuGet package. No code changes should be made until sign-off is received. |

**Dead code in `Hooks.cs` that can be removed once confirmed unused:**

```csharp
// Fields — all Extent-related, all removable
public static ExtentReports extent = new ExtentReports();
private static ExtentTest extentTest;
private static ExtentTest scenarioTest;
public static string testResultsRootPath;

// BeforeTestRun — remove the reporter setup block (keep Serilog and SetBaseUrl)
var sparkReporter = new ExtentSparkReporter(...);
sparkReporter.Config.ReportName = "Automation Status Report";
sparkReporter.Config.DocumentTitle = "Automation Status Report";
sparkReporter.Config.Theme = Theme.Dark;
extent.AttachReporter(sparkReporter);

// BeforeFeature — entire body removable (logging line via Serilog can stay)
extentTest = extent.CreateTest<Feature>(context.FeatureInfo.Title.ToString());

// BeforeScenario — remove scenario node creation
scenarioTest = extentTest.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

// AfterStep — entire 40-line switch statement removable
// (replaced by PDF_Utility consolidation from Issue 1)

// AfterTestRun — remove flush (keep Log.CloseAndFlush)
extent.Flush();
```

**NuGet packages that can be uninstalled once ExtentReports is removed:**

| Package | Used for |
|---|---|
| `ExtentReports` 5.0.2 | Core reporting library |
| `AventStack.ExtentReports` | Same package (check for duplicates) |

**Confirmed:** `Report.html` does not exist on VMs after test runs — ExtentReports is neither
generating nor being used. Safe to remove immediately. Once the pipeline `PublishTestResults`
task is fixed to publish NUnit XML (per the DevOps pipeline fixes document), the Azure DevOps
Test Results tab will replace the need for a separate HTML report entirely.

---

## Recommended Implementation Order

| Order | Issue | Effort | Impact | Status |
|---|---|---|---|---|
| — | Issue 11 — Remove `testThreadCount` from `reqnroll.json` | Low | High | Resolved |
| — | Issue 12 — Remove process-killing from `BeforeScenario` | Low | High | Resolved |
| 1 | Issue 2 — Standardise wait step spelling | Low | High — fixes silent duplicate bindings immediately | Open |
| 2 | Issue 4 — Collapse duplicate `Given`/`When` step definitions | Low | Medium — reduces CommonSteps size | Open |
| 3 | Issue 6 — Merge micro step files | Low | Medium — reduces file noise | Open |
| 4 | Issue 5 — Replace hard sleeps with dynamic waits | Medium | High — critical for consistent results across VMs with varying network latency | Open |
| 5 | Issue 7 — Delete commented-out steps | Low | Low — improves readability | Open |
| 6 | Issue 8 — Rename scenarios (remove ticket IDs) | Low | Low — improves report readability | Open |
| 7 | Issue 3 — Add `Background:` blocks to feature files | Medium | High — removes repetition across all features | Open |
| 8 | Issue 9 — Promote `CommonPage` to a field | Medium | Low — minor cleanup | Open |
| 9 | Issue 10 — Fix `Given`/`Then` keyword misuse | Medium | Low — improves readability | Open |
| 10 | Issue 13 — ExtentReports confirmed unused — remove from `Hooks.cs` pending team sign-off | Low | Medium — simplifies Hooks.cs, removes dead step logging, removes unused NuGet package | Confirmed — awaiting sign-off |
| 11 | Issue 1 — Consolidate `PDF_Utility` into `AfterStep` hook | High | High — biggest noise reduction across all step files; PDF evidence is preserved and centralised | Open |
