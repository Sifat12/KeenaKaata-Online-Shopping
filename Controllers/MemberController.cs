using KeenaKaata.DAL;
using KeenaKaata.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeenaKaata.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public List<SelectListItem> GetMember()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Members>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.MemberId.ToString(), Text = item.FristName});
            }
            return list;
        }
        public ActionResult Member()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Members>().GetProduct());
        }
        public ActionResult MemberEdit(int memberId)
        {
            ViewBag.CategoryList = GetMember();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Members>().GetFirstorDefault(memberId));
        }
        [HttpPost]
        public ActionResult MemberEdit(Tbl_Members tbl)
        {
            
           
            tbl.ModifiedOn = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Members>().Update(tbl);
            return RedirectToAction("Member");
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