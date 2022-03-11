using Dotsql.DTOs;

namespace Dotsql.Models;



public record User
{
    /// <summary>
    /// Primary Key - NOT NULL, Unique, Index is Available
    /// </summary>
    public long UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string MailId { get; set; }
    public long Mobile { get; set; }


    /// <summary>
    /// Can be NULL
    /// </summary>


    public UserDTO asDto => new UserDTO
    {
        UserId = UserId,
        UserName = UserName,
        Password = Password,
        Mobile = Mobile,
        MailId = MailId,


    };
}