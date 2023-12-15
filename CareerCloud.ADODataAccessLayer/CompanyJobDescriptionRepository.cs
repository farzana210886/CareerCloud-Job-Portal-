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
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        private readonly string connectionString;
        public CompanyJobDescriptionRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                foreach (CompanyJobDescriptionPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Company_Jobs_Descriptions(Id, Job, Job_Name, Job_Descriptions) " +
                                      "Values (@Id, @Job, @JobName, @JobDescriptions)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Job", p.Job);
                    cmd.Parameters.AddWithValue("@JobName", p.JobName);
                    cmd.Parameters.AddWithValue("@JobDescriptions", p.JobDescriptions);
                   
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Company_Jobs_Descriptions", conn).ExecuteReader();

                var companyJobDescriptions = new List<CompanyJobDescriptionPoco>();

                while (r.Read())
                {
                    companyJobDescriptions.Add(new CompanyJobDescriptionPoco()
                    {
                        Id = (Guid)r["Id"],
                        Job = (Guid)r["Job"],
                        JobName = r["Job_Name"] == DBNull.Value ? null : (string)r["Job_Name"],                      
                        JobDescriptions = r["Job_Descriptions"] == DBNull.Value ? null : (string)r["Job_Descriptions"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                    }
                    );
                }
                return companyJobDescriptions;
            }
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions]
                                                     WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyJobDescriptionPoco p in items)
                {
                    cmd.CommandText = @"update Company_Jobs_Descriptions
                                       set Job = @Job
                                          ,Job_Name = @JobName
                                          ,Job_Descriptions = @JobDescriptions
                                         where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Job", p.Job);
                    cmd.Parameters.AddWithValue("@JobName", p.JobName);
                    cmd.Parameters.AddWithValue("@JobDescriptions", p.JobDescriptions);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
