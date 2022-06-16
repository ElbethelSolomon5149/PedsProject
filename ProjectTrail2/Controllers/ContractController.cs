using ProjectTrail2.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTrail2.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        TrainingScheduleEntities db = new TrainingScheduleEntities();
        public ActionResult Schedule(Schedule obj)
        {
            return View(obj);
        }
        [HttpPost]
        public ActionResult AddSchedule(Schedule model)
        {
            Schedule obj = new Schedule();
            if (ModelState.IsValid)
            {
                obj.ScheduleCode = model.ScheduleCode;
                obj.Scheduleid = model.Scheduleid;
                obj.ScheduleDate = model.ScheduleDate;
                obj.ScheduleStatus = model.ScheduleStatus;
                obj.ScheduleTime = model.ScheduleTime;
                obj.Remark = model.Remark;

                if (model.Scheduleid > 0)
                {
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Schedules.Add(obj);
                    db.SaveChanges();
                }
                ModelState.Clear();
            }


            return RedirectToAction("ScheduleList");
        }


        public ActionResult ScheduleList()
        {
            var res = db.Schedules.ToList();
            return View(res);
        }

        public ActionResult Delete(int id)
        {
            var res = db.Schedules.Where(x => x.id == id).First();
            db.Schedules.Remove(res);
            db.SaveChanges();

            var list = db.Schedules.ToList();

            return View("ScheduleList", list);
        }
    }
}