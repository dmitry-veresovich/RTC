using Rtc.Bll.Mappers;
using Rtc.BllInterface.Entities;
using Rtc.BllInterface.Services;
using Rtc.DalInterface.Repository;

namespace Rtc.Bll.Services
{
    public class RoleService : IRoleService
    {
        #region Ctor

        private readonly IUnitOfWork uow;
        private readonly IRoleRepository repository;

        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            this.repository = repository;
            //Debug.WriteLine("service create!");
        }

        #endregion


        public void CreateRole(RoleEntity role)
        {
            repository.Create(role.ToDto());
            uow.Commit();
        }

        public RoleEntity GetRole(string name)
        {
            return repository.GetFirstOrDefault(dto => dto.Name == name).ToEntity();
        }

        public RoleEntity GetRole(int id)
        {
            return repository.GetFirstOrDefault(dto => dto.Id == id).ToEntity();
        }

    }
}
