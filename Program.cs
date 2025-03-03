using Microsoft.Data.SqlClient;
using Dapper;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=SchoolDB;Trusted_Connection=True;";

using (var connection = new SqlConnection(connectionString))
{
    Console.WriteLine("Database Connected!");
}
        var dbHelper = new DatabaseHelper(connectionString);
        var studentRepo = new StudentRepository(dbHelper);
        var enrollmentRepo = new EnrollmentRepository(dbHelper);

        // Add Students
        studentRepo.AddStudent("Alice", 22);
        studentRepo.AddStudent("Bob", 24);
        Console.WriteLine("Students Added.");

        // Get All Students
        var students = studentRepo.GetAllStudents();
        Console.WriteLine("\nAll Students:");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id}: {student.Name}");
        }

        // Enroll Alice in Course 1
        enrollmentRepo.EnrollStudent(1, 1);
        Console.WriteLine("\nAlice enrolled in Course 1.");

        // Get Alice's Courses
        var courses = enrollmentRepo.GetStudentCourses(1);
        Console.WriteLine("\nAlice's Courses:");
        foreach (var course in courses)
        {
            Console.WriteLine($"{course.Id}: {course.Title}");
        }

        // Update Student
        studentRepo.UpdateStudent(1, "Alice Updated", 23);
        Console.WriteLine("\nStudent Updated.");

        // Delete Student
        studentRepo.DeleteStudent(2);
        Console.WriteLine("\nStudent Deleted.");
    }
}


