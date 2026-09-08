# KickHub Defect Log

The following defects were identified during development and black-box testing of the KickHub application. The defect log records the issue, reproduction steps, expected and actual behaviour, severity, priority, fix, retest result, and supporting evidence.

## Defect Summary

| Defect ID | Title | Severity | Priority | Status |
|---|---|---|---|---|
| DEF-01 | Newly inserted Match object did not receive generated database ID | High | High | Fixed |
| DEF-02 | Shirt number 0 was accepted | Medium | Medium | Fixed |
| DEF-03 | Past match date was accepted | Medium | Medium | Fixed |

---

## DEF-01 — Newly Inserted Match Object Did Not Receive Generated Database ID

**Description:**  
The original `MatchRepository.Add()` implementation inserted a match into SQLite but did not assign the newly generated database ID back to the `Match` object.

**Environment:**  
- C# / .NET 10
- SQLite
- KickHub.Data
- MatchRepository

**Preconditions:**  
- The database has been initialized.
- A valid `Match` object is ready to be stored.

**Steps to Reproduce:**
1. Use the original `MatchRepository.Add()` implementation from the initial project version.
2. Create a valid Match object.
3. Insert the Match into SQLite.
4. Inspect the Match object's `Id` after the insertion.

**Expected Result:**  
The new database record should be inserted and the generated SQLite primary key should be assigned to `match.Id`.

**Actual Result:**  
The match was inserted, but the original repository implementation did not retrieve and assign the generated database ID to the Match object.

**Severity:** High

**Priority:** High

**Root Cause:**  
The original `Add()` implementation executed the INSERT operation but did not query SQLite for the ID of the newly inserted row.

**Fix:**  
The repository was updated to execute the INSERT and then retrieve the generated row ID using:

```sql
SELECT last_insert_rowid();
```

The returned value is assigned to:

```csharp
match.Id
```

**Status:** Fixed

**Retest Result:**  
The corrected repository successfully inserts matches and assigns the generated database ID to the Match object.

**Evidence:**  
`docs/evidence/DEF-01-match-id-fix.png`

The Git history also records the change in commit `c32e92c` (`Connect referee match management to database`).

---

## DEF-02 — Shirt Number 0 Was Accepted

**Description:**  
The Manage Players form originally accepted `0` as a valid shirt number because the validation only checked whether the entered value could be parsed as an integer.

**Testing Technique:**  
Boundary Value Analysis

**Environment:**  
- KickHub.Desktop
- Admin role
- Manage Players window
- .NET 10 / Avalonia

**Preconditions:**  
- The application is running.
- The user is logged in as Admin.
- At least one valid team exists.

**Steps to Reproduce:**
1. Log in using the Admin account.
2. Open Manage Players.
3. Enter a valid player name.
4. Enter `0` as the shirt number.
5. Select a valid team.
6. Click Add Player.

**Test Data:**  
- Player Name: `Boundary Test`
- Shirt Number: `0`
- Team: Any valid team

**Expected Result:**  
The application should reject the value because a valid shirt number must be a positive integer.

**Actual Result:**  
The player was successfully created with shirt number `0`.

**Severity:** Medium

**Priority:** Medium

**Root Cause:**  
The original validation only used `int.TryParse()` and therefore accepted zero and negative integers as valid numeric values.

**Fix:**  
The validation was changed to reject any value less than or equal to zero:

```csharp
if (!int.TryParse(ShirtNumberBox.Text, out var shirtNumber) || shirtNumber <= 0)
{
    MessageText.Text = "Please enter a valid positive shirt number.";
    return;
}
```

**Status:** Fixed

**Retest Result:**  
The same test was repeated using shirt number `0`. The application rejected the input and displayed:

`Please enter a valid positive shirt number.`

**Evidence:**  
- `docs/evidence/DEF-02-zero-shirt-number.png`
- `docs/evidence/DEF-02-zero-shirt-number-fixed.png`

---

## DEF-03 — Past Match Date Was Accepted

**Description:**  
The Manage Matches form originally accepted a match date in the past because the validation only checked whether the text could be parsed into a valid `DateTime`.

**Testing Technique:**  
Boundary Value Analysis and Error Guessing

**Environment:**  
- KickHub.Desktop
- Admin role
- Manage Matches window
- .NET 10 / Avalonia

**Preconditions:**  
- The application is running.
- The user is logged in as Admin.
- At least two different teams are available.

**Steps to Reproduce:**
1. Log in using the Admin account.
2. Open Manage Matches.
3. Select two different valid teams.
4. Enter a date and time that has already passed.
5. Click Schedule Match.

**Test Data:**  
- Match Date: `01/01/2025 15:00`
- Home Team: Valid team
- Away Team: Different valid team

**Expected Result:**  
The application should reject the match because scheduled matches must use a future date and time.

**Actual Result:**  
The application accepted the past date and successfully scheduled the match.

**Severity:** Medium

**Priority:** Medium

**Root Cause:**  
The original validation used `DateTime.TryParse()` but did not compare the parsed date against the current date and time.

**Fix:**  
The validation was changed so that the date must both parse successfully and be later than the current time:

```csharp
if (!DateTime.TryParse(DateBox.Text, out var matchDate) || matchDate <= DateTime.Now)
{
    MessageText.Text = "Please enter a future date and time.";
    return;
}
```

**Status:** Fixed

**Retest Result:**  
The same past date was entered again after the fix. The application rejected the input and displayed:

`Please enter a future date and time.`

**Evidence:**  
- `docs/evidence/DEF-03-past-date-accepted.png`
- `docs/evidence/DEF-03-past-date-fixed.png`

---

## Final Defect Status

Three defects were formally documented.

- DEF-01 was identified during development and corrected through a repository implementation change.
- DEF-02 was discovered through Boundary Value Analysis of the shirt-number field.
- DEF-03 was discovered through Boundary Value Analysis and Error Guessing of the match-date field.
- All three defects were fixed and successfully retested.

The discovery of DEF-02 and DEF-03 also demonstrates that passing the original system test suite did not guarantee that all input boundaries had been tested. Additional boundary-focused testing revealed validation weaknesses that were then corrected before final submission.