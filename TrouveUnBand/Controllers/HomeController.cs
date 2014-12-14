﻿using System.Collections.Generic;
using System.Web.Mvc;
using TrouveUnBand.Classes;
using TrouveUnBand.Models;
using TrouveUnBand.Services;
using TrouveUnBand.ViewModels;

namespace TrouveUnBand.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var viewModel = new HomePageViewModel();
            int i;

            var users = UserDao.GetAllUsers();
            i = 0;
            while (i < 8 && i < users.Count)
            {
                var userInfo = new UserInfo(users[i]);
                viewModel.UserList.Add(userInfo);
                i++;
            }

            var bands = BandDao.GetAllBands();
            i = 0;
            while (i < 8 && i < bands.Count)
            {
                var bandInfo = new BandInfo(bands[i]);
                viewModel.BandList.Add(bandInfo);
                i++;
            }

            var events = EventDao.GetAllEvents();
            i = 0;
            while (i < 5 && i < events.Count)
            {
                var eventInfo = new EventInfo(events[i]);
                viewModel.EventList.Add(eventInfo);
                i++;
            }

            var adverts = AdvertDao.GetAllAdverts();
            i = 0;
            while (i < 5 && i < adverts.Count)
            {
                var advertInfo = new AdvertInfo(adverts[i]);
                viewModel.AdvertList.Add(advertInfo);
                i++;
            }

            return View(viewModel);
        }
    }
}


