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
    public class HopDongController : Controller
    {
        Models.ChauEntities _context = new Models.ChauEntities(); // Tạo đối tượng để truy cập dữ liệu

        // GET: HANGHOA
        public ActionResult Index()


        {

            var listOfData = _context.HopDongVay.ToList();
            return View(listOfData);
        }

        [HttpGet]
        public ActionResult Create()
        {



            // Lấy danh sách mã hoa và mã khu vực để hiển thị trên dropdownlist
            var machinhanhList = _context.DMChiNhanh.Select(x => new SelectListItem { Value = x.MaChiNhanh.ToString(), Text = x.MaChiNhanh }).ToList();
            ViewBag.MaChiNhanhList = new SelectList(machinhanhList, "Value", "Text");
            var makhachhangList = _context.DMKhachHang.Select(x => new SelectListItem { Value = x.MaKhachHang.ToString(), Text = x.MaKhachHang }).ToList();
            ViewBag.MaKhachHangList = new SelectList(makhachhangList, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.HopDongVay model)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            {
                try
                {


                    

                    // Kiểm tra xem mã hoa và mã khu vực đã tồn tại trong cơ sở dữ liệu hay chưa
                    var machinhanh = _context.DMChiNhanh.Find(model.MaChiNhanh);
                    var makhachhang = _context.DMKhachHang.Find(model.MaKhachHang);

                    if (machinhanh == null)
                    {
                        ModelState.AddModelError("MaHoa", "Mã hoa không hợp lệ.");
                    }

                    if (makhachhang == null)
                    {
                        ModelState.AddModelError("MaKhuVuc", "Mã khu vực không hợp lệ.");
                    }

                    if (ModelState.IsValid) // Kiểm tra xem sau khi kiểm tra mã hoa và mã khu vực, ModelState có còn hợp lệ không
                    {
                        // Thêm model vào cơ sở dữ liệu
                        _context.HopDongVay.Add(model);
                        _context.SaveChanges();

                        ViewBag.Message = "Data inserted successfully";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error occurred: " + ex.Message;
                }
            }

            var machinhanhList = _context.DMChiNhanh.Select(x => new SelectListItem { Value = x.MaChiNhanh.ToString(), Text = x.MaChiNhanh }).ToList();
            ViewBag.MaChiNhanhList = new SelectList(machinhanhList, "Value", "Text");
            var makhachhangList = _context.DMKhachHang.Select(x => new SelectListItem { Value = x.MaKhachHang.ToString(), Text = x.MaKhachHang }).ToList();
            ViewBag.MaKhachHangList = new SelectList(makhachhangList, "Value", "Text");

            return View(model); // Trả về View với model để hiển thị lại dữ liệu đã nhập
        }





        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm phần tử cần xóa trong cơ sở dữ liệu
            Models.HopDongVay itemToDelete = _context.HopDongVay.Find(id);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            return View(itemToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            // Tìm phần tử cần xóa trong cơ sở dữ liệu
            Models.HopDongVay itemToDelete = _context.HopDongVay.Find(id);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            _context.HopDongVay.Remove(itemToDelete); // Xóa phần tử
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToAction("Index");
        }
    }
}