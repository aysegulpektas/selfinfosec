using System;
using System.Net.Mail;
using System.Text;
using Abstract;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.DTOs;
using Entities.Models;
using Microsoft.Extensions.Configuration;

namespace Concrete
{
    public class ResetCodeManager : IResetCodeService
    {
        IResetCodeDao _resetCodeDao;
        IUserService _userService;
        IConfiguration _configuration;
        public ResetCodeManager(IResetCodeDao resetCodeDao, IUserService userService, IConfiguration configuration)
        {
            _resetCodeDao = resetCodeDao;
            _userService = userService;
            _configuration = configuration;
        }

        public IResult SendResetCode(SendResetCodeDto dto)
        {
            var user = _userService.GetByEmail(dto.Email);
            ResetCode resetCode = new ResetCode();
            Random random = new Random();
            var guid = Guid.NewGuid().ToString("N");
            resetCode.PasswordResetCode = guid.Substring(0, 8).ToUpper();
            resetCode.UserId = user.Data.UserId;
            resetCode.Expiration = DateTime.Now.AddMinutes(15);
            var mail = "Şifre sıfırlama kodunuz : " + resetCode.PasswordResetCode;
            _resetCodeDao.Add(resetCode);
            var client = Client();
            
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(_configuration.GetSection("SMTPInfo:Address").Value, "FinalProject");
                mm.To.Add(user.Data.Email);
                mm.IsBodyHtml = true;
                mm.Subject = "Password Reset";
                mm.Body = mail;
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                client.Send(mm);
            }
            catch (SmtpException e)
            {
                throw e;
            }
            return new SuccessResult();
        }

        public IResult VerifyResetCode(string email, string code)
        {
            string userId = "";
            var user = _userService.GetByEmail(email);
            if (user != null || user.Data != null)
            {
                userId = user.Data.UserId;
            }
            else
            {
                return new ErrorResult();
            }
            ResetCode resetCode = null;
            try
            {
                resetCode = _resetCodeDao.Get(x => x.UserId == userId && x.PasswordResetCode == code && x.Expiration > DateTime.Now && x.IsUsed == false);
            }
            catch
            {
                return new ErrorResult();
            }
            if (resetCode != null)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }

        }
        public SmtpClient Client()
        {
            var SMTPPort = int.Parse(_configuration.GetSection("SMTPInfo:Port").Value);
            var SMTPUsername = _configuration.GetSection("SMTPInfo:Username").Value;
            var SMTPPassword = _configuration.GetSection("SMTPInfo:Password").Value;
            var SMTPAddress = _configuration.GetSection("SMTPInfo:Address").Value;
            SmtpClient client = new SmtpClient();
            client.Port = SMTPPort;
            client.Host = SMTPAddress;
            client.EnableSsl = true;
            client.Timeout = 20000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network; 
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTPUsername, SMTPPassword); 
            return client;

        }

        public IResult ResetCode(ResetPasswordWithCodeDto resetpassworddto)
        {
            var isVerified = VerifyResetCode(resetpassworddto.Email,resetpassworddto.Code);
            var user = _userService.GetByEmail(resetpassworddto.Email);
            if(isVerified.Success){
                var result = _userService.ResetUserPassword(user.Data.UserId,resetpassworddto.NewPassword);
                if(result.Success){
                    var resetStr =_resetCodeDao.Get(x => x.UserId == user.Data.UserId && x.PasswordResetCode == resetpassworddto.Code && x.Expiration > DateTime.Now && x.IsUsed == false);
                    resetStr.IsUsed = true;
                    _resetCodeDao.Update(resetStr);
                }
                return result;
            }else{
                return new ErrorResult();
            }
        }
    }
}