using API.Entities;
using API.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Hubs
{
        public static class Manage
        {
        public static List<ChatUserSingleR> Users = new List<ChatUserSingleR>();
        }
    public class ChutHub:Hub
    {

        public void Connect(string userId)
        {

            Manage.Users.Add(new ChatUserSingleR()
            {
                Id = userId,
                ConnectionId = this.Context.ConnectionId
            });

        }

        public void SendAll(string text)
        {

            Clients.Clients(Manage.Users.Select(t => t.ConnectionId).ToList()).SendAsync("sendAll", text);

        }

        public void SendToUser(string text,string userId)
        {

            Clients.Clients(Manage.Users.FirstOrDefault(t => t.Id == userId).ConnectionId).SendAsync("sendToUser", text);

        }

        public void Disconnect(string userId)
        {

            Manage.Users.Remove(Manage.Users.FirstOrDefault(x => x.Id == userId));
           

        }

    }
}
