﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tinder.API.Services.Interfaces;
using Tinder.DataModel.Entities;
using Tinder.DataModel.Repositories.Interfacies;

namespace Tinder.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<bool> IsUserExist(string name)
        {
            return await _accountRepository.IsUserExist(name);
        }

        public async Task<User> Login(string name, string password)
        {
            return await _accountRepository.Login(name, password);
        }

        public async Task<User> Register(string name, string password, string gender, string country)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Country = country,
                Gender = gender,
                Name = name.ToLower()
            };
            
            return await _accountRepository.Register(user);
        }
        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
