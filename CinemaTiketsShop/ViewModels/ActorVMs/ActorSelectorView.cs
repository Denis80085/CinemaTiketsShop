using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CinemaTiketsShop.ViewModels.ActorVMs
{
    public class ActorSelectorView
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
    }
}
