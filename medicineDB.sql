CREATE DATABASE medicineDB
GO
USE medicineDB
GO
CREATE TABLE medicineType
(
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(50) NOT NULL,
	serialNo INT NOT NULL
)
GO
CREATE TABLE [type]
(
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(50) NOT NULL
)
GO
CREATE TABLE price
(
	medicineTypeId INT,
	typeId INT,
	price MONEY,
	PRIMARY KEY(medicineTypeId,typeId)
)
GO
CREATE TABLE users
(
	userId INT PRIMARY KEY IDENTITY,
	userName NVARCHAR(50) NOT NULL UNIQUE,
	fullName NVARCHAR(50) NOT NULL,
	email NVARCHAR(30) NOT NULL,
	contactNo NVARCHAR(30) NOT NULL,
	userPassword NVARCHAR(50) NOT NULL
)
GO
CREATE TABLE medicines
(
	medicineId INT PRIMARY KEY IDENTITY,
	medicineName NVARCHAR(50) NOT NULL,
	price MONEY NOT NULL,
	generic NVARCHAR(30) NOT NULL,
	picture VARBINARY(MAX) NULL,
	typeId INT REFERENCES [type](id),
	expiredDate DATE NULL,
	stock BIT NULL
)
GO


