using Dotsql.DTOs;

namespace Dotsql.Models;



public record Hash
{
    /// <summary>
    /// Primary Key - NOT NULL, Unique, Index is Available
    /// </summary>
    public long HashId { get; set; }
    public string HashName { get; set; }

    public long PostId { get; set; }



    /// <summary>
    /// Can be NULL
    /// </summary>


    public HashDTO asDto => new HashDTO
    {
        HashId = HashId,
        HashName = HashName,

        PostId = PostId,



    };
}