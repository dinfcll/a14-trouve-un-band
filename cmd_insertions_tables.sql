
insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location) 
	values('Steven','Seagel',convert(datetime,'10/04/1952'),'Stevy','stevenseagel@hotmail.com','12345','Lévis');

insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('Chuck','Norris',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');

insert into Bands(Name, Description, Location)
	values('The wild Mofos', 'We are the Wild Mofos and we fear nothing, even your mom', 'Las Vegas');

insert into Bands(Name, Description, Location)
	values('Loss Sanchez', 'Lost in the desert cooking pancakes!', 'Unknown');

insert into Musicians(UserId, Description)
  values(1, 'Good');

insert into Musicians(UserId, Description)
  values(2, 'Expérience comme joueur de triangle');

insert into Join_Band_Musician values(1, 1);
insert into Join_Band_Musician values(1, 2);

insert into Genres(Name) values ('Metal');
insert into Genres(Name) values ('Rock');
insert into Genres(Name) values ('Classique');
insert into Genres(Name) values ('Jazz');
insert into Genres(Name) values ('Chuck Norris Style');

insert into Instruments(Name) Values('Guitare');
insert into Instruments(Name) Values('Basse');
insert into Instruments(Name) Values('Piano');
insert into Instruments(Name) Values('Drums');

insert into Join_Band_Genre values(1, 1);
insert into Join_Band_Genre values(2, 2);
insert into Join_Band_Genre values(2, 4);

insert into Join_Musician_Instrument values(1,1);
insert into Join_Musician_Instrument values(1,2);
insert into Join_Musician_Instrument values(2,3);