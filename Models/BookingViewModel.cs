using ASolCarRental.Data.DTO;
using ASolCarRental.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ASolCarRental.Models
{
    public class BookingViewModel
    {
        public List<BookingDTO> Booking;

        public static async Task<int> BookCar(SqlConnection connection, BookingDTO booking)
        {
            RentalsRepository repo = new(connection);
            int BookingId = 0;


            await Task.Run(() =>
            {
                BookingId = repo.BookCar(booking);
            });

            return BookingId; //  0 if booking failed
        }

        public static async Task<int> SetCarAsUnAvailable(SqlConnection connection, int carId)
        {
            RentalsRepository repo = new(connection);
            int result = 0; // expecting 1 on success as 1 row should be affected

            await Task.Run(() =>
            {
                result = repo.SetCarAsUnAvailable(carId);
            });

            return result; //  0 if failed
        }

        public static async Task<BookingViewModel> GetBookingData(SqlConnection connection, string bookingId)
        {
            RentalsRepository repo = new(connection);
            BookingViewModel model = new();

            await Task.Run(() =>
            {
                model.Booking = repo.GetBookingData(bookingId).ToList();
            });

            return model; //  0 if failed
        }
    }
}
