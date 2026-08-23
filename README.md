# CapEx - Capital Expenditure Request & Multi-Level Approval
Internal tool that replaces the email-and-Excel CapEx approval process.
Blazor Web App (.NET 8, interactive server rendering) + SQL Server via EF Core.

## Running it

**Requirements:** .NET 8 SDK, SQL Server (LocalDB or a full instance), SSMS for the restore.

1. Restore the database (see below).
2. Point `ConnectionStrings:DefaultConnection` in `appsettings.json` at your instance.
3. `dotnet run`, or F5 in Visual Studio.
4. Open `https://localhost:7094` and sign in with any account below.

## Approval tiers

Any request of R5,000 and over needs formal approval. Approvers act in order.

| Amount requested    | Required approvers (in order)                   | Final status if all approve |
| ------------------- | ----------------------------------------------- | --------------------------- |
| Under R5,000        | None                                            | Approved                    |
| R5,000 to R24,999   | Department Manager                              | Approved                    |
| R25,000 to R99,999  | Department Manager, then Finance Director        | Approved                    |
| R100,000 and above  | Department Manager, then Finance Director, then CEO | Approved                |

A single rejection at any stage sets the request to **Rejected** immediately.

## Approvers

| Role               | User            |
| ------------------ | --------------- |
| Department Manager | Bob Lockwood    |
| Finance Director   | Alice Liddle    |
| CEO                | Jeff Sidebottom |

## Seeded users

All eight seeded users share the password **`123`**.

| ID | Name | Email | Role value | Role |
|---|---|---|---|---|
| 1 | Thomas Goff | tgoff@inambu.co.za | 0 | Employee |
| 2 | Mark Freedman | mfreedman@inambu.co.za | 0 | Employee |
| 3 | Pieter Venter | pventer@inambu.co.za | 0 | Employee |
| 4 | Thato Dlamini | tdlamini@inambu.co.za | 0 | Employee |
| 5 | Kelly Patterson | kpatterson@inambu.co.za | 0 | Employee |
| 6 | Bob Lockwood | blockwood@inambu.co.za | 1 | Department Manager |
| 7 | Alice Liddle | aliddle@inambu.co.za | 2 | Finance Director |
| 8 | Jeff Sidebottom | jsidebottom@inambu.co.za | 3 | CEO |

Users 1 to 5 can only raise requests. Users 6 to 8 can raise requests and approve them.

The role value is the approval order. Do not reshuffle it.

## Assumptions and decisions

### Requests under R5,000 are approved on submission

No approver is required, so no approval row is written. The audit trail for these requests is empty on purpose.

### An approver may act on their own request

The brief does not forbid it, so it is not blocked. `ApprovalWorkflow.GetEligibility` is where that rule would go.

### Status is derived, never stored

Which approver is next comes from the amount plus the approval rows already recorded. Nothing on the request says "waiting on X", so a counter cannot drift away from the audit trail.

### Passwords are stored and compared in plain text

This is a fixed demo user list with no real accounts. `IPasswordVerifier` exists so swapping in hashing is a one-line change in `Program.cs`. Do not copy this pattern into anything real.

### Sign-in lives per browser connection

There are no cookies and no tokens. Refreshing the page signs you out.

## Project structure

```
Models/        entities and enums
Data/          DbContext and one EF config class per table
Repositories/  database reads and writes only
Services/      business rules - approvals, requests, auth, dashboard
Components/    Blazor pages and shared components
ViewModels/    form models for the login and new-request pages
Common/        shared helpers, currently just currency formatting
Database/      SQL scripts and the database backup
```

The approval tiers live in one place: `Services/Approvals/AmountBasedApprovalTierPolicy.cs`. Nothing else decides who must approve what.

## Restoring the database

1. Open SSMS and connect to your local SQL Server instance.
2. Right-click **Databases**, then **Restore Database...**
3. Choose **Device**, click **...**, then **Add**, and select the `.bak` file in the `/Database` folder.
4. Click **OK**, then **OK** again to restore.
5. Open `appsettings.json` and set `ConnectionStrings:DefaultConnection` to point at your instance and the restored database.
6. Run the project and sign in with any of the test accounts above.

### Building it from scratch instead

If you would rather create an empty database, run the two scripts in `/Database` in order:

1. `CreateTablesScript.sql` creates the database and the three tables with their constraints.
2. `SeedUserScript.sql` inserts the eight users above.

Both are one-shot. They do not drop or clear anything, so run them against a fresh database only.

<img width="1912" height="980" alt="image" src="https://github.com/user-attachments/assets/a4791bde-19b2-40a2-a364-91213e0e2f65" />
<img width="1896" height="975" alt="image" src="https://github.com/user-attachments/assets/53c8d669-413a-4a08-b5c4-37668e103446" />
<img width="1895" height="982" alt="image" src="https://github.com/user-attachments/assets/6aa1cb7c-4276-4322-b40f-7ca6efcc0df7" />
<img width="1895" height="982" alt="image" src="https://github.com/user-attachments/assets/009e7d9d-f91b-48ca-8655-fc50721215b4" />
<img width="1892" height="987" alt="image" src="https://github.com/user-attachments/assets/ac3235b2-892f-47a6-96bf-d021bd38ae27" />



