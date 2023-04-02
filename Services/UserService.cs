using System;
using System.Collections.Generic;
using System.Linq;
using ordermanager_dotnet.Entities;
using ordermanager_dotnet.Helpers;
using ordermanager_dotnet.Data;

namespace ordermanager_dotnet.Services
{
	public class UserService : IUserService
	{
		private ApplicationDbContext _context;
		public UserService(ApplicationDbContext context)
		{
			_context = context;
		}
		public User Authenticate(string username, string password)
		{
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
				return null;

			var user = _context.Users.SingleOrDefault(x => x.Username == username);

			if (user == null)
				return null;

			if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
				return null;

			return user;
		}
		public IEnumerable<User> GetAll()
		{
			return _context.Users;
		}
		public User GetById(int id)
		{
			return _context.Users.Find(id);
		}
		public User Create(User user, string password)
		{
			if (string.IsNullOrWhiteSpace(password))
				throw new AppException("Password is required");

			if (_context.Users.Any(x => x.Username == user.Username))
				throw new AppException("Username " + user.Username + " is already taken");

			byte[] passwordHash, passwordSalt;

			CreatePasswordHash(password, out passwordHash, out passwordSalt);

			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			_context.Users.Add(user);
			_context.SaveChanges();

			return user;
		}
		public void Update(User userParam, string password = null)
		{
			var user = _context.Users.Find(userParam.Id);
			if (user == null)
				throw new AppException("User not found");
			if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
			{
				if (_context.Users.Any(x => x.Username == userParam.Username))
					throw new AppException("Username " + userParam.Username + " is already taken");
				user.Username = userParam.Username;
			}
			if (!string.IsNullOrWhiteSpace(userParam.FirstName))
				user.FirstName = userParam.FirstName;
			if (!string.IsNullOrEmpty(userParam.LastName))
				user.LastName = userParam.LastName;
			if (!string.IsNullOrEmpty(password))
			{
				byte[] passwordHash, passwordSalt;
				CreatePasswordHash(password, out passwordHash, out passwordSalt);
				user.PasswordHash = passwordHash;
				user.PasswordSalt = passwordSalt;
			}
			_context.Users.Update(user);
			_context.SaveChanges();
		}
		public void Delete(int id)
		{
			var user = _context.Users.Find(id);
			if (user != null)
			{
				_context.Users.Remove(user);
				_context.SaveChanges();
			}
		}
		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			if (password == null)
				throw new ArgumentException("password");
			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentException("Value cannot be empty or whitespace only string", "password");
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}
		public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
		{
			if (password == null)
				throw new ArgumentException("password");
			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentException("Value cannot be empty or whitespace only string", "password");
			using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != storedHash[i])
						return false;
				}
			}
			return true;
		}
	}
}
