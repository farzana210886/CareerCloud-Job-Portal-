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
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        private readonly string connectionString;
        public ApplicantResumeRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
               
                foreach (ApplicantResumePoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Applicant_Resumes (Id, Applicant, Resume, Last_Updated) " +
                                      "Values (@Id, @Applicant, @Resume, @Last_Updated)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Applicant", p.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", p.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", p.LastUpdated);
                   

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Applicant_Resumes", conn).ExecuteReader();

                var applicantResumes = new List<ApplicantResumePoco>();

                while (r.Read())
                {
                    applicantResumes.Add(new ApplicantResumePoco()
                    {
                        Id = (Guid)r["Id"],
                        Applicant = (Guid)r["Applicant"],
                        Resume = (string)r["Resume"],
                        LastUpdated = Convert.IsDBNull(r["Last_Updated"]) ? null : (DateTime)r["Last_Updated"]
                    }
                    );
                }
                return applicantResumes;
            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantResumePoco p in items)
                {
                    cmd.CommandText = @"delete from [dbo].[Applicant_Resumes]
                                                     where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantResumePoco p in items)
                {
                    cmd.CommandText = @"update Applicant_Resumes
                                       set [Applicant] = @Applicant
                                          ,[Resume] = @Resume
                                          ,[Last_Updated] = @LastUpdated
                                       where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Applicant", p.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", p.Resume);
                    cmd.Parameters.AddWithValue("@LastUpdated", p.LastUpdated);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
