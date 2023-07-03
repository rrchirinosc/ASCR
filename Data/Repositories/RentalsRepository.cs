using ASolCarRental.Data.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ASolCarRental.Repositories
{
    public class RentalsRepository
    {

        private SqlConnection _connection;

        public RentalsRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<AvailableCarsDTO> GetAvailableCars()
        {
            string sql = string.Format("SELECT [Cars].Id, [Cars].Registration, [Cars].Type, [Cars].Description" +
                                        " FROM [Cars]" +
                                        " WHERE [Cars].Available = 1");

            return _connection.Query<AvailableCarsDTO>(sql);           
        }

        public IEnumerable<AvailableCarsDTO> GetAvailableCarsByType(short carType)
        {
            string sql = string.Format("SELECT [Cars].Id, [Cars].Registration, [Cars].Type, [Cars].Description" +
                                        " FROM [Cars]" +
                                        " WHERE [Cars].Available = 1 AND [Cars].type = {0}", carType);

            return _connection.Query<AvailableCarsDTO>(sql);
        }

        public IEnumerable<AvailableCarsDTO> LoadCarById(int carId)
        {
            string sql = string.Format("SELECT [Cars].Id, [Cars].Registration, [Cars].Mileage, [Cars].Description, [Cars].Type" +
                                        " FROM [Cars]" +
                                        " WHERE [Cars].Available = 1 AND [Cars].Id = {0}", carId);

            return _connection.Query<AvailableCarsDTO>(sql);
        }


        public int BookCar(BookingDTO Booking)
        {
            string sql = "INSERT INTO [Rentals] (CarId, RentalDate, CustomerId, CarMileage, CarType, CarRegistration)" +
                    " Output Inserted.BookingId" +
                    " Values (@CarId, @RentalDate, @CustomerId, @CarMileage, @CarType, @CarRegistration)";

            var obj = _connection.QuerySingle(sql, new { Booking.CarId, Booking.RentalDate, Booking.CustomerId, Booking.CarMileage, Booking.CarType, Booking.CarRegistration});

            return obj != null ? obj.BookingId : 0;
        }

        public int SetCarAsUnAvailable(int carId){
            string sql = "UPDATE [Cars] SET [Available] = @state" +
                        " WHERE[Id] = @id";

            return _connection.Execute(sql, new
            {
                state = false,
                id = carId
            });
        }


        public IEnumerable<BookingDTO> GetBookingData(string bookingId)
        {
            string sql = string.Format("SELECT *" +
                                        " FROM [Rentals]" +
                                        " WHERE [Rentals].BookingId = {0}", bookingId);

            return _connection.Query<BookingDTO>(sql);
        }
    }
}
