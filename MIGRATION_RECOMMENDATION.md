# Framework Migration Recommendation
## BSTVOAQAAutomation — Playwright + Reqnroll on .NET 8

---

## Current Stack

| Component | Current | Version | Status |
|---|---|---|---|
| Runtime | .NET Framework | 4.7.2 | Legacy — out of mainstream support |
| BDD Framework | Reqnroll | 3.0.1 | Current — keep as-is |
| Browser Automation | Selenium WebDriver | 4.35.0 | Functional but manual wait management |
| Test Framework | NUnit | 3.13.2 | Keep as-is |
| Reporting | ExtentReports | 5.0.2 | Keep as-is |
| Logging | Serilog | 4.3.0 | Keep as-is |
| Excel Test Data | EPPlus | 4.5.3.3 | Keep as-is |
| REST API | RestSharp | 106.11.7 | Upgrade to 107+ alongside migration |
| Database | Npgsql | 8.0.2 | Keep as-is |

---

## Recommendation

> **Migrate to: Playwright (`Microsoft.Playwright`) + Reqnroll on .NET 8**

This approach preserves the entire BDD investment (all 23 feature files and Gherkin scenarios remain
unchanged) while replacing the browser automation layer with a modern, more reliable alternative.
The .NET 8 retarget comes as a natural step alongside the Playwright adoption.

---

## Layer-by-Layer Decision

| Layer | Current | Decision | Effort |
|---|---|---|---|
| Runtime | .NET Framework 4.7.2 | Migrate to .NET 8 LTS | Low — project retarget |
| BDD framework | Reqnroll 3.0.1 | Keep — already the right choice | None |
| Feature files | 23 `.feature` files, Gherkin scenarios | Keep entirely unchanged | None |
| Step definition signatures | Reqnroll `[Binding]` classes | Keep — only internal implementations change | None |
| Browser automation | Selenium WebDriver | Replace with `Microsoft.Playwright` | High — page objects rewrite |
| Wait strategy | Manual `WebDriverWait` + hard sleeps | Eliminated — Playwright auto-waits on every action | Eliminated |
| Driver lifecycle | Static `DriverHelper.Driver` | Replaced by scoped `IPage` per scenario | Redesign |
| Screenshot / evidence | Manual `PDF_Utility` in every step | Playwright built-in trace, screenshot, video | Simplified |
| Reporting | ExtentReports | Keep — or optionally switch to Playwright HTML reporter | Low |
| Logging | Serilog | Keep | None |

---

## Why Playwright Over Staying on Selenium

| Pain Point Today | Selenium | Playwright |
|---|---|---|
| Hard sleeps (`waits for 'N' secs`) | All waits managed manually — unreliable across VMs with varying network latency | Every action auto-waits for element to be ready, visible, and stable — hard sleeps become unnecessary |
| `DriverHelper.Driver` static field | One global driver shared across the run — unsafe for any future parallel use | Each scenario gets its own `IPage` instance — isolated and scoped by design |
| Screenshot on failure | `PDF_Utility` instantiated inside every step method — hundreds of copies | Built-in trace viewer, per-test screenshots, and video recording — zero step-level code |
| Iframe switching | Manual `Driver.SwitchTo().Frame()` — must switch back after every interaction | `FrameLocator` keeps the frame in scope — no switching in/out |
| `StaleElementReferenceException` | Common when the DOM updates between finding and clicking an element | Locators re-query on every interaction — stale element errors essentially eliminated |
| Cross-VM reliability | Hard sleeps sized for the slowest VM — waste time on faster VMs | Condition-based waits adapt to actual application speed on any VM |
| Debugging failures | Screenshot file on disk | Full trace file — step-by-step DOM snapshots, network calls, console logs, video |

---

## What You Keep — The BDD Investment Is Preserved

All 23 feature files and every Gherkin scenario survive **completely unchanged**.
Step definition method signatures stay identical.
Only the implementation inside the step body and the page objects change.

**Feature file — no change:**
```gherkin
And User clicks on 'Validate Request' under 'Request Action'
And user waits till progress indicator disappears
And User clicks on 'Proceed' button on 'Confirm' dialog
```

**Step definition — signature unchanged, body updated:**
```csharp
// Before (Selenium)
[Given(@"User clicks on '(.*)' under '(.*)'")]
public void UserClicksUnder(string item, string section)
{
    _requestPage.ClickUnderSection(item, section);
}

// After (Playwright) — signature identical, page object call unchanged
[Given(@"User clicks on '(.*)' under '(.*)'")]
public async Task UserClicksUnder(string item, string section)
{
    await _requestPage.ClickUnderSection(item, section);
}
```

---

## What Changes — Page Objects

The page objects are the main rewrite effort. Element interactions move from
`IWebElement` + `WebDriverWait` to Playwright `ILocator` with auto-waiting built in.

**Before (Selenium):**
```csharp
public void ClickValidateRequest()
{
    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
    var element = wait.Until(ExpectedConditions.ElementToBeClickable(
        By.XPath("//button[@aria-label='Validate Request']")));
    element.Click();
}
```

**After (Playwright):**
```csharp
public async Task ClickValidateRequest()
{
    await _page.GetByRole(AriaRole.Button, new() { Name = "Validate Request" }).ClickAsync();
    // auto-waits for button to be visible, enabled, and stable — no explicit wait needed
}
```

