using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Bibby.Bot.Services
{
    public class MessageService : IMessageService
    {
        public async Task<IUserMessage> SendAsync(ISocketMessageChannel channel, string response, int autoDeleteSeconds = -1)
        {
            return await channel.SendMessageAsync(response);
        }
    }
}
