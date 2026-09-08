# KickHub User Stories and Acceptance Criteria

## Personas
- **Administrator** — manages teams, players, and match scheduling.
- **Referee** — views assigned matches, updates scores, and completes matches.
- **Player** — views personal information, statistics, and match history.

## US01 — Administrator Login
**User Story:** As an administrator, I want to log in so that I can access administration functions.

**Acceptance Criteria:**
- Valid administrator credentials open the Admin Dashboard.
- Incorrect credentials are rejected.
- Empty username/password fields are rejected with a validation message.

## US02 — Manage Teams
**User Story:** As an administrator, I want to create teams so that teams can be used when adding players and scheduling matches.

**Acceptance Criteria:**
- A team with a valid name can be created.
- A successfully created team appears in the team list.
- An empty team name is rejected.
- Team data is stored in SQLite.

## US03 — Manage Players
**User Story:** As an administrator, I want to add players to teams so that player information can be managed centrally.

**Acceptance Criteria:**
- A player can be created with a valid name, team, and numeric shirt number.
- The player appears in the player list after creation.
- A non-numeric shirt number is rejected.
- The selected team is associated with the player.

## US04 — Schedule Match
**User Story:** As an administrator, I want to schedule a match and assign a referee so that an upcoming fixture is recorded in the system.

**Acceptance Criteria:**
- Two different teams must be selected.
- A valid date/time must be entered.
- A referee must be assigned.
- A valid match is saved with status `Scheduled` and score `0-0`.
- Selecting the same team as both home and away is rejected.
- Invalid date/time input is rejected.

## US05 — Referee Match Management
**User Story:** As a referee, I want to view and manage my assigned matches so that I can record the match result.

**Acceptance Criteria:**
- Assigned matches are shown on the Referee Dashboard.
- The referee can open a selected assigned match.
- Recording a goal updates the score.
- Recording a goal changes the match status to `In Progress`.
- The updated score is persisted in SQLite.

## US06 — Complete Match
**User Story:** As a referee, I want to finish a match so that the final result is protected from further changes.

**Acceptance Criteria:**
- Clicking Finish Match changes the status to `Completed`.
- The current score is retained as the final score.
- Attempts to modify a completed match are rejected.
- The completed result remains after the application is restarted.

## US07 — View Player Information
**User Story:** As a player, I want to view my information and statistics so that I can see my current football profile.

**Acceptance Criteria:**
- The Player Dashboard loads successfully after valid player login.
- Player name, team, shirt number, goals, yellow cards, and red cards are displayed.
- Data is loaded from the application database.

## US08 — View Match History
**User Story:** As a player, I want to view my team's match history so that I can see saved fixtures, scores, and statuses.

**Acceptance Criteria:**
- Matches involving the player's team are displayed.
- Each entry displays the match date, teams, score, and status.
- Saved results remain visible after the application is restarted.
