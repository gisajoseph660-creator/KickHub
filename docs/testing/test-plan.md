# KickHub Test Plan

## 1. Purpose

This test plan defines the testing approach used to evaluate the KickHub desktop application. The purpose of testing is to verify that the implemented functionality behaves as expected, that important user workflows operate correctly, and that invalid or unexpected inputs are handled appropriately.

Testing focuses on the functionality implemented in the final KickHub prototype, including authentication, team and player management, match scheduling, referee match management, player information, match history, persistence, reusable football-statistics logic, and database repository behaviour.

---

## 2. Scope

### 2.1 In Scope

The following areas are included in testing:

- User login for Admin, Referee, and Player roles
- Rejection of invalid or incomplete login credentials
- Team creation and validation
- Player creation and validation
- Match scheduling
- Validation preventing a team from playing itself
- Validation of match dates
- Referee access to assigned matches
- Goal recording
- Match completion
- Prevention of match modification after completion
- Persistence of match scores and status in SQLite
- Player information display
- Player match-history display
- Authentication service behaviour
- Match service behaviour
- Football statistics calculations
- Repository insert, retrieve, and update behaviour

### 2.2 Out of Scope

The following areas are outside the scope of the final prototype testing:

- Production-level user registration
- Password recovery
- Online or cloud deployment
- Multi-user concurrency
- Load and stress testing
- Network-performance testing
- Mobile-device testing
- Advanced security and penetration testing
- Full accessibility testing
- Automated graphical user-interface testing
- Features not implemented in the final prototype

These areas were excluded because the project is a local desktop prototype and the available testing effort was focused on the implemented functional requirements.

---

## 3. Test Environment

Testing was performed using the following environment:

- Application: KickHub Desktop
- Programming language: C#
- Framework: .NET 10
- User interface: Avalonia
- Database: SQLite
- Automated testing framework: xUnit
- Development environment: Visual Studio Code
- Operating system: macOS

The desktop application was tested locally using its SQLite database and the demonstration Admin, Referee, and Player accounts.

---

## 4. Testing Levels

KickHub was tested at three main testing levels: Unit Testing, Integration Testing, and System Testing.

### 4.1 Unit Testing

Unit testing verifies individual pieces of application logic independently from the complete user interface.

Unit tests were used for areas where behaviour could be checked directly and repeatedly without requiring manual interaction with the desktop interface.

Examples include:

- Authentication with valid credentials
- Authentication with an incorrect password
- Authentication with an unknown username
- Recording a home-team goal
- Recording an away-team goal
- Completing a match and updating its status
- Calculating league points
- Determining a match result
- Calculating total player cards

Unit testing was appropriate because these functions contain clear inputs and outputs and can therefore be verified automatically.

The automated test suite contains 12 tests in total when the integration tests are included.

---

### 4.2 Integration Testing

Integration testing verifies whether components that work separately also communicate correctly when combined.

KickHub uses repositories to connect application objects to SQLite. Repository integration tests were therefore used to verify interaction between the data-access code and a real temporary SQLite database.

Integration testing included:

- Adding a Match through `MatchRepository` and retrieving it from SQLite
- Adding a Player through `PlayerRepository`
- Updating the player's goal value
- Retrieving the updated player from SQLite

Integration testing was selected because repository behaviour cannot be sufficiently verified by testing only isolated C# objects. The test must also confirm that SQL operations, mappings, persistence, and retrieval work together correctly.

---

### 4.3 System Testing

System testing evaluates the completed application from the user's perspective.

The full KickHub desktop application was executed and tested through the graphical user interface using realistic user workflows.

Examples include:

- Logging in with different roles
- Creating teams
- Creating players
- Scheduling matches
- Managing matches as a referee
- Recording goals
- Completing matches
- Attempting to modify completed matches
- Restarting the application and verifying persistence
- Viewing player information
- Viewing match history

System testing was necessary because successful unit and integration tests alone do not guarantee that complete user workflows function correctly. The GUI, repositories, services, database, and navigation must work together as one system.

A total of 20 manual/system test cases were ultimately executed. The original 18 passed, while two additional boundary-focused tests initially revealed defects. Those defects were fixed and both tests passed during retesting.

---

## 5. Black-Box Testing Techniques

Black-box testing was used during system testing because the application was evaluated according to externally visible behaviour, input, and output rather than the internal implementation of the code.

The main techniques used were Equivalence Partitioning, Boundary Value Analysis, State Transition Testing, and Error Guessing.

---

### 5.1 Equivalence Partitioning

Equivalence Partitioning divides possible inputs into groups that are expected to behave in the same way. A representative value can then be selected from each group instead of testing every possible input.

This technique was appropriate for KickHub because many application inputs have clear valid and invalid categories.

Examples include:

#### Login Credentials

Possible input classes include:

- Valid username and valid password
- Valid username and invalid password
- Unknown username
- Empty credentials

Testing representative values from these groups provides coverage without attempting every possible username and password combination.

#### Shirt Number

Possible classes include:

- Valid numeric value
- Invalid non-numeric value
- Non-positive numeric value

For example, `"10"` represents a normal valid numeric shirt number while `"abc"` represents an invalid non-numeric class.

#### Match Date

Possible classes include:

- Valid future date
- Invalid date format
- Validly formatted date that is already in the past

Equivalence Partitioning was therefore selected because it provides efficient input coverage while avoiding unnecessary duplicate tests.

---

### 5.2 Boundary Value Analysis

Boundary Value Analysis focuses on values at the edges between valid and invalid input ranges. Errors frequently occur at these boundaries.

This technique was particularly useful for input validation in KickHub.

#### Required Fields

An empty required field represents the boundary between no input and valid input.

Examples include:

- Empty username
- Empty password
- Empty team name
- Empty player name

These values were tested to verify that required fields are correctly rejected.

#### Shirt Number Boundary

Additional testing revealed that the original application accepted shirt number `0`.

The important lower-bound values are:

- `-1` — invalid
- `0` — invalid boundary value
- `1` — minimum valid positive value

Testing `0` identified DEF-02 because the original implementation checked only whether the value could be converted to an integer.

The validation was updated to require:

```text
shirtNumber > 0
```

and the defect passed retesting.

#### Match-Date Boundary

The match scheduler originally verified whether the input could be parsed as a date but did not verify that the date was in the future.

The important boundary is the current date and time:

- Date before the current time — invalid
- Current time — invalid for a future fixture
- Date after the current time — valid

Testing a past date identified DEF-03. The application was subsequently updated to require the scheduled date to be later than the current date and time.

Boundary Value Analysis was therefore highly valuable because it revealed defects that were not detected by the initial functional test cases.

---

### 5.3 State Transition Testing

State Transition Testing verifies systems whose allowed behaviour changes according to their current state.

This technique was appropriate for KickHub because a football match has a defined lifecycle.

The primary states are:

```text
Scheduled -> In Progress -> Completed
```

Different actions are permitted depending on the current state.

Examples include:

- A scheduled match can begin when match activity is recorded.
- Recording a goal changes the match to `In Progress`.
- Finishing a match changes its status to `Completed`.
- A completed match should not accept further score modifications.

State Transition Testing was used particularly in:

- TC13 — recording a goal
- TC14 — completing a match
- TC15 — attempting to modify a completed match

This technique was selected because testing only individual buttons would not verify whether the application correctly enforces rules across the complete match lifecycle.

---

### 5.4 Error Guessing

Error Guessing is an experience-based black-box technique where likely user mistakes and failure conditions are deliberately tested.

It was used alongside the structured techniques because users may enter combinations that are technically valid data types but logically incorrect.

Examples include:

- Selecting the same team as both home and away
- Entering a past date for a future fixture
- Attempting to change a match after it has been completed

Error Guessing was appropriate because these situations represent realistic user mistakes that may not always be identified through simple valid-versus-invalid partitions.

For example, two selected teams can each be individually valid while the combination is still invalid because a team cannot play against itself.

---

### 5.5 Use Case Testing

Use Case Testing verifies complete user interactions from the perspective of a particular actor.

This technique was appropriate for KickHub because several requirements involve complete user workflows rather than only individual input fields.

Examples include:

- A Player logging in and viewing personal information
- A Player viewing team match history
- An Admin scheduling a match
- A Referee opening and managing an assigned match

Use Case Testing was especially applied to TC17 and TC18 because these cases verify whether the application successfully completes the intended Player workflows from start to finish.

This technique complements Equivalence Partitioning, Boundary Value Analysis, State Transition Testing, and Error Guessing by focusing on end-to-end user goals.

---

## 6. Test Case Selection

The system test suite was designed to cover both positive and negative scenarios.

Positive testing verifies that valid actions are accepted, such as:

- Correct login
- Valid team creation
- Valid player creation
- Valid match scheduling
- Recording a goal
- Completing a match

Negative testing verifies that invalid actions are rejected, such as:

- Incorrect password
- Empty credentials
- Empty team name
- Non-numeric shirt number
- Shirt number `0`
- Selecting the same team twice
- Invalid date format
- Past match date
- Modifying a completed match

Using both positive and negative testing provides stronger coverage than testing only successful workflows.

---

## 7. Entry Criteria

Testing could begin when:

- The KickHub solution compiled successfully.
- The required projects were included in the solution.
- The SQLite database could be initialized.
- The application could be launched.
- Demo accounts were available.
- Core implemented functionality was accessible.
- Automated test projects could be executed.

---

## 8. Exit Criteria

Testing was considered complete when:

- All planned manual/system test cases had been executed.
- Automated unit and integration tests had been executed.
- Actual results had been compared with expected results.
- Identified defects had been documented.
- DEF-02 and DEF-03 had been corrected and retested.
- No known high-severity unresolved defect remained in the tested functionality.
- Final automated tests passed.
- Relevant screenshots and test evidence had been stored in `docs/evidence/`.

---

## 9. Testing Outcome

The testing process demonstrated that the main KickHub workflows operate successfully under the tested conditions.

The original 18 system tests passed, but additional Boundary Value Analysis revealed two validation defects:

- Shirt number `0` was accepted.
- Past match dates were accepted.

Both defects were corrected and passed retesting.

The final manual/system state was therefore 20 passing test cases with no unresolved failures in the defined test scope.

The automated test suite also completed successfully with 12 passing tests.

The results demonstrate the benefit of combining multiple testing levels and black-box techniques. Functional workflow testing confirmed the main application behaviour, while targeted boundary testing identified validation weaknesses that were not discovered by the original test suite.