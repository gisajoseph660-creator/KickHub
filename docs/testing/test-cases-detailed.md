# KickHub Detailed Test Cases

**Total executed manual/system tests:** 20
**Original test cases passed:** 18
**Additional test cases initially failed:** 2
**Retests passed:** 2
**Final unresolved failures:** 0 
**Passed:** 18  
**Failed:** 0  

> Severity is referring to to the impact if the feature had failed it doesnt mean a defect currently exists.

## TC01
**Objective:** Valid Player login
**Preconditions:** App running; player account exists
**Procedure:**
1. Open app.
2. enter player/player123.
3. click Login.
**Specific Test Data:** player / player123
**Expected Result:** Player Dashboard opens
**Actual Result:** Player Dashboard opened and showed player information
**Status:** Pass
**Severity:** High
**Testing Technique:** Equivalence Partitioning

---

## TC02
**Objective:** Valid Referee login
**Preconditions:** App running; referee account exists
**Procedure:**
1. Open app.
2. enter referee/referee123.
3. click Login.
**Specific Test Data:** referee / referee123
**Expected Result:** Referee Dashboard opens with assigned matches
**Actual Result:** Referee Dashboard opened and showed assigned matches
**Status:** Pass
**Severity:** High
**Testing Technique:** Equivalence Partitioning

---

## TC03
**Objective:** Valid Admin login
**Preconditions:** App running; admin account exists
**Procedure:**
1. Open app.
2. enter admin/admin123.
3. click Login.
**Specific Test Data:** admin / admin123
**Expected Result:** Admin Dashboard opens
**Actual Result:** Admin Dashboard opened
**Status:** Pass
**Severity:** High
**Testing Technique:** Equivalence Partitioning

---

## TC04
**Objective:** Reject wrong password
**Preconditions:** App running
**Procedure:**
1. Enter player with wrong password.
2. click Login.
**Specific Test Data:** player / wrong password
**Expected Result:** Login rejected
**Actual Result:** Displayed 'Invalid username or password.'
**Status:** Pass
**Severity:** High
**Testing Technique:** Equivalence Partitioning

---

## TC05
**Objective:** Reject empty login
**Preconditions:** App running
**Procedure:**
1. Leave username/password empty.
2. click Login.
**Specific Test Data:** length 0 / length 0
**Expected Result:** Login rejected
**Actual Result:** Displayed 'Please enter your username and password.'
**Status:** Pass
**Severity:** High
**Testing Technique:** Boundary Value Analysis

---

## TC06
**Objective:** Create valid team
**Preconditions:** Admin logged in
**Procedure:**
1. Open Manage Teams.
2. enter valid name.
3. create team.
**Specific Test Data:** Valid team name
**Expected Result:** Team created and listed
**Actual Result:** Team created and appeared in team list
**Status:** Pass
**Severity:** Medium
**Testing Technique:** Equivalence Partitioning

---

## TC07
**Objective:** Reject empty team name
**Preconditions:** Admin logged in
**Procedure:**
1. Open Manage Teams.
2. leave name empty.
3. create team.
**Specific Test Data:** Team name length 0
**Expected Result:** Team not created
**Actual Result:** Displayed 'Team name is required.'
**Status:** Pass
**Severity:** Medium
**Testing Technique:** Boundary Value Analysis

---

## TC08
**Objective:** Add valid player
**Preconditions:** Admin logged in; team exists
**Procedure:**
1. Open Manage Players.
2. enter valid name, numeric shirt number, team.
3. add.
**Specific Test Data:** Valid player data
**Expected Result:** Player created and listed
**Actual Result:** Player added and appeared in player list
**Status:** Pass
**Severity:** Medium
**Testing Technique:** Equivalence Partitioning

---

## TC09
**Objective:** Reject non-numeric shirt number
**Preconditions:** Admin logged in; team exists
**Procedure:**
1. Enter valid name.
2. enter abc as shirt number.
3. select team.
4. add.
**Specific Test Data:** abc
**Expected Result:** Player not created
**Actual Result:** Displayed 'Please enter a valid shirt number.'
**Status:** Pass
**Severity:** Medium
**Testing Technique:** Equivalence Partitioning

---

## TC10
**Objective:** Schedule valid match
**Preconditions:** Admin logged in; 2 teams exist
**Procedure:**
1. Open Manage Matches.
2. choose different teams, valid date, referee.
3. schedule.
**Specific Test Data:** Different teams + valid date + referee
**Expected Result:** Match saved and assigned
**Actual Result:** Match scheduled successfully
**Status:** Pass
**Severity:** High
**Testing Technique:** Equivalence Partitioning

---

## TC11
**Objective:** Reject same home/away team
**Preconditions:** Admin logged in
**Procedure:**
1. Select same team as home and away.
2. schedule.
**Specific Test Data:** Home Team = Away Team
**Expected Result:** Match rejected
**Actual Result:** Displayed 'A team cannot play against itself.'
**Status:** Pass
**Severity:** High
**Testing Technique:** Error Guessing

---

## TC12
**Objective:** Reject invalid match date
**Preconditions:** Admin logged in; 2 teams exist
**Procedure:**
1. Choose different teams.
2. enter invalid date text.
3. schedule.
**Specific Test Data:** Invalid text
**Expected Result:** Match not scheduled
**Actual Result:** Displayed 'Please enter a valid date and time.'
**Status:** Pass
**Severity:** Medium
**Testing Technique:** Equivalence Partitioning

---

