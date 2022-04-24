using JosephHungerman.Data.Models;

namespace JosephHungerman.Models.ViewModels
{
    public class AboutViewModel
    {
        public Quote Quote { get; set; }
        public IList<Section> Sections { get; set; }
    }
}
