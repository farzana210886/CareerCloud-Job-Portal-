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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        private readonly string connectionString;
        public CompanyProfileRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                foreach (CompanyProfilePoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Company_Profiles(Id, Registration_Date, Company_Website, Contact_Phone, Contact_Name, Company_Logo) " +
                                      "Values (@Id, @RegistrationDate, @CompanyWebsite, @ContactPhone, @ContactName, @CompanyLogo)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@RegistrationDate", p.RegistrationDate);
                    cmd.Parameters.AddWithValue("@CompanyWebsite", p.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@ContactPhone", p.ContactPhone);
                    cmd.Parameters.AddWithValue("@ContactName", p.ContactName);
                    cmd.Parameters.AddWithValue("@CompanyLogo", p.CompanyLogo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Company_Profiles", conn).ExecuteReader();

                var companyProfiles = new List<CompanyProfilePoco>();

                while (r.Read())
                {
                    companyProfiles.Add(new CompanyProfilePoco()
                    {
                        Id = (Guid)r["Id"],
                        RegistrationDate = (DateTime)r["Registration_Date"],
                        CompanyWebsite = r.IsDBNull(r.GetOrdinal("Company_Website")) ? (string?)null : (string)r["Company_Website"],
                        //CompanyWebsite = (string)r["Company_Website"],
                        ContactPhone = (string)r["Contact_Phone"],
                        //ContactName = (string)r["Contact_Name"],
                        //ContactName = Convert.IsDBNull(r["ContactName"]) ? null : (string?)r["ContactName"],
                        ContactName = r.IsDBNull(r.GetOrdinal("Contact_Name")) ? (string?)null : (string)r["Contact_Name"],
                        //CompanyLogo = (byte[])r["Company_Logo"],
                        //CompanyLogo = Convert.IsDBNull(r["Company_Logo"]) ? null : (byte[]?)r["Company_Logo"],
                        CompanyLogo = r.IsDBNull(r.GetOrdinal("Company_Logo")) ? (byte[]?)null : (byte[])r["Company_Logo"],
                        TimeStamp = (byte[]?)r["Time_Stamp"]
                    }
                    );
                }
                return companyProfiles;
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles]
                                                     WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyProfilePoco p in items)
                {
                    cmd.CommandText = @"update Company_Profiles
                                       SET Registration_Date = @RegistrationDate
                                          ,Company_Website = @CompanyWebsite
                                          ,Contact_Phone = @ContactPhone
                                          ,Contact_Name = @ContactName
                                          ,Company_Logo = @CompanyLogo
                                       where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@RegistrationDate", p.RegistrationDate);
                    cmd.Parameters.AddWithValue("@CompanyWebsite", p.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@ContactPhone", p.ContactPhone);
                    cmd.Parameters.AddWithValue("@ContactName", p.ContactName);
                    cmd.Parameters.AddWithValue("@CompanyLogo", p.CompanyLogo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
