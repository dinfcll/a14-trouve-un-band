using System;
using System.Collections.Generic;
using TrouveUnBand.Models;
using System.Linq;

namespace TrouveUnBand.Classes
{
    public static class CreateProfile
    {
        private static TrouveUnBandEntities db = new TrouveUnBandEntities();

        public static MusicianProfileViewModel CreateMusicianProfileView(Musician musician)
        {
            MusicianProfileViewModel MusicianView = new MusicianProfileViewModel();
            User user = db.Users.FirstOrDefault(x => x.UserId == musician.UserId);

            List<Musician> MusicianList = new List<Musician>();
            MusicianList.Add(musician);
            List<Musician_Instrument> InstrumentInfos = SetMusician_Instrument(MusicianList);

            MusicianView.InstrumentInfo = InstrumentInfos[0];
            MusicianView.Description = musician.Description;
            MusicianView.Name = user.FirstName + " " + user.LastName;
            MusicianView.Location = user.Location;
            MusicianView.ProfilePicture.PhotoSrc = "data:image/jpeg;base64," + Convert.ToBase64String(user.Photo);

            return MusicianView;
        }

        public static BandProfileViewModel CreateBandProfileView(Band band)
        {
            BandProfileViewModel BandView = new BandProfileViewModel();

            BandView.InstrumentInfoList = SetMusician_Instrument(band.Musicians.ToList());
            BandView.Name = band.Name;
            BandView.Description = band.Description;
            BandView.Location = band.Location;

            return BandView;
        }

        private static List<Musician_Instrument> SetMusician_Instrument(List<Musician> musicians)
        {
            List<Musician_Instrument> InstrumentInfoList = new List<Musician_Instrument>();
            ICollection<Join_Musician_Instrument> ListOfInstruments;
            List<string> SkillList = new List<string> { "Aucun", "Débutant", "Initié", "Intermédiaire", "Avancé", "Légendaire" };

            foreach (var musician in musicians)
            {
                ListOfInstruments = musician
                                    .Join_Musician_Instrument
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