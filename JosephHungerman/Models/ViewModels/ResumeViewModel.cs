using JosephHungerman.Models.Work;

namespace JosephHungerman.Models.ViewModels
{
    public class ResumeViewModel
    {
        public Quote Quote { get; set; }
        public Resume Resume { get; set; } = new();
    }
}
