using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dotsql.DTOs;

public record HashDTO
{
    [JsonPropertyName("hash-id")]
    public long HashId { get; set; }

    [JsonPropertyName("hash_name")]
    public string HashName { get; set; }

    [JsonPropertyName("post_id")]
    public long PostId { get; set; }

    [JsonPropertyName("post")]

    public List<PostDTO> Post { get; internal set; }

}

public record HashCreateDTO
{
    [JsonPropertyName("hash-id")]
    public long HashId { get; set; }

    [JsonPropertyName("hash_name")]
    public string HashName { get; set; }

    [JsonPropertyName("post_id")]
    public long PostId { get; set; }

     [JsonPropertyName("posts")]
    public List<PostDTO>Post { get; internal set; }





}

public record HashUpdateDTO
{


    [JsonPropertyName("hash_name")]
    public string HashName { get; set; }


}