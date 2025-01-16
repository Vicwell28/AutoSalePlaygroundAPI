namespace AutoSalePlaygroundAPI.Domain.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public bool IsActive { get; protected set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public virtual void MarkUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual void Deactivate()
        {
            IsActive = false;
            MarkUpdated();
        }

        public virtual void Activate()
        {
            IsActive = true;
            MarkUpdated();
        }
    }
}
