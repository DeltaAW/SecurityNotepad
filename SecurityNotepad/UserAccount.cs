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
    class UserAccount
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

        public UserAccount()
        {
            FolderName = new Random().Next(100000, 999999).ToString();
        }
    }

    class AccountManager
    {
        private List<UserAccount> accounts = new List<UserAccount>();

        public AccountManager()
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            if (File.Exists(CheckingForFoldersAndFiles.FilePath))
            {
                string json = File.ReadAllText(CheckingForFoldersAndFiles.FilePath);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    accounts = JsonSerializer.Deserialize<List<UserAccount>>(json) ?? new List<UserAccount>();
                }
            }
        }

        private void SaveAccounts()
        {
            string json = JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CheckingForFoldersAndFiles.FilePath, json);
        }

        /// <summary>
        /// Проверка логина: возвращает true, если логина НЕТ в списке (свободен).
        /// </summary>
        public bool IsLoginAvailable(string loginToCheck)
        {
            return !accounts.Any(acc => acc.Login == loginToCheck);
        }

        /// <summary>
        /// Добавление нового аккаунта.
        /// </summary>
        public bool AddAccount(UserAccount account)
        {
            if (!IsLoginAvailable(account.Login))
                return false; // Логин уже занят

            accounts.Add(account);
            SaveAccounts();
            return true;
        }

        /// <summary>
        /// Получить все аккаунты.
        /// </summary>
        public List<UserAccount> GetAllAccounts()
        {
            return accounts;
        }
    }
}

