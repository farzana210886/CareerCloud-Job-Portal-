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
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string connectionString;
        public ApplicantJobApplicationRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                foreach (ApplicantJobApplicationPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Applicant_Job_Applications (Id, Applicant, Job, Application_Date) " +
                                      "Values (@Id, @Applicant, @Job, @Application_Date)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Applicant", p.Applicant);
                    cmd.Parameters.AddWithValue("@Job", p.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", p.ApplicationDate);
                 
                  
                   conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Applicant_Job_Applications", conn).ExecuteReader();

                var applicantJobEducations = new List<ApplicantJobApplicationPoco>();

                while (r.Read())
                {
                    applicantJobEducations.Add(new ApplicantJobApplicationPoco()
                    {
                        Id = (Guid)r["Id"],
                        Applicant = (Guid)r["Applicant"],
                        Job = (Guid)r["Job"],
                        ApplicationDate = (DateTime)r["Application_Date"],
                        TimeStamp = (byte[])r["Time_Stamp"],                       
                    }
                    );
                }
                return applicantJobEducations;
            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantJobApplicationPoco p in items)
                {
                    cmd.CommandText = @"delete from Applicant_Job_Applications
                                                     where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantJobApplicationPoco p in items)
                {
                    cmd.CommandText = "update Applicant_Job_Applications " +
                                      "set Applicant = @Applicant," +
                                          "Job = @Job," +
                                          "Application_Date = @ApplicationDate " +
                                           "where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Applicant", p.Applicant);
                    cmd.Parameters.AddWithValue("@Job", p.Job);
                    cmd.Parameters.AddWithValue("@ApplicationDate", p.ApplicationDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

