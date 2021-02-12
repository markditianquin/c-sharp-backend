using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dtos.User;
using backend.Models;

namespace backend.Data
{
  public interface IAuthRepository
  {
    Task<ServiceResponse<List<GetUserDto>>> GetAllUser();
    Task<ServiceResponse<int>> Register(User user);
  }
}