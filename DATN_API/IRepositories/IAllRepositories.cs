namespace DATN_API.IRepositories
{
    internal interface IAllRepositories<T>    where T : class
    {
        public Task<ICollection<T>> GetAll();
        public Task<T> GetById(dynamic id);
        public Task<bool> Create(T obj);
        public Task<bool> Update(T obj);
        public Task<bool> Delete(dynamic obj);
    }
}
