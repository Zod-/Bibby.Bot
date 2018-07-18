using System.Threading.Tasks;
using Discord;

namespace Bibby.Bot.Services
{
    public interface IMessageService
    {
        Task<IUserMessage> SendAsync(IMessageChannel channel, string text);
    }
}