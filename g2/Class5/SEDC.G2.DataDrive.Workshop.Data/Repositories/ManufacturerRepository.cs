using Dapper;
using SEDC.G2.DataDrive.Workshop.Data.Interfaces;
using SEDC.G2.DataDrive.Workshop.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SEDC.G2.DataDrive.Workshop.Data.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private string _connectionString;

        public ManufacturerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool AddManufacturer(Manifacturer manufacturer)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Manifacturer ([Name], Description, CountryId) VALUES (@Name, @Description, @CountryId)";
                try
                {
                    var affected = conn.Execute(sql, new { @Name = manufacturer.Name, @Description = manufacturer.Description, @CountryId = manufacturer.CountryId });
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteManufacturer(int manufacturarId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE FROM MAnifacturer WHERE ManifacturerId = @ManufacturerId";
                try
                {
                    var affected = conn.Execute(sql, new { @ManufacturerId = manufacturarId });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool EditManufacturer(Manifacturer manufacturer)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Manifacturer SET [Name] = @Name, Description = @Description, CountryId = @CountryId WHERE ManifacturerId = @ManifacturerId";
                try
                {
                    var affected = conn.Execute(sql, new { @Name = manufacturer.Name, @Description = manufacturer.Description, @CountryId = manufacturer.CountryId, @ManifacturerId = manufacturer.ManifacturerId });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Manifacturer> GetAllManufacturers()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Manifacturer AS m INNER JOIN Country AS c ON m.CountryId = c.CountryId";
                try
                {
                    return conn.Query<Manifacturer, Country, Manifacturer>(sql,
                        (manufacturer, country) =>
                        {
                            manufacturer.Country = country;
                            return manufacturer;
                        },
                        splitOn: "CountryId").Distinct().ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public Manifacturer GetManufacturerById(int manufacturerId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT TOP 1 * FROM Manifacturer";
                try
                {
                    return conn.QueryFirstOrDefault<Manifacturer>(sql);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
