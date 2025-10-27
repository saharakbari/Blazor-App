using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RegisterUserService
    {
        private readonly IRepository<User> _userRepository;
        public RegisterUserService(IRepository<User> userRepository)
        {

            _userRepository = userRepository;
        }

        public async Task RegisterAsync(string email, string username, string password, UserRole role)
        {
            var existing = await _userRepository.GetByPropAsync(email);
            if (existing != null)
            {
                throw new Exception("email is already registered");
            }
            var user = new User();
            user.Email = email;
            user.UserName = username;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.Role = role;

            await _userRepository.AddAsync(user);

        }
    }
}
