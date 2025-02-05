namespace CinemaTiketsShop.ViewModels.CinemaVMs
{
    public abstract class AbstractAddNewTemplate
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Logo { get; set; } = string.Empty;

        public double Price { get; set; }
    }
}
