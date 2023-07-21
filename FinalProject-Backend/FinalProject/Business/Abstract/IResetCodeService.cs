using System;
using Core.Utilities.Results;
using Entities.Concrete.DTOs;

namespace Abstract
{
    public interface IResetCodeService
    {
        public IResult SendResetCode(SendResetCodeDto dto);
        public IResult VerifyResetCode(string email,string code);
        public IResult ResetCode(ResetPasswordWithCodeDto resetpassworddto);
    }
}