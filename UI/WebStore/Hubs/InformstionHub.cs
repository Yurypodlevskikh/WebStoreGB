using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebStore.Hubs
{
    public class InformstionHub : Hub
    {
        public async Task Send(string Message) => await Clients.All.SendAsync("Send", Message);
    }
}
