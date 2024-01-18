namespace Infrastructure.Repository.Primitives
{
    public class RepositoryResponse<T>
    {
        public T Data { get; private set; }
        public Meta Meta { get; private set; }
        public RepositoryResponse(T data, ref Meta meta)
        {
            Data = data;
            Meta = meta;
        }
    }
    public class Meta
    {
        public int TotalItems {  get; set; }
        public Meta() 
        {
        }
    }
}
