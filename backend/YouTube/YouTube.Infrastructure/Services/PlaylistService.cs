using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Infrastructure.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IDbContext _context;

    public PlaylistService(IDbContext context)
    {
        _context = context;
    }

    public async Task CreateDefaultPlaylists(Channel channel, CancellationToken cancellationToken)
    {
        var playlists = new List<Playlist>
        {
            new()
            {
                IsHidden = true,
                Name = PlaylistMessages.LikedPlaylist,
                Description = PlaylistMessages.LikedPlaylistDescription,
                CreateDate = DateOnly.FromDateTime(DateTime.Now),
                ChannelId = channel.Id,
                Channel = channel
            },
            new()
            {
                IsHidden = true,
                Name = PlaylistMessages.HistoryPlaylist,
                Description = PlaylistMessages.HistoryPlaylistDescription,
                CreateDate = DateOnly.FromDateTime(DateTime.Now),
                ChannelId = channel.Id,
                Channel = channel
            },
            new()
            {
                IsHidden = true,
                Name = PlaylistMessages.WatchLaterPlaylist,
                Description = PlaylistMessages.WatchLaterPlaylistDescription,
                CreateDate = DateOnly.FromDateTime(DateTime.Now),
                ChannelId = channel.Id,
                Channel = channel
            }
        };
        
        await _context.Playlists.AddRangeAsync(playlists, cancellationToken);
    }
}