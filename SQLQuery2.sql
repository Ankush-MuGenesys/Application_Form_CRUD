use AppFormDB;

select * from Users;

CREATE TABLE Users (
    Id INT PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Grade NVARCHAR(10) NOT NULL
);
insert into Users(Id, UserName, Password) Values (101,'Ankush','Ankush@101'),
	(102, 'Tushar', 'Tushar@101'), (103,'Adhar','Adhar@103'), (104,'Shweta','Shweta@104');