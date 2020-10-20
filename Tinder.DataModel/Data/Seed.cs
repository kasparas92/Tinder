using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tinder.DataModel.Context;
using Tinder.DataModel.Entities;

namespace Tinder.DataModel.Data
{
    public static class Seed
    {
        public static void SeedUsers(TinderContext context)
        {
            if(!context.User.Any())
            {
                var userData = File.ReadAllText("C:\\Users\\kasparas.jurkevicius\\Desktop\\Software Engineering Trainings\\Tinder\\Tinder\\Tinder.DataModel\\Data\\UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach(var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Name = user.Name.ToLower();
                    context.User.Add(user);
                }
            }
            context.SaveChanges();
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
