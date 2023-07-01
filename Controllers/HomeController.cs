using ASolCarRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASolCarRental.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace ASolCarRental.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<ApplicationOptions> _appOptions;


        public HomeController(ILogger<HomeController> logger,
                              IOptions<ApplicationOptions> appOptions) : base (appOptions)
        {
            _logger = logger;
            _appOptions = appOptions;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Booking()
        {
            PreBookingViewModel model;

            model = await PreBookingViewModel.LoadCars(Connection());
            return View(model);
        }
        public IActionResult BookingReturn()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
