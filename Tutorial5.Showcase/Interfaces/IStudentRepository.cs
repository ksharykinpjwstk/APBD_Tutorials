public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAll();
    Student Get(int id);
    void Add(Student student);
    void Edit(Student student);
    bool Delete(Student student);
}