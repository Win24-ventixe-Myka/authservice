using Presentation.Entities;
using Presentation.Models;

namespace Presentation.Extensions;

public static class UserMappingExtensions
{
    public static UserEntity MapToEntity(this SignUpRequest request)
    {
        return new UserEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
        };
    }
}