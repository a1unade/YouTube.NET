using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces;

public interface IJwtGenerator
{
    public string GenerateToken(User user);

}