using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces;

public interface IJwtGenerator
{
    public Task<string> GenerateToken(User user);
}