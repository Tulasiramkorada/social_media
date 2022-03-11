using Dotsql.DTOs;
using Dotsql.Models;
using Dotsql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/like")]
public class LikeController : ControllerBase
{
    private readonly ILogger<LikeController> _logger;
    private readonly ILikeRepository _like;
    private readonly IPostRepository _post;
    public LikeController(ILogger<LikeController> logger, ILikeRepository like,IPostRepository post)
    {
        _logger = logger;
        _like = like;
        _post = post;

    }





    [HttpPost]
    public async Task<ActionResult<LikeDTO>> CreateLike([FromBody] LikeCreateDTO Data)
    {

        var toCreateLike = new Like
        {
            UserId = Data.UserId,
            PostId = Data.PostId

        };

        var createdLike = await _like.Create(toCreateLike);

        return StatusCode(StatusCodes.Status201Created, createdLike.asDto);
    }





    [HttpDelete("{like_id}")]
    public async Task<ActionResult> DeleteLike([FromRoute] long like_id)
    {
        var existing = await _like.GetById(like_id);
        if (existing is null)
            return NotFound("No user found with given like id");

        var didDelete = await _like.Delete(like_id);

        return NoContent();
    }
}


