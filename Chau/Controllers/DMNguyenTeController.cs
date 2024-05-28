using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chau.Models;
using System.Net;
using System.Web.Mvc;
using System.Globalization;

namespace Chau.Controllers
{
    public class DMNguyenTeController : Controller
    {
        Models.ChauEntities _context = new Models.ChauEntities(); // Tạo đối tượng để truy cập dữ liệu

        public ActionResult Index()
        {
            var lisofdata = _context.DMNguyenTe.ToList();
            return View(lisofdata);
        }
        [HttpGet]

        public ActionResult Create() //khi truy cap vao "/Home/Create" thi ham nay se duoc chay va tao 1 form rong de nguoi dung nhap du lieu
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Models.DMNguyenTe model) // yeu cau POST tu form tao moi va them mot bang ghi moi vao NhomHangs
        {
            // Chuyển đổi định dạng ngày từ MM/dd/yyyy sang dd/MM/yyyy


            _context.DMNguyenTe.Add(model); // nhan tham so( model) la doi tuong NhomHang chua thong tin nguoi dung nhap vao form va them vao NhomHangs
            _context.SaveChanges();//du lieu se duoc luu qua day
            ViewBag.Message = "Data insert successfully";//thong bao cho nguoi dung
            return View();
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = _context.DMNguyenTe.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(string id)
        {
            // Lấy đối tượng cần xóa từ id
            var model = _context.DMNguyenTe.Find(id.ToString());
            if (model == null)
            {
                return HttpNotFound();
            }
            // Xóa đối tượng
            _context.DMNguyenTe.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}