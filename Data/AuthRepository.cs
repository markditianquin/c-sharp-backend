using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
  public class AuthRepository : IAuthRepository
  {
    private readonly DataContext _context;
    public AuthRepository(DataContext context)
    {
      _context = context;

    }

    public async Task<ServiceResponse<List<GetUserDto>>> GetAllUser()
    {
      ServiceResponse<List<GetUserDto>> response = new ServiceResponse<List<GetUserDto>>();
      List<GetUserDto> users = await _context.Users.Select(x => new GetUserDto { DateCreated = x.DateCreated, Id = x.Id, DisplayName = x.DisplayName, Email = x.Email }).ToListAsync();
      response.Data = users;
      return response;
    }

    public async Task<ServiceResponse<int>> Register(User user)
    {
      ServiceResponse<int> response = new ServiceResponse<int>();
      
      if(await UserExists(user.Email))
      {
        response.Success = false;
        response.Message = "User exist";

        return response;
      }

      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();
      
      response.Data = user.Id;

      return response;
    }

    private async Task<bool> UserExists(string email)
    {
      if (await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
      {
        return true;
      }

      return false;
    }
  }
}