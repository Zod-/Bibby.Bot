using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Bibby.Bot.Services
{
    public interface IMessageService
    {
        Task<IUserMessage> SendAsync(ISocketMessageChannel channel, string response, [Optional] int autoDeleteSeconds);
    }
}