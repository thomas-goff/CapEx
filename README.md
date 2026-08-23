# CapEx - Capital Expenditure Request & Multi-Level Approval
Internal tool that replaces the email-and-Excel CapEx approval process.
Blazor Web App (.NET 8, interactive server rendering) + SQL Server via EF Core.

## Approval tiers

Any request over R5,000 needs formal approval. Approvers act in order.

| Amount requested    | Required approvers (in order)                   | Final status if all approve |
| ------------------- | ----------------------------------------------- | --------------------------- |
| Under R5,000        | None                                            | Approved                    |
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

## Seeded Users 
All eight seeded users share the password **`123`**.

| ID | Name | Email | Role |
|---|---|---|---|
| 1 | Thomas Goff | tgoff@inambu.co.za | 0 — Employee |
| 2 | Mark Freedman | mfreedman@inambu.co.za | 0 — Employee |
| 3 | Pieter Venter | pventer@inambu.co.za | 0 — Employee |
| 4 | Thato Dlamini | tdlamini@inambu.co.za | 0 — Employee |
| 5 | Kelly Patterson | kpatterson@inambu.co.za | 0 — Employee |
| 6 | Bob Lockwood | blockwood@inambu.co.za | 1 — Department Manager |
| 7 | Alice Liddle | aliddle@inambu.co.za | 2 — Finance Director |
| 8 | Jeff Sidebottom | jsidebottom@inambu.co.za | 3 — CEO |

Users 1–5 raise requests, 6–8 approve them, and no one does both.

## Restoring the database

1. Open SSMS and connect to your local SQL Server instance.
2. Right-click **Databases** → **Restore Database…**
3. Choose **Device**, click **…** → **Add**, and select the `.bak` file in the `/Database` folder.
4. Click **OK**, then **OK** again to restore.
5. Open `appsettings.json` and set `ConnectionStrings:DefaultConnection` to point at your instance and the restored database.
6. Run the project and sign in with any of the test accounts above.

<img width="1891" height="990" alt="image" src="https://github.com/user-attachments/assets/0efbb0b4-a637-4730-886e-22116cd4f057" />
<img width="1897" height="987" alt="image" src="https://github.com/user-attachments/assets/e54b23f8-5e8e-4736-b2b0-e468f339f35c" />
<img width="1900" height="986" alt="image" src="https://github.com/user-attachments/assets/7a638f20-9bb6-4770-8969-fb127b66d9e4" />
<img width="1899" height="984" alt="image" src="https://github.com/user-attachments/assets/67c7eb04-9a47-4eeb-b43e-490388b6b5be" />
