# CapEx — Capital Expenditure Request & Multi-Level Approval

Internal tool that replaces the email-and-Excel CapEx approval process.
Blazor Web App (.NET 8, interactive server rendering) + SQL Server via EF Core.

## Approval tiers

Any request over R5,000 needs formal approval. Approvers act in order.

| Amount requested    | Required approvers (in order)                   | Final status if all approve |
| ------------------- | ----------------------------------------------- | --------------------------- |
| Under R5,000        | None — auto-approved                            | Approved                    |
| R5,000 – R24,999    | Department Manager                              | Approved                    |
| R25,000 – R99,999   | Department Manager → Finance Director           | Approved                    |
| R100,000 and above  | Department Manager → Finance Director → CEO     | Approved                    |

A single rejection at any stage sets the request to **Rejected** immediately.

## Approvers

| Role               | User            |
| ------------------ | --------------- |
| Department Manager | Bob Lockwood    |
| Finance Director   | Alice Liddle    |
| CEO                | Jeff Sidebottom |

## Database

- `dbo.Users` — UserId, UserName, Email, Password, Role (TINYINT → enum)
- `dbo.Requests` — RequestId, RequestedByUserId, Title, Amount, Motivation, CreatedUtc, Status (TINYINT → enum)
- `dbo.Approvals` — ApprovalId, RequestId, ActedByUserId, Approved, Comment, CreatedUtc

## Running it

```
dotnet run
```

Connection string lives in `appsettings.json` under `ConnectionStrings:DefaultConnection`.
