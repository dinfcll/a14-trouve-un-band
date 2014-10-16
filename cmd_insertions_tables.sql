insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location) 
	values('Steven','Seagel',convert(datetime,'10/04/1952'),'Stevy','stevenseagel@hotmail.com','12345','Lévis');

insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('Chuck','Norris',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');

insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user1','last1',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');

	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user2','last2',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user3','last3',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user4','last4',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user5','last5',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user6','last6',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user7','last7',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user8','last8',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user9','last9',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user10','last10',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user11','last11',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user12','last12',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
	insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('user13','last13',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');
	
insert into Bands(Name, Description, Location)
	values('The wild Mofos', 'We are the Wild Mofos and we fear nothing, even your mom', 'Las Vegas');

insert into Bands(Name, Description, Location)
	values('Loss Sanchez', 'Lost in the desert cooking pancakes!', 'Unknown');
	
insert into Bands(Name, Description, Location)
	values('band1', 'test', 'Unknown');
	
	insert into Bands(Name, Description, Location)
	values('band2', 'test', 'Unknown');
	
	insert into Bands(Name, Description, Location)
	values('band3', 'test', 'Unknown');
	
	insert into Bands(Name, Description, Location)
	values('band4', 'test', 'Unknown');
	
	insert into Bands(Name, Description, Location)
	values('band5', 'test', 'Unknown');
	
	insert into Bands(Name, Description, Location)
	values('band6', 'test', 'Unknown');
	
	insert into Bands(Name, Description, Location)
	values('band7', 'test', 'Unknown');
	
	insert into Bands(Name, Description, Location)
	values('band8', 'test', 'Unknown');
	
	insert into Bands(Name, Description, Location)
	values('band9', 'test', 'Unknown');
	
	insert into Bands(Name, Description, Location)
	values('band10', 'test', 'Unknown');
	
	insert into Bands(Name, Description, Location)
	values('band11', 'test', 'Unknown');
	
	
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

insert into Join_Band_Genre values(1, 1);
insert into Join_Band_Genre values(2, 2);
insert into Join_Band_Genre values(2, 4);

insert into Join_Musician_Instrument values(1,1);
insert into Join_Musician_Instrument values(1,2);
insert into Join_Musician_Instrument values(2,3);
