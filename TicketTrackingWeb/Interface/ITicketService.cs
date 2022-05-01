using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrackingWeb.Models;

namespace TicketTrackingWeb.Interface
{
    public interface ITicketService
    {
        void SetAPIHost(string APIHost);

        List<Ticket> GetAll();

        Ticket Get(string ticketID);

        bool Add(Ticket ticket);

        bool Update(Ticket ticket);

        bool Delete(string ticketID);
    }
}
