using datagenerator.core.models;
using datagenerator.core.repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace datagenerator.core.services
{
    public sealed class UserService
    {
        public readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public List<User> GetAll() => _context.Users.ToList();

        public User GetOne(string nickname) => _context.Users.FirstOrDefault(u => u.Nickname == nickname);

        private User GetOneWithUserData(string nickname) => _context.Users.Include(u => u.UserData).FirstOrDefault(u => u.Nickname == nickname);

        public User Create(string nickname, string password)
        {
            if (nickname.Length < 3 || nickname.Length > 32)
                throw new ArgumentException("Nickname should be greater than 3 symbols and less than 32");

            if (password.Length < 3 || password.Length > 32)
                throw new ArgumentException("Password should be greater than 3 symbols and less than 32");

            User user = new() { Nickname = nickname, IsDeleted = false };

            try
            {
                _context.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating user: " + ex.Message, ex);
            }

            UserData userData = new() { Password = EncryptionPassword(password, nickname), User = user };

            try
            {
                _context.Add(userData);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating password: " + ex.Message, ex);
            }

            return user;
        }

        public User ChangePassword(string nickname, string currentPassword, string newPassword)
        {
            User user = GetOneWithUserData(nickname);

            if (user == null || user.UserData == null)
                throw new ArgumentNullException($"{nickname} user not found");

            if (newPassword.Length < 3 || newPassword.Length > 32)
                throw new ArgumentException("Password should be greater than 3 symbols and less than 32");

            string encryptionCurrentPassword = EncryptionPassword(currentPassword, nickname);

            if (user.UserData.Password != encryptionCurrentPassword)
                throw new ArgumentException("Incorrect password");

            user.UserData.Password = EncryptionPassword(newPassword, nickname);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("error changing password: " + ex.Message, ex);
            }

            return user;
        }

        public User Remove(string nickname)
        {
            User user = GetOne(nickname);

            if (user == null) 
                throw new ArgumentNullException($"{nickname} user not found");

            if (user.IsDeleted)
                throw new ArgumentException($"{nickname} already removed");

            user.IsDeleted = true;

            try
            {
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("Error removing user: " + ex.Message, ex);
            }

            return user;
        }

        private string EncryptionPassword(string password, string salt)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(password + salt);

            for (int i = 0; i < 3; i++)
                saltBytes = SHA256.HashData(saltBytes);

            StringBuilder stringBuilder = new ();

            foreach (byte b in saltBytes)
                stringBuilder.Append(b);

            return stringBuilder.ToString();
        }
    }
}
