IF DB_ID('CapEx') IS NULL
    CREATE DATABASE CapEx;
GO

USE CapEx;
GO

CREATE TABLE dbo.Users
(
    UserId      INT             IDENTITY(1,1) NOT NULL,
    UserName    NVARCHAR(100)   NOT NULL,
    Email       NVARCHAR(256)   NOT NULL,
    Password    NVARCHAR(200)   NOT NULL,
    Role        TINYINT         NOT NULL CONSTRAINT DF_Users_Role DEFAULT (0),

    CONSTRAINT PK_Users            PRIMARY KEY CLUSTERED (UserId),
    CONSTRAINT UQ_Users_Email      UNIQUE (Email),
    CONSTRAINT CK_Users_Role       CHECK (Role BETWEEN 0 AND 3),
    CONSTRAINT CK_Users_Email      CHECK (Email LIKE '%_@_%._%')
);
GO

CREATE TABLE dbo.Requests
(
    RequestId           INT             IDENTITY(1,1) NOT NULL,
    RequestedByUserId   INT             NOT NULL,
    Title               NVARCHAR(200)   NOT NULL,
    Amount              DECIMAL(18,2)   NOT NULL,
    Motivation          NVARCHAR(2000)  NOT NULL,
    CreatedUtc          DATETIME2(3)    NOT NULL CONSTRAINT DF_Requests_CreatedUtc DEFAULT (SYSUTCDATETIME()),
    Status              TINYINT         NOT NULL CONSTRAINT DF_Requests_Status DEFAULT (0),

    CONSTRAINT PK_Requests                PRIMARY KEY CLUSTERED (RequestId),
    CONSTRAINT FK_Requests_Users          FOREIGN KEY (RequestedByUserId) REFERENCES dbo.Users (UserId),
    CONSTRAINT CK_Requests_Amount         CHECK (Amount > 0),
);
GO

CREATE INDEX IX_Requests_RequestedByUserId ON dbo.Requests (RequestedByUserId);
CREATE INDEX IX_Requests_Status            ON dbo.Requests (Status);
GO

CREATE TABLE dbo.Approvals
(
    ApprovalId      INT             IDENTITY(1,1) NOT NULL,
    RequestId       INT             NOT NULL,
    ActedByUserId   INT             NOT NULL,
    Approved        BIT             NOT NULL,
    Comment         NVARCHAR(1000)  NULL,
    CreatedUtc      DATETIME2(3)    NOT NULL CONSTRAINT DF_Approvals_CreatedUtc DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT PK_Approvals                 PRIMARY KEY CLUSTERED (ApprovalId),
    CONSTRAINT FK_Approvals_Requests        FOREIGN KEY (RequestId) REFERENCES dbo.Requests (RequestId) ON DELETE CASCADE,
    CONSTRAINT FK_Approvals_Users           FOREIGN KEY (ActedByUserId) REFERENCES dbo.Users (UserId),
);
GO

CREATE INDEX IX_Approvals_RequestId ON dbo.Approvals (RequestId);
GO
