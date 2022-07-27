namespace BusinessLogic
{
    public interface IBusinessLogic<T>
    {
        public Task<T> Get(T condition);

        public Task<IEnumerable<T>> GetList(T condition);

        public Task<int> InsertListAsync(IEnumerable<T> list);

        public Task<int> MergeListAsync(IEnumerable<T> list);

        public Task<int> DeleteListAsync();
    }
}
