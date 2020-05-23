using mvc1.Daos.Member;
using mvc1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc1.Controllers
{
    public class MemberController : Controller
    {
        private static readonly MemberDao memberDao = new MemberDao();

        public ActionResult Index(int page=1)
        {
            List<member> members = memberDao.Pagination(page, 5);
            int totalPages = memberDao.TotalPages(5);
            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            return View(members);
        }

        public ActionResult GetMembers()
        {
            if (Session["member"] is null)
                return RedirectToAction("Index", "Home");
            
            List<member> members = memberDao.FindAll();
            if (members.Count == 0)
                return RedirectToAction("index");

            return View(members);
        }

        [HttpGet]
        public ActionResult CreateMember()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMember([Bind(Exclude = "registerDate")] member m)
        {
            m.registerDate = DateTime.Now;

            if(!ModelState.IsValid)
                return View(m);

            bool flag = memberDao.FindByUsername(m.username);
            if (flag)
            {
                ViewBag.Error = "Username has existed";
                return View(m);
            }

            member member = memberDao.Save(m);
            if(member is null)
            {
                ViewBag.Error = "Create member failed";
                return View(m);
            }

            return RedirectToAction("GetMembers");
        }

        [HttpGet]
        public ActionResult UpdateMember(int? id)
        {
            member member = memberDao.FindOne(id);
            if (member is null)
                return RedirectToAction("GetMembers");

            return View(member);
        }

        [HttpPost]
        public ActionResult UpdateMember([Bind(Exclude = "registerDate")] member m)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Data format invalid error";
                return View(m);
            }

            member member = memberDao.FindOne(m.id);
            if (member is null) 
            {
                ViewBag.Error = $"Member ID:{m.id} not found";
                return View(m);
            }

            m.registerDate = member.registerDate;
            member newMember = memberDao.Update(m.id, m);
            if (newMember is null)
            {
                ViewBag.Error = $"Updating member id:{m.id} failed";
                return View(m);
            }

            return RedirectToAction("GetMembers");
        }

        [HttpGet]
        public ActionResult DeleteMember(int? id)
        {
            member member = memberDao.FindOne(id);
            if (member is null)
                return RedirectToAction("GetMembers");

            return View(member);
        }

        [HttpPost]
        public ActionResult DeleteMember(member m)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Data format invalid error";
                return View(m);
            }

            bool flag = memberDao.Delete(m.id);
            if (!flag)
            {
                ViewBag.Error = $"Deleting member id:{m.id} failed";
                return View(m);
            }

            return RedirectToAction("GetMembers");
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LogIn(string username, string password)
        {
            if (username.Equals("admin") && password.Equals("admin"))
            {
                Session["member"] = true;
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ViewBag.Error = "Log in failed";
                return View();
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Remove("member");
            return RedirectToAction("Index", "Home");
        }

    }
}