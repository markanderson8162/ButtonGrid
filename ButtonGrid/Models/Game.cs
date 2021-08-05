using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ButtonGrid.Models
{
	public class Game
	{
		public IEnumerable<ButtonModel> Buttons { get; set; }
		public Users GameUsers { get; set; }
		public int Id { get; set; }


	}
}
