using System.Reflection.Metadata;
using System.Threading.Tasks;
using Bibby.Bot.Services;
using Discord.Commands;

namespace Bibby.Bot.Modules
{
    public class PingModule : ModuleBase<SocketCommandContext>
    {
        public IMessageService MessageService { get; set; }

        //MIT License https://github.com/Sirush/UDHBot
        private async Task PingAsync(string response)
        {
            var message = await MessageService.SendAsync(Context.Channel, response);
            var time = message.Timestamp.Subtract(Context.Message.Timestamp);
            await message.ModifyAsync(m => m.Content = $"{response} (**{time.TotalMilliseconds}** *ms*)");
        }

        [Command("ping")]
        public async Task PingAsync()
        {
            await PingAsync("Pong");
        }

        [Command("pong")]
        public async Task PongAsync()
        {
            await PingAsync("Ping");
        }

        [Command("ding")]
        public async Task DingAsync()
        {
            await PingAsync("Dong");
        }

        [Command("Dong")]
        public async Task DongAsync()
        {
            await PingAsync("Ding");
        }
    }
}
