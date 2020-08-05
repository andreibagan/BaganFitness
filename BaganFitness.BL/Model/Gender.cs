using System;

namespace BaganFitness.BL.Model
{
    /// <summary>
    /// Пол
    /// </summary>
    [Serializable]
    public class Gender
    {
        /// <summary>
        /// Название пола
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Создать новый пол
        /// </summary>
        /// <param name="name"> Название пола </param>
        public Gender(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя поля не может быть пустым или null", nameof(name));
            }

            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
