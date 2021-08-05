using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ButtonGrid.Models
{
	public class UserRepository : IUserRepository
	{
        private readonly IDbConnection _conn;

        public UserRepository(IDbConnection conn)
        {
            _conn = conn;
        }
		public IEnumerable<User> GetAllUsers()
        {
            return _conn.Query<User>("SELECT * FROM username Order by Id desc;");
        }

		public void DeleteUser(User username)
		{
			_conn.Execute("DELETE FROM username WHERE id = @Id;",
									   new { id = username.Id });

		}

		public User GetUser(int id)
		{
			return (User)_conn.QuerySingle<User>("SELECT * FROM username WHERE id = @Id",
			new { Id = id });
		}

		public void InsertUser(User user)
		{
			_conn.Execute("INSERT INTO username (username, score ) VALUES (@username, @score);", new { username = user.UserName, score = user.Score });
		}

		public void UpdateUser(User username)
		{
			_conn.Execute("UPDATE username SET Name = @username, Score = @score WHERE id = @id",
			new { username = username.UserName, score = username.Score, id = username.Id });
		}
	}
}
