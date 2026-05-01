# Test Code Quality Analysis
## Features · Steps · Pages

---

## Summary Table

| # | Area | Issue | Files Affected | Priority |
|---|---|---|---|---|
| 1 | Steps | `PDF_Utility` try-catch block copy-pasted into every step method | All step files | High |
| 2 | Steps | Three different spellings of the same wait step create three separate bindings | `CommonSteps.cs` + all `.feature` files | High |
| 3 | Features | No `Background:` blocks — identical setup steps repeated in every scenario | All `.feature` files | High |
| 4 | Steps | Duplicate step definitions for identical actions (`Given`/`When` same text, same body) | `CommonSteps.cs` | Medium |
| 5 | Features | Hard `waits for 'N' secs` mixed with dynamic waits | All `.feature` files | Medium |
| 6 | Steps | Micro step files (2–3 methods each) add file noise with no organisational value | `CompositePropertyChangeSteps.cs`, `PartDemolitionSteps.cs`, `PADEntrySteps.cs`, `CTInformalChallengeSteps.cs` | Medium |
| 7 | Features | Commented-out steps accumulating throughout feature files | All `.feature` files | Low |
| 8 | Features | Scenario names embed ticket IDs instead of describing what is verified | All `.feature` files | Low |
| 9 | Steps | `CommonPage` instantiated inside every step body instead of once as a field | `CommonSteps.cs` | Low |
| 10 | Features | Assertions written as `Given`/`And` instead of `Then` | All `.feature` files | Low |

---

## Detailed Findings

---

### Issue 1 — `PDF_Utility` try-catch block copy-pasted into every step method
**Area:** Steps &nbsp;|&nbsp; **Priority:** High

| | Detail |
|---|---|
| **What** | Every step method in every step file wraps its body in an identical try-catch block that instantiates `PDF_Utility` twice — once on success, once on failure |
| **Where** | `CommonSteps.cs`, `LoginSteps.cs`, `RequestSteps.cs`, and all other step files — hundreds of occurrences |
| **Why it matters** | Duplicates ~8 lines per method; the failure screenshot is already captured by `AfterStep` in `Hooks.cs` via Extent Reports, making the catch-block screenshot redundant |
| **Example (before)** | See below |
| **Recommendation** | Remove `PDF_Utility` from all step methods; centralise in `AfterStep` if the PDF evidence is still needed |

**Before — repeated in every step method:**
```csharp
try
{
    DoSomething();
    var pdf_Util = new PDF_Utility();
    pdf_Util.takeScreenshot();
}
catch (Exception e)
{
    var pdf_Util = new PDF_Utility();
    pdf_Util.takeScreenshot();
    pdf_Util.exceptionPdFLogger(e);
    throw new Exception("Exception details : " + e.Message);
}
```

**After — clean step body:**
```csharp
[Given(@"User navigates to '(.*)' under '(.*)'")]
public void GivenUserNavigatesTo(string menuItem, string section)
{
    NavigateToMenuItem(section, menuItem);
}
```

---

### Issue 2 — Three spellings of the same wait step create three separate bindings
**Area:** Steps &nbsp;|&nbsp; **Priority:** High

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
**Area:** Features &nbsp;|&nbsp; **Priority:** High

| | Detail |
|---|---|
| **What** | Every scenario in every feature file starts with the same 2–3 setup steps |
| **Where** | All `.feature` files |
| **Why it matters** | Setup intent is buried inside every scenario; adding a new scenario means copy-pasting the setup block; a change to the login step must be made in every scenario |

**Before — repeated at the top of every scenario:**
```gherkin
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

**After — shared setup extracted to `Background:`:**
```gherkin
Background:
    And User collapse if site map navigation expanded on left pane

Scenario: NP01_...
    Given User uses test data with ID 'NP_024' from 'NewProperty' sheet
    And User is logged in to Dynamics application to work for "NewProp_E2E_Flow" case
    ...
```

> **Note:** Where the test data ID or case name varies per scenario, use `Scenario Outline` with an `Examples:` table rather than repeating the setup.

---

### Issue 4 — Duplicate step definitions for identical actions
**Area:** Steps &nbsp;|&nbsp; **Priority:** Medium

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
**Area:** Features &nbsp;|&nbsp; **Priority:** Medium

| | Detail |
|---|---|
| **What** | Fixed-duration sleeps are used alongside proper condition-based waits |
| **Where** | All `.feature` files |
| **Why it matters** | Hard sleeps add fixed time to every run regardless of application state; they slow the suite on fast machines and cause flakiness on slow ones |

| Hard sleep (bad) | Dynamic wait (good) |
|---|---|
| `And user waits for '10' secs` | `And user waits till progress indicator disappears` |
| `And user waits for '5' secs` | `And user waits till 'Saving...' progressbar disappears` |
| `And user waits for '2' secs` | `And User waits till ProgressRing disappears` |

**Recommendation:** Replace every `waits for 'N' secs` with a polling wait on a visible UI condition. Where no condition exists, investigate why the test needs to pause — usually it signals a missing wait condition in the page object.

---

### Issue 6 — Micro step files with 2–3 methods each
**Area:** Steps &nbsp;|&nbsp; **Priority:** Medium

| File | Methods | Lines | Recommendation |
|---|---|---|---|
| `CompositePropertyChangeSteps.cs` | 2 | 38 | Move to `CommonSteps.cs` or a domain-specific file |
| `PartDemolitionSteps.cs` | 2 | 37 | Move to `CommonSteps.cs` or `FullDemolitionSteps.cs` |
| `PADEntrySteps.cs` | 1 | 26 | Move to `CommonSteps.cs` |
| `CTInformalChallengeSteps.cs` | ~4 | 45 | Merge into `InformalChallengeSteps.cs` if it exists, else `CommonSteps.cs` |

Files with genuine domain cohesion and meaningful size (e.g., `EstateFileSteps.cs`, `LAPortal.cs`, `RequestSteps.cs`) are fine as separate files.

---

### Issue 7 — Commented-out steps accumulating in feature files
**Area:** Features &nbsp;|&nbsp; **Priority:** Low

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
**Area:** Features &nbsp;|&nbsp; **Priority:** Low

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
**Area:** Steps &nbsp;|&nbsp; **Priority:** Low

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

**After — single field, injected once:**
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
**Area:** Features &nbsp;|&nbsp; **Priority:** Low

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

## Recommended Implementation Order

| Order | Issue | Effort | Impact |
|---|---|---|---|
| 1 | Issue 2 — Standardise wait step spelling | Low | High — fixes silent duplicate bindings immediately |
| 2 | Issue 4 — Collapse duplicate `Given`/`When` step definitions | Low | Medium — reduces CommonSteps size |
| 3 | Issue 6 — Merge micro step files | Low | Medium — reduces file noise |
| 4 | Issue 7 — Delete commented-out steps | Low | Low — improves readability |
| 5 | Issue 8 — Rename scenarios (remove ticket IDs) | Low | Low — improves report readability |
| 6 | Issue 3 — Add `Background:` blocks to feature files | Medium | High — removes repetition across all features |
| 7 | Issue 5 — Replace hard sleeps with dynamic waits | Medium | High — improves run speed and stability |
| 8 | Issue 9 — Promote `CommonPage` to a field | Medium | Low — minor cleanup |
| 9 | Issue 10 — Fix `Given`/`Then` keyword misuse | Medium | Low — improves readability |
| 10 | Issue 1 — Remove `PDF_Utility` from step methods | High | High — biggest noise reduction, do last once PDF strategy is decided |
