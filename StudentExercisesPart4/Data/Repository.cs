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

        //---------------------------------------------SELECT ALL EXERCIES------------------------------------------
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
        //---------------------------------------------ADD AN EXERCIES------------------------------------------
        public void AddExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"INSERT INTO Exercise (ExerciseName, ExerciseLanguage)
                                           VALUES (@ExerciseName, @ExerciseLanguage)";

                    cmd.Parameters.Add(new SqlParameter("@ExerciseName", exercise.ExerciseName));
                    cmd.Parameters.Add(new SqlParameter("@ExerciseLanguage", exercise.ExerciseLanguage));
                    cmd.ExecuteNonQuery();
    
                }
            }
        }
        // ---------------------------------------------SELECT ALL INSTRUCTORS------------------------------------------
        public List<Instructor> GetAllInstructors()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT i.FirstName, i.LastName, c.CohortName
                                        FROM Instructor i
                                        LEFT JOIN Cohort c on i.cohort_id = c.Id";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        Instructor instructor = new Instructor
                        {
                            //Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Cohort = new Cohort
                            {
                                CohortName = reader.GetString(reader.GetOrdinal("CohortName"))
                            }
                        };

                        instructors.Add(instructor);
                    }

                    reader.Close();
                    return instructors;
                }
            }
        }
        // ---------------------------------------------SELECT ALL INSTRUCTORS------------------------------------------
        public void AddInstructor(Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"INSERT INTO Instructor (FirstName, LastName, Slack, Cohort_id)
                                           VALUES (@FirstName, @LastName, @Slack, @Cohort_id)";

                    cmd.Parameters.Add(new SqlParameter("@FirstName", instructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", instructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Slack", instructor.Slack));
                    cmd.Parameters.Add(new SqlParameter("@Cohort_id", instructor.CohortId));
                    cmd.ExecuteNonQuery();

                }
            }
        }
        //--------------------------------------- UPDATE STUDENT EXERCISE-------------------------------------
        public void UpdateStudentWithExercise(int studentId, int exerciseId)
        {           
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    /*
                    cmd.CommandText = @"UPDATE Student
                                           SET FirstName = @FirstName, LastName = @LastName, Slack = @Slack, Cohort_id = @Cohort_id
                                         WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", student.Id));
                    cmd.Parameters.Add(new SqlParameter("@FirstName", student.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", student.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Slack", student.Slack));
                    cmd.Parameters.Add(new SqlParameter("@Cohort_id", student.CohortId));
                    */

                    cmd.CommandText = $@"INSERT INTO StudentExercise (Student_id, Exercise_id)
                                        VALUES (@Student_id, @Exercise_id)";
                    cmd.Parameters.Add(new SqlParameter("@Student_id", studentId));
                    cmd.Parameters.Add(new SqlParameter("@Exercise_id", exerciseId));
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