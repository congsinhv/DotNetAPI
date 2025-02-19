using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;


namespace DotnetAPIProject.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }
      
        // code
        public async Task<IEnumerable<Account>> GetLoginAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

      
        public async Task<JwtDto> CheckLoginAsync(string userName, string password)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == userName);

            if (account == null)
            {
                throw new ArgumentException("Tên tài khoản không tồn tại!");
            }

            // Dùng BCrypt.Verify() để kiểm tra mật khẩu
            if (!BCrypt.Net.BCrypt.Verify(password, account.Password))
            {
                throw new ArgumentException("Sai mật khẩu!");
            }
            // Tạo JWT Token
            var tokenResponse = _jwtService.GenerateToken(account.UserName);

            return tokenResponse;
            //return account;
        }



        public Task<Account> CheckLoginAsync(string userName, AccountDto account)
        {
            throw new NotImplementedException();
        }

        Task<Account?> ILoginService.CheckLoginAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        private readonly IJwtService _jwtService;

        public LoginService(ApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

    }
}
