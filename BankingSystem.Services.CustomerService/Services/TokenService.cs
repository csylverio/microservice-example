using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankingSystem.Services.CustomerService.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BankingSystem.Services.CustomerService.Services;

public class TokenService(IConfiguration configuration, IUserRepository userRepository) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<string> GetTokenAsync(string login, string password)
    {
        var user = await _userRepository.GetByLoginAsync(login);
        if (user == null) {
            return string.Empty;
        }
        if (!user.Password.Equals(password)) {
            return string.Empty;
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = _configuration.GetValue<string>("SecretJWT") ?? throw new Exception("SecretJWT não configurado");
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenProperties = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
            [
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role.ToString()),
                new("ClaimPersonalizada", "Conteúdo Personalizado")
            ]),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenProperties);
        return tokenHandler.WriteToken(token);
    }

}
