# Job Description
## Automated Test Developer / SDET
### CT Programme — VOA BSTAR System

---

## Role Overview

We are looking for an experienced **Automated Test Developer / SDET** to join the CT
programme QA team. The successful candidate will maintain and extend an enterprise-grade
.NET test automation framework built on Reqnroll (BDD) and Microsoft Playwright on .NET 8.

The role sits within a cross-functional delivery team working on the VOA BSTAR system —
a Microsoft Dynamics 365-based application for council tax property assessment. The
candidate will work closely with developers, BAs and DevOps engineers to embed automated
testing into the CI/CD pipeline and shift testing left across the delivery lifecycle.

---

## Key Responsibilities

| # | Responsibility |
|---|---|
| 1 | Maintain and extend the Reqnroll (SpecFlow) BDD automation framework written in C# / .NET 8 using Microsoft Playwright |
| 2 | Write and review Gherkin feature files, step definitions and page objects to support new and changed functionality |
| 3 | Ensure automated tests are submitted alongside pull requests for new features — contributing to a shift-left testing model |
| 4 | Investigate and resolve test flakiness — replacing hard sleeps with condition-based waits, standardising step definitions, eliminating duplicate bindings |
| 5 | Maintain and improve the Azure DevOps CI/CD pipeline — test result publishing, environment switching, and PR gate configuration |
| 6 | Triage pipeline failures and distinguish test failures from environment or infrastructure issues |
| 7 | Work with the DevOps team to configure and maintain the `VOA-TESTTEAM-AUTOMATION` agent pool |
| 8 | Manage test data in Excel and database — ensuring data integrity and environment isolation between SIT, UAT and FAT |
| 9 | Participate in sprint ceremonies — providing test estimates, flagging automation risk, and contributing to Definition of Done |
| 10 | Mentor junior QA team members on framework usage, Gherkin best practices and Playwright |
| 11 | Contribute to the Ways of Working improvement programme — defining PR gate criteria and embedding tests into the delivery pipeline |

---

## Technical Skills — Essential

### Test Automation

| Skill | Detail |
|---|---|
| **C# (.NET 8)** | Solid hands-on C# experience — OOP, generics, async/await, LINQ |
| **Playwright** | Hands-on experience with `Microsoft.Playwright` — locators, auto-wait, trace viewer, assertions (`Expect`), async/await patterns |
| **Reqnroll or SpecFlow** | Experience writing and maintaining BDD feature files, step definitions and hooks in a .NET context |
| **Gherkin / BDD** | Ability to write clear, readable Gherkin scenarios — correct use of `Given/When/Then`, `Background:`, `Scenario Outline` |
| **NUnit or xUnit** | Test framework experience in a .NET context |
| **Page Object Model** | Strong POM implementation — separation of locators and functions, reusable components |

### CI/CD and DevOps

| Skill | Detail |
|---|---|
| **Azure DevOps** | Pipeline authoring in YAML — `VSTest`, `PublishTestResults`, variable groups, agent pool configuration |
| **Git** | Branch strategy, PR workflow, commit hygiene |
| **NuGet** | Package management, version control, dependency resolution |

### Supporting Skills

| Skill | Detail |
|---|---|
| **REST API testing** | Experience with RestSharp or similar — cookie-based auth, GET/POST/PUT, response validation |
| **Excel test data management** | EPPlus or similar — reading parameterised test data from Excel sheets |
| **SQL / database** | Ability to write and validate SQL queries for test data retrieval and assertion |
| **Serilog or similar logging** | Structured logging in a test context |

---

## Technical Skills — Desirable

| Skill | Detail |
|---|---|
| **Microsoft Dynamics 365** | Experience automating D365 CRM — iframe handling, BPF stages, lookup fields, command bar interactions |
| **Selenium WebDriver** | Prior Selenium experience — useful for understanding existing test patterns and diagnosing legacy issues |
| **Azure AD / OAuth** | Understanding of session tokens, re-authentication popups, service account configuration for pipelines |
| **Accessibility testing** | Axe integration or similar — `a11y` test execution |
| **PostgreSQL / Npgsql** | Database query execution against Azure-hosted PostgreSQL |
| **PDF evidence generation** | PDF-based screenshot capture and evidence trail (PdfSharp or similar) |
| **AutoIt** | Windows automation for file upload dialogs |
| **PowerShell** | Pipeline scripting — config file manipulation, Azure CLI commands |

---

## Experience Required

| Level | Requirement |
|---|---|
| **Minimum** | 3+ years hands-on test automation experience in a .NET / C# environment |
| **Preferred** | 5+ years with at least 1 year of Playwright experience |
| **BDD** | Demonstrable experience writing and maintaining a BDD framework in a production delivery context |
| **CI/CD** | Experience configuring or maintaining test execution in an Azure DevOps or similar pipeline |
| **Domain** | Government, local authority or enterprise CRM test automation experience is advantageous |

---

## Behaviours and Ways of Working

| Behaviour | Detail |
|---|---|
| **Shift-left mindset** | Writes test scenarios at refinement — not after development is complete |
| **Quality ownership** | Takes responsibility for the health of the automation suite — proactively fixes flakiness rather than waiting for failures to accumulate |
| **Collaborative** | Works closely with developers to agree on testability — raises concerns about untestable designs early |
| **Pragmatic** | Knows when to automate and when not to — does not automate for the sake of it |
| **Documentation** | Keeps framework documentation, analysis documents and pipeline fix guides up to date |
| **Communication** | Can explain technical test findings clearly to non-technical stakeholders |

---

## Framework Context

The candidate will be joining a team that has already made significant framework improvements.
Familiarity with the following patterns is expected:

| Area | Current state |
|---|---|
| **Runtime** | .NET 8 |
| **BDD** | Reqnroll 3.0.1 — 23 feature files, ~2,000 scenarios across SIT, UAT, FAT, PPE environments |
| **Browser automation** | Microsoft Playwright on .NET 8 |
| **Environments** | SIT, UAT, FAT, PPE, MO, TRN — environment switching via Azure DevOps variable groups |
| **Execution model** | 5 VMs, 2 users per VM (10 users total) via RDP — CI pipeline with PR gate in place |
| **Reporting** | PDF evidence files generated per step — Azure DevOps NUnit XML results via pipeline |
| **Pipeline** | Azure DevOps — agent pool `VOA-TESTTEAM-AUTOMATION` — PR gate active |
| **Application under test** | Microsoft Dynamics 365 CRM — council tax property assessment workflows |

---

## Nice to Have — Certifications

| Certification | Relevance |
|---|---|
| ISTQB Advanced — Test Automation Engineer | Demonstrates structured automation knowledge |
| Microsoft AZ-900 / AZ-204 | Azure fundamentals useful for pipeline and environment work |
| Playwright certification (if available) | Demonstrates current framework knowledge |

---

## What We Offer

| | |
|---|---|
| **Impact** | Direct influence on the quality and velocity of a significant government programme |
| **Technical growth** | Working on a modern Playwright + Reqnroll stack on .NET 8 — interesting domain with real-world complexity |
| **Autonomy** | Ownership of the automation framework — not just test execution |
| **Visibility** | Contributing to Ways of Working workshops — work presented to stakeholders |
| **Environment** | Collaborative team — developers, BAs, DevOps and QA working closely together |
