using mvc1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc1.Daos.Member
{
    public class MemberDao : IDao<member>
    {
        private static readonly MemberEntities db = new MemberEntities();

        public List<member> FindAll()
        {
            List<member> members = db.members.ToList();
            return members;
        }

        public member FindOne(int? id)
        {
            if (id is null | id < 0) return null;

            member member = db.members.SingleOrDefault(m => m.id == id);
            if (member is null) return null;

            return member;
        }

        public bool FindByUsername(string username)
        {
            member member = db.members.Where(m => m.username == username).FirstOrDefault();
            if (member is null) return false;
            
            return true;
        }

        public member Save(member obj)
        {
            if (obj is null) return null;

            db.members.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public member Update(int? id, member obj)
        {
            if (id is null || id < 0 || obj is null) return null;

            obj.id = id.Value;
            member member = db.members.SingleOrDefault(m => m.id == id);
            if (member is null) return null;

            db.Entry(member).CurrentValues.SetValues(obj);
            db.SaveChanges();
            return member;
        }

        public bool Delete(int? id)
        {
            if (id is null) return false;

            member member = db.members.SingleOrDefault(m => m.id == id);
            if (member is null) return false;

            db.members.Remove(member);
            db.SaveChanges();
            return true;
        }

        public int TotalPages(int rowsPerPage)
        {
            int totalPages = Convert.ToInt32( Math.Ceiling( db.members.Count() / Convert.ToDouble(rowsPerPage) ) );
            return totalPages;
        }

        public List<member> Pagination(int page, int rowsPerPage)
        {
            List<member> members = db.members.OrderBy(m => m.id).Skip((page - 1) * rowsPerPage).Take(rowsPerPage).ToList();
            return members;
        }
    }
}