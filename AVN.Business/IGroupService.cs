using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVN.Model.Entities;

namespace AVN.Business
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetGroupsByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Student>> GetStudentsByGroupIdAsync(int groupId);
        Task AssignScoreToStudentAsync(int studentId, int score);
    }
}
