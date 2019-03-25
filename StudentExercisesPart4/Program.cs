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
            Console.WriteLine($"{Stocks.ExerciseName} was added to the database");


            // Find all instructors in the database.Include each instructor's cohort
            List<Instructor> Allinstructors = repository.GetAllInstructors();

            foreach (Instructor i in Allinstructors)
            {
                Console.WriteLine($"{i.FirstName} {i.LastName}, {i.Cohort.CohortName}");
            }

            // Insert a new instructor into the database.Assign the instructor to an existing cohort.

            Instructor McBoaty = new Instructor
            {
               FirstName = "Boaty",
               LastName = "McBoatFace",
               Slack = "@plane",
               CohortId = 2
            };

            repository.AddInstructor(McBoaty);
            Console.WriteLine($"{McBoaty.FirstName} {McBoaty.LastName}, was added to the database");

            repository.UpdateStudentWithExercise(4, 1);

            // Assign an existing exercise to an existing student.

        
            // ------------------------------------------------PAUSE CONSOLE-------------------------------------------
            Console.ReadKey();
        }
    }
}
