﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Domain.Abstract;
using SimpleShop.Domain.Entities;
using SimpleShop.WebUI.Models;


namespace SimpleShop.WebUI.Controllers
{
    public class GameController : Controller
    {
        // Declaring variables
        private IGameRepository repository;
        public int pageSize = 4;
        // Declaring constructors
        public GameController(IGameRepository repo)
        {
            repository = repo;
        }
        // Declaring methods
        public ViewResult List(string category, int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel()
            {
                Games = repository.Games.Where(w => category == null || w.Category == category).OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Games.Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }



    } // end controller

} // end namespace