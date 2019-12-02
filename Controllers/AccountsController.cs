using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KeenaKaata.DAL;
using KeenaKaata.Models;
using KeenaKaata.Repository;

namespace KeenaKaata.Controllers
{
    public class AccountsController : Controller
    {
       
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public List<SelectListItem> GetMember()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Members>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.MemberId.ToString(), Text = item.FristName });
            }
            return list;
        }
        // GET: Accounts
        public ActionResult Login()
        {
            ViewBag.CategoryList = GetMember();
            return View();
        }
        [HttpPost]
        public ActionResult Login(Tbl_Members model)
        {

            var user = from i in _unitOfWork.GetRepositoryInstance<Tbl_Members>().GetMember()
                        where i.EmailId == model.EmailId
                       select i ;
                     
            var userPass = from i in _unitOfWork.GetRepositoryInstance<Tbl_Members>().GetMember()
                           where i.Password == model.Password
                           select i;

            if(user != null && userPass != null)
            {
                FormsAuthentication.SetAuthCookie(model.EmailId, false);
                
            }
            else
            {
                ModelState.AddModelError("", "Invalid Username and pass");
            }
            return RedirectToAction("Member", "Member");

        }
        public ActionResult MemberRegister()
        {
            ViewBag.CategoryList = GetMember();
            return View();
        }
        [HttpPost]
        public ActionResult MemberRegister(Tbl_Members tbl)
        {


            tbl.CreatedOn = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Members>().Add(tbl);
            return RedirectToAction("Index");

        }
    }
}