# KickHub

KickHub is a desktop football match and team management prototype built using C#, .NET 10, Avalonia UI, and SQLite.

The application supports three implemented user roles:

- Administrator
- Referee
- Player

It demonstrates role-based access,football data management,match scheduling,refereee match control,persistent SQLite storage,reusable football-statistics logic and multiple levels of software testing.

---

## Features

### Administrator

The Administrator can:

- Log in using the Admin account
- Create football teams
- Add players to teams
- Enter player shirt numbers
- Schedule matches
- Select different home and away teams
- Assign the demo referee
- Enter match date and time
- Receive validation messages for invalid input

Important validation includes:

- Empty team names are rejected
- Non-numeric shirt numbers are rejected
- Shirt numbers less than or equal to zero are rejected
- A team cannot play against itself
- Invalid date formats are rejected
- Past match dates are rejected

---

### Referee

The Referee can:

- Log in using the Referee account
- View assigned matches
- Open a selected match
- Record home-team goals
- Record away-team goals
- Change the match to `In Progress`
- Finish a match
- Store the final result in SQLite
- Prevent changes after a match is `Completed`

The current prototype also contains Yellow Card and Red Card interface actions, but detailed card events are not yet fully persisted and linked to individual players.

---

### Player

The Player can:

- Log in using the Player account
- View player information
- View team information
- View shirt number
- View goals
- View yellow and red card totals
- View team match history
- View match dates, teams, scores, and statuses

The current prototype uses a demonstration player record instead of directly mapping each Player login to a unique `PlayerId`.

---

## Demo Accounts

| Role | Username | Password |
|---|---|---|
| Administrator | `admin` | `admin123` |
| Referee | `referee` | `referee123` |
| Player | `player` | `player123` |

These credentials are intended only for demonstration and testing.

---

## Technology Stack

KickHub was developed using:

- C#
- .NET 10
- Avalonia UI
- SQLite
- xUnit
- Git
- GitHub
- Visual Studio Code

---

## Project Structure

```text
KickHub/
├── docs/
│   ├── evidence/
│   └── testing/
│       ├── analysis-recommendations.md
│       ├── defects.md
│       ├── test-cases-detailed.md
│       ├── test-execution-findings.md
│       ├── test-plan.md
│       └── user-stories-acceptance-criteria.md
├── KickHub.Core/
├── KickHub.Data/
├── KickHub.Desktop/
├── KickHub.FootballStatistics/
├── KickHub.Tests/
├── .gitignore
├── README.md
└── KickHub.slnx
```

---

## Architecture

KickHub is separated into multiple projects to improve maintainability and separation of concerns.

### KickHub.Core

Contains core domain models, interfaces, and services.

Important models include:

- `User`
- `Team`
- `Player`
- `Match`
- `MatchEvent`

Important services include:

- AuthenticationService
- MatchService
- MatchEventService
- TeamService

---

### KickHub.Data

Contains SQLite database access and repository classes.

Important components include:

- DatabaseConnection
- DatabaseInitializer
- DatabaseSeeder
- MatchRepository
- PlayerRepository
- TeamRepository
- UserRepository
- MatchEventRepository

The Repository pattern is used to separate SQL/database logic from the desktop interface.

---

### KickHub.Desktop

Contains the Avalonia desktop user interface.

It provides the implemented Administrator, Referee, and Player workflows.

The interface communicates with repository and service classes rather than directly managing SQLite operations.

---

### KickHub.FootballStatistics

This is a separate reusable class library containing football-related calculations.

Examples include:

- League-points calculation
- Match-result calculation
- Player-card total calculation

Separating these calculations from the desktop interface makes the functionality more reusable and easier to test independently.

---

### KickHub.Tests

Contains automated xUnit tests.

The test suite covers:

- Authentication behaviour
- Match-service behaviour
- Football-statistics calculations
- SQLite repository integration

Final automated result:

- 12 tests executed
- 12 passed
- 0 failed

---

## Running the Application

From the repository root, restore and build the solution:

```bash
dotnet build
```

Run the desktop application:

```bash
dotnet run --project KickHub.Desktop
```

The SQLite database is initialized when required by the application.

---

## Running Automated Tests

From the repository root, run:

```bash
dotnet test
```

The automated suite contains Unit and Integration tests.

Final result:

```text
12 Passed
0 Failed
```

---

## Testing Strategy

KickHub was tested at three levels.

### Unit Testing

Used to test isolated logic such as:

- Authentication
- Goal recording
- Match completion
- League-points calculation
- Match-result calculation
- Player-card totals

### Integration Testing

Used to verify communication between repository classes and SQLite.

Examples include:

- Adding and retrieving Match records
- Adding Player records
- Updating and retrieving Player statistics

### System Testing

Used to test the complete desktop application through the graphical interface.

The final manual/system suite contained 20 test cases.

The original 18 tests passed.

Two additional boundary-focused tests initially failed:

- TC19 — Shirt number `0` was accepted
- TC20 — Past match date was accepted

Both defects were fixed and successfully retested.

Final manual/system state:

```text
20 test cases passed after defect correction and retesting
0 unresolved failures
```

---

## Black-Box Testing Techniques

The system test suite uses several black-box testing techniques.

### Equivalence Partitioning

Used for representative valid and invalid input groups such as:

- Valid and invalid login credentials
- Numeric and non-numeric shirt numbers
- Valid and invalid match dates

### Boundary Value Analysis

Used for important input boundaries such as:

- Empty required fields
- Shirt number `0`
- Future versus past match dates

Boundary Value Analysis identified two genuine validation defects during testing.

### State Transition Testing

Used for the Match lifecycle:

```text
Scheduled -> In Progress -> Completed
```

It was also used to verify that completed matches cannot be modified.

### Error Guessing

Used for realistic user mistakes such as:

- Selecting the same team as home and away
- Entering a past match date
- Attempting to modify a completed match

### Use Case Testing

Used for complete user workflows such as:

- Viewing the Player Dashboard
- Viewing match history
- Restarting the application and verifying persisted match state

---

## Testing Documentation

Detailed testing documentation is available in:

```text
docs/testing/
```

Files include:

### `user-stories-acceptance-criteria.md`

Contains personas, user stories, and specific acceptance criteria.

### `test-plan.md`

Contains:

- Testing scope
- Test environment
- Unit Testing
- Integration Testing
- System Testing
- Black-box testing techniques
- Entry criteria
- Exit criteria

### `test-cases-detailed.md`

Contains the 20 detailed manual/system test cases.

Each test includes:

- Test Case ID
- Objective
- Preconditions
- Procedure
- Specific test data
- Expected result
- Actual result
- Pass/fail status
- Severity
- Testing technique

### `test-execution-findings.md`

Contains:

- Test environment
- Execution results
- Key findings
- Manual/system test summary
- Automated-test summary
- Defect summary
- Evidence references

### `defects.md`

Contains the formal defect log.

Three defects are documented:

- DEF-01 — Newly inserted Match object did not receive generated database ID
- DEF-02 — Shirt number `0` was accepted
- DEF-03 — Past match date was accepted

All three were fixed.

### `analysis-recommendations.md`

Contains evaluation of:

- System strengths
- Remaining risks
- Current limitations
- Recommendations for future development
- Evaluation of the testing process

---

## Test Evidence

Screenshots and other supporting evidence are stored in:

```text
docs/evidence/
```

Defect evidence includes:

- `DEF-01-match-id-fix.png`
- `DEF-02-zero-shirt-number.png`
- `DEF-02-zero-shirt-number-fixed.png`
- `DEF-03-past-date-accepted.png`
- `DEF-03-past-date-fixed.png`

Additional evidence covers important workflows such as:

- Login
- Admin Dashboard
- Invalid login
- Match scheduling
- Goal recording
- Match completion
- Completed-match protection
- Persistence
- Player information
- Player match history
- Automated test execution

---

## Documented Defects

### DEF-01 — Match ID Not Assigned After Insert

The original repository implementation inserted a Match into SQLite but did not assign the generated database ID back to the Match object.

The repository was updated to retrieve the generated ID using:

```sql
SELECT last_insert_rowid();
```

---

### DEF-02 — Shirt Number 0 Accepted

Boundary Value Analysis revealed that the Manage Players form accepted shirt number `0`.

Validation was updated so that shirt numbers must be greater than zero.

---

### DEF-03 — Past Match Date Accepted

Testing revealed that a validly formatted date in the past could be scheduled.

Validation was updated so that match dates must be later than the current date and time.

---

## Known Limitations

The current KickHub prototype has several known limitations:

- Authentication uses predefined demo users
- Player login is not directly mapped to a unique Player record
- Detailed goals and cards are not fully stored as player-linked MatchEvent records
- UI testing is currently manual
- No load or concurrency testing has been performed
- Production-level security is not implemented
- Some broader design features are outside the scope of the implemented prototype

These limitations are discussed in more detail in:

```text
docs/testing/analysis-recommendations.md
```

---

## Future Improvements

Potential future improvements include:

- Direct User-to-Player account mapping
- Secure registration and authentication
- Password hashing and recovery
- Detailed goal and card event persistence
- Expanded automated validation tests
- Automated UI testing
- League tables and standings
- Performance testing
- Usability and accessibility testing
- Improved interface design

---

## Version Control

Git and GitHub were used throughout development.

The repository contains milestone commits for:

- Initial application setup
- Role-based dashboards
- Referee match management
- Database integration
- Admin match scheduling
- Team and player management
- Player Dashboard database integration
- Unit and Integration testing
- System-test planning and evidence

This provides traceability between development stages and the final implementation.

---

## Repository

GitHub repository:

`https://github.com/gisajoseph660-creator/KickHub`

---

## Authors

Joseph Gisa  
Elvis Shema
BEng Software Engineering