__author__ = 'Renaud Laine'


class SqlGenerator():

    """
    Class used to generate SQL scripts from text files.

    :param main_script: Use if you which to append the generated SQL scripts to an existing file.
    :type main_script: String containing the path to a SQL file.
    """
    def __init__(self, main_script=None):
        self.main_script = main_script

    """
    Use this to generate a genres insertion script.

    :param source_path: Path of the text file from which to generate the script.
    :param destination_path: Path of the generated SQL script. This parameter is mandatory of you did not enter a
                             main_script path.
    """
    def generate_genres_script(self, source_path, destination_path=None):

        if destination_path is None and self.main_script is None:
            raise Exception("You did not specify any destination path.")

        if destination_path is None:
            destination_path = self.main_script
            sql_file = open(destination_path, 'a')
        else:
            sql_file = open(destination_path, 'w')

        text_file = open(source_path, 'r')

        genre_id = 0
        nb_genres = 0
        iterations = 0

        text_file.readline()

        for line in text_file:
            genre = line.rstrip('\n')
            iterations += 1

            if line.startswith('::'):
                genre = genre[2:]  # Removes '::'
                sql_file.write('--Insertion des genres appartenant à %s\n' % genre)
                sql_file.write('insert into Genres (name) values (\'%s\');\n' % genre)
                genre_id = iterations - nb_genres
                nb_genres += 1

            elif not line.startswith('\n'):
                sql_file.write("insert into Genres (name, parent_id) values ('%s', %s);\n" % (genre, genre_id))


if __name__ == '__main__':
    generator = SqlGenerator()
    generator.generate_genres_script('musical_genres.txt', 'insertions_genres.sql')
    print("done")
