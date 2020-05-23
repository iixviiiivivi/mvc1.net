using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc1.Daos
{
    public interface IDao<T>
    {
        List<T> FindAll();
        T FindOne(int? id);
        T Save(T obj);
        T Update(int? id, T obj);
        bool Delete(int? id);
    }
}
