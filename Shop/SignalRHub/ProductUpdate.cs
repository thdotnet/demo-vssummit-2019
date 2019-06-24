using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.SignalRHub
{
    public class ProductUpdate : Hub
    {
        public void Notify(string id)
        {
            Clients.All.SendAsync("notify", id);
        }
    }
}
