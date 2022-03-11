using Dotsql.DTOs;
using Dotsql.Models;
using Dotsql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/hash")]
public class HashController : ControllerBase
{
    private readonly ILogger<HashController> _logger;
    private readonly IHashRepository _hash;
 private readonly IPostRepository _post;
    public HashController(ILogger<HashController> logger, IHashRepository hash,IPostRepository post)
    {
        _logger = logger;
        _hash = hash;
        _post = post;
    }

    // [HttpGet]
    // public async Task<ActionResult<List<HashDTO>>> GetAllHash()
    // {
    //     var hashList = await _hash.GetList();

    //     // User -> UserDTO
    //     var dtoList = hashList.Select(x => x.asDto);

    //     return Ok(dtoHash);
    // }

    [HttpGet("{hash_id}")]
    public async Task<ActionResult<HashDTO>> GetHashById([FromRoute] long hash_id)
    {
        var hash = await _hash.GetById(hash_id);

        if (hash is null)
            return NotFound("No hash found with given  id");


            var dto = hash.asDto;
            dto.Post = await _post.GetAllForHash(hash.HashId);

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<HashDTO>> CreateHash([FromBody] HashCreateDTO Data)
    {

        var toCreateHash = new Hash
        {
            PostId = Data.PostId,
            HashName = Data.HashName.Trim(),


        };

        var createdHash = await _hash.Create(toCreateHash);

        return StatusCode(StatusCodes.Status201Created, createdHash.asDto);
    }

    [HttpPut("{hash_id}")]
    public async Task<ActionResult> UpdateHash([FromRoute] long hash_id,
    [FromBody] HashUpdateDTO Data)
    {
        var existing = await _hash.GetById(hash_id);
        if (existing is null)
            return NotFound("No user found with given hash_id");

        var toUpdateLike = existing with
        {
            //HashName = Data.HashName?.Trim()?.ToLower() ?? existing.Name,
            HashName = Data.HashName?.Trim()?.ToLower() ?? existing.HashName,



        };

        var didUpdate = await _hash.Update(toUpdateLike);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update hash");

        return NoContent();
    }

    [HttpDelete("{hash_id}")]
    public async Task<ActionResult> DeleteHash([FromRoute] long hash_id)
    {
        var existing = await _hash.GetById(hash_id);
        if (existing is null)
            return NotFound("No user found with given hash id");

        var didDelete = await _hash.Delete(hash_id);

        return NoContent();
    }
}