## TC13
**Objective:** Record home goal
**Preconditions:** Referee logged in; assigned Scheduled match at 0-0
**Procedure:**
1. Open match.
2. click Home Goal.
**Specific Test Data:** 0-0
**Expected Result:** Score becomes 1-0 and match enters In Progress
**Actual Result:** Score changed 0-0 to 1-0
**Status:** Pass
**Severity:** High
**Testing Technique:** State Transition Testing

---

## TC14
**Objective:** Finish match
**Preconditions:** Referee logged in; match at 1-0 and not Completed
**Procedure:**
1. Open match.
2. click Finish Match.
**Specific Test Data:** 1-0
**Expected Result:** Status becomes Completed
**Actual Result:** Final score 1-0; status Completed
**Status:** Pass
**Severity:** High
**Testing Technique:** State Transition Testing

---

## TC15
**Objective:** Reject change after completion
**Preconditions:** Referee logged in; Completed match at 1-0
**Procedure:**
1. Open completed match.
2. attempt another goal.
**Specific Test Data:** Completed / 1-0
**Expected Result:** Change rejected; score unchanged
**Actual Result:** Displayed 'This match has already been completed.'
**Status:** Pass
**Severity:** High
**Testing Technique:** State Transition Testing

---

## TC16
**Objective:** Persist saved score after restart
**Preconditions:** Saved match exists at 1-0
**Procedure:**
1. Close app.
2. reopen.
3. log in.
4. reopen match.
**Specific Test Data:** Saved 1-0
**Expected Result:** Score remains 1-0
**Actual Result:** Score still displayed as 1-0
**Status:** Pass
**Severity:** High
**Testing Technique:** State Transition Testing and Use Case Testing
**Justification:** The test verifies that tge match remains in its previously saved state after the application is restarted. The score and status should persist rather than returning to an earlier state. 

---

## TC17
**Objective:** Display player information
**Preconditions:** Player exists in SQLite
**Procedure:**
1. Login as player.
2. open dashboard.
**Specific Test Data:** Existing player record
**Expected Result:** Name/team/shirt/stats shown
**Actual Result:** Player information displayed successfully
**Status:** Pass
**Severity:** Medium
**Testing Technique:** Use Case Testing
**Justification:** This test validates the complete Player Dashboard use case from the user's perspective, confirming that the player's stored information is retrieved and displayed correctly.

---

## TC18
**Objective:** Display player match history
**Preconditions:** Player logged in; team matches exist
**Procedure:**
1. Open dashboard.
2. review match history.
**Specific Test Data:** Existing match records
**Expected Result:** Dates/teams/scores/statuses shown
**Actual Result:** Match history displayed successfully
**Status:** Pass
**Severity:** Medium
**Testing Technique:** Use Case Testing
**Justification:** This test validates the player's match-history use case by checking that relevant team matches are retrieved and presented with the expected date, teams, score, and status.
---

## TC19 — Reject Non-Positive Shirt Number

**Objective:**  
Verify that the Manage Players form rejects a shirt number at the invalid lower boundary.

**Preconditions:**  
- The application is running.
- The user is logged in as Admin.
- At least one valid team exists.

**Test Procedure:**
1. Open Manage Players.
2. Enter a valid player name.
3. Enter `0` as the shirt number.
4. Select a valid team.
5. Click Add Player.

**Test Data:**  
- Player Name: `Boundary Test`
- Shirt Number: `0`
- Team: Any valid team

**Expected Result:**  
The application should reject the player because the shirt number must be a positive integer.

**Initial Actual Result:**  
The application accepted the player and created a record with shirt number `0`.

**Initial Status:** FAIL

**Defect Raised:**  
DEF-02 — Shirt number 0 was accepted.

**Severity:** Medium

**Testing Technique:**  
Boundary Value Analysis

**Fix Applied:**  
Validation was changed so that values less than or equal to zero are rejected.

**Retest Result:**  
The application rejected shirt number `0` and displayed:

`Please enter a valid positive shirt number.`

**Final Status:** PASS

**Evidence:**
- [DEF-02 zero shirt number accepted](../evidence/DEF-02-zero-shirt-number.png)
- [DEF-02 zero shirt number fixed](../evidence/DEF-02-zero-shirt-number-fixed.png)

---

## TC20 — Reject Past Match Date

**Objective:**  
Verify that the Manage Matches form rejects a match date that has already passed.

**Preconditions:**  
- The application is running.
- The user is logged in as Admin.
- At least two different teams exist.

**Test Procedure:**
1. Open Manage Matches.
2. Select two different valid teams.
3. Enter `01/01/2025 15:00` as the match date.
4. Click Schedule Match.

**Test Data:**  
- Match Date: `01/01/2025 15:00`
- Home Team: Valid team
- Away Team: Different valid team

**Expected Result:**  
The application should reject the match because scheduled matches must use a future date and time.

**Initial Actual Result:**  
The application accepted the past date and scheduled the match.

**Initial Status:** FAIL

**Defect Raised:**  
DEF-03 — Past match date was accepted.

**Severity:** Medium

**Testing Technique:**  
Boundary Value Analysis and Error Guessing

**Fix Applied:**  
The date validation was changed so that the parsed date must be later than `DateTime.Now`.

**Retest Result:**  
The application rejected the past date and displayed:

`Please enter a future date and time.`

**Final Status:** PASS

**Evidence:**
- [DEF-03 past date accepted](../evidence/DEF-03-past-date-accepted.png)
- [DEF-03 past date fixed](../evidence/DEF-03-past-date-fixed.png)