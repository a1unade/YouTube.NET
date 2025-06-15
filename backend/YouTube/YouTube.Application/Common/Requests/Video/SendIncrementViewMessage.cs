namespace YouTube.Application.Common.Requests.Video;

public record SendIncrementViewMessage(Guid Id, Guid VideoId, long ViewCount);
