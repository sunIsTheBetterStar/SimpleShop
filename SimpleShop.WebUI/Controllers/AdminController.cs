﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Domain.Abstract;
using SimpleShop.Domain.Entities;

namespace SimpleShop.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // Declaring variables
        private IGameRepository repository;
        // Declaring constructors
        public AdminController(IGameRepository repo)
        {
            repository = repo;
        }
        // Declaring methods
        public ViewResult Index()
        {
            return View(repository.Games);
        } // end Index()

        public ViewResult Edit(int Id)
        {
            Game game = repository.Games.FirstOrDefault(s => s.Id == Id);
            return View(game);
        }// end Edit() #1

        [HttpPost]
        public ActionResult Edit(Game game, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    game.ImageMimeType = image.ContentType;
                    game.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(game.ImageData, 0, image.ContentLength);
                }

                repository.SaveGame(game);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены ", game.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(game);
            }
        } // end Edit() #2

        public ViewResult Create()
        {
            return View("Edit", new Game());
        } // end Create()

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Game deletedGame = repository.DeleteGame(Id);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Игра \"{0}\" была удалена", deletedGame.Name);
            }
            return RedirectToAction("Index");
        } // end Delete()

    } // end controller

} // end namespace