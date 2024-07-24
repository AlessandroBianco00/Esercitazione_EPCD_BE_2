using HotelWebApp.Dto;
using HotelWebApp.Interfaces;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Data.SqlClient;

namespace HotelWebApp.Services.Dao
{
    public class CustomerDao : BaseDao, ICustomerDao
    {
        public CustomerDao(IConfiguration configuration) : base(configuration) { }

        private const string INSERT_COMMAND = "INSERT INTO Customers(FiscalCode, FirstName, LastName, City, Province, Email, PhoneNumber, MobilePhoneNumber) VALUES(@fiscalCode, @firstName, @lastName, @city, @province, @email, @phoneNumber, @mobilePhoneNumber)";
        private const string SELECT_ALL_COMMAND = "SELECT FiscalCode, FirstName, LastName, City, Province, Email, PhoneNumber, MobilePhoneNumber FROM Customers";
        private const string SELECT_BY_CF_COMMAND = "SELECT FiscalCode, FirstName, LastName, City, Province, Email, PhoneNumber, MobilePhoneNumber FROM Customers WHERE FiscalCode = @fiscalCode";
        private const string DELETE_COMMAND = "DELETE FROM Customers WHERE FiscalCode = @fiscalCode";
        public void CreateCustomer(CustomerDto customer)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(INSERT_COMMAND, conn);
                cmd.Parameters.Add(new SqlParameter("@fiscalCode", customer.FiscalCode));
                cmd.Parameters.Add(new SqlParameter("@firstName", customer.FirstName));
                cmd.Parameters.Add(new SqlParameter("@lastName", customer.LastName));
                cmd.Parameters.Add(new SqlParameter("@city", customer.City));
                cmd.Parameters.Add(new SqlParameter("@province", customer.Province));
                cmd.Parameters.Add(new SqlParameter("@email", customer.Email));
                cmd.Parameters.Add(new SqlParameter("@phoneNumber", customer.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@mobilePhoneNumber", customer.MobilePhoneNumber));
                var result = cmd.ExecuteNonQuery();
                if (result != 1) throw new Exception("Inserimento fallito");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nella creazione del cliente", ex);
            }
        }

        public List<CustomerDto> GetAllCustomers()
        {
            var list = new List<CustomerDto>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_ALL_COMMAND, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new CustomerDto
                    {
                        FiscalCode = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        City = reader.GetString(3),
                        Province = reader.GetString(4),
                        Email = reader.GetString(5),
                        PhoneNumber = reader.GetString(6),
                        MobilePhoneNumber = reader.GetString(7)
                    });
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero clienti", ex);
            }
        }

        public CustomerDto GetCustomerByCode(string fiscalCode)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BY_CF_COMMAND, conn);
                cmd.Parameters.AddWithValue("@fiscalCode", fiscalCode);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) return new CustomerDto
                {
                    FiscalCode = reader.GetString(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    City = reader.GetString(3),
                    Province = reader.GetString(4),
                    Email = reader.GetString(5),
                    PhoneNumber = reader.GetString(6),
                    MobilePhoneNumber = reader.GetString(7)
                };
                throw new Exception("Customer with id = {fiscalCode} not found");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero cliente", ex);
            }
        }

        public void DeleteCustomer(string fiscalCode)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(DELETE_COMMAND, conn);
                cmd.Parameters.AddWithValue("@fiscalCode", fiscalCode);
                var result = cmd.ExecuteNonQuery();
                if (result != 1) throw new Exception("Inserimento fallito");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nella cancellazione del cliente", ex);
            }
        }
    }
}
