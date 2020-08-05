using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using BaganFitness.BL.Model;

namespace BaganFitness.BL.Controller
{
    /// <summary>
    /// Контроллер пользователя
    /// </summary>
    public class UserController : ControllerBase
    {
        /// <summary>
        /// пользователя приложения
        /// </summary>
        public List<User> Users { get; }

        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public User CurrentUser { get; }

        /// <summary>
        /// Создание нового конроллера пользователя
        /// </summary>
        /// <param name="user"> пользователя приложения </param>

        public bool IsNewUser { get; } = false;

        private const string USERS_FILE_NAME = "users.dat";

        public UserController(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым", nameof(userName));
            }

            Users = GetUsersData();

            CurrentUser = Users.Where(x => x.Name == userName).FirstOrDefault(); //TODO: Или  CurrentUser = Users.SingleOrDefault(x => x.Name == userName);

            if (CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
                Save();
            }
        }

        /// <summary>
        /// Получить сохранённый список поьзователей
        /// </summary>
        /// <returns></returns>
        private List<User> GetUsersData()
        {
            return Load<List<User>>(USERS_FILE_NAME) ?? new List<User>();
        }

        /// <summary>
        /// Сохранить данные пользователя
        /// </summary>
        public void Save()
        {
            Save(USERS_FILE_NAME, Users);
        }

        public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1)
        {
            //TODO: Проверка

            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Save();
        }
    }
}
