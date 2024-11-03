using Microsoft.AspNetCore.Identity;

namespace YouTube.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public Role(string roleName) : base(roleName)
    {
    }

    public Role()
    {
    }
}