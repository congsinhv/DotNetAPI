﻿using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace DotnetAPIProject.Services.Implementations
{

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> AddAccountAsync(AccountDto accountDto)
        {
            bool CheckUser = await _context.Accounts.AnyAsync(p => p.UserName == accountDto.UserName);
            bool CheckEmail = await _context.Accounts.AnyAsync(p => p.Email == accountDto.Email);

            if (CheckUser)
            {
                // nếu lỗi thì controller sẽ xử  lý đến và thông báo ra
                throw new ArgumentException("Tên tài khoản đã tồn tại!"); 
              
            }
            else if (CheckEmail)
            {
                throw new ArgumentException("Tên email đã tồn tại!");

            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(accountDto.Password);

            var account = new Account
            {
                UserName = accountDto.UserName,
                Email = accountDto.Email,
                NumberPhone = accountDto.PhoneNumber,
                Password = hashedPassword,
                ConfirmPassword = accountDto.ConfirmPassword
            };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync(); // Lưu vào database

            return account;
        }




    }

}