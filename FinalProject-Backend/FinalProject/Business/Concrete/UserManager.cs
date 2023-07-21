using System;
using Business.Abstract;
using Business.ConstantsFolder;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.DTOs;
using Entities.Models;
using Microsoft.VisualBasic;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDao _userDao;
        IHttpContextHelperService _httpHelperService;
        //IPasswordResetCodeDao _passwordResetCodeDao;


        public UserManager(IUserDao userDao, IHttpContextHelperService httpContextHelperService)
        {
            _userDao = userDao;
            _httpHelperService = httpContextHelperService;
            //_passwordResetCodeDao = passwordResetCodeDao;


        }

        public IResult AddUser(User user)
        {
            var getByEmail = _userDao.Get(x => x.Email.ToLower() == user.Email.ToLower());
            var getByUsername = _userDao.Get(x => x.UserName.ToLower() == user.UserName.ToLower());
            if (getByEmail != null)
            {
                return new ErrorResult(Business.ConstantsFolder.Constants.User_EmailExists);
            }

            if (getByUsername != null)
            {
                return new ErrorResult(Business.ConstantsFolder.Constants.User_UsernameExists);
            }

            _userDao.Add(user);
            return new SuccessResult(Business.ConstantsFolder.Constants.User_Added);
        }



        public IResult DeleteUser(string userId)
        {
            var user = _userDao.Get(x => x.UserId == userId);
            _userDao.Delete(user);
            return new SuccessResult(Business.ConstantsFolder.Constants.User_Deleted);
        }


        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDao.GetAll());
        }



        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDao.Get(x => x.Email == email));
        }



        public IDataResult<User> GetById(string id)
        {
            return new SuccessDataResult<User>(_userDao.Get(x => x.UserId == id));
        }



        public IDataResult<User> GetByUsername(string username)
        {
            return new SuccessDataResult<User>(_userDao.Get(x => x.UserName == username));
        }

        public IResult ResetPasswordWithCode(ResetPasswordWithCodeDto resetPasswordDto)
        {
            throw new NotImplementedException();
        }

        public IResult SendResetCode(SendResetCodeDto resetCodeDto)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Düzenlenecek")]
        public IResult UpdatePassword(PasswordResetDto passwordResetDto)
        {


            var requestedUser = _userDao.Get(x => x.UserId == _httpHelperService.GetUserId());
            if (requestedUser != null)
            {
                var passwordVerify = HashingHelper.VerifyPasswordHash(passwordResetDto.OldPassword, requestedUser.PasswordHash, requestedUser.PasswordSalt);
                if (!passwordVerify)
                {
                    return new ErrorResult(ConstantsFolder.Constants.Auth_Login_WrongPassword);
                }
                {

                    string password = passwordResetDto.NewPassword;
                    if (password.Length < 8)
                    {
                        return new ErrorResult("Parola 8 karakterden kısa olamaz");
                    }

                    byte[] passwordHash;
                    byte[] passwordSalt;
                    HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                    requestedUser.PasswordHash = passwordHash;
                    requestedUser.PasswordSalt = passwordSalt;
                    _userDao.Update(requestedUser);
                    return new SuccessResult("Şifre başarıyla değiştirildi");
                }
            }
            return new ErrorResult();

        }
        public IResult ResetUserPassword(string userId, string newPassword)
        {
            var requestedUser =_userDao.Get(x=>x.UserId == userId);
            string password = newPassword;
            if (password.Length < 8)
            {
                return new ErrorResult("Parola 8 karakterden kısa olamaz");
            }

            byte[] passwordHash;
            byte[] passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            requestedUser.PasswordHash = passwordHash;
            requestedUser.PasswordSalt = passwordSalt;
            _userDao.Update(requestedUser);
            return new SuccessResult("Şifre başarıyla değiştirildi");
        }


        public IResult UpdateUser(EditProfileDto user)
        {
            var userId = _httpHelperService.GetUserId();
            var userinfo = _userDao.Get(x => x.UserId == userId);
            if (userinfo.Email.ToLower() != user.Email.ToLower())
            {
                var getByEmail = _userDao.Get(x => x.Email.ToLower() == user.Email.ToLower());
                if (getByEmail != null)
                    return new ErrorResult("E-Posta adresi mevcut");
                userinfo.Email = user.Email;
            }
            userinfo.FirstName = user.FirstName;
            userinfo.Lastname = user.LastName;

            _userDao.Update(userinfo);
            return new SuccessResult(ConstantsFolder.Constants.User_Updated);
        }
    }
}

