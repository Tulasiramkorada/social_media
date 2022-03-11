using Dotsql.DTOs;
using Dotsql.Models;
using Dotsql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _User;
    private readonly IPostRepository _post;
    public UserController(ILogger<UserController> logger, IUserRepository user, IPostRepository post)
    {
        _logger = logger;
        _User = user;
        _post = post;

    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var usersList = await _User.GetList();

        // User -> UserDTO
        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{user_id}")]
    public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] long user_id)
    {
        var user = await _User.GetById(user_id);

        if (user is null)
            return NotFound("No user found with given user id");

        var dto = user.asDto;
        dto.Post = await _post.GetAllForUser(user.UserId);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserCreateDTO Data)
    {

        var toCreateUser = new User
        {
            UserName = Data.UserName.Trim(),
            Password = Data.Password.Trim(),
            MailId = Data.MailId.Trim().ToLower(),
            Mobile = Data.Mobile,
        };

        var createdUser = await _User.Create(toCreateUser);

        return StatusCode(StatusCodes.Status201Created, createdUser.asDto);
    }

    [HttpPut("{user_id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] long user_id,
    [FromBody] UserUpdateDTO Data)
    {
        var existing = await _User.GetById(user_id);
        if (existing is null)
            return NotFound("No user found with given user_id");

        var toUpdateUser = existing with
        {
            Password = Data.Password?.Trim()?.ToLower() ?? existing.Password,
            MailId = Data.MailId?.Trim()?.ToLower() ?? existing.MailId,
            UserName = Data.LastName?.Trim() ?? existing.UserName,
            Mobile = Data.Mobile,

        };

        var didUpdate = await _User.Update(toUpdateUser);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

        return NoContent();
    }

    [HttpDelete("{user_id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] long user_id)
    {
        var existing = await _User.GetById(user_id);
        if (existing is null)
            return NotFound("No user found with given user id");

        var didDelete = await _User.Delete(user_id);

        return NoContent();
    }
}

