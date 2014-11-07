create table Users(
	User_ID int IDENTITY(1,1) primary key,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Nickname NVARCHAR(100) NOT NULL,
	BirthDate DATETIME NOT NULL,
	Gender NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	Password NVARCHAR(100) NOT NULL,
	Photo varbinary(max),
	Location NVARCHAR(100) NOT NULL,
	Latitude FLOAT(40),
	Longitude FLOAT(40),
	Description text null
);

create table Bands(
	Band_ID int IDENTITY(1,1) Primary key,
	Name NVARCHAR(50) not null,
	Description text null,
	Location NVARCHAR(100) NOT NULL,
	Photo varbinary(max)
);

create table Instruments(
	Instrument_ID int IDENTITY(1,1) Primary Key,
	Name NVARCHAR(50) not null
);

create table Genres(
	Genre_ID int IDENTITY(1,1) Primary Key,
	Parent_ID int null references Genres(Genre_ID),
	Name NVARCHAR(50) not null
);

create table Events(
	Event_ID int IDENTITY(1,1) Primary key,
	Name NVARCHAR(100) NOT NULL,
	Location NVARCHAR(100) NOT NULL,
	Address NVARCHAR(100) NOT NULL,
	City NVARCHAR(100) NOT NULL,
	EventDate datetime NOT NULL,
	MaxAudience int NOT NULL,
	Salary float(10) NOT NULL,
	StageSize CHAR not null,
	Photo varbinary(max),
	Creator_ID int constraint fk_event_creator_id references Users(User_ID)
);

create table Events_Genres(
	Event_ID int not null references Events(Event_ID),
	Genre_ID int not null references Genres(Genre_ID),
	constraint pk_events_genres primary key (Event_ID, Genre_ID)
);

create table Events_Bands(
	Event_ID int not null references Events(Event_ID),
	Band_ID int not null references Bands(Band_ID),
	constraint pk_events_bands primary key (Event_ID, Band_ID)
);

create table Events_Users(
	Event_ID int not null references Events(Event_ID),
	User_ID int not null references Users(User_ID),
	constraint pk_events_users primary key (Event_ID, User_ID)
);

create table Users_Genres(
	User_ID int REFERENCES Users(User_ID) not null,
	Genre_ID int REFERENCES Genres(Genre_ID) not null,
	constraint pk_join_User_genre Primary Key (User_ID, Genre_ID)
);

create table Users_Instruments(
	User_ID int REFERENCES Users(User_ID) not null,
	Instrument_ID int REFERENCES Instruments(Instrument_ID) not null,
	Skills int not null,
	constraint pk_join_User_instrument primary key (User_ID, Instrument_ID)
);

create table Bands_Genres(
	Band_ID int REFERENCES Bands(Band_ID) not null,
	Genre_ID int REFERENCES Genres(Genre_ID) not null,
	constraint pk_join_bands_genres Primary Key (Band_ID, Genre_ID)
);

create table Bands_Users(
	Band_ID int REFERENCES Bands(Band_ID) not null,
	User_ID int REFERENCES Users(User_ID) not null,
	constraint pk_join_bands_Users Primary Key (Band_ID, User_ID)
);

create table Adverts(
	Advert_ID int IDENTITY(1,1) Primary key,
	Type NVARCHAR(100) NOT NULL,
	Creator_ID int FOREIGN KEY REFERENCES Users(User_ID) NOT NULL,
	Description NVARCHAR(max) NOT NULL,
	Status nvarchar(100) NOT NULL,
	CreationDate DATETIME NOT NULL,
	ExpirationDate DATETIME NOT NULL,
	Location NVARCHAR(100),
	Photo varbinary(max)
);

create table Adverts_Genres(
	Advert_ID int references Adverts(Advert_ID),
	Genre_ID int references Genres(Genre_ID),
	constraint pk_join_Adverts_Genres Primary Key (Advert_ID, Genre_ID)
);

insert into Instruments(Name) Values('Aucun');
insert into Instruments (Name) values('Guitare');
insert into Instruments (Name) values('Basse');
insert into Instruments (Name) values('Batterie');
insert into Instruments (Name) values('Chant');
insert into Instruments (Name) values('Piano');