using ButtonGrid.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ButtonGrid.Controllers
{
	public class ButtonController : Controller
	{
		private readonly IUserRepository userRepository;

		static List<ButtonModel> buttons = new List<ButtonModel>();
		Random random = new Random();
		const int GRID_SIZE = 25;
		static int score = 1;

		public ButtonController(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}
		

		public IActionResult Index(string sortOrder)
		{

			var user = new User();
			var game = new Game();
			game.GameUsers = new Users();
			game.GameUsers.AllUsers = userRepository.GetAllUsers();

			if (buttons.Count < GRID_SIZE)
			{
				for (int i = 0; i < GRID_SIZE; i++)
				{
					buttons.Add(new ButtonModel { Id = i, ButtonState = random.Next(2) });

				}
			}

			//game.Id = user.Id;

			//var userId = from a in game.GameUsers.AllUsers select a;
			//ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
			//switch (sortOrder)
			//{
			//	case "id_desc":
			//		game.GameUsers.AllUsers.OrderByDescending(a => a.Id);
			//		break;

			//	default:
			//		break;
			//}
			


			game.Buttons = buttons;
			
			return View("Index", game);
		}





		public IActionResult HandleButtonClick(string buttonNumber)
		{
			var game = new Game();
			game.GameUsers = new Users();
			game.GameUsers.AllUsers = userRepository.GetAllUsers();
			ButtonModel.ButtonScore = score;
			//int score = buttonScore;
			int bn = int.Parse(buttonNumber);
			buttons.ElementAt(bn).ButtonState = (buttons.ElementAt(bn).ButtonState + 1) % 2;
			ButtonModel btnState = new ButtonModel();
			
			score++;

			
			if (bn + 1< buttons.Count)
			{
				if(bn %5 != 4)
				{
					if(buttons[bn + 1].ButtonState == 0)
					{
						buttons[bn + 1].ButtonState = 1;
						
					}
					else
					{
						buttons[bn + 1].ButtonState = 0;
						
					}
				}
			}

			if (bn - 1 < buttons.Count)
			{
				if (bn % 5 != 0)
				{
					if (buttons[bn - 1].ButtonState == 0)
					{
						buttons[bn - 1].ButtonState = 1;
						
					}
					else
					{
						buttons[bn - 1].ButtonState = 0;
						
					}
				}
			}

			if(bn + 5 < buttons.Count)
			{
				if(buttons[bn + 5].ButtonState == 0)
				{
					buttons[bn + 5].ButtonState = 1;
					
				}
				else
				{
					buttons[bn + 5].ButtonState = 0;
					
				}
			}

			if (bn - 5 >= 0)
			{
				if (buttons[bn - 5].ButtonState == 0)
				{
					buttons[bn - 5].ButtonState = 1;
					
				}
				else
				{
					buttons[bn - 5].ButtonState = 0;
					
				}
			}


			//score = game.GameUsers.NewUser.Score;
			game.Buttons = buttons;

			return View("index", game);
		}


		public IActionResult InsertUserToDatabase(User userToInsert)
		{
			userToInsert.Score = score - 1;

			userRepository.InsertUser(userToInsert);

			return RedirectToAction("Index");
		}

		public IActionResult ResetScore()
		{
			ButtonModel.ButtonScore = 0;
			score = ButtonModel.ButtonScore +1;


			buttons.Clear();
			for (int i = 0; i < GRID_SIZE; i++)
			{
				buttons.Add(new ButtonModel { Id = i, ButtonState = random.Next(2) });

			}

			return RedirectToAction("Index");
		}
	}
}
