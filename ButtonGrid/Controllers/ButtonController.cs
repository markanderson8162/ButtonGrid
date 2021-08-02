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
		

		public IActionResult Index()
		{
			if (buttons.Count < GRID_SIZE)
			{
				for (int i = 0; i < GRID_SIZE; i++)
				{
					buttons.Add(new ButtonModel { Id = i, ButtonState = random.Next(2) });

				}
			}

			return View("Index", buttons);
		}

		public IActionResult HandleButtonClick(string buttonNumber)
		{
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

			foreach(var item in buttons)
			{
				
			}	

			return View("index", buttons);
		}


		public IActionResult InsertUserToDatabase(User userToInsert)
		{

			userRepository.InsertUser(userToInsert);

			return RedirectToAction("Index");
		}
	}
}
