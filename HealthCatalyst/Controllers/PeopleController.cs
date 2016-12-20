using HealthCatalyst.DAL;
using HealthCatalyst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthCatalyst.Controllers
{
    public class PeopleController : Controller
    {
        HealthCatalystContext db = new HealthCatalystContext();

        public ActionResult Index()
        {
            return View(db.Peoples.ToList());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(HttpPostedFileBase file, [Bind(Include = "personID, firstName, lastName, email, address, city, state, zipcode, age, interests, picture")] People peoples)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        var guid = Guid.NewGuid();
                        //var fileName = Path.GetFileName(file.FileName);
                        //var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Images"), guid.ToString());
                        file.SaveAs(path);


                        db.Database.ExecuteSqlCommand(
                        "INSERT INTO People (firstName, lastName, email, address, city, state, zipcode, age, interests, picture) " +
                        "VALUES ('" + peoples.firstName + "', '" + peoples.lastName + "', '" + peoples.email + "', '" + peoples.address +
                        "', '" + peoples.city + "', '" + peoples.state + "', '" + peoples.zipcode + "', '" + peoples.age + "', '" + peoples.interests + "', " +
                        "(SELECT BulkColumn FROM Openrowset(Bulk '" + path + "', Single_Blob) as img))");

                        return RedirectToAction("Index");

                    }
                }
                catch
                {
                    ViewBag.error = true;
                    ViewBag.Alert = "Image upload failed";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if(id != null)
            {
                IEnumerable<People> peoples =
                    db.Database.SqlQuery<People>("SELECT personID, firstName, lastName, email, address, city, state, zipcode, age, interests, picture " +
                    "FROM People " +
                    "WHERE personID = " + id);

                return View(peoples.FirstOrDefault());
            }
            else
            {
                ViewBag.error = true;
                ViewBag.Alert = "Invalid record id";
                return RedirectToAction("Index", "Person");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "personID, firstName, lastName, email, address, city, state, zipcode, age, interests, picture")] People peoples)
        {
            if (ModelState.IsValid)
            {
                        //var guid = Guid.NewGuid();
                        //var path = Path.Combine(Server.MapPath("~/Content/Images"), guid.ToString());
                        //file.SaveAs(path);

                        //db.Database.ExecuteSqlCommand("UPDATE People " +
                        //"SET firstName='" + peoples.firstName + "', lastName='" + peoples.lastName + "', email='" + peoples.email + "', " +
                        //"address='" + peoples.address + "', city='" + peoples.city + "', state='" + peoples.state + "', zipcode='" + peoples.zipcode + "', age='" + peoples.age + "', interests='" + peoples.interests + "', " +
                        //"picture=(SELECT BulkColumn FROM Openrowset(Bulk '" + path + "', Single_Blob) as img))" +
                        //"WHERE personID = " + peoples.personID);

                        //ViewBag.success = true;
                        //ViewBag.Message = "Person succesfully updated with a picture";
                        //return RedirectToAction("Index", "Person");
                  
                        db.Database.ExecuteSqlCommand("UPDATE People " +
                        "SET firstName='" + peoples.firstName + "', lastName='" + peoples.lastName + "', email='" + peoples.email + "', " +
                        "address='" + peoples.address + "', city='" + peoples.city + "', state='" + peoples.state + "', zipcode='" + peoples.zipcode + "', age='" + peoples.age + "', interests='" + peoples.interests + "' " +
                        "WHERE personID = " + peoples.personID);

                        ViewBag.success = true;
                        ViewBag.Message = "Person succesfully updated without picture";
                        return RedirectToAction("Index", "People");
                    
            }

            ViewBag.error = true;
            ViewBag.Alert = "Model is not valid";
            return RedirectToAction("Index", "People");
        }

        public ActionResult EditImage(int? id)
        {
            if (id != null)
            {
                IEnumerable<People> peoples =
                    db.Database.SqlQuery<People>("SELECT personID, firstName, lastName, email, address, city, state, zipcode, age, interests, picture " +
                    "FROM People " +
                    "WHERE personID = " + id);

                return View(peoples.FirstOrDefault());
            }
            else
            {
                ViewBag.error = true;
                ViewBag.Alert = "Invalid record id";
                return RedirectToAction("Index", "Person");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditImage(HttpPostedFileBase file, [Bind(Include = "personID, firstName, lastName, email, address, city, state, zipcode, age, interests, picture")] People peoples)
        {
            //if (ModelState.IsValid)
            //{
                //try
                //{
                    if (file.ContentLength > 0)
                    {
                        var guid = Guid.NewGuid();
                        var path = Path.Combine(Server.MapPath("~/Content/Images"), guid.ToString());
                        file.SaveAs(path);

                        db.Database.ExecuteSqlCommand("UPDATE People " +
                        "SET picture = (SELECT BulkColumn FROM Openrowset(Bulk '" + path + "', Single_Blob) as img) " +
                        "WHERE personID = " + peoples.personID);

                        return RedirectToAction("Index");
                    }
                //}
                //catch
                //{
                    ViewBag.error = true;
                    ViewBag.Alert = "Image upload failed";
                    return View();
                //}
            //}
            //ViewBag.error = true;
            //ViewBag.Alert = "Model is invalid";
            //return View();
        }

        public ActionResult Delete(int? id)
        {
            if(id != null)
            {
                db.Database.ExecuteSqlCommand("DELETE FROM People WHERE personID = " + id);
            }
            return RedirectToAction("Index", "People");
        }
    }
}