using Dapper;
using SEDC.G2.DataDrive.Workshop.Data.Interfaces;
using SEDC.G2.DataDrive.Workshop.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SEDC.G2.DataDrive.Workshop.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private string _connectionString;
        
        public CountryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddCountry(Country country)
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Country ([Name]) VALUES (@Name)";
                try
                {
                    var affected = conn.Execute(sql, new { @Name = country.Name });
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteCountry(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE FROM Country WHERE CountryID = @CountryId";
                try
                {
                    var affected = conn.Execute(sql, new { @CountryId = id });
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool EditCountry(Country country)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Country SET [Name] = @Name WHERE CountryId = @CountryId";
                try
                {
                    var affected = conn.Execute(sql, new { @Name = country.Name, @CountryId = country.CountryId });
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Country> GetAllCountries()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var countryDictionary = new Dictionary<int, Country>();
                var sql = @"SELECT * FROM Country AS c INNER JOIN Manifacturer AS m ON c.CountryId = m.CountryId";
                return conn.Query<Country, Manifacturer, Country>(sql,
                        (country, manufacturer) =>
                        {
                            Country countryEntry;

                            if (!countryDictionary.TryGetValue(country.CountryId, out countryEntry))
                            {
                                countryEntry = country;
                                countryEntry.Manifacturers = new List<Manifacturer>();
                                countryDictionary.Add(countryEntry.CountryId, countryEntry);
                            }

                            countryEntry.Manifacturers.Add(manufacturer);
                            return countryEntry;
                        },
                        splitOn: "CountryId").Distinct().ToList();
            }
        }

        public Country GetCountryById(int countryId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT TOP 1 * FROM Country WHERE CountryId = @CountryId";
                try
                {
                    return conn.QueryFirstOrDefault(sql, new { @CountryId = countryId });
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }
    }
}
