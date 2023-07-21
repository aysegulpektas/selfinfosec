using System;
using Core.Utilities.Results;
using Entities.Concrete.DTOs;
using Entities.Models;

namespace Business.Abstract
{
	public interface IUserService
	{
        IResult AddUser(User user);
        IResult UpdateUser(EditProfileDto user);
        IResult UpdatePassword(PasswordResetDto passwordResetDto);
        IResult DeleteUser(string userId);
        IDataResult<User> GetByUsername(string username);
        IDataResult<User> GetByEmail(string email);
        IDataResult<User> GetById(string id);
        IDataResult<List<User>> GetAll();
        IResult SendResetCode(SendResetCodeDto resetCodeDto);
        IResult ResetPasswordWithCode(ResetPasswordWithCodeDto resetPasswordDto);
        IResult ResetUserPassword(string userId, string newPassword);

    }
}

