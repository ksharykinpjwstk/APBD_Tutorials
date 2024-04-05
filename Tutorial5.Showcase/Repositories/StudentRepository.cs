
public class StudentRepository : IStudentRepository
{
    private List<Student> _students {get; set;} = [];
    public void Add(Student student)
    {
        _students.Add(student);
    }

    public bool Delete(Student student)
    {
        return _students.Remove(student);
    }

    public void Edit(Student student)
    {
        throw new NotImplementedException();
    }

    public Student Get(int id)
    {
        Thread.Sleep(3000);
        var specificStudent = _students[id];
        Thread.Sleep(3000);
        return specificStudent;
    }

    public Task<IEnumerable<Student>> GetAll()
    {
        return Task.Run(() => {
            return _students.AsEnumerable();
        });
    }
}