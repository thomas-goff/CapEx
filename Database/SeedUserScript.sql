USE CapEx;
GO

SET IDENTITY_INSERT dbo.Users ON;

INSERT INTO dbo.Users (UserId, UserName, Email, Password, Role)
VALUES
    (1, N'Thomas Goff',     N'tgoff@inambu.co.za',       N'123', 0),
    (2, N'Mark Freedman',   N'mfreedman@inambu.co.za',   N'123', 0),
    (3, N'Pieter Venter',   N'pventer@inambu.co.za',     N'123', 0),
    (4, N'Thato Dlamini',   N'tdlamini@inambu.co.za',    N'123', 0),
    (5, N'Kelly Patterson', N'kpatterson@inambu.co.za',  N'123', 0),
    (6, N'Bob Lockwood',    N'blockwood@inambu.co.za',   N'123', 1),
    (7, N'Alice Liddle',    N'aliddle@inambu.co.za',     N'123', 2),
    (8, N'Jeff Sidebottom', N'jsidebottom@inambu.co.za', N'123', 3);

SET IDENTITY_INSERT dbo.Users OFF;
GO

DBCC CHECKIDENT ('dbo.Users', RESEED, 8) WITH NO_INFOMSGS;
GO
