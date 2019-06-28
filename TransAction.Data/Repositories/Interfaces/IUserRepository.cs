using System.Collections.Generic;
using TransAction.Data.Models;

namespace TransAction.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<TraUser> GetAll(int page, int pageSize);
        TraUser GetById(int id);
        TraUser GetByGuid(string guid);
        IEnumerable<TraUser> GetUserInTeam(int teamId);
        void Create(TraUser newEvent);
    }
}
