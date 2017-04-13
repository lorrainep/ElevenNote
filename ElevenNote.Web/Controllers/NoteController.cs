﻿using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.Web.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        public ActionResult Index()
        {

            var service = CreateNoteService();
            var model = service.GetNotes();

            return View(model);
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);


            // var userId = Guid.Parse(User.Identity.GetUserId());
            //var service = new NoteService(userId);
            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");

            }

            ModelState.AddModelError("", "Your note could not be created.");
            return View(model);

        }

        private NoteService CreateNoteService()
        {
            //can't do 
            //public NoteControlller() 
            //{
            //  _scc = CreateNoteService();
            //}
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
}