# BSTVOAQAAutomation

.NET test automation framework for the VOA BSTAR system using Reqnroll (BDD), Selenium WebDriver, NUnit, ExtentReports, and Serilog.

---

## Framework Optimisation Summary

### Phase 1 — Completed prior to session

| Area | Before | After | Why |
|---|---|---|---|
| `TakeScreenShot()` in `DriverHelper.cs` | Called `DriverDispose()` at the end — screenshot capture also killed the browser | Removed the `DriverDispose()` call — screenshot only captures evidence | Single responsibility: capture and cleanup are separate concerns |
| `DriverDispose()` in `DriverHelper.cs` | Called `Quit()`, then `Close()`, then `Quit()` again — extra calls after first quit can throw | One `Driver?.Quit()` followed by `Driver = null` | One clean shutdown is sufficient; double-quit exceptions were silently swallowed |
| `extent.Flush()` in `Hooks.cs` | Commented out in `AfterScenario` — report could be incomplete if the run ended unexpectedly | Moved to `AfterTestRun` — flushes once at the very end of the run | Report is always written in full regardless of how the run ends |
| Extent reporter setup in `Hooks.cs` | `ExtentSparkReporter` created inside every scenario via `BeforeScenario` — reporter re-initialised per test | Moved entirely into `BeforeTestRun` — configured once per run | A report is a run-level artefact, not a scenario-level one |
| Duplicate `[BeforeFeature]` hooks in `Hooks.cs` | Two separate `[BeforeFeature]` methods — one logged the feature name, one created the Extent node | Consolidated into a single `BeforeFeature` method that does both | Easier to reason about; fewer entry points for the same lifecycle event |

---

### Phase 2 — Completed in this session

| Area | Before | After | Why |
|---|---|---|---|
| Class declaration in `Hooks.cs` | `public class Hooks : DriverHelper` — inherited just to call three static/public methods | `public class Hooks` — calls `DriverHelper.X()` explicitly | No IS-A relationship exists; inheritance was misused for convenience |
| Process-killing in `Hooks.cs` | `CloseExistingChromeInstances()` / `CloseExistingEdgeInstances()` called in `BeforeScenario`, killing all matching processes on the machine | Both calls and both private methods removed entirely | Kills the developer's own open browsers; `DriverDispose()` already handles cleanup from the previous scenario |
| `SetBaseUrl()` in `Hooks.cs` | Private instance method called in every `BeforeScenario` — read the properties file once per test | Private `static` method called once in `BeforeTestRun` | The environment does not change mid-run; reading the file once is sufficient |
| `AfterScenario` screenshot in `Hooks.cs` | Called `TakeScreenShot()` on failure in `AfterScenario` — in addition to `AfterStep` doing the same | Removed the call — `AfterScenario` only disposes the driver | `AfterStep` already captures the failure screenshot at the exact failing step; the second capture was redundant |
| Dead `[AfterStep]` hook in `Hooks.cs` | `TakeScreenShotAfterEachStep()` — cast the driver, built a path, then had both screenshot lines commented out; still fired after every step | Entire method removed | Dead code executing on every step; the active `AfterStep` static method covers everything needed |
| `ScenarioContext.Current` in instance methods in `Hooks.cs` | `BeforeScenario` and `AfterScenario` used static `ScenarioContext.Current` even though `_scenarioContext` was injected | Instance methods now use `_scenarioContext.ScenarioInfo.Title` and `_scenarioContext.TestError` | Injected context is the correct pattern for instance methods; static lookup is a fallback for static methods only |
| `DriverInitiation()` in `DriverHelper.cs` | Instance method — required an object instance to call, despite not using `this` | `public static IWebDriver DriverInitiation()` | No instance state is accessed; static makes the intent clear and removes the need to instantiate `DriverHelper` |
| Screenshot filename in `DriverHelper.cs` | `DateTime.Now.ToString("ssfff")` — seconds + milliseconds only; two screenshots in different minutes of the same second produce identical names | `DateTime.Now.ToString("yyyyMMdd_HHmmss_fff")` — full date and time stamp | Unique across dates; avoids silent file overwrites on long runs |
| Unknown browser fallback in `DriverHelper.cs` | `return Driver` after the switch — silently returned `null` if `BrowserType` did not match any case | `throw new InvalidOperationException($"Unknown BrowserType '{Config.BrowserType}' — check App.config.")` | A misconfigured browser name should fail loudly with a clear message, not silently return null and crash later |
| Stale `using` directives in `DriverHelper.cs` | 8 unused imports: `System.Configuration`, `System.Diagnostics`, `System.Drawing.Imaging`, `System.Linq`, `System.Text`, `System.Threading.Tasks`, `System.Web.Security`, `Microsoft.ApplicationInsights…` | Removed all 8; kept only `OpenQA.Selenium.*`, `Reqnroll`, `System`, `System.Collections.Generic`, `System.IO` | No-op code that adds noise and signals the file's history rather than its current purpose |

---

## Known Deferred Items

| Item | Reason deferred |
|---|---|
| `static` fields (`Driver`, `extentTest`, `scenarioTest`) conflict with `testThreadCount: 2` in `reqnroll.json` | Parallel-execution refactor is a larger piece of work; fields need to become `[ThreadStatic]` or scenario-scoped before parallel runs are enabled |
| `ScenarioContext.Current` in static `AfterStep` method | Static hooks cannot receive injected context; fixing requires converting `AfterStep` to an instance method, which is a broader structural change |
