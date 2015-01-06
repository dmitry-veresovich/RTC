using System;
using Rtc.Bll.Infrastructure;
using Rtc.Bll.Mappers;
using Rtc.BllInterface.Entities;
using Rtc.BllInterface.Services;
using Rtc.BllInterface.VO;
using Rtc.DalInterface.Dto;
using Rtc.DalInterface.Repository;

namespace Rtc.Bll.Services
{
    public class AccountService : IAccountService
    {
        #region Ctor

        private readonly IUnitOfWork uow;
        private readonly IUserRepository repository;

        public AccountService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.repository = repository;
            //Debug.WriteLine("service create!");
        }

        #endregion

        public void CreateAccount(UserEntity user, string hashedPassword)
        {
            user.PhoneNumber = Extentions.NormalizePhoneNumber(user.PhoneNumber);
            user.Email = user.Email.ToLower();
            var dto = user.ToDto(hashedPassword);
            repository.Create(dto);
            uow.Commit();
        }

        public UserEntity GetAccount(int id)
        {
            return repository.Get(id).ToUserEntity();
        }

        public UserEntity GetAccount(string logInToken)
        {
            return GetAccount(logInToken, Extentions.GetLogInType(logInToken));
        }

        public UserEntity GetAccount(string logInToken, LogInType logInType)
        {
            var predicate = GetLogInPredicate(logInToken, logInType);
            return repository.GetFirstOrDefault(predicate).ToUserEntity();
        }

        public string GetHashedPassword(string logInToken)
        {
            return GetHashedPassword(logInToken, Extentions.GetLogInType(logInToken));
        }

        public string GetHashedPassword(string logInToken, LogInType logInType)
        {
            var predicate = GetLogInPredicate(logInToken, logInType);
            var user = repository.GetFirstOrDefault(predicate);
            return user != null ? user.Password : null;
        }

        public void SetHashedPassword(string logInToken, string newHashedPassword)
        {
            throw new NotImplementedException();
        }

        public void SetHashedPassword(string logInToken, string newHashedPassword, LogInType logInType)
        {
            throw new NotImplementedException();
        }

        public bool AccountExists(string logInToken)
        {
            return AccountExists(logInToken, Extentions.GetLogInType(logInToken));
        }

        public bool AccountExists(string logInToken, LogInType logInType)
        {
            var predicate = GetLogInPredicate(logInToken, logInType);
            return repository.GetFirstOrDefault(predicate) != null;
        }

        //public IEnumerable<UserEntity> GetAllUsers()
        //{
        //    return repository.GetAll().Select(user => user.ToUserEntity());
        //}

        //public UserEntity GetUserById(int id)
        //{
        //    return userRepository.Get(id).ToUserEntity();
        //}

        //public IEnumerable<UserEntity> GetUsersById(IEnumerable<int> usersIds)
        //{
        //    return
        //        userRepository.GetAll()
        //            .Join(usersIds, user => user.Id, userId => userId, (user, i) => user)
        //            .Select(userDto => userDto.ToUserEntity());
        //}


        #region Private

        private static Predicate<UserDto> GetLogInPredicate(string logInToken, LogInType logInType)
        {
            Predicate<UserDto> predicate;
            switch (logInType)
            {
                case LogInType.Email:
                    predicate = dto => dto.Email.Equals(logInToken.ToLower());
                    break;
                case LogInType.PhoneNumber:
                    predicate = dto => dto.PhoneNumber.Equals(Extentions.NormalizePhoneNumber(logInToken));
                    break;
                default:
                    throw new Exception();
            }
            return predicate;
        }

        #endregion

    }
}