using System;
using System.Collections.Generic;
using System.Linq;

namespace BaganFitness.BL.Model
{
    /// <summary>
    /// Приём пищи
    /// </summary>
    [Serializable]
    public class Eating
    {
        public User User { get; }

        public DateTime Moment { get; }

        public Dictionary<Food, double> Foods { get; }

        public Eating(User user)
        {
            User = user ?? throw new ArgumentNullException("Пользователь не может быть пустым", nameof(user));
            Moment = DateTime.UtcNow;
            Foods = new Dictionary<Food, double>();
        }

        public void Add(Food food, double weight)
        {
            Food product = Foods.Keys.FirstOrDefault(f => f.Name == food.Name);

            if (product == null)
            {
                Foods.Add(food, weight);
            }
            else
            {
                Foods[product] += weight;
            }
        }
    }
}
