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

            if (model.Booking.Count == 0)
                return null;

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
            booking.Concluded = false;

            BookingId = await BookingViewModel.BookCar(connection, booking);

            if(BookingId != 0) {
                // Mark booked car as unavailable
                //TODO: Handle possible error
                int result = await SetCarAvailability(CarId, false);
            }

            return BookingId;
        }


        
        public async Task<float> CalculateBookingCost(string BookingId, int Mileage)
        {
            BookingViewModel model, model2;
            short carType = 0;
            float totalPrice = 0;
            int days = 0;
            int mileage = 0;

            //TODO: refactor should retrieve DTOs directly instead
            model = await BookingViewModel.GetBookingData(connection, BookingId);

            carType = model.Booking[0].CarType;
            model2 = await BookingViewModel.GetPricingData(connection, carType);

            // get days and mileage
            //!!! For testing purposes the return date is moved ahead 7 days to have valid booking data
            DateTime returnDate = DateTime.Now.AddDays(7);
            days = returnDate.Subtract(model.Booking[0].RentalDate).Days;
            mileage = Mileage - model.Booking[0].CarMileage;

            if (days < 0 || mileage < 0)
                return -1;

            // calculate price
            totalPrice = model2.Pricing[0].BaseDailyPrice * days * model2.Pricing[0].DayMultiplier + model2.Pricing[0].BaseMileagePrice * model2.Pricing[0].MileageMultiplier * mileage;

            return (float)Math.Round(totalPrice, MidpointRounding.ToEven);
        }


        private async Task<int> SetCarAvailability(int carId, bool available)
        {
            int result = await BookingViewModel.SetCarAvailability(connection, carId, available);
            return result;
        }

        public async Task<int> FinishBooking(string bookingId) {

            BookingViewModel model;
            model = await BookingViewModel.GetBookingData(connection, bookingId);
            await BookingViewModel.SetCarAvailability(connection, model.Booking[0].BookingId, true);

            int result = await BookingViewModel.MarkBookingAsCompleted(connection, bookingId);
           
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
