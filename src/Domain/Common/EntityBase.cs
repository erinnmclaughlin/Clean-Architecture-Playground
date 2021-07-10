namespace Domain.Common
{
    public abstract class EntityBase<T> : EntityBase
    {
        public T Id { get; set; }
    }

    public abstract class EntityBase
    {
    }
}