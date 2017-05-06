﻿using HashLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TIPySerwer.Models;

namespace TIPySerwer
{
    class UserManager
    {
        public async static Task<bool> IsLoginExists(string login)
        {
            using (tipBDEntities db = new tipBDEntities())
            {
                return db.Users.Where(u => u.Login.Equals(login)).Any();
            }
        }
        public async static Task<byte[]> HashPassword(string password)
        {
            IHash hash = HashFactory.Crypto.SHA3.CreateKeccak512();
            HashAlgorithm hashAlgo = HashFactory.Wrappers.HashToHashAlgorithm(hash);
            byte[] input = Encoding.UTF8.GetBytes(password);
            byte[] output = hashAlgo.ComputeHash(input);
            return output;
        }
        
        public async static Task AddUser(UserView user)
        {
            using (tipBDEntities db = new tipBDEntities())
            {
                Users newUser = new Users();
                newUser.Login = user.Login;
                newUser.Password = await HashPassword(user.Password);
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }
        
        
    }
}
