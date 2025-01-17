﻿using learning_aspnetcore_mvc_users_and_logins.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace learning_aspnetcore_mvc_users_and_logins.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IActionResult Index()
    {
        return View();
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
}