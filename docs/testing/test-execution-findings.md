# KickHub Test Execution and Findings

## 1. Test Environment

Testing was performed using the following environment:

- Application: KickHub Desktop
- Programming Language: C#
- Framework: .NET 10
- User Interface: Avalonia
- Database: SQLite
- Automated Testing Framework: xUnit
- Development Environment: Visual Studio Code
- Operating System: macOS

The application was tested locally using the implemented Admin, Referee, and Player workflows.

---

## 2. Test Execution Summary

A total of 20 manual/system test cases were executed.

The original suite contained 18 test cases. All 18 passed during execution.

Two additional boundary-focused tests were then added:

- TC19 — Reject Non-Positive Shirt Number
- TC20 — Reject Past Match Date

Both TC19 and TC20 initially failed and revealed validation defects.

The defects were corrected and both tests passed during retesting.

### Manual/System Test Results

| Result | Count |
|---|---:|
| Original test cases executed | 18 |
| Original test cases passed | 18 |
| Additional boundary tests | 2 |
| Additional tests initially failed | 2 |
| Defects raised | 2 |
| Defects fixed | 2 |
| Retests passed | 2 |
| Final unresolved failures | 0 |

The final manual/system test state was:

**20 test cases passed after defect correction and retesting.**

---

## 3. Automated Test Execution

The automated xUnit test suite contained 12 tests covering Unit and Integration testing.

The automated tests included:

- Authentication with valid credentials
- Authentication with incorrect credentials
- Authentication with an unknown username
- Recording home-team goals
- Recording away-team goals
- Completing a match
- League-points calculation
- Match-result calculation
- Player-card calculation
- Match repository integration with SQLite
- Player repository insertion
- Player repository update and retrieval

### Automated Test Result

- Tests Executed: 12
- Passed: 12
- Failed: 0

The successful automated test execution confirmed that the tested service logic, statistics functions, and repository/database interactions behaved as expected.

---

## 4. Key Findings

### 4.1 Authentication

The authentication tests confirmed that valid Player, Referee, and Admin accounts opened the correct dashboards.

Invalid passwords and empty login fields were correctly rejected.

---

### 4.2 Team and Player Management

Valid teams and players could be created successfully and stored through the application.

Initial testing confirmed rejection of:

- Empty team names
- Non-numeric shirt numbers

Additional Boundary Value Analysis later identified that shirt number `0` was incorrectly accepted.

This issue was recorded as DEF-02.

The validation was updated so that shirt numbers must be positive integers, and TC19 passed during retesting.

---

### 4.3 Match Scheduling

A valid match could be created using two different teams and a valid date.

The system correctly rejected:

- A team playing against itself
- Invalid date text

Additional testing identified that the application accepted dates that had already passed.

This issue was recorded as DEF-03.

The date validation was updated so that scheduled matches must use a future date and time. TC20 then passed during retesting.

---

### 4.4 Referee Match Management

The Referee workflow behaved as expected.

The tested functionality included:

- Viewing assigned matches
- Opening a selected match
- Recording a home-team goal
- Changing match status to In Progress
- Finishing the match
- Changing status to Completed
- Preventing further score changes after completion

The completed-match protection successfully rejected attempts to modify an already completed match.

---

### 4.5 Persistence

Persistence testing confirmed that the saved match score remained stored after the application was closed and restarted.

A completed match with score `1-0` remained `1-0` after the application was reopened.

This confirms that match results and status were being stored in SQLite rather than existing only in application memory.

---

### 4.6 Player Dashboard

The Player Dashboard successfully displayed stored player information, including:

- Player name
- Team
- Shirt number
- Goals
- Yellow cards
- Red cards

The application also displayed team match history with relevant date, team, score, and status information.

---

## 5. Defects Identified

Three defects were formally documented during development and testing.

| Defect ID | Related Test | Description | Severity | Status |
|---|---|---|---|---|
| DEF-01 | Development testing | Newly inserted Match object did not receive its generated database ID | High | Fixed |
| DEF-02 | TC19 | Shirt number `0` was accepted | Medium | Fixed |
| DEF-03 | TC20 | Past match date was accepted | Medium | Fixed |

Detailed reproduction steps, expected and actual results, root causes, fixes, retest results, and evidence are recorded in:

`docs/testing/defects.md`

---

## 6. Evidence

Test evidence is stored in:

`docs/evidence/`

Important defect evidence includes:

- `DEF-01-match-id-fix.png`
- `DEF-02-zero-shirt-number.png`
- `DEF-02-zero-shirt-number-fixed.png`
- `DEF-03-past-date-accepted.png`
- `DEF-03-past-date-fixed.png`

System-test evidence was also captured for important workflows such as:

- Player login
- Admin login
- Invalid password rejection
- Match scheduling
- Goal recording
- Match completion
- Completed-match rejection
- Match persistence
- Player information
- Player match history

Automated-test evidence should include the terminal output showing all 12 automated tests passing.

---

## 7. Overall Findings

The original system test suite showed that the main KickHub workflows were functioning correctly.

However, the later addition of Boundary Value Analysis exposed two input-validation problems that were not covered by the original 18 tests.

This demonstrates that a high pass rate does not necessarily mean that the application is free from defects.

The additional tests improved the overall quality of the test suite by checking edge cases rather than only common valid and invalid inputs.

After both validation defects were corrected and retested, all 20 manual/system test cases passed and no unresolved failure remained within the defined testing scope.

The final automated test execution also produced 12 passing tests.