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
using ASolCarRental.Data.DTO;


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


        public IActionResult Booking()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCarList(int carType)
        {
            PreBookingViewModel model;

            model = await PreBookingViewModel.LoadCarsByType(connection, carType);
            return Json(model.AvailableCars);
        }

        public async Task<JsonResult> GetCarData(int carId)
        {
            PreBookingViewModel model;

            model = await PreBookingViewModel.LoadCarById(connection, carId);
            return Json(model.AvailableCars);
        }


        public async Task<JsonResult> GetBookingData(string bookingId)
        {
            BookingViewModel model;

            model = await BookingViewModel.GetBookingData(connection, bookingId);
            return Json(model.Booking);
        }

        [HttpPost]
        public async Task<int> BookCar(int CarId, int Mileage, DateTime BookingDate, short BookingTime, string CustomerId, string CarRegistration,
                                        short CarType)
        {
            BookingDTO booking = new();
            int BookingId = 0;

            booking.BookingId = 0;  // will be generated
            booking.CarId = CarId;
            booking.RentalDate = BookingDate.AddHours(BookingTime + 8);
            booking.CarMileage = Mileage;
            booking.CustomerId = CustomerId;
            booking.CarRegistration = CarRegistration;
            booking.CarType = CarType;

            BookingId = await BookingViewModel.BookCar(connection, booking);

            if(BookingId != 0) {
                // Mark booked car as unavailable
                //TODO: Handle possible error
                int result = await SetCarAsUnAvailable(CarId);
            }

            return BookingId;
        }


        private async Task<int> SetCarAsUnAvailable(int carId)
        {
            int result = await BookingViewModel.SetCarAsUnAvailable(connection, carId);
            return result;
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
