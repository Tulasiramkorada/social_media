using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dotsql.DTOs;

public record PostDTO
{
    [JsonPropertyName("post_id")]
    public long PostId { get; set; }

    [JsonPropertyName("post_title")]
    public string PostTitle { get; set; }

    [JsonPropertyName("post_date")]
    public DateTimeOffset PostDate { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("post_type")]
    public string Type { get; set; }
    public List<LikeDTO> Like { get; internal set;}




}

public record PostCreateDTO
{
    [JsonPropertyName("post_title")]
    [Required]
    [MaxLength(50)]
    public string PostTitle { get; set; }

    [JsonPropertyName("post_date")]
    [Required]
    public DateTimeOffset PostDate { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("post_type")]
    [MaxLength(255)]
    public string Type { get; set; }




}

// public record PostUpdateDTO
// {
//     [JsonPropertyName("post_title")]
//     [MaxLength(50)]
//     public string PostTitle { get; set; }

//     [JsonPropertyName("post_date")]
//     public DateTimeOffset PostDate { get; set; }

//     [JsonPropertyName("user_id")]
//     public long UserId { get; set; }

//     [JsonPropertyName("post_type")]
//     [MaxLength(255)]
//     public string Type { get; set; }
// }