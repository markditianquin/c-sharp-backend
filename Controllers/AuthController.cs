using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.User;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _authRepository;
    public AuthController(IAuthRepository authRepository)
    {
      _authRepository = authRepository;
    }

    [HttpGet("GetAll")]
    [Authorize(Policy = "ReadScope")]
    public async Task<IActionResult> GetAllUser()
    {
      
      string disPlayName = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
      string email = User.Claims.FirstOrDefault(c => c.Type == "emails").Value;

      await _authRepository.Register(
          new User { DisplayName = disPlayName, Email = email }
      );
      ServiceResponse<List<GetUserDto>> response = await _authRepository.GetAllUser();
      return Ok(response);
    }

    [HttpPost("Register")]
    [Authorize(Policy = "ReadScope")]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
      ServiceResponse<int> response = await _authRepository.Register(
          new User { DisplayName = request.DisplayName, Email = request.Email }
      );

      if (!response.Success)
      {
        return BadRequest(response);
      }

      return Ok(response);
    }
  }
}