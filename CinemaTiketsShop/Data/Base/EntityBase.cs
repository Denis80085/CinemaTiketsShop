namespace CinemaTiketsShop.Data.Base
{
    public class EntityBase : IEntityBase
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
    }
}
