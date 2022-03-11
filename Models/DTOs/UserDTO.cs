using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dotsql.DTOs;

public record UserDTO
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("user_name")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("mail_id")]
    public string MailId { get; set; }

    [JsonPropertyName("contact_number")]
    public long Mobile{ get; set; }

    [JsonPropertyName("posts")]
    public List<PostDTO> Post { get; set;}



}

public record UserCreateDTO
{
    [JsonPropertyName("user_name")]
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    [MaxLength(50)]
    [Required]
    public string Password { get; set; }

    [JsonPropertyName("mail_id")]
    [MaxLength(255)]
    public string MailId { get; set; }

    [JsonPropertyName("contact_number")]
    [Required]
    public long Mobile { get; set; }

}

public record UserUpdateDTO
{
    [JsonPropertyName("user_name")]
    [MaxLength(50)]
    public string LastName { get; set; }

      [JsonPropertyName("password")]
    public string Password { get; set; } 


    [JsonPropertyName("contact_number")]
    public long Mobile { get; set; } 

   [JsonPropertyName("mail_id")]
    [MaxLength(255)]
    public string MailId { get; set; }
}