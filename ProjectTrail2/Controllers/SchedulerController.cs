using ProjectTrail2.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace ProjectTrail2.Controllers
{
    public class SchedulerController : Controller
    {

        // GET: Schedule

        TrainingScheduleEntities db = new TrainingScheduleEntities();
        
        [HttpGet]
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
                obj.ScheduleStatus = model.ScheduleStatus;
                obj.ScheduleDate = model.ScheduleDate;
                obj.ScheduleTime = model.ScheduleTime;
                obj.Remark = model.Remark;

                if (model.id > 0)
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


            return RedirectToAction("StudentList");
        }


        public ActionResult StudentList()
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
      /* public HttpResponse Delete(int id)
        {
            try
            {
                using (TrainingScheduleEntities entities = new TrainingScheduleEntities())
                {
                    var entity = entities.Schedules.FirstOrDefault(e => e.id == id);
                    if (entities == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Schedule with id =" + id.ToString());
                    }
                    else
                    {
                        entities.Schedules.Remove(entity);
                        entities.SaveChanges();
                    }
                    entities.Schedules.Remove(entities.Schedules.FirstOrDefault(e => e.id == id));
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }*/

    }
}