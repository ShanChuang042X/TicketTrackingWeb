using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketTrackingWeb.Models;

namespace TicketTrackingWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }


        public IActionResult CheckLogin(string charater)
        {
            //檢查驗證
            //因時程問題暫時跳過

            //檢查授權
            //因時程問題暫時跳過

            //根據login的button選擇給予身分
            HttpContext.Session.SetString("charater", charater);

            return RedirectToAction("Index","Ticket");
        }

        public IActionResult Index()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
