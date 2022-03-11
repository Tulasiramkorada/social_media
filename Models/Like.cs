using Dotsql.DTOs;

namespace Dotsql.Models;



public record Like
{
    /// <summary>
    /// Primary Key - NOT NULL, Unique, Index is Available
    /// </summary>
    public long LikeId { get; set; }
    public long PostId { get; set; }
    public long UserId { get; set; }



    /// <summary>
    /// Can be NULL
    /// </summary>


    public Like asDto => new Like
    {
        LikeId = LikeId,
        PostId = PostId,
        UserId = UserId,



    };
}