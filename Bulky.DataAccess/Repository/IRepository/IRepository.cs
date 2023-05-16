using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
	public interface IRepository <T> where T : class
	{
		//T -  Vai ser qualquer modelo que vai comunicar com a base de dados
		IEnumerable<T> GetAll ();
		T Get(Expression<Func<T, bool>> filter);
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
