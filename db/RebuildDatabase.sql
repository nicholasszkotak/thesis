USE [master]

IF EXISTS (SELECT name FROM sys.tables WHERE name = N'Ports')
BEGIN
  DROP TABLE [dbo].[Ports]
END

IF EXISTS (SELECT name FROM sys.tables WHERE name = N'Computers')
BEGIN
  DROP TABLE [dbo].[Computers]
END

CREATE TABLE [dbo].[Computers]
(
  [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  [RAM] INT NOT NULL,
  [DiskSpace] INT NOT NULL,
  [DiskType] NVARCHAR(3) NOT NULL,
  [GraphicsCard] NVARCHAR(255) NOT NULL,
  [Weight] DECIMAL(10,2) NOT NULL,
  [Power] INT NOT NULL,
  [Processor] NVARCHAR(255) NOT NULL
)

CREATE TABLE [dbo].[Ports]
(
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [ComputerId] UNIQUEIDENTIFIER NOT NULL,
    [Type] NVARCHAR(10) NOT NULL,
    [Quantity] INT NOT NULL,
    CONSTRAINT FK_ComputerId FOREIGN KEY (ComputerId) REFERENCES [dbo].[Computers] (Id)
)

-- Populate static data

DECLARE @firstId UNIQUEIDENTIFIER = NEWID();
DECLARE @secondId UNIQUEIDENTIFIER = NEWID();
DECLARE @thirdId UNIQUEIDENTIFIER = NEWID();
DECLARE @fourthId UNIQUEIDENTIFIER = NEWID();
DECLARE @fifthId UNIQUEIDENTIFIER = NEWID();
DECLARE @sixthId UNIQUEIDENTIFIER = NEWID();
DECLARE @seventhId UNIQUEIDENTIFIER = NEWID();
DECLARE @eighthId UNIQUEIDENTIFIER = NEWID();
DECLARE @ninthId UNIQUEIDENTIFIER = NEWID();
DECLARE @tenthId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[Computers]
(
    Id,
    RAM,
    DiskSpace,
    DiskType,
    GraphicsCard,
    Weight,
    Power,
    Processor
)
VALUES
    (@firstId, 8*1024,  1*1024, 'SSD','NVIDIA GeForce GTX 770',  8.1,       500,  'Intel Celeron N3050 Processor'),
    (@secondId, 16*1024, 2*1024, 'HDD','NVIDIA GeForce GTX 960',  12,        500,  'AMD FX 4300 Processor'),
    (@thirdId, 8*1024,  3*1024, 'HDD','Radeon R7360',            16*.454,   450,  'AMD Athlong Quad-Core APU Atholon 5150'),
    (@fourthId, 16*1024, 4*1024, 'HDD','NVIDIA GeForce GTX 1080', 13.8*.454, 500,  'AMD FX 8-Core Black Edition FX-8350'),
    (@fifthId, 32*1024, 750,    'SSD','Radeon RX 480',           7,         1000, 'AMD FX 8-Core Black Edition FX-8370'),
    (@sixthId, 32*1024, 2*1024, 'SSD','Radeon R9 380',           6,         450,  'Intel Core i7-6700K 4GHz Processor'),
    (@seventhId, 8*1024,  2*1024, 'HDD','NVIDIA GeForce GTX 1080', 15*.454,   1000, 'Intel Core i5-6400 Processor'),
    (@eighthId, 16*1024, 500,    'SDD','NVIDIA GeForce GTX 770',  8*.454,    750,  'Intel Core i5-6400 Processor'),
    (@ninthId, 2*1024,  2*1024, 'HDD','AMD FirePro W4100',       9,         508,  'Intel Core i7 Extreme Edition 3 GHz Processor'),
    (@tenthId, 512,     80,     'SSD','Radeon RX 480',           22*.454,   700,  'Intel Core i5-6400 Processor')

INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @firstId, 'USB 3.0', 2);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @firstId, 'USB 2.0', 4);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @secondId, 'USB 3.0', 3);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @secondId, 'USB 2.0', 4);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @thirdId, 'USB 3.0', 4);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @thirdId, 'USB 2.0', 4);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @fourthId, 'USB 2.0', 5);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @fourthId, 'USB 3.0', 4);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @fifthId, 'USB 2.0', 2);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @fifthId, 'USB 3.0', 2);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @fifthId, 'USB C', 1);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @sixthId, 'USB C', 2);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @sixthId, 'USB 3.0', 4);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @seventhId, 'USB 3.0', 8);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @eighthId, 'USB 2.0', 4);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @ninthId, 'USB 2.0', 10);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @ninthId, 'USB 3.0', 10);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @ninthId, 'USB C', 10);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @tenthId, 'USB 3.0', 19);
INSERT INTO Ports (Id, ComputerId, Type, Quantity) VALUES (NEWID(), @tenthId, 'USB 2.0', 4);
