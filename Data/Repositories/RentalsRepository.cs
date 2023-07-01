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

    }
}
