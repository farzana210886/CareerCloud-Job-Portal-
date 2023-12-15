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
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        private readonly string connectionString;
        public CompanyDescriptionRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
               
                foreach (CompanyDescriptionPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Company_Descriptions(Id, Company, Company_Name, Company_Description, LanguageId) " +
                                      "Values (@Id, @Company, @CompanyName, @CompanyDescription, @LanguageId)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Company", p.Company);
                    cmd.Parameters.AddWithValue("@CompanyName", p.CompanyName);
                    cmd.Parameters.AddWithValue("@CompanyDescription", p.CompanyDescription);
                    cmd.Parameters.AddWithValue("@LanguageId", p.LanguageId);
                                 
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Company_Descriptions", conn).ExecuteReader();

                var companyDescriptions = new List<CompanyDescriptionPoco>();

                while (r.Read())
                {
                    companyDescriptions.Add(new CompanyDescriptionPoco()
                    {
                        Id = (Guid)r["Id"],
                        Company = (Guid)r["Company"],
                        CompanyName = r["Company_Name"] == DBNull.Value ? null : (string)r["Company_Name"],
                        CompanyDescription = (string)r["Company_Description"],
                        LanguageId = (string)r["LanguageId"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                    }
                    );
                } 
                return companyDescriptions;
            }
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Descriptions]
                                                     WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyDescriptionPoco p in items)
                {
                    cmd.CommandText = @"update Company_Descriptions
                                       set Company = @Company
                                          ,LanguageID = @LanguageID
                                          ,Company_Name = @CompanyName
                                          ,Company_Description = @CompanyDescription
                                      where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Company", p.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", p.LanguageId);
                    cmd.Parameters.AddWithValue("@CompanyName", p.CompanyName);
                    cmd.Parameters.AddWithValue("@CompanyDescription", p.CompanyDescription);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
