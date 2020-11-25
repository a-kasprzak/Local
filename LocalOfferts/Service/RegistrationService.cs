using Dapper;
using LocalOfferts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace LocalOfferts.Service
{
    public class RegistrationService : IRegistrationService
    {

        private readonly SqlConnectionConfiguration _configuration;

        public RegistrationService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateRegistration(Registration registration)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", registration.Name, DbType.String);
            parameters.Add("ShopName", registration.Name, DbType.String);
            parameters.Add("City", registration.Name, DbType.String);
            parameters.Add("PhoneNumber", registration.Name, DbType.String);
            parameters.Add("CreationDate", registration.Name, DbType.DateTime);
            parameters.Add("Email", registration.Name, DbType.String);

            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                try
                {
                    string query = "INSERT INTO REGISTRATION(Name,ShopName,City,PhoneNumber,CreationDate,Email) VALUES (@Name,@ShopName,@City,@PhoneNumber,getdate(),@Email)";
                    await conn.ExecuteAsync(query, registration);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
            return true;
        }
        public async Task<IEnumerable<Registration>> GetRegistrationList()
        {
            IEnumerable<Registration> registrations;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                string query = "SELECT * FROM Registration";

                if (conn.State == ConnectionState.Closed) conn.Open();

                try
                {
                    registrations = await conn.QueryAsync<Registration>(query);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }

            return registrations;
        }

    }
}
