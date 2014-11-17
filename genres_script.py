#!/usr/bin/python
# -*- coding: utf-8 -*-

f_src = open('musical_genres.txt', 'r')
f_dest = open('insertions_genres.sql', 'w')

genre_id = 0
subgenre_id = 0

f_src.readline()

for line in f_src:

	genre = line.rstrip('\n')

	if line.startswith('::'):
		genre_id += 1
		genre = genre[2:]
		f_dest.write('--Insertion des genres appartenant à %s\n' % genre)
		f_dest.write("insert into Genres values('%s');\n" % genre)

		print "::" + genre

	elif not line.startswith('\n'):
		subgenre_id += 1
		f_dest.write("insert into Sub_Genres (GenreId, Name) values ('%s', '%s');\n" % (genre_id, genre))

		print genre


