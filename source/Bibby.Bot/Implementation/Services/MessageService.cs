using System.Threading.Tasks;
using Bibby.Bot.Utilities;
using Discord;

namespace Bibby.Bot.Services
{
    public class MessageService : IMessageService
    {
        public async Task<IUserMessage> SendAsync(IMessageChannel channel, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new EmptyMessage();
            }
            return await channel.SendMessageAsync(text);
        }
    }
}
