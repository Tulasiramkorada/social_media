using Dotsql.Models;
using Dapper;
using Dotsql.Utilities;
using Dotsql.DTOs;

namespace Dotsql.Repositories;

public interface IPostRepository
{
    Task<Post> Create(Post Item);

    Task<bool> Delete(long EmployeeNumber);
    Task<Post> GetById(long EmployeeNumber);
    Task<List<Post>> GetList();
    Task<List<PostDTO>> GetAllForHash(long PostId);
    Task<List<PostDTO>> GetAllForUser(long UserId);



}
public class PostRepository : BaseRepository, IPostRepository
{
    public PostRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Post> Create(Post Item)
    {
        var query = $@"INSERT INTO public.""{TableNames.post}"" 
        (post_title, post_date, user_id,post_type) 
        VALUES (@PostTitle, @Postdate, @UserId, @Type) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Post>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long PostId)
    {
        var query = $@"DELETE FROM ""{TableNames.post}"" 
        WHERE post_id = @PostId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { PostId });
            return res > 0;
        }
    }

    public async Task<List<PostDTO>> GetAllForHash(long HashId)
    {
        var query = $@"SELECT * FROM {TableNames.hashpost} hp
        LEFT JOIN {TableNames.post} p ON p.post_id = hp.post_id
        WHERE hash_id = @HashId";

        
        using (var con = NewConnection)
           return (await con.QueryAsync<PostDTO>(query, new {HashId})).AsList();
    }

    public async Task<List<PostDTO>> GetAllForUser(long UserId)
    {
        var query = $@"SELECT * FROM ""{TableNames.post}""
        WHERE user_id = @UserId";

        using (var con = NewConnection)
            return (await con.QueryAsync<PostDTO>(query,new {UserId})).AsList();
    }

    public async Task<Post> GetById(long PostId)
    {
        var query = $@"SELECT * FROM ""{TableNames.post}"" 
        WHERE post_id = @PostId";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Post>(query, new { PostId });
    }

    public async Task<List<Post>> GetList()
    {
        // Query
        var query = $@"SELECT * FROM ""{TableNames.post}""";

        List<Post> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Post>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
    }

    // public async Task<bool> Update(Post Item)
    // {
    //     var query = $@"UPDATE ""{TableNames.user}"" SET post_title = @PostTitle, 
    //     postdate = @PostDate, 
    //     type = @Type,  WHERE post_id = @PostId";

    //     using (var con = NewConnection)
    //     {
    //         var rowCount = await con.ExecuteAsync(query, Item);
    //         return rowCount == 1;
    //     }
    // }
}