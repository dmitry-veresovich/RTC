using System.Data.Entity;
using Rtc.Dal.Dao;
using Rtc.Dal.Mappers;
using Rtc.DalInterface.Dto;
using Rtc.DalInterface.Repository;

namespace Rtc.Dal.Repository
{
    public class RoleRepository : BaseRepository<RoleDto, Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context, new RoleMapper()) { }
       
    }

}