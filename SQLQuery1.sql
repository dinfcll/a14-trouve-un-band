﻿create table Users(
IDUser int IDENTITY(1,1) Primary key,
FirstName NVARCHAR(100) NOT NULL,
LastName NVARCHAR(100) NOT NULL,
BirthDate DATETIME NOT NULL,
Nickname NVARCHAR(100) NOT NULL,
Email NVARCHAR(100) NOT NULL,
Password NVARCHAR(100) NOT NULL);

create table Musician(
IDUser int REFERENCES Users(IDUser) not null,
IDMusician int IDENTITY(1,1) Primary key,
Instrument NVARCHAR(30))
;

create table Band(
IDBand int IDENTITY(1,1) Primary key,
Name NVARCHAR(50),
Style NVARCHAR(50))
;

create table Join_Musician_Band(
IDMusician int REFERENCES Musician(IDMusician) not null,
IDBand int REFERENCES Band(IDBand) not null,
constraint JoinMB_pk primary key(IDMusician,IDBand))
;
 


insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password)
values('Steven','Seagel',convert(datetime,'10/04/1952'),'Stevy','stevenseagel@hotmail.com','12345');

insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password)
values('Steven','Seagull',convert(datetime,'10/04/1952'),'Stevie','stevie@hotmail.com','12345');

insert into Musician(IDUser,Instrument) 
values(1,'Flute de pan');

insert into Musician(IDUser,Instrument) 
values(2,'Guitar');

insert into Band(Name,Style)
values('Steven Seagel Band','Death metal');

insert into Band(Name,Style)
values('Steven Seagel Band Tribute','Christian Rock');

insert into join_musician_band
values(1,1);

insert into join_musician_band
values(2,1);

insert into join_musician_band
values(1,2);

insert into join_musician_band
values(2,2);

--select * from Users;
--select * from Musician;
--select * from band;
--select * from join_musician_band;

select firstname,lastname, b.Name from Users u
join Musician m on m.IDUser=u.IDUser
join join_musician_band j on m.IDMusician=j.IDMusician
join band b on b.IDBand=j.IDBand;