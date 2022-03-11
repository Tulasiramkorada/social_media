using Dotsql.Models;
using Dapper;
using Dotsql.Utilities;


namespace Dotsql.Repositories;

public interface IUserRepository
{
    Task<User> Create(User Item);
    Task<bool> Update(User Item);
    Task<bool> Delete(long UserId);
    Task<User> GetById(long UserId);
    Task<List<User>> GetList();



}
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<User> Create(User Item)
    {
        var query = $@"INSERT INTO public.""user""(user_name, password, mail_id, contact_number)
	VALUES (@UserName, @Password, @Email, @Mobile) 
        RETURNING *";

        //INSERT INTO public."user"(
        // user_id, user_name, password, mail_id, contact_number, post_id)
        // VALUES (?, ?, ?, ?, ?, ?);
        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<User>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long UserId)
    {
        var query = $@"DELETE FROM ""{TableNames.user}"" 
        WHERE user_id = @UserId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { UserId });
            return res > 0;
        }
    }

    public async Task<User> GetById(long UserId)
    {
        var query = $@"SELECT * FROM ""{TableNames.user}"" 
        WHERE user_id = @UserId";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<User>(query, new { UserId });
    }

    public async Task<List<User>> GetList()
    {
        // Query
        var query = $@"SELECT * FROM ""{TableNames.user}""";

        List<User> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<User>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
    }

    public async Task<bool> Update(User Item)
    {
        var query = $@"UPDATE ""{TableNames.user}"" SET user_name = @UserName,password = @Password,
         mail_id = @MailId, contact_number = @Mobile WHERE user_id = @UserId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }
}