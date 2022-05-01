using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TicketTrackingWeb.Interface;
using TicketTrackingWeb.Models;

namespace TicketTrackingWeb.Service
{
    public class TicketService : ITicketService
    {
        private static string _APIHost { get; set; }

        public bool Add(Ticket ticket)
        {
            using (var client = new HttpClient())
            {

                var bodyJson = JsonConvert.SerializeObject(ticket);
                var stringContent = new StringContent(bodyJson, System.Text.Encoding.UTF8, "application/json");
                var reponseTask = new HttpClient().PostAsync($"{_APIHost}/api/Ticket/Add", stringContent);

                reponseTask.Wait();
                if (reponseTask.Result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Delete(string ticketID)
        {
            using (var client = new HttpClient())
            {

                //var bodyJson = "{\"ticketID\":\"" + ticketID + "\"}";//JsonConvert.SerializeObject(ticketID);
                var bodyJson = JsonConvert.SerializeObject(this.Get(ticketID));
                var stringContent = new StringContent(bodyJson, System.Text.Encoding.UTF8, "application/json");
                var reponseTask = new HttpClient().PostAsync($"{_APIHost}/api/Ticket/Delete", stringContent);

                reponseTask.Wait();
                if (reponseTask.Result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Ticket Get(string ticketID)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{_APIHost}/api/Ticket/{ticketID}/");

                var reponseTask = client.GetAsync("Get");
                reponseTask.Wait();

                if (reponseTask.Result.IsSuccessStatusCode)
                {
                    var readTask = reponseTask.Result.Content.ReadAsAsync<Ticket>();
                    readTask.Wait();

                    return readTask.Result;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Ticket> GetAll()
        {
            List<Ticket> tickets;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{_APIHost}/api/Ticket/");

                var reponseTask = client.GetAsync("GetAll");
                reponseTask.Wait();

                if (reponseTask.Result.IsSuccessStatusCode)
                {
                    var readTask = reponseTask.Result.Content.ReadAsAsync<List<Ticket>>();
                    readTask.Wait();

                    tickets = readTask.Result;
                }
                else
                {
                    tickets = null;
                }
            }

            return tickets;
        }

        public void SetAPIHost(string APIHost)
        {
            _APIHost = APIHost;
        }

        public bool Update(Ticket ticket)
        {
            using (var client = new HttpClient())
            {

                var bodyJson = JsonConvert.SerializeObject(ticket);
                var stringContent = new StringContent(bodyJson, System.Text.Encoding.UTF8, "application/json");
                var reponseTask = new HttpClient().PostAsync($"{_APIHost}/api/Ticket/Update", stringContent);

                reponseTask.Wait();
                if (reponseTask.Result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
