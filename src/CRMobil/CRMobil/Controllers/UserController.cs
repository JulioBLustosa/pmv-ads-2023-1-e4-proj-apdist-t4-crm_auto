using CRMobil.Entities.Cliente;
using CRMobil.Entities.Usuarios;
using CRMobil.Services;
using CRMobil.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRMobil.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User userModel)
        {
            var user = await _userService.GetAsync(userModel.Nome_Usuario, userModel.Senha);

            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(user);

            return new { username = user.Nome_Usuario, token = token };
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<dynamic>> CreateUser([FromBody] User userModel)
        {
            try
            {
                string response = await _userService.CreateUser(userModel);
                return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
