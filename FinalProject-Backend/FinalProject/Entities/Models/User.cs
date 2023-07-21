using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;

namespace Entities.Models
{
  public class User : IEntity
  {
    public User()
    {
    }
    public User(string username,string firstName,string lastName,string email,byte[] passwordHash,byte[] passwordSalt,int roleId)
    {
      this.UserName = username;
      this.FirstName = firstName;
      this.Lastname = lastName;
      this.Email = email;
      this.PasswordHash = passwordHash;
      this.PasswordSalt = passwordSalt;
      this.Roleld = roleId;
      this.UserId = Guid.NewGuid().ToString("N");
    }
    [Key]
    public string UserId { get; set; }
    [MaxLength(50)]
    public string UserName { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string Lastname { get; set; }
    [MaxLength(50)]
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public int Roleld { get; set; }
    public virtual Role? Role {get;set;}

  }


}

