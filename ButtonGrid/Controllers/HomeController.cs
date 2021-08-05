using ButtonGrid.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ButtonGrid.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUserRepository userRepository;

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
		{
			_logger = logger;
			this.userRepository = userRepository;
		}

		public IActionResult Index()
		{
			Users users = new Users();
			users.AllUsers = userRepository.GetAllUsers();
			return View(users);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult InsertUserToDatabase(User userToInsert)
		{

			userRepository.InsertUser(userToInsert);

			return RedirectToAction("Index","Button");
		}

		public IActionResult DeleteUser (User userToDelete)
		{
			throw new NotImplementedException();
		}
	}
}
