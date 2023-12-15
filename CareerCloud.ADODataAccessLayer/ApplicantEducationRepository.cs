using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using FluentAssertions.Execution;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        private readonly string connectionString;
        public ApplicantEducationRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
      
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                
                foreach (ApplicantEducationPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Applicant_Educations(Id, Applicant, Major, Certificate_Diploma, Start_Date, Completion_Date, Completion_Percent) " +
                                      "Values (@Id,@Applicant, @Major, @CertificateDiploma, @StartDate, @CompletionDate, @CompletionPercent)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Applicant", p.Applicant);
                    cmd.Parameters.AddWithValue("@Major", p.Major);

                    cmd.Parameters.AddWithValue("@CertificateDiploma", p.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@StartDate", p.StartDate);
                    cmd.Parameters.AddWithValue("@CompletionDate", p.CompletionDate);
                    cmd.Parameters.AddWithValue("@CompletionPercent", p.CompletionPercent);
                   
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            //no need to implement
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Applicant_Educations", conn).ExecuteReader();

                var applicantEducations = new List<ApplicantEducationPoco>();

                while (r.Read())
                {
                    applicantEducations.Add(new ApplicantEducationPoco()
                    {
                        Id = (Guid)r["Id"],
                        Applicant = (Guid)r["Applicant"],
                        CertificateDiploma = (string)r["Certificate_Diploma"],
                        StartDate = (DateTime)r["Start_Date"],
                        CompletionDate = (DateTime)r["Completion_Date"],
                        CompletionPercent = (byte)r["Completion_Percent"],
                        TimeStamp = (byte[])r["Time_Stamp"],
                        Major = (string)r["Major"]
                    }
                    );

                }
                return applicantEducations;
            }
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            //no need to implement
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)

        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantEducationPoco p in items)
                {
                    cmd.CommandText = @"delete from Applicant_Educations
                                                     where Id = @Id";
                    
                    cmd.Parameters.AddWithValue("@Id", p.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantEducationPoco p in items)
                {
                    cmd.CommandText = "update [dbo].[Applicant_Educations] " +
                                       "set [Applicant] = @Applicant," +
                                          "[Major] = @Major,"+
                                          "[Certificate_Diploma] = @CertificateDiploma," +
                                          "[Start_Date] = @StartDate, " +
                                          "[Completion_Date] = @CompletionDate," +
                                          "[Completion_Percent] = @CompletionPercent " +
                                          "where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Applicant", p.Applicant);
                    cmd.Parameters.AddWithValue("@Major", p.Major);
                    cmd.Parameters.AddWithValue("@CertificateDiploma", p.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@StartDate", p.StartDate);
                    cmd.Parameters.AddWithValue("@CompletionDate", p.CompletionDate);
                    cmd.Parameters.AddWithValue("@CompletionPercent", p.CompletionPercent);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
