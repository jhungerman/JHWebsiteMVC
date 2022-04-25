using JosephHungerman.Data.Models;

namespace JosephHungerman.UI.Models.ViewModels
{
    public class AboutViewModel
    {
        public Quote Quote { get; set; }
        public IList<Section> Sections { get; set; }
    }
}
