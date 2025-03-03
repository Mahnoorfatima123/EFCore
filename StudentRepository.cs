using System.Collections.Generic;
using System.Linq;
using Dapper;

public class StudentRepository
{
    private readonly DatabaseHelper _dbHelper;

    public StudentRepository(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public void AddStudent(string name, int age)
    {
        using (var db = _dbHelper.GetConnection())
        {
            string sql = "INSERT INTO Students (Name, Age) VALUES (@Name, @Age)";
            db.Execute(sql, new { Name = name, Age = age });
        }
    }

    public List<Student> GetAllStudents()
    {
        using (var db = _dbHelper.GetConnection())
        {
            string sql = "SELECT * FROM Students";
            return db.Query<Student>(sql).ToList();
        }
    }

    public Student GetStudentById(int id)
    {
        using (var db = _dbHelper.GetConnection())
        {
            string sql = "SELECT * FROM Students WHERE Id = @Id";
            return db.QueryFirstOrDefault<Student>(sql, new { Id = id });
        }
    }

    public void UpdateStudent(int id, string name, int age)
    {
        using (var db = _dbHelper.GetConnection())
        {
            string sql = "UPDATE Students SET Name = @Name, Age = @Age WHERE Id = @Id";
            db.Execute(sql, new { Id = id, Name = name, Age = age });
        }
    }

    public void DeleteStudent(int id)
    {
        using (var db = _dbHelper.GetConnection())
        {
            string sql = "DELETE FROM Students WHERE Id = @Id";
            db.Execute(sql, new { Id = id });
        }
    }
}
