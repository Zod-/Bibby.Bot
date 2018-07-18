using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;

namespace Bibby.Bot.Utilities
{
    public class EmptyMessage : IUserMessage
    {
        public ulong Id { get; } = default;
        public DateTimeOffset CreatedAt { get; } = default;

        public Task DeleteAsync(RequestOptions options = null)
        {
            return Task.CompletedTask;
        }

        public MessageType Type { get; } = default;
        public MessageSource Source { get; } = default;
        public bool IsTTS { get; } = default;
        public bool IsPinned { get; } = default;
        public string Content { get; } = default;
        public DateTimeOffset Timestamp { get; } = default;
        public DateTimeOffset? EditedTimestamp { get; } = default;
        public IMessageChannel Channel { get; } = default;
        public IUser Author { get; } = default;
        public IReadOnlyCollection<IAttachment> Attachments { get; } = default;
        public IReadOnlyCollection<IEmbed> Embeds { get; } = default;
        public IReadOnlyCollection<ITag> Tags { get; } = default;
        public IReadOnlyCollection<ulong> MentionedChannelIds { get; } = default;
        public IReadOnlyCollection<ulong> MentionedRoleIds { get; } = default;
        public IReadOnlyCollection<ulong> MentionedUserIds { get; } = default;

        public Task ModifyAsync(Action<MessageProperties> func, RequestOptions options = null)
        {
            return Task.CompletedTask;
        }

        public Task PinAsync(RequestOptions options = null)
        {
            return Task.CompletedTask;
        }

        public Task UnpinAsync(RequestOptions options = null)
        {
            return Task.CompletedTask;
        }

        public Task AddReactionAsync(IEmote emote, RequestOptions options = null)
        {
            return Task.CompletedTask;
        }

        public Task RemoveReactionAsync(IEmote emote, IUser user, RequestOptions options = null)
        {
            return Task.CompletedTask;
        }

        public Task RemoveAllReactionsAsync(RequestOptions options = null)
        {
            return Task.CompletedTask;
        }

        public IAsyncEnumerable<IReadOnlyCollection<IUser>> GetReactionUsersAsync(IEmote emoji, int limit,
            RequestOptions options = null)
        {
            return null;
        }

        public string Resolve(TagHandling userHandling = TagHandling.Name,
            TagHandling channelHandling = TagHandling.Name, TagHandling roleHandling = TagHandling.Name,
            TagHandling everyoneHandling = TagHandling.Ignore, TagHandling emojiHandling = TagHandling.Name)
        {
            return string.Empty;
        }

        public IReadOnlyDictionary<IEmote, ReactionMetadata> Reactions { get; } = default;
    }
}