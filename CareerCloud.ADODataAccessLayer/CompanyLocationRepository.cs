using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        private readonly string connectionString;
        public CompanyLocationRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                foreach (CompanyLocationPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Company_Locations (Id, Company, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code) " +
                                      "Values (@Id, @Company, @CountryCode, @StateProvinceCode, @StreetAddress, @CityTown, @ZipPostalCode)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Company", p.Company);
                    cmd.Parameters.AddWithValue("@CountryCode", p.CountryCode);
                    cmd.Parameters.AddWithValue("@StateProvinceCode", p.Province);
                    cmd.Parameters.AddWithValue("@StreetAddress", p.Street);
                    cmd.Parameters.AddWithValue("@CityTown", p.City);
                    cmd.Parameters.AddWithValue("@ZipPostalCode", p.PostalCode);
              
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Company_Locations", conn).ExecuteReader();

                var companyLocations = new List<CompanyLocationPoco>();

                while (r.Read())
                {
                    companyLocations.Add(new CompanyLocationPoco()
                    {
                        Id = (Guid)r["Id"],
                        Company = (Guid)r["Company"],
                        CountryCode = (string)r["Country_Code"],
                        Province = Convert.IsDBNull(r["State_Province_Code"]) ? null : (string)r["State_Province_Code"],
                        Street = Convert.IsDBNull(r["Street_Address"]) ? null : (string)r["Street_Address"],
                        City = Convert.IsDBNull(r["City_Town"]) ? null : (string)r["City_Town"],
                        PostalCode = Convert.IsDBNull(r["Zip_Postal_Code"]) ? null : (string)r["Zip_Postal_Code"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                    }
                    );
                }
                return companyLocations;
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyLocationPoco p in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Locations]
                                                     WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyLocationPoco p in items)
                {
                    cmd.CommandText = @"update Company_Locations
                                       set Company = @Company
                                          ,Country_Code = @CountryCode
                                          ,State_Province_Code = @StateProvinceCode
                                          ,Street_Address = @StreetAddress
                                          ,City_Town = @CityTown
                                          ,Zip_Postal_Code = @ZipPostalCode
                                        where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Company", p.Company);
                    cmd.Parameters.AddWithValue("@CountryCode", p.CountryCode);
                    cmd.Parameters.AddWithValue("@StateProvinceCode", p.Province);
                    cmd.Parameters.AddWithValue("@StreetAddress", p.Street);
                    cmd.Parameters.AddWithValue("@CityTown", p.City);
                    cmd.Parameters.AddWithValue("@ZipPostalCode", p.PostalCode);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
