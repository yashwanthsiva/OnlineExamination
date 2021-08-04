using System;
using System.Threading.Tasks;

namespace OnlineExamination.DataAccess.Repository
{
    internal class Dbset<T> where T : class
    {
        internal T Find(object id)
        {
            throw new NotImplementedException();
        }

        internal Task<T> FindAsync(object id)
        {
            throw new NotImplementedException();
        }

        internal void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        internal void Attach<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        internal void Remove<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }
    }
}