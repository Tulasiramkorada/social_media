using Dotsql.Models;
using Dapper;
using Dotsql.Utilities;
using System.Collections.Generic;
using Dotsql.DTOs;

namespace Dotsql.Repositories;

public interface ILikeRepository
{
    Task<Like> Create(Like Item);

    Task<bool> Delete(long EmployeeNumber);
    Task<Like> GetById(long like_id);
    Task<List<Like>> GetList();

    Task<Like> GetById(int Id);
    Task<List<LikeDTO>>GetAllForPost(long PostId);
}
public class LikeRepository : BaseRepository, ILikeRepository
{
    public LikeRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Like> Create(Like Item)
    {
        var query = $@"INSERT INTO ""{TableNames.like}"" 
        (like_id, user_id, post_id) 
        VALUES (@LikeId, @UserId, @PostId) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Like>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long LikeId)
    {
        var query = $@"DELETE FROM ""{TableNames.like}"" 
        WHERE like_id = @LikeId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { LikeId });
            return res > 0;
        }
    }

    public async Task<List<LikeDTO>> GetAllForPost(long PostId)
    {
        var query = $@"SELECT * FROM ""{TableNames.like}"" WHERE post_id = @PostId";
        using(var con = NewConnection)
             return(await con.QueryAsync<LikeDTO>(query, new{PostId})).AsList();  
    }

    public async Task<Post> GetById(long LikeId)
    {
        var query = $@"SELECT * FROM {TableNames.like} 
        WHERE likeid = @LikeId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Post>(query, new { LikeId });

    }

    public Task<Like> GetById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Like>> GetList()
    {
        throw new NotImplementedException();
    }

    Task<Like> ILikeRepository.GetById(long like_id)
    {
        throw new NotImplementedException();
    }




    // Task<Post> ILikeRepository.Create(Like Item)
    // {
    //     throw new NotImplementedException();
    // }
}