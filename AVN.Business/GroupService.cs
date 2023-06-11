using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;

namespace AVN.Business
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Group>> GetGroupsByEmployeeIdAsync(int employeeId)
        {
            return await _unitOfWork.GroupRepository.GetGroupsByEmployeeIdAsync(employeeId);
        }

        public async Task<IEnumerable<Student>> GetStudentsByGroupIdAsync(int groupId)
        {
            var group = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
            if (group == null)
            {
                throw new Exception("Группа не найдена");
            }

            return group.Students;
        }

        public async Task AssignScoreToStudentAsync(int studentId, int score)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                throw new Exception("Студент не найден");
            }

            student.Score = score;
            _unitOfWork.StudentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
