using ButtonGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ButtonGrid.Models
{
	public interface IUserRepository
	{
		public IEnumerable<User> GetAllUsers();
		public void InsertUser(User username);
		
		public void DeleteUser(User username);
		
		public void UpdateUser(User username);
		public User GetUser(int id);

	}
}
