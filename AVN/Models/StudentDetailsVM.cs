using AVN.Model.Entities;

namespace AVN.Models
{
    public class StudentDetailsVM
    {
        public Student Student { get; set; }
        public List<StudentPayment> Contracts { get; set; }
    }
}
