namespace GenericMinimalApi.Infrastructures
{
    public interface IEntityDto<TKey>
    {
        public TKey Id { get; set; }
    }
}