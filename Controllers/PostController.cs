using Dotsql.DTOs;
using Dotsql.Models;
using Dotsql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostRepository _post;

    private readonly IUserRepository _user;
    private readonly ILikeRepository _like;
    public PostController(ILogger<PostController> logger, IPostRepository post,IUserRepository user,ILikeRepository like)
    {
        _logger = logger;
        _post = post;
        _user = user;
        _like = like;
    }

    [HttpGet]
    public async Task<ActionResult<List<PostDTO>>> GetAllPost()
    {
        var postList = await _post.GetList();

        var dtoList = postList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{post_id}")]
    public async Task<ActionResult<PostDTO>> GetPostById([FromRoute] long post_id)
    {
        var post = await _post.GetById(post_id);

        if (post is null)
            return NotFound("No user found with given  id");

            var dto = post.asDto;
            dto.Like = await _like.GetAllForPost(post.PostId);

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<PostDTO>> CreatePost([FromBody] PostCreateDTO Data)
    {

        var toCreatePost = new Post
        {
            PostTitle = Data.PostTitle.Trim(),
            PostDate = Data.PostDate.UtcDateTime,
            UserId = Data.UserId,
            PostType = Data.Type.Trim(),

        };

        var createdPost = await _post.Create(toCreatePost);

        return StatusCode(StatusCodes.Status201Created, createdPost.asDto);
    }

    // // [HttpPut("{post_id}")]
    // // public async Task<ActionResult> UpdatePost([FromRoute] long post_id,
    // // [FromBody] PostUpdateDTO Data)
    // // {
    // //     var existing = await _post.GetById(post_id);
    // //     if (existing is null)
    // //         return NotFound("No user found with given post_id");

    // //     var toUpdatePost = existing with
    // //     {
    // //         PostTitle = Data.PostTitle?.Trim()?.ToLower() ?? existing.PostTitle,
    // //         PostDate = Data.Date ?? existing.PostDate,
    // //         UserId = Data.UserId ?? existing.UserId,
    // //         Type = Data.Type?.Trim() ?? existing.Type,


    // //     };

    //     var didUpdate = await _post.Update(toUpdatePost);

    //     if (!didUpdate)
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Could not update post");

    //     return NoContent();
    // }

    [HttpDelete("{post_id}")]
    public async Task<ActionResult> DeletePost([FromRoute] long post_id)
    {
        var existing = await _post.GetById(post_id);
        if (existing is null)
            return NotFound("No user found with given post id");

        var didDelete = await _post.Delete(post_id);

        return NoContent();
    }
}

