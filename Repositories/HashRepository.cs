using Dotsql.Models;
using Dapper;
using Dotsql.Utilities;
using Dotsql.DTOs;

namespace Dotsql.Repositories;

public interface IHashRepository
{
    Task<Hash> Create(Hash Item);
    Task<bool> Update(Hash Item);
    Task<bool> Delete(long EmployeeNumber);
    Task<Hash> GetById(long EmployeeNumber);




}
public class HashRepository : BaseRepository, IHashRepository
{
    public HashRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Hash> Create(Hash Item)
    {
        var query = $@"INSERT INTO ""{TableNames.hash}"" 
        (hash_id, hash_name, user_id,type) 
        VALUES (@HashName, @UserId) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Hash>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long HashId)
    {
        var query = $@"DELETE FROM ""{TableNames.hash}"" 
        WHERE hash_id = @HashId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { HashId });
            return res > 0;
        }
    }


    public async Task<List<HashDTO>> GetAllForPost(long PostId)
    {
        var query = $@"SELECT * FROM {TableNames.hash} 
        WHERE post_id = @PostId";

        using (var con = NewConnection)
            return (await con.QueryAsync<HashDTO>(query, new { PostId })).AsList();
    }


    public async Task<Hash> GetById(long HashId)
    {
        var query = $@"SELECT * FROM ""{TableNames.hash}"" 
        WHERE hash_id = @HashId";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Hash>(query, new { HashId });
    }



    public async Task<bool> Update(Hash Item)
    {
        var query = $@"UPDATE ""{TableNames.hash}"" SET hash_name = @HashName, 
          WHERE hash_id = @HashId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }
}