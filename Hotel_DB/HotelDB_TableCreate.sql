-- Table Guest

CREATE TABLE Guest (
  Guest_Id INTEGER NOT NULL PRIMARY KEY,
  FullName NVARCHAR(100) NULL,
  BirthDate DATETIME NULL,
  Address NVARCHAR(100) NULL ,
);


-- Table Category
	
CREATE TABLE Category (
  Category_Id INTEGER NOT NULL  PRIMARY KEY,
  CategoryName NVARCHAR(20) NULL,
  CategoryDescripton NVARCHAR(300) NULL,
);


-- Table Room
		
CREATE TABLE Room (
  Room_Id INTEGER NOT NULL  PRIMARY KEY,
  Category_Id INTEGER NOT NULL FOREIGN KEY REFERENCES [Category](Category_Id),
  Number INTEGER NULL ,
  Cost MONEY NULL ,
  Places TINYINT NULL ,
);


-- Table Status
		
CREATE TABLE ReservationStatus (
  Status_Id INTEGER NOT NULL  PRIMARY KEY,
  StatusName NVARCHAR(20) NULL ,
);


-- Table Reservation
		
CREATE TABLE Reservation (
  Reservation_Id INTEGER NOT NULL  PRIMARY KEY,
  Room_Id INTEGER NOT NULL FOREIGN KEY REFERENCES [Room](Room_Id),
  Guest_Id INTEGER NOT NULL FOREIGN KEY REFERENCES [Guest](Guest_Id),
  Status_Id INTEGER NOT NULL FOREIGN KEY REFERENCES [ReservationStatus](Status_Id),
  ArrivalDate DATETIME NULL ,
  CheckOutDate DATETIME NULL ,
);