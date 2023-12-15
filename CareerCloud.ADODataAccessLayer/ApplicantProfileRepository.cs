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
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private readonly string connectionString;
        public ApplicantProfileRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                   
                foreach (ApplicantProfilePoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Applicant_Profiles(Id, Login, Current_Salary, Current_Rate, Country_Code, State_Province_Code, Street_Address, City_Town,Zip_Postal_Code,Currency) " +
                                      "Values (@Id, @Login, @CurrentSalary, @CurrentRate, @CountryCode, @StateProvinceCode, @StreetAddress, @CityTown, @ZipPostalCode, @Currency)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Login", p.Login);
                    cmd.Parameters.AddWithValue("@CurrentSalary", p.CurrentSalary);
                    cmd.Parameters.AddWithValue("@CurrentRate", p.CurrentRate);
                    cmd.Parameters.AddWithValue("@CountryCode", p.Country);
                    cmd.Parameters.AddWithValue("@StateProvinceCode", p.Province);
                    cmd.Parameters.AddWithValue("@StreetAddress", p.Street);
                    cmd.Parameters.AddWithValue("@CityTown", p.City);
                    cmd.Parameters.AddWithValue("@ZipPostalCode", p.PostalCode);
                    cmd.Parameters.AddWithValue("@Currency", p.Currency);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Applicant_Profiles", conn).ExecuteReader();

                var applicantProfiles = new List<ApplicantProfilePoco>();

                while (r.Read())
                {
                    applicantProfiles.Add(new ApplicantProfilePoco()
                    {
                        Id = (Guid)r["Id"],
                        Login = (Guid)r["Login"],
                        CurrentSalary = (Decimal)r["Current_Salary"],
                        CurrentRate = (Decimal)r["Current_Rate"],
                        Country = (string)r["Country_Code"],
                        Province = (string)r["State_Province_Code"],
                        Street = (string)r["Street_Address"],
                        City = (string)r["City_Town"],
                        PostalCode = (string)r["Zip_Postal_Code"],
                        Currency = (string)r["Currency"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                    }
                    );
                }
                return applicantProfiles;
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantProfilePoco p in items)
                {
                    cmd.CommandText = @"delete from [dbo].[Applicant_Profiles] where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantProfilePoco p in items)
                {
                    cmd.CommandText = "update [dbo].[Applicant_Profiles]" +
                                       "set [Login] = @Login," +
                                          "[Current_Salary] = @CurrentSalary," +
                                          "[Current_Rate] = @CurrentRate," +
                                          "[Currency] = @Currency," +
                                          "[Country_Code] = @CountryCode," +
                                          "[State_Province_Code] = @StateProvinceCode," +
                                          "[Street_Address] = @StreetAddress," +
                                          "[City_Town] = @CityTown," +
                                          "[Zip_Postal_Code] = @ZipPostalCode " +
                                          "where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Login", p.Login);
                    cmd.Parameters.AddWithValue("@CurrentSalary", p.CurrentSalary);
                    cmd.Parameters.AddWithValue("@CurrentRate", p.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", p.Currency);
                    cmd.Parameters.AddWithValue("@CountryCode", p.Country);
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
