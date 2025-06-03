using Presentation.Entities;

namespace Presentation.Interfaces;

public interface IAuthRepository
{
    Task<UserEntity> GetByEmailAsync(string email);
}