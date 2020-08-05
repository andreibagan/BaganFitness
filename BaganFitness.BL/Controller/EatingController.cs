using System;
using System.Collections.Generic;
using System.Linq;
using BaganFitness.BL.Model;

namespace BaganFitness.BL.Controller
{
    public class EatingController : ControllerBase
    {
        private readonly User user;

        public List<Food> Foods { get; }

        public Eating Eating { get; }

        private const string FOODS_FILE_NAME = "foods.dat";

        private const string EATINGS_FILE_NAME = "eatings.dat";

        public EatingController(User user)
        {
            this.user = user ?? throw new ArgumentNullException("пользователь не можте быть пустым", nameof(user));

            Foods = GetAllFoods();

            Eating = GetEating();
        }

        public void Add(Food food, double weight)
        {
            Food product = Foods.SingleOrDefault(f => f.Name == food.Name);

            if (product == null)
            {
                Foods.Add(food);
                Eating.Add(food, weight);
                Save();
            }
            else
            {
                Eating.Add(product, weight);
                Save();
            }
        }

        private Eating GetEating()  
        {
            return base.Load<Eating>(EATINGS_FILE_NAME) ?? new Eating(user);
        }

        private List<Food> GetAllFoods()
        {
            return base.Load<List<Food>>(FOODS_FILE_NAME) ?? new List<Food>(); 
        }
         
        public void Save()
        {
            base.Save(FOODS_FILE_NAME, Foods);
            base.Save(EATINGS_FILE_NAME, Eating);
        }
    }
}
