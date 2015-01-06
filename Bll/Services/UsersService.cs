using System;
using System.Collections.Generic;
using System.Linq;
using Rtc.Bll.Infrastructure;
using Rtc.Bll.Mappers;
using Rtc.BllInterface.Entities;
using Rtc.BllInterface.Services;
using Rtc.BllInterface.VO;
using Rtc.DalInterface.Dto;
using Rtc.DalInterface.Repository;

namespace Rtc.Bll.Services
{
    public class UsersService : IUsersService
    {
        #region Ctor

        private readonly IUserRepository repository;

        public UsersService(IUserRepository repository)
        {
            this.repository = repository;
            //Debug.WriteLine("service create!");
        }

        #endregion

        public IEnumerable<UserEntity> Search(int userId, string searchToken, SearchUserKind searchUserKind)
        {
            var predicate = GetSearchPredicate(searchToken, searchUserKind);
            if (predicate == null)
                return Enumerable.Empty<UserEntity>();
            var users =
                repository.Get(dto => predicate(dto) && dto.Id != userId)
                    .Select(dto => dto.ToUserEntity())
                    .OrderBy(entity => entity.Name);
            return users;
        }

        public int GetAmount()
        {
            return repository.GetAll().Count();
        }

        #region Private

        private static Predicate<UserDto> GetSearchPredicate(string searchToken, SearchUserKind searchUserKind)
        {
            if (string.IsNullOrWhiteSpace(searchToken))
                return null;
            Predicate<UserDto> predicate;
            switch (searchUserKind)
            {
                case SearchUserKind.Email:
                    predicate = dto => dto.Email.Contains(searchToken.ToLower());
                    break;
                case SearchUserKind.PhoneNumber:
                    predicate = dto => dto.PhoneNumber.Equals(Extentions.NormalizePhoneNumber(searchToken));
                    break;
                case SearchUserKind.Name:
                    var names = searchToken.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    predicate = dto => names.Any(s => dto.Name.ToLower().Contains(s));
                    break;
                default:
                    return null;
            }
            return predicate;
        }

        #endregion

    }
}




