using System;
using BankingSystem.Services.CustomerService.Dtos;
using BankingSystem.Services.CustomerService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Services.CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService tokenService;

        public TokenController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync([FromBody] UserLoginDto userLoginDto)
        {
            var token = await tokenService.GetTokenAsync(userLoginDto.Login, userLoginDto.Password);

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(token);
            }

            return Unauthorized();
        }
    }
}