using AVN.Model.Entities;

namespace AVN.Business
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetGroupsByEmployeeIdAsync(string employeeId);
        Task<IEnumerable<Student>> GetStudentsByGroupIdAsync(string groupId);
        Task AssignScoreToStudentAsync(string studentId, int score);
    }
}
