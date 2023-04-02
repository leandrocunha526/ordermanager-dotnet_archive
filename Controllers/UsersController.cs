using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using ordermanager_dotnet.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ordermanager_dotnet.Services;
using ordermanager_dotnet.Entities;
using ordermanager_dotnet.Models;

namespace ordermanager_dotnet.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private IUserService _userService;
		private IMapper _mapper;
		private readonly AppSettings _appSettings;
		public UsersController(
			IUserService userService,
			IMapper mapper,
			IOptions<AppSettings> appSettings)
		{
			_userService = userService;
			_mapper = mapper;
			_appSettings = appSettings.Value;
		}

		//Authenticator (post) -> https://localhost:5000/api/user/authenticate
		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody] AuthenticateModel model)
		{
			var user = _userService.Authenticate(model.Username, model.Password);
			if (user == null)
				return BadRequest(new
				{
					message = "Username or password is incorret"
				});
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[] {
					new Claim(ClaimTypes.Name, user.Id.ToString())
			}),
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);
			return Ok(new
			{
				Id = user.Id,
				Username = user.Username,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Token = tokenString
			});
		}

		//Register (post) -> https://localhost:5000/api/user/register
		[AllowAnonymous]
		[HttpPost("register")]
		public IActionResult Register([FromBody] RegisterModel model)
		{
			var user = _mapper.Map<User>(model);
			try
			{
				_userService.Create(user, model.Password);
				return StatusCode(200, "message: Registered successfully");
			}
			catch (AppException ex)
			{
				return BadRequest(new
				{
					message = ex.Message
				});
			}
		}

		//Get (get) users -> https://localhost:5000/api/users
		[HttpGet("list")]
		public IActionResult GetAll()
		{
			var user = _userService.GetAll();
			var model = _mapper.Map<IList<UserModel>>(user);
			return Ok(model);
		}

		//Get (get) user by id -> https://localhost:5000/api/users/id
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var user = _userService.GetById(id);
			var model = _mapper.Map<UserModel>(user);
			return Ok(model);
		}

		//Edit (put) user by id -> https://localhost:5000/api/users/id
		[HttpPut("edit/{id}")]
		public IActionResult Update(int id, [FromBody] UpdateUserModel model)
		{
			var user = _mapper.Map<User>(model);
			user.Id = id;
			try
			{
				_userService.Update(user, model.Password);
				return Ok();
			}
			catch (AppException ex)
			{
				return BadRequest(new
				{
					message = ex.Message
				});
			}
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_userService.Delete(id);
			return Ok();
		}
	}
}
