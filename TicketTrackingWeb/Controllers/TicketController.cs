using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TicketTrackingWeb.Interface;
using TicketTrackingWeb.Models;

namespace TicketTrackingWeb.Controllers
{
    public class TicketController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ITicketService _ticketService;

        public TicketController(IConfiguration configuration, ITicketService ticketService)
        {
            Configuration = configuration;
            
            //注入ticket service，負責發查API與後端溝通
            _ticketService = ticketService;
            _ticketService.SetAPIHost(Configuration["ApiHost"]);

        }
        
        /*返回前端後提示訊息文字*/
        public static string AlertMSG { get; set; }

        /*登入角色*/
        public string Charater {
            get
            {
                return  HttpContext.Session.GetString("charater");
            }
        }

        public IActionResult Index()
        {
            if(Charater == null)
            {   
                //若登出則轉至登入畫面
                return RedirectToAction("LogIn", "Home");
            }

            //取得清單
            List<Ticket> tickets = _ticketService.GetAll();


            //登入角色傳入畫面，將依此顯示不同權限的功能別
            ViewBag.charater = Charater;
            //alert mesage
            TempData["mesage"] = string.IsNullOrEmpty(AlertMSG) ? "" : AlertMSG;


            return View(tickets);
        }

        #region Add

        public IActionResult OpenAddPage()
        {
            return PartialView("_AddPartial");
        }

        /*Submit From _AddPartial*/
        public IActionResult Add(Ticket ticket)
        {
            var resault = _ticketService.Add(ticket);

            switch (resault)
            {
                case true:
                    AlertMSG = "create success";
                    break;
                case false:
                    AlertMSG = "create fail";
                    break;
            }

            return Redirect("Index");
        }
        #endregion

        #region Check details
        public IActionResult OpenDetailsPage(string ticketID)
        {
            Ticket ticket = _ticketService.Get(ticketID);
            return PartialView("_DetailPartial", ticket);
        }
        #endregion

        #region edit
        public IActionResult OpenEditPage(string ticketID)
        {
            Ticket ticket = _ticketService.Get(ticketID);
            return PartialView("_EditPartial", ticket);
        }

        /*Submit From _EditPartial*/
        public IActionResult SaveEdit(Ticket ticket)
        {
            var resault = _ticketService.Update(ticket);

            switch (resault)
            {
                case true:
                    AlertMSG = "update success";
                    break;
                case false:
                    AlertMSG = "update fail";
                    break;
            }

            return Redirect("Index");
        }
        #endregion

        #region delete

        public IActionResult Delete(string ticketID)
        {
            var resault = _ticketService.Delete(ticketID);

            switch (resault)
            {
                case true:
                    AlertMSG = "delete success";
                    break;
                case false:
                    AlertMSG = "delete fail";
                    break;
            }

            return Redirect("Index");
        }

        #endregion

        #region solve

        public IActionResult OpenSolvePage(string ticketID)
        {
            Ticket ticket = _ticketService.Get(ticketID);
            return PartialView("_SolvePartial", ticket);
        }
        
        /*Submit From _SolvePartial*/
        public IActionResult Solve(Ticket ticket)
        {
            ticket.Status = "Solved";
            var resault = _ticketService.Update(ticket);

            switch (resault)
            {
                case true:
                    AlertMSG = "solve success";
                    break;
                case false:
                    AlertMSG = "solve fail";
                    break;
            }

            return Redirect("Index");
        }
        
        #endregion
    }
}