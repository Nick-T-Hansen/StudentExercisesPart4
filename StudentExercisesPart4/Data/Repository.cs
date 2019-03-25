using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StudentExercisesPart4.Data
{
    public class Repository
    {
        public SqlConnection Connection
        {
            get
            {
                string connectionString = "Server=DESKTOP-T1CK9M7\\SQLEXPRESS;Database=StudentExercises;Trusted_Connection=True;";
                return new SqlConnection(connectionString);
            }
        }

        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, [ExerciseName], [ExerciseLanguage] from Exercise;";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        Exercise exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            ExerciseName = reader.GetString(reader.GetOrdinal("ExerciseName")),
                            ExerciseLanguage = reader.GetString(reader.GetOrdinal("ExerciseLanguage"))
                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();
                    return exercises;
                }
            }
        }
        public void AddExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"INSERT INTO Exercise (ExerciseName, ExerciseLanguage)
                                           VALUES ('{exercise.ExerciseName}', '{exercise.ExerciseLanguage}')";
                    cmd.ExecuteNonQuery();
    
                }
            }
        }
        /*
        public List<Student> GetAllStudents()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select s.id as StudentId,
                                        s.FirstName,
                                        s.LastName,
                                        s.SlackHandle,
                                        s.CohortId,
                                        c.[Name] as CohortName,
                                        e.id as ExerciseId,
                                        e.[name] as ExerciseName,
                                        e.[Language]
                                    from student s
                                        left join Cohort c on s.CohortId = c.id
                                        left join StudentExercise se on s.id = se.studentid
                                        left join Exercise e on se.exerciseid = e.id;";
                    SqlDataReader reader = cmd.ExecuteReader();

                   
                    Dictionary<int, Student> students = new Dictionary<int, Student>();
                    while (reader.Read())
                    {
                        int studentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        if (!students.ContainsKey(studentId))
                        {
                            Student newStudent = new Student
                            {
                                Id = studentId,
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Slack = reader.GetString(reader.GetOrdinal("SlackHandle")),
                                CohortId = reader.GetInt32(reader.GetOrdinal("CohortId")),

                            };

                            students.Add(newStudent);
                            reader.Close();
                            return students;
                        }
                        /*
                        if (!reader.IsDBNull(reader.GetOrdinal("ExerciseId")))
                        {
                            Student currentStudent = students[studentId];
                            currentStudent.Exercise.Add(
                                new Exercise
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ExerciseId")),
                                    ExerciseLanguage = reader.GetString(reader.GetOrdinal("Language")),
                                    ExerciseName = reader.GetString(reader.GetOrdinal("ExerciseName")),
                                }
                            );
                        }
                    }
                    reader.Close();

                    return students.Values.ToList();
                    */
               // }
          //  }
        //
    }
}       