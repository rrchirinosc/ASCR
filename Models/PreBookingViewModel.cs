﻿using ASolCarRental.Data.DTO;
using ASolCarRental.Infrastructure.Data;
using ASolCarRental.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ASolCarRental.Models
{
    public class PreBookingViewModel
    {
        public List<AvailableCarsDTO> AvailableCars;

        public static async Task<PreBookingViewModel> LoadCars(SqlConnection connection)
        {
            PreBookingViewModel model = new();
            RentalsRepository repo = new(connection);

            await Task.Run(() =>
            {
                model.AvailableCars = repo.GetAvailableCars().ToList();
            });

            return model;
        }

        public static async Task<PreBookingViewModel> LoadCarsByType(SqlConnection connection, int carType)
        {
            PreBookingViewModel model = new();
            RentalsRepository repo = new(connection);

            await Task.Run(() =>
            {
                model.AvailableCars = repo.GetAvailableCarsByType((short)carType).ToList();
            });

            return model;
        }

        public static async Task<PreBookingViewModel> LoadCarById(SqlConnection connection, int carId)
        {
            PreBookingViewModel model = new();
            RentalsRepository repo = new(connection);

            await Task.Run(() =>
            {
                model.AvailableCars = repo.LoadCarById((short)carId).ToList();
            });

            return model;
        }
    }
}
