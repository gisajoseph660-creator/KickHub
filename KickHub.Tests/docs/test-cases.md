# KickHub Test Cases

The following system and integration test cases were carried out on the KickHub desktop application. The tests cover authentication,team and player management,match scheduling,referee match management,database persistence and the player dashboard.

| ID | What I'm Testing | Test Input / Action | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|
| TC01 | Player login | Enter `player` / `player123` | Player Dashboard should open | Player Dashboard opened successfully and displayed the player's information | Pass |
| TC02 | Referee login | Enter `referee` / `referee123` | Referee Dashboard should open | Referee Dashboard opened and displayed the assigned matches | Pass |
| TC03 | Admin login | Enter `admin` / `admin123` | Admin Dashboard should open | Admin Dashboard opened successfully | Pass |
| TC04 | Incorrect password | Enter `player` with an incorrect password | Login should be rejected and an error shown | Login was rejected and "Invalid username or password." was displayed | Pass |
| TC05 | Empty login | Leave username and password empty and click Login | Login should be rejected | Login was rejected and "Please enter your username and password." was displayed | Pass |
| TC06 | Create a team | Admin creates a team with a valid name | New team should appear in the team list | Team was created successfully and appeared in the existing teams list | Pass |
| TC07 | Empty team name | Admin attempts to create a team without a name | Team should not be created | Team was not created and "Team name is required." was displayed | Pass |
| TC08 | Add a player | Admin adds a player with a valid name, shirt number and team | Player should appear in the player list | Player was added successfully and appeared in the player list | Pass |
| TC09 | Invalid shirt number | Enter text instead of a number for the shirt number | Player should not be created | Player was not created and "Please enter a valid shirt number." was displayed | Pass |
| TC10 | Schedule a match | Select two different teams, enter a valid date and assign a referee | Match should be saved and assigned to the referee | Match was scheduled successfully and a confirmation message showed the selected teams | Pass |
| TC11 | Same team match | Select the same team as both home and away team | Match should be rejected | Match was rejected and "A team cannot play against itself." was displayed | Pass |
| TC12 | Invalid match date | Enter invalid text in the match date field | Match should not be scheduled | Match was not scheduled and "Please enter a valid date and time." was displayed | Pass |
| TC13 | Record a goal | Referee records a home goal during a scheduled match | Home score should increase by one | Home goal was recorded successfully and the score changed from 0-0 to 1-0 | Pass |
| TC14 | Finish a match | Referee clicks Finish Match after recording the score | Match status should change to Completed | Match was finished successfully with the final score 1-0 and status changed to Completed | Pass |
| TC15 | Modify completed match | Attempt to record another goal after the match has been completed | Modification should be rejected | Modification was rejected and "This match has already been completed." was displayed | Pass |
| TC16 | Score persistence | Close and reopen KickHub after saving the 1-0 result | Saved score and match status should remain after restart | After restarting KickHub, the match still displayed the saved score of 1-0 | Pass |
| TC17 | Player information | Login as player and open the Player Dashboard | Player name, team, shirt number and statistics should load | Player information was loaded and displayed successfully on the Player Dashboard | Pass |
| TC18 | Player match history | View the player's team matches on the Player Dashboard | Saved matches, scores and statuses should be displayed | The player's team match history was loaded and displayed on the Player Dashboard | Pass |

## Test Summary

- Total manual/system test cases executed: **18**
- Passed: **18**
- Failed: **0**
- Pass rate: **100%**

The testing confirmed that the main KickHub workflows operate correctly, including role-based authentication, administrative data management match scheduling, referee match management SQLite persistence and database-backed player information.

In addition to these manual and system tests, the KickHub automated test suite contains **12 unit and integration tests**, all of which passed successfully using `dotnet test`.