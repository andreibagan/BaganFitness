using System;
using System.Globalization;
using System.Resources;
using BaganFitness.BL.Controller;
using BaganFitness.BL.Model;

namespace BaganFitness.CMD
{
    class Program
    {
        static void Main()
        {
            //CultureInfo culture = CultureInfo.CreateSpecificCulture("de-de");
            CultureInfo culture = CultureInfo.CurrentCulture;
            ResourceManager resourceManager = new ResourceManager("BaganFitness.CMD.Languages.Messages", typeof(Program).Assembly);

            Console.WriteLine(resourceManager.GetString("Welcome", culture));

            Console.WriteLine(resourceManager.GetString("EnterName", culture));

            string name = Console.ReadLine();

            UserController userController = new UserController(name);
            EatingController eatingController = new EatingController(userController.CurrentUser);
            ExerciseController exerciseController = new ExerciseController(userController.CurrentUser);

            if (userController.IsNewUser)
            {
                Console.Write("Введите пол: ");
                string gender = Console.ReadLine();

                DateTime birthDate = ParseDateTime("дата рождения");
                double weight = ParseDouble("вес");
                double height = ParseDouble("рост");

                userController.SetNewUserData(gender, birthDate, weight, height);
            }

            Console.WriteLine(userController.CurrentUser);

            while (true)
            {
                Console.WriteLine("Что вы хотите сделать?");

                Console.WriteLine("E - ввести приём пищи");
                Console.WriteLine("A - ввести упражнение");
                Console.WriteLine("Q - выход");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.E:
                        (Food Food, double Weight) foods = EnterEating();
                        eatingController.Add(foods.Food, foods.Weight);

                        foreach (var item in eatingController.Eating.Foods)
                        {
                            Console.WriteLine($"\t{item.Key} - {item.Value}");
                        }
                        break;

                    case ConsoleKey.A:
                        (DateTime Begin, DateTime End, Activity activity) exe = EnterExercise();
                        exerciseController.Add(exe.activity, exe.Begin, exe.End);

                        foreach (var item in exerciseController.Exercises)
                        {
                            Console.WriteLine($"{item.Activity} с {item.Start.ToShortTimeString()} до {item.Finish.ToShortTimeString()}");
                        }
                        break;

                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;

                }

                Console.ReadLine();
            }
        }

        private static (DateTime Begin, DateTime End, Activity activity) EnterExercise()
        {
            Console.Write("Введите название упражнения: ");

            string name = Console.ReadLine();

            DateTime begin = ParseDateTime("начало упражнения");
            DateTime end = ParseDateTime("окончание упражнения");

            double energy = ParseDouble("расход энергии в минуту");

            Activity activity = new Activity(name, energy);

            return (begin, end, activity);
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

        private static DateTime ParseDateTime(string description = "")
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write($"Введите '{description}' (dd.MM.yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Неверный формат '{description}'");
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
