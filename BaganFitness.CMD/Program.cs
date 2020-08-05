using System;
using BaganFitness.BL.Controller;
using BaganFitness.BL.Model;

namespace BaganFitness.CMD
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Вас приветсвует приложение BaganFitness");

            Console.WriteLine("Введите имя пользователя");

            string name = Console.ReadLine();

            UserController userController = new UserController(name);
            EatingController eatingController = new EatingController(userController.CurrentUser);

            if (userController.IsNewUser)
            {
                Console.Write("Введите пол: ");
                string gender = Console.ReadLine();

                DateTime birthDate = ParseDateTime();
                double weight = ParseDouble("вес");
                double height = ParseDouble("рост");

                userController.SetNewUserData(gender, birthDate, weight, height);
            }

            Console.WriteLine(userController.CurrentUser);

            Console.WriteLine("Что вы хотите сделать?");

            Console.WriteLine("E - ввести приём пищи");

            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();

            if (key.Key == ConsoleKey.E)
            {
                (Food Food, double Weight) foods = EnterEating();
                eatingController.Add(foods.Food, foods.Weight);

                foreach (var item in eatingController.Eating.Foods)
                {
                    Console.WriteLine($"\t{item.Key} - {item.Value}");
                }
            }

            Console.ReadLine();
        }

        private static (Food Food, double Weight) EnterEating()
        {
            Console.Write("Введите имя продукта: ");

            string food = Console.ReadLine();

            double calories = ParseDouble("калорийность");
            double proteins = ParseDouble("белка");
            double fats = ParseDouble("жиры");
            double сarbohydrates = ParseDouble("углеводы");

            double weight = ParseDouble("вес порции");

            return (Food: new Food(food, proteins, fats, сarbohydrates, calories), Weight: weight);
        }

        private static DateTime ParseDateTime()
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write("Введите дату рождения (dd.MM.yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный формат даты рождения");
                }
            }

            return birthDate;
        }

        private static double ParseDouble(string name)
        {
            while (true)
            {
                Console.Write($"Введите {name}: ");
                
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Неверный формат поля {name}");
                }
            }
        }
    }
}
