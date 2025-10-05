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

namespace SecurityNotepad
{
    class UserAccount
    {
        CheckingForFoldersAndFiles checkFile = new();
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; } // Хэш пароля
        public string FolderName { get; set; } // для личной папки пользователя

        public Random random = new Random();

        [JsonIgnore]
        public List<UserAccount> accounts { get; set; }

        public UserAccount(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public UserAccount() 
        {
            FolderName = random.Next(100000, 999999).ToString(); // Генерация случайного имени папки
        }

        public void CheckingAccounts() 
        {
            if (File.Exists(checkFile.FilePath))
            {
                string json = File.ReadAllText(checkFile.FilePath);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    accounts = JsonSerializer.Deserialize<List<UserAccount>>(json) ?? new List<UserAccount>();
                }
                else
                {
                    accounts = new List<UserAccount>();
                }
            }
            else
            {
                accounts = new List<UserAccount>();
            }
        }
        public void SaveAccount(UserAccount account)
        {
            // Загружаем уже существующие аккаунты
            CheckingAccounts();

            // Добавляем новый аккаунт в список
            accounts.Add(account);

            // Сериализуем список обратно в JSON
            string json = JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true });

            // Записываем в файл
            File.WriteAllText(checkFile.FilePath, json);
        }
    }
}
