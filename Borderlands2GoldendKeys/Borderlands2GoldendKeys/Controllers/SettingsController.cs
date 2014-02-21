using Borderlands2GoldendKeys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Borderlands2GoldendKeys.Controllers
{
    [Authorize(Roles=RoleNames.Admin)]
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Settings/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Settings/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Settings/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Settings/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Settings/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // TODO import/export Datas: http://westdiscgolf.blogspot.fr/2014/01/ravendb-import-export-in-code.html

        //
        // POST: /Settings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
