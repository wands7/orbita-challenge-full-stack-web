using OrbitaChallengerBackEnd.Models;
using OrbitaChallengerBackEnd.ViewModels;

namespace OrbitaChallengerBackEnd.Interfaces
{
    public interface IStudentRepository
    {
        Task<ResultViewModel<bool>> AddAsync(Student student);
        Task<ResultViewModel<IEnumerable<Student>>> GetAllAsync();
        Task<ResultViewModel<List<Student>>> GetByNameAsync(string name);
        Task<ResultViewModel<bool>> DeleteAsync(string ra);
        Task<ResultViewModel<bool>> EditAsync(Student updatedStudent);
        Task<ResultViewModel<IEnumerable<Student>>> GetByAsync(string searchTerm);
        Task<ResultViewModel<Student>> GetByRAAsync(string RA);

    }
}
