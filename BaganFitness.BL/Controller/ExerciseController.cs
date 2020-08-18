using System;
using System.Collections.Generic;
using System.Linq;
using BaganFitness.BL.Model;

namespace BaganFitness.BL.Controller
{
    public class ExerciseController : ControllerBase
    {
        private readonly User user;

        public List<Exercise> Exercises { get; }
        public List<Activity> Activities { get; }
         
        private const string EXERCISES_FILE_NAME = "exercises.dat"; 
        private const string ACTIVITIES_FILE_NAME = "activities.dat";

        public ExerciseController(User user)
        {
            this.user = user ?? throw new ArgumentNullException("Пользователь не может быть пустым", nameof(user));

            Exercises = GetAllExercises();
            Activities = GetAllActivities();
        }

        public void Add(Activity activity, DateTime Begin, DateTime End)
        {
            var act = Activities.SingleOrDefault(a => a.Name == activity.Name);

            if (act == null)
            {
                Activities.Add(activity);

                Exercise exercise = new Exercise(Begin, End, activity, user);
                Exercises.Add(exercise);
            }
            else
            {
                Exercise exercise = new Exercise(Begin, End, act, user);
                Exercises.Add(exercise);
            }

            Save();
        }

        private List<Exercise> GetAllExercises()
        {
            return Load<List<Exercise>>(EXERCISES_FILE_NAME) ?? new List<Exercise>();
        }

        private List<Activity> GetAllActivities()
        {
            return Load<List<Activity>>(ACTIVITIES_FILE_NAME) ?? new List<Activity>();
        }

        private void Save()
        {
            base.Save(EXERCISES_FILE_NAME, Exercises);
            base.Save(ACTIVITIES_FILE_NAME, Activities);
        }
    }
}
