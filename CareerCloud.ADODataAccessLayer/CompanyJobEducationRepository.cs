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
    public class CompanyJobEducationRepository : IDataRepository<CompanyJobEducationPoco>
    {
        private readonly string connectionString;
        public CompanyJobEducationRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
              
                foreach (CompanyJobEducationPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Company_Job_Educations(Id, Job, Major, Importance) " +
                                      "Values (@Id, @Job, @Major, @Importance)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Job", p.Job);
                    cmd.Parameters.AddWithValue("@Major", p.Major);
                    cmd.Parameters.AddWithValue("@Importance", p.Importance);
                   

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Company_Job_Educations", conn).ExecuteReader();

                var CompanyJobEducations = new List<CompanyJobEducationPoco>();

                while (r.Read())
                {
                    CompanyJobEducations.Add(new CompanyJobEducationPoco()
                    {
                        Id = (Guid)r["Id"],
                        Job = (Guid)r["Job"],
                        Major = (string)r["Major"],
                        Importance = (short)r["Importance"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                    }
                    );
                }
                return CompanyJobEducations;
            }
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Job_Educations]
                                                     WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyJobEducationPoco p in items)
                {
                    cmd.CommandText = @"update Company_Job_Educations
                                       set Job = @Job
                                          ,Major = @Major
                                          ,Importance = @Importance
                                        where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Job", p.Job);
                    cmd.Parameters.AddWithValue("@Major", p.Major);
                    cmd.Parameters.AddWithValue("@Importance", p.Importance);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
