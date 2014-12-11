using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrouveUnBand.Models;

namespace TrouveUnBand.POCO
{     
    public static class Messages
    {
        //SUCCESS
        public static string BAND_CREATION_SUCCESS (Band band)
        {
            return String.Format("{0} a été créé avec succès.", band.Name);
        }

        public static string PROFILE_UPDATED = "Le profil a été mis à jour.";
        public static string MUSICIAN_PROFILE_UPDATED = "Le profil musicien a été mis à jour.";
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

        public static string INSTRUMENT_ALREADY_SELECTED = "Vous ne pouvez pas sélectionner deux fois le même instrument";
        //DANGER
        public static string EXISTING_USER(User user)
        {
            return String.Format("L'utilisateur {0} existe déja", user.Nickname);
        }

        public static string NOT_CONNECTED = "Vous n'êtes pas connecté. Veuillez vous connecter ou vous enregistrer.";
        public static string NOT_MUSICIAN = "Aucun profile musicien n'est associé a votre compte.";
        public static string INTERNAL_ERROR = "Une erreur interne s'est produite, veuillez réessayer plus tard.";
        public static string EMPTY_INPUT = "Veuillez remplir tout les champs.";
        public static string EMPTY_SEARCH = "Le champ de recherche est vide";
        public static string REGISTRATION_CONFIRMED = "L'inscription est confirmée!";
        public static string POSTED_FILES_ERROR = "Une erreur s'est produite lors de l'ouverture du fichier. Veuillez réessayer.";
        public static string FILE_TYPE_INVALID = "Le type du fichier n'est pas valide. Assurez-vous que le fichier soit bien une image. ";
        public static string PICTURE_CHANGED = "La photo de profil a été modifiée avec succès.";
        public static string PASSWORD_NOT_MATCHING = "Le mot de passe et sa confirmation ne sont pas identiques.";
        public static string INVALID_LOGIN = "Votre identifiant/courriel ou mot de passe est incorrect. S'il vous plait, veuillez réessayer.";
    }

    public class Alert
    {
        public const string TempDataKey = "TempDataAlerts";

        public string AlertStyle { get; set; }
        public string Message { get; set; }
        public bool Dismissable { get; set; }
    }

    public static class Styles
    {
        public const string Success = "success";
        public const string Information = "info";
        public const string Warning = "warning";
        public const string Danger = "danger";
    }
}
