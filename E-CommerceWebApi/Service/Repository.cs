using E_CommerceWebApi.Data;
using E_CommerceWebApi.Repository;

namespace E_CommerceWebApi.Service
{
    public class Repository<T> : IRepository<T> where T : class
	{

		private readonly ECEntity _context;

		public Repository(ECEntity context)
		{
			this._context = context;
		}
		public void Add(T entity)
		{
			_context.Set<T>().Add(entity);
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public IEnumerable<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public T GetById(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public void Update(int id, T entity)
		{
			_context.Set<T>().Update(entity);
		}
	}
}
