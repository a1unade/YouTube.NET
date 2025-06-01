using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses;
using YouTube.Application.Common.Responses.User;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Mobile.Data.Data.Queries;

/// <summary>
/// 
/// </summary>
[ExtendObjectType("Query")]
public class UserQuery
{
    // public IAsyncEnumerable<User> GetUsers([Service] IDbContext dbContext) => 
    //     dbContext.Users.AsNoTracking().AsAsyncEnumerable();
    
    
    [GraphQLDescription("Получить пользователя")]
    public async Task<UserInfoResponse> GetUser(
        [ID] Guid id,
        [Service] IDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var user = await dbContext.Users
            .Include(x => x.UserInfo)
            .Include(x => x.Channels)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user == null || user.UserInfo == null!)
        {
            return new UserInfoResponse
            {
                IsSuccessfully = false,
                Message = UserErrorMessage.UserNotFound
            };
        }
        
        Guid channelId = Guid.Empty;

        if (user.Channels != null)
        {
            channelId = user.Channels.FirstOrDefault()!.Id;
        }
        return new UserInfoResponse
        {
            IsSuccessfully = true,
            Name = user.UserInfo.Name,
            SurName = user.UserInfo.Surname!,
            Email = user.Email!,
            UserName = user.DisplayName,
            IsPremium = user.Subscriptions != null,
            ChannelId = channelId
        };
    }
    
    [GraphQLDescription("Получить Канал пользователя")]
    public async Task<BaseResponse> GetUserChannel(
        [ID] Guid id,
        [Service] IDbContext context,
        CancellationToken cancellationToken
    )
    {
        var user = await context.Users
                .Include(x => x.Channels)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user == null || user.Channels == null)
        {
            return new BaseResponse(false, UserErrorMessage.UserNotFound);
        }
        
        return new BaseResponse
        {
            IsSuccessfully = true,
            EntityId = user.Channels.FirstOrDefault()?.Id,
            Message = $"User channel id: {user.Channels.FirstOrDefault()?.Id}"
        };
    }
}