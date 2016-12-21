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
















        //public ActionResult Step1()
        //{
        //    return View();
        //}

        //public PartialViewResult SearchPeople(string keyword)
        //{
        //    System.Threading.Thread.Sleep(2000);
        //    var data = db.Peoples.Where(f => f.firstName.StartsWith(keyword)).ToList();
        //    return PartialView(data);
        //}

        public ActionResult Results(string keyword1, string keyword2)
        {
            System.Threading.Thread.Sleep(2000);
            
            var data = db.Peoples.Where(s => s.lastName.Contains(keyword2));

            string concat = keyword1 + " " + keyword2;


            if (!String.IsNullOrEmpty(keyword1) && String.IsNullOrEmpty(keyword2))
            {
                var data1 = db.Peoples.Where(s => s.firstName.Contains(keyword1));
                return View(data1);
            }
            else if (!String.IsNullOrEmpty(keyword2) && String.IsNullOrEmpty(keyword1))
            {
                var data2 = db.Peoples.Where(s => s.lastName.Contains(keyword2));
                return View(data2);
            }
            else
            {
                var strings = concat.Split(' ');
                var finalPosts = new List<People>();
                if (!String.IsNullOrEmpty(concat))
                {
                    foreach (var splitString in strings)
                    {
                        finalPosts.Add(db.Peoples.FirstOrDefault(s => s.firstName.Contains(splitString)));
                        finalPosts.Add(db.Peoples.FirstOrDefault(s => s.lastName.Contains(splitString)));

                        //WHAT IF THERE ARE MULTIPLE PEOPLE WITH THE SAME FIRST AND/OR LAST NAME??????
                        //db.Peoples.SelectMany
                        //db.Peoples.ToArray
                        //db.Peoples.ToList
                    }
                }
                //return View(finalPosts);
            }


            //var data = db.Peoples.Where(f => f.firstName.StartsWith(keyword)).ToList();
            return View(data);
        }

        //public ActionResult IndexJson()
        //{
        //    return View();
        //}

        //public JsonResult SearchPeopleJson(string keyword)
        //{
        //    var data = db.Peoples.Where(f => f.firstName.Contains(keyword)).ToList();

        //    var jsonData = Json(data, JsonRequestBehavior.AllowGet);
        //    return jsonData;
        //}
    }
}