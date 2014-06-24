using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minibus.Models.Context;
using MB.Models.POCO;

namespace Minibus.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            MinibusContext mb = new MinibusContext();
            List<Bus> buses = mb.Buses.ToList();
            return View(buses);
        }

        public ActionResult Edit(int id)
        {
            MinibusContext mb = new MinibusContext();
            Bus b = mb.Buses.Where(bus => bus.ID == id).SingleOrDefault();
            return PartialView(b);
        }

        [HttpPost]
        public ActionResult Edit(Bus model)
        {
            if (ModelState.IsValid)
            {
                MinibusContext mb = new MinibusContext();
                Bus b = mb.Buses.Where(bs => bs.ID == model.ID).FirstOrDefault();
                b.MakeOfBus = model.MakeOfBus;
                
                mb.SaveChanges();
                return Content(Boolean.TrueString);
                //return Json(JsonResponseFactory.SuccessResponse(booking), JsonRequestBehavior.DenyGet);

            }
            else
            {
                return Content("please review your form");
            }
        }
    }
}
