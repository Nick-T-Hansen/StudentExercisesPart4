using StudentExercisesPart4.Data;
using System;
using System.Collections.Generic;

namespace StudentExercisesPart4
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Query the database for all the Exercises.
            Find all the exercises in the database where the language is JavaScript.
            Insert a new exercise into the database.
            Find all instructors in the database.Include each instructor's cohort.
            Insert a new instructor into the database.Assign the instructor to an existing cohort.
            Assign an existing exercise to an existing student.
            */

            Repository repository = new Repository();

            //GET all exercises

            List<Exercise> AllExercises = repository.GetAllExercises();

            foreach(Exercise e in AllExercises)
            {
                Console.WriteLine($"{e.ExerciseName}");
            }

            //Insert a new exercise in the database

            Exercise Stocks = new Exercise {
                ExerciseName = "Stocks",
                ExerciseLanguage = "C#" };

            repository.AddExercise(Stocks);
            Console.WriteLine($"{Stocks.ExerciseLanguage} was added to the database");

            foreach (Exercise e in AllExercises)
            {
                Console.WriteLine($"{e.ExerciseName}");
            }
            Console.ReadKey();
        }
    }
}
