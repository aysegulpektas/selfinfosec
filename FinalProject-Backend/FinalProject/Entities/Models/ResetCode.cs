using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;

namespace Entities.Models
{
    public class ResetCode:IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResetCodeId { get; set; }
        public string UserId { get; set; }
        public User? User {get;set;}
        public string PasswordResetCode { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsUsed { get; set; }
    }
}