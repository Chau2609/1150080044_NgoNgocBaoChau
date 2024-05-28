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
    public class ChiTietHopDongController : Controller
    {
        Models.ChauEntities _context = new Models.ChauEntities(); // Tạo đối tượng để truy cập dữ liệu

        // GET: HANGHOA
        public ActionResult Index()


        {

            var listOfData = _context.ChiTietHopDong.ToList(); 
            return View(listOfData);
        }

        [HttpGet]
        public ActionResult Create()
        {



            // Lấy danh sách mã hoa và mã khu vực để hiển thị trên dropdownlist
            var sohopdongList = _context.HopDongVay.Select(x => new SelectListItem { Value = x.SoHopDong.ToString(), Text = x.SoHopDong }).ToList();
            ViewBag.SoHopDongList = new SelectList(sohopdongList, "Value", "Text");
            var manguyenteList = _context.DMNguyenTe.Select(x => new SelectListItem { Value = x.MaNguyenTe.ToString(), Text = x.MaNguyenTe }).ToList();
            ViewBag.MaNguyenTeList = new SelectList(manguyenteList, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.ChiTietHopDong model)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            {
                try
                {




                    // Kiểm tra xem mã hoa và mã khu vực đã tồn tại trong cơ sở dữ liệu hay chưa
                    var sohopdong = _context.HopDongVay.Find(model.SoHopDong);
                    var manguyente = _context.DMNguyenTe.Find(model.MaNguyenTe);

                    if (sohopdong == null)
                    {
                        ModelState.AddModelError("SoHopDong", "Mã hoa không hợp lệ.");
                    }

                    if (manguyente == null)
                    {
                        ModelState.AddModelError("MaNguyenTe", "Mã khu vực không hợp lệ.");
                    }

                    if (ModelState.IsValid) // Kiểm tra xem sau khi kiểm tra mã hoa và mã khu vực, ModelState có còn hợp lệ không
                    {
                        // Thêm model vào cơ sở dữ liệu
                        _context.ChiTietHopDong.Add(model);
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

            var sohopdongList = _context.HopDongVay.Select(x => new SelectListItem { Value = x.SoHopDong.ToString(), Text = x.SoHopDong }).ToList();
            ViewBag.SoHopDongList = new SelectList(sohopdongList, "Value", "Text");
            var manguyenteList = _context.DMNguyenTe.Select(x => new SelectListItem { Value = x.MaNguyenTe.ToString(), Text = x.MaNguyenTe }).ToList();
            ViewBag.MaNguyenTeList = new SelectList(manguyenteList, "Value", "Text");

            return View(model); // Trả về View với model để hiển thị lại dữ liệu đã nhập
        }




        [HttpGet]
        public ActionResult Delete(string id1, string id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm phần tử cần xóa trong cơ sở dữ liệu
            Models.ChiTietHopDong itemToDelete = _context.ChiTietHopDong
                .SingleOrDefault(x => x.SoHopDong == id1 && x.MaNguyenTe == id2);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            return View(itemToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id1, string id2)
        {
            // Tìm phần tử cần xóa trong cơ sở dữ liệu
            Models.ChiTietHopDong itemToDelete = _context.ChiTietHopDong
                .SingleOrDefault(x => x.SoHopDong == id1 && x.MaNguyenTe == id2);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            _context.ChiTietHopDong.Remove(itemToDelete); // Xóa phần tử
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToAction("Index");
        }
    }
}