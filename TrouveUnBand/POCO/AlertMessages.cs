using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrouveUnBand.Models;

namespace TrouveUnBand.POCO
{
    public static class AlertMessages
    {
        //SUCCESS
        public static string BAND_CREATION_SUCCESS (Band band)
        {
            return String.Format("{0} a été créé avec succès.", band.Name);
        }  
        //INFO

        //WARNING
        public static string MUSICIAN_ALREADY_SELECTED (User user)
        {
            return String.Format("Le musicien {0} {1} est déjà sélectionné.", user.FirstName, user.LastName);
        }
        public static string GENRE_ALREADY_SELECTED (Genre genre)
        {
            return String.Format("Le genre {0} est déjà sélectionné.", genre.Name);
        }
        public static string EXISTING_BAND (Band OldBand, Band NewBand)
        {
            return String.Format("le nom de band {0} ({1}) existe déjà. Il sera différencié par sa location: {2} ({3}).", OldBand.Name, OldBand.Location, NewBand.Name, NewBand.Location);
        }
        //DANGER
        public static string NOT_CONNECTED = "Vous n'êtes pas connecté. Veuillez vous connecter ou vous enregistrer.";
        public static string NOT_MUSICIAN = "Aucun profile musicien n'est associé a votre compte.";
        public static string INTERNAL_ERROR = "Une erreur interne s'est produite, veuillez réessayer plus tard.";
        public static string EMPTY_INPUT = "Veuillez remplir tout les champs.";
        public static string EMPTY_SEARCH = "Le champ de recherche est vide";
        public static string REGISTRATION_CONFIRMED = "L'inscription est confirmée!";
        public static string POSTED_FILES_ERROR = "Une erreur s'est produite lors de l'ouverture du fichier. Veuillez réessayer.";
        public static string FILE_TYPE_INVALID = "Le type du fichier n'est pas valide. Assurez-vous que le fichier soit bien une image. ";
        public static string PICTURE_CHANGED = "La photo de profil a été modifiée avec succès.";
    }
}