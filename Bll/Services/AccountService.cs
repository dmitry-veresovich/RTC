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
        }

        #endregion

        public void CreateAccount(UserEntity user, string hashedPassword)
        {
            user.PhoneNumber = Extentions.NormalizePhoneNumber(user.PhoneNumber);
            user.Email = user.Email.ToLower();
            user.Name = user.Name.Trim();
            var dto = user.ToDto(hashedPassword);
            repository.Create(dto);
            uow.Commit();
        }

        public void UpdateName(string logInToken, string newName, LogInType logInType)
        {
            var userDto = repository.GetFirstOrDefault(GetLogInPredicate(logInToken, logInType));
            userDto.Name = newName.Trim();
            repository.Update(userDto);
            uow.Commit();
        }

        public void UpdateEmail(string logInToken, string newEmal, LogInType logInType)
        {
            var userDto = repository.GetFirstOrDefault(GetLogInPredicate(logInToken, logInType));
            userDto.Email = newEmal.ToLower();
            repository.Update(userDto);
            uow.Commit();
        }

        public void UpdatePhoneNumber(string logInToken, string newPhoneNumber, LogInType logInType)
        {
            var userDto = repository.GetFirstOrDefault(GetLogInPredicate(logInToken, logInType));
            userDto.PhoneNumber = Extentions.NormalizePhoneNumber(newPhoneNumber);
            repository.Update(userDto);
            uow.Commit();
        }

        public void UpdatePhoto(string logInToken, byte[] photo, LogInType logInType)
        {
            var userDto = repository.GetFirstOrDefault(GetLogInPredicate(logInToken, logInType));
            userDto.Photo = photo;
            repository.Update(userDto);
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
            SetHashedPassword(logInToken, newHashedPassword, Extentions.GetLogInType(logInToken));
        }

        public void SetHashedPassword(string logInToken, string newHashedPassword, LogInType logInType)
        {
            var userDto = repository.GetFirstOrDefault(GetLogInPredicate(logInToken, logInType));
            userDto.Password = newHashedPassword;
            repository.Update(userDto);
            uow.Commit();
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