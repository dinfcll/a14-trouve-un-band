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
	Description text null,
	CreationDate datetime not null DEFAULT (GETDATE())
);

create table Bands(
	Band_ID int IDENTITY(1,1) Primary key,
	Name NVARCHAR(50) not null,
	Description text null,
	Location NVARCHAR(100) NOT NULL,
	Photo varbinary(max),
	CreationDate datetime not null DEFAULT (GETDATE())
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
	Creator_ID int constraint fk_event_creator_id references Users(User_ID),
	Description text not null,
	CreationDate datetime not null DEFAULT (GETDATE())
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
	Creator_ID int FOREIGN KEY REFERENCES Users(User_ID) NOT NULL,
	Type NVARCHAR(100) NOT NULL,	
	Description NVARCHAR(max) NOT NULL,
	Status nvarchar(100) NOT NULL,
	CreationDate datetime not null DEFAULT (GETDATE()),
	ExpirationDate DATETIME NOT NULL,
	Location NVARCHAR(100),
	Photo varbinary(max)
);

create table Adverts_Genres(
	Advert_ID int references Adverts(Advert_ID) not null,
	Genre_ID int references Genres(Genre_ID) not null,
	constraint pk_join_Adverts_Genres Primary Key (Advert_ID, Genre_ID)
);

insert into Instruments(Name) Values('Aucun');
insert into Instruments (Name) values('Guitare');
insert into Instruments (Name) values('Basse');
insert into Instruments (Name) values('Batterie');
insert into Instruments (Name) values('Chant');
insert into Instruments (Name) values('Piano');

--Insertion des genres appartenant à Blues
insert into Genres (name) values ('Blues');
insert into Genres (name, parent_id) values ('Blues rock', 1);
insert into Genres (name, parent_id) values ('Country blues', 1);
insert into Genres (name, parent_id) values ('Jazz blues', 1);
insert into Genres (name, parent_id) values ('Piano blues', 1);
insert into Genres (name, parent_id) values ('Soul blues', 1);
--Insertion des genres appartenant à Easy Listening
insert into Genres (name) values ('Easy Listening');
insert into Genres (name, parent_id) values ('Background music', 7);
insert into Genres (name, parent_id) values ('Beautiful music', 7);
insert into Genres (name, parent_id) values ('Lounge music', 7);
insert into Genres (name, parent_id) values ('New-age music', 7);
--Insertion des genres appartenant à Electronic
insert into Genres (name) values ('Electronic');
insert into Genres (name, parent_id) values ('Ambient', 12);
insert into Genres (name, parent_id) values ('Asian Underground', 12);
insert into Genres (name, parent_id) values ('Breakbeat', 12);
insert into Genres (name, parent_id) values ('Chiptune', 12);
insert into Genres (name, parent_id) values ('Disco', 12);
insert into Genres (name, parent_id) values ('Downtempo', 12);
insert into Genres (name, parent_id) values ('Drum and bass', 12);
insert into Genres (name, parent_id) values ('Electro', 12);
insert into Genres (name, parent_id) values ('Electroacoustic', 12);
insert into Genres (name, parent_id) values ('Electronica', 12);
insert into Genres (name, parent_id) values ('Electronic rock', 12);
insert into Genres (name, parent_id) values ('Hardcore/Hard dance', 12);
insert into Genres (name, parent_id) values ('House', 12);
insert into Genres (name, parent_id) values ('Industrial', 12);
insert into Genres (name, parent_id) values ('Progressive', 12);
insert into Genres (name, parent_id) values ('Techno', 12);
insert into Genres (name, parent_id) values ('Trance', 12);
--Insertion des genres appartenant à Folk
insert into Genres (name) values ('Folk');
insert into Genres (name, parent_id) values ('Contemporary folk', 30);
insert into Genres (name, parent_id) values ('Celtic music', 30);
insert into Genres (name, parent_id) values ('Indie folk', 30);
insert into Genres (name, parent_id) values ('Neofolk', 30);
insert into Genres (name, parent_id) values ('Progressive folk', 30);
insert into Genres (name, parent_id) values ('Industrial folk', 30);
insert into Genres (name, parent_id) values ('Techno-folk', 30);
insert into Genres (name, parent_id) values ('Psychedelic folk', 30);
insert into Genres (name, parent_id) values ('Sung poetry', 30);
insert into Genres (name, parent_id) values ('Cowboy/Western music', 30);
--Insertion des genres appartenant à Hip hop
insert into Genres (name) values ('Hip hop');
insert into Genres (name, parent_id) values ('Crunkcore', 41);
insert into Genres (name, parent_id) values ('Freestyle music', 41);
insert into Genres (name, parent_id) values ('Freestyle rap', 41);
insert into Genres (name, parent_id) values ('G-Funk', 41);
insert into Genres (name, parent_id) values ('Gangsta rap', 41);
insert into Genres (name, parent_id) values ('Grime', 41);
insert into Genres (name, parent_id) values ('Hip pop', 41);
insert into Genres (name, parent_id) values ('Industrial hip hop', 41);
insert into Genres (name, parent_id) values ('Instrumental hip hop', 41);
insert into Genres (name, parent_id) values ('Jazz rap', 41);
insert into Genres (name, parent_id) values ('Ragga', 41);
insert into Genres (name, parent_id) values ('Reggaeton', 41);
insert into Genres (name, parent_id) values ('Rap rock', 41);
insert into Genres (name, parent_id) values ('Trap', 41);
insert into Genres (name, parent_id) values ('Trip hop', 41);
insert into Genres (name, parent_id) values ('Turntablism', 41);
insert into Genres (name, parent_id) values ('Underground hip hop', 41);
--Insertion des genres appartenant à Jazz
insert into Genres (name) values ('Jazz');
insert into Genres (name, parent_id) values ('Acid jazz', 59);
insert into Genres (name, parent_id) values ('Avant-garde jazz', 59);
insert into Genres (name, parent_id) values ('Free improvisation', 59);
insert into Genres (name, parent_id) values ('Gypsy jazz', 59);
insert into Genres (name, parent_id) values ('Hard bop', 59);
insert into Genres (name, parent_id) values ('Jazz fusion', 59);
insert into Genres (name, parent_id) values ('Latin jazz', 59);
insert into Genres (name, parent_id) values ('Modal jazz', 59);
insert into Genres (name, parent_id) values ('Neo-bop jazz', 59);
insert into Genres (name, parent_id) values ('Neo-swing', 59);
insert into Genres (name, parent_id) values ('Novelty ragtime', 59);
insert into Genres (name, parent_id) values ('Nu jazz', 59);
insert into Genres (name, parent_id) values ('Orchestral jazz', 59);
insert into Genres (name, parent_id) values ('Smooth jazz', 59);
insert into Genres (name, parent_id) values ('Swing', 59);
insert into Genres (name, parent_id) values ('Vocal jazz', 59);
--Insertion des genres appartenant à Rock
insert into Genres (name) values ('Rock');
insert into Genres (name, parent_id) values ('Alternative rock', 76);
insert into Genres (name, parent_id) values ('Grunge', 76);
insert into Genres (name, parent_id) values ('Indie rock', 76);
insert into Genres (name, parent_id) values ('Nu metal', 76);
insert into Genres (name, parent_id) values ('Hard rock', 76);
insert into Genres (name, parent_id) values ('Heavy metal', 76);
insert into Genres (name, parent_id) values ('Alternative metal', 76);
insert into Genres (name, parent_id) values ('Black metal', 76);
insert into Genres (name, parent_id) values ('Death metal', 76);
insert into Genres (name, parent_id) values ('Doom metal', 76);
insert into Genres (name, parent_id) values ('Metalcore', 76);
insert into Genres (name, parent_id) values ('Deathcore', 76);
insert into Genres (name, parent_id) values ('Progressive metal', 76);
insert into Genres (name, parent_id) values ('Djent', 76);
insert into Genres (name, parent_id) values ('Rap metal', 76);
insert into Genres (name, parent_id) values ('Speed metal', 76);
insert into Genres (name, parent_id) values ('Stoner rock', 76);
insert into Genres (name, parent_id) values ('Symphonic metal', 76);
insert into Genres (name, parent_id) values ('Thrash metal', 76);
insert into Genres (name, parent_id) values ('Jazz rock', 76);
insert into Genres (name, parent_id) values ('Pop rock', 76);
insert into Genres (name, parent_id) values ('Progressive rock', 76);
insert into Genres (name, parent_id) values ('Psychedelic rock', 76);
insert into Genres (name, parent_id) values ('Deathrock', 76);
insert into Genres (name, parent_id) values ('Grindcore', 76);
insert into Genres (name, parent_id) values ('Emo', 76);
insert into Genres (name, parent_id) values ('Punk', 76);
