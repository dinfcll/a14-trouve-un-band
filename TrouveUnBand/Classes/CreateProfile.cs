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
            
            List<User> MusicianList = new List<User>();
            MusicianList.Add(user);
            List<Musician_Instrument> instrumentInfos = SetMusician_Instrument(MusicianList);
            var musicianView = new MusicianProfileViewModel
            {


                InstrumentInfo = instrumentInfos[0],
                Description = user.Description,
                Name = user.FirstName + " " + user.LastName,
                Location = user.Location,
                Photo = user.Photo,
                Id = user.User_ID
            };

            return musicianView;
        }

        public static BandProfileViewModel CreateBandProfileView(Band band)
        {
            var bandView = new BandProfileViewModel
            {
                InstrumentInfoList = SetMusician_Instrument(band.Users.ToList()),
                Name = band.Name,
                Description = band.Description,
                Location = band.Location,
                Photo = band.Photo,
                id = band.Band_ID
            };

            return bandView;
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
