using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TaxCalculator.Infrastructure;
using TaxCalculator.Models;
using TaxCalculator.Repositories.Interfaces;

namespace TaxCalculator.Repositories
{
    public class TaxBandsRepository : ITaxBandsRepository
    {
        private readonly string _connString;

        public TaxBandsRepository(IOptions<DatabaseSettings> connString)
        {
            _connString = connString.Value.DBConnectionString;
        }

        /// <summary>
        /// Returns all tax bands for a particular scheme/group (could represent a country's tax code)
        /// </summary>
        public List<TaxBand> GetTaxBands(int groupId)
        {
            var results = new List<TaxBand>();

            using var connection = new SqlConnection(_connString);
            connection.Open();

            string sql = "SELECT lowerLimit, upperLimit, rate FROM TaxBands WHERE bandGroup = @group ORDER BY LowerLimit";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@group", groupId));
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int lower = reader.GetInt32(0);
                int? upper = reader.IsDBNull(1) ? null : reader.GetInt32(1);
                int rate = reader.GetInt32(2);

                results.Add(new TaxBand(lower, upper, rate));
            }

            return results;
        }
    }
}
