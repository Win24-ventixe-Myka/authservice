using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Entities;
using Presentation.Interfaces;

namespace Presentation.Repositories;

public class AuthRepository(DataContext context) : IAuthRepository
{
    private readonly DataContext _context = context;

    public async Task<UserEntity> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}