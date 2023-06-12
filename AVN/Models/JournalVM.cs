using AVN.Model.Entities;

namespace AVN.Models
{
    public class JournalVM
    {
        public Employee Employee { get; set; }
        public List<GroupViewModel> Groups { get; set; }
    }
    public class GroupViewModel
    {
        public string Name { get; set; }
        public List<StudentViewModel> Students { get; set; }
    }

    public class StudentViewModel
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