**Iframe handling — before (Selenium):**
```csharp
Driver.SwitchTo().Frame("assessmentFrame");
// interact with element
Driver.SwitchTo().DefaultContent();
```

**Iframe handling — after (Playwright):**
```csharp
var frame = _page.FrameLocator("#assessmentFrame");
await frame.GetByRole(AriaRole.Button, new() { Name = "Created Assessments" }).ClickAsync();
// no switching needed — frame stays in scope via FrameLocator
```

---

## Driver Lifecycle — Before and After

**Before — static global driver (Selenium):**
```csharp
// DriverHelper.cs
public static IWebDriver Driver;   // one instance for the whole run

// Hooks.cs
[BeforeScenario("UI")]
public void BeforeScenario()
{
    DriverHelper.Driver = DriverHelper.DriverInitiation();
}

[AfterScenario("UI")]
public void AfterScenario()
{
    DriverHelper.DriverDispose();
}
```

**After — scoped IPage per scenario (Playwright):**
```csharp
// PlaywrightHelper.cs
public class PlaywrightHelper
{
    public IPage Page { get; private set; }
    private IBrowser _browser;
    private IPlaywright _playwright;

    public async Task InitAsync()
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        _browser    = await _playwright.Chromium.LaunchAsync(new() { Headless = false });
        Page        = await _browser.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        await Page.CloseAsync();
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}

// Hooks.cs
[BeforeScenario("UI")]
public async Task BeforeScenario()
{
    await _playwrightHelper.InitAsync();
}

[AfterScenario("UI")]
public async Task AfterScenario()
{
    await _playwrightHelper.DisposeAsync();
}
```

---

## The One Honest Tradeoff

Playwright uses `async/await` throughout. The current Selenium codebase is fully synchronous.
Reqnroll supports async step definitions, but adopting `async Task` consistently across every
page object method and step definition is the biggest learning curve for the team.
The Playwright API itself is straightforward — it is the async pattern that requires discipline.

| | Selenium (current) | Playwright (target) |
|---|---|---|
| Step methods | `public void StepMethod()` | `public async Task StepMethod()` |
| Page object methods | `public void ClickButton()` | `public async Task ClickButton()` |
| Assertions | `Assert.IsTrue(element.Displayed)` | `await Expect(locator).ToBeVisibleAsync()` |
| Exception handling | `try/catch` synchronous | `try/catch` works the same with async/await |

---

## Suggested Migration Path — No Big Bang

| Phase | Work | Risk | Outcome |
|---|---|---|---|
| 1 | Retarget project from `.NET Framework 4.7.2` to `.NET 8` | Low | Modern runtime, C# 12 features, LTS support to Nov 2026 |
| 2 | Add `Microsoft.Playwright` NuGet package alongside Selenium — both coexist | Low | Playwright available; no existing tests broken |
| 3 | Migrate `Hooks.cs` to use `PlaywrightHelper` for driver lifecycle | Medium | One browser lifecycle replaced; Selenium still used in page objects |
| 4 | Migrate page objects feature-by-feature, retiring Selenium calls incrementally | Medium | Team builds Playwright confidence; existing suite keeps running during transition |
| 5 | Remove Selenium, `DriverHelper`, `waitHelpers`, and `PDF_Utility` once all features migrated | Low | Clean codebase with no legacy browser automation code |

---

## Problems Solved by Migration

| Problem | Solved by |
|---|---|
| Hard sleeps causing flaky results across VMs | Playwright auto-wait — condition-based, not time-based |
| `PDF_Utility` in every step method | Playwright built-in trace and screenshot — zero step-level code |
| Static `DriverHelper.Driver` thread-safety risk | `IPage` scoped per scenario via DI |
| `StaleElementReferenceException` flakiness | Playwright locators re-query on every use |
| Manual iframe switching | `FrameLocator` — in-scope throughout interaction |
| `.NET Framework 4.7.2` — no LTS, no modern C# features | .NET 8 LTS |
| Hard-coded `user-data-dir` pointing to real browser profile | Playwright uses isolated browser contexts by default |

---

## Problems Not Solved by Migration Alone

| Problem | Still Requires |
|---|---|
| Commented-out steps in feature files | Manual cleanup (Issue 7 in `TEST_CODE_ANALYSIS.md`) |
| Scenario names embedding ticket IDs | Manual rename (Issue 8) |
| `Given` used for assertions instead of `Then` | Manual Gherkin review (Issue 10) |
| No `Background:` blocks | Manual feature file restructure (Issue 3) |
| Three spellings of the same wait step | Manual step consolidation (Issue 2) — though the wait steps themselves become redundant after migration |

---

## Summary

| Question | Answer |
|---|---|
| Keep Reqnroll? | Yes — BDD layer is sound, all feature files survive unchanged |
| Keep feature files? | Yes — zero changes to Gherkin scenarios |
| Keep step definition signatures? | Yes — only async keyword added, text patterns unchanged |
| Replace Selenium? | Yes — with `Microsoft.Playwright` |
| Target runtime? | .NET 8 LTS |
| Biggest effort? | Rewriting 55 page objects from `IWebElement` to `ILocator` |
| Biggest learning curve? | Adopting `async/await` consistently across the codebase |
| Migration approach? | Incremental feature-by-feature — no big bang rewrite |
