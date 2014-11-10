using System;
using System.Collections.Generic;
using TrouveUnBand.Models;
using System.Linq;

namespace TrouveUnBand.Classes
{
    public static class CreateProfile
    {
        private static TrouveUnBandEntities db = new TrouveUnBandEntities();

        public static MusicianProfileViewModel CreateMusicianProfileView(User user)
        {
            MusicianProfileViewModel MusicianView = new MusicianProfileViewModel();

            List<User> MusicianList = new List<User>();
            MusicianList.Add(user);
            List<Musician_Instrument> InstrumentInfos = SetMusician_Instrument(MusicianList);

            MusicianView.InstrumentInfo = InstrumentInfos[0];
            MusicianView.Description = user.Description;
            MusicianView.Name = user.FirstName + " " + user.LastName;
            MusicianView.Location = user.Location;
            MusicianView.ProfilePicture.PhotoSrc = "data:image/jpeg;base64," + Convert.ToBase64String(user.Photo);

            return MusicianView;
        }

        public static BandProfileViewModel CreateBandProfileView(Band band)
        {
            BandProfileViewModel BandView = new BandProfileViewModel();

            BandView.InstrumentInfoList = SetMusician_Instrument(band.Users.ToList());
            BandView.Name = band.Name;
            BandView.Description = band.Description;
            BandView.Location = band.Location;

            return BandView;
        }

        private static List<Musician_Instrument> SetMusician_Instrument(List<User> musicians)
        {
            List<Musician_Instrument> InstrumentInfoList = new List<Musician_Instrument>();
            ICollection<Users_Instruments> ListOfInstruments;
            List<string> SkillList = new List<string> { "Aucun", "Débutant", "Initié", "Intermédiaire", "Avancé", "Légendaire" };

            foreach (var musician in musicians)
            {
                ListOfInstruments = musician
                                    .Users_Instruments
                                    .OrderByDescending(x => (x.Skills))
                                    .ToList();

                var InstrumentInfo = new Musician_Instrument();

                foreach (var instrument in ListOfInstruments)
                {
                    InstrumentInfo.InstrumentNames
                       .Add(instrument.Instrument.Name);

                    InstrumentInfo.Skills
                        .Add(SkillList[instrument.Skills]);
                }
                InstrumentInfoList.Add(InstrumentInfo);
            }

            return InstrumentInfoList;
        }
    }
}
