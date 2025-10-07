using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SecurityNotepad
{
    public class UserAccount
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; } // Хэш пароля
        public string FolderName { get; set; }   // для личной папки пользователя

        public UserAccount(string login, string passwordhash)
        {
            Login = login;
            PasswordHash = passwordhash;
            FolderName = new Random().Next(100000, 999999).ToString(); // Генерация случайного имени папки
        }
    }

    public class AccountManager
    {
        private readonly string accountsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts");

        public AccountManager()
        {
            if (!Directory.Exists(accountsFolder))
                Directory.CreateDirectory(accountsFolder);
        }

        /// <summary>
        /// Добавление нового аккаунта (принимает логин и пароль).
        /// </summary>
        public bool AddAccount(string login, string password)
        {
            try
            {
                string userFile = Path.Combine(accountsFolder, login + ".txt");

                if (File.Exists(userFile))
                    return false; // уже существует

                string hash = PasswordHasher.Hash(password);
                File.WriteAllText(userFile, hash);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Добавление нового аккаунта (принимает UserAccount с уже хэшированным паролем).
        /// </summary>
        public bool AddAccount(UserAccount user)
        {
            try
            {
                string userFile = Path.Combine(accountsFolder, user.Login + ".txt");

                if (File.Exists(userFile))
                    return false; // уже существует

                File.WriteAllText(userFile, user.PasswordHash);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsLoginAvailable(string login)
        {
            string userFile = Path.Combine(accountsFolder, login + ".txt");
            return !File.Exists(userFile);
        }

        public string? GetPasswordHash(string login)
        {
            string userFile = Path.Combine(accountsFolder, login + ".txt");

            if (!File.Exists(userFile))
                return null;

            return File.ReadAllText(userFile);
        }
    }
}

