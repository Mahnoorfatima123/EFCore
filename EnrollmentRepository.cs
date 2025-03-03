using System.Collections.Generic;
using System.Linq;
using Dapper;

public class EnrollmentRepository
{
    private readonly DatabaseHelper _dbHelper;

    public EnrollmentRepository(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public void EnrollStudent(int studentId, int courseId)
    {
        using (var db = _dbHelper.GetConnection())
        {
            string sql = "INSERT INTO StudentCourses (StudentId, CourseId) VALUES (@StudentId, @CourseId)";
            db.Execute(sql, new { StudentId = studentId, CourseId = courseId });
        }
    }

    public List<Course> GetStudentCourses(int studentId)
    {
        using (var db = _dbHelper.GetConnection())
        {
            string sql = @"
                SELECT c.* FROM Courses c
                INNER JOIN StudentCourses sc ON c.Id = sc.CourseId
                WHERE sc.StudentId = @StudentId";

            return db.Query<Course>(sql, new { StudentId = studentId }).ToList();
        }
    }
}
