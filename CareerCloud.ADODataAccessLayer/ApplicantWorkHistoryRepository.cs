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
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string connectionString;
        public ApplicantWorkHistoryRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
              
                foreach (ApplicantWorkHistoryPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Applicant_Work_History(Id, Applicant, Company_Name, Country_Code, Location, Job_Title, Job_Description, Start_Month, Start_Year, End_Month, End_Year) " +
                                      "Values (@Id, @Applicant, @CompanyName, @CountryCode, @Location, @JobTitle, @JobDescription, @StartMonth, @StartYear, @EndMonth, @EndYear)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Applicant", p.Applicant);
                    cmd.Parameters.AddWithValue("@CompanyName", p.CompanyName);
                    cmd.Parameters.AddWithValue("@CountryCode", p.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", p.Location);
                    cmd.Parameters.AddWithValue("@JobTitle", p.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", p.JobDescription);
                    cmd.Parameters.AddWithValue("@StartMonth", p.StartMonth);
                    cmd.Parameters.AddWithValue("@StartYear", p.StartYear);
                    cmd.Parameters.AddWithValue("@EndMonth", p.EndMonth);
                    cmd.Parameters.AddWithValue("@EndYear", p.EndYear);
                          
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Applicant_Work_History", conn).ExecuteReader();

                var applicantSkills = new List<ApplicantWorkHistoryPoco>();

                while (r.Read())
                {
                    applicantSkills.Add(new ApplicantWorkHistoryPoco()
                    {
                        Id = (Guid)r["Id"],
                        Applicant = (Guid)r["Applicant"],
                        CompanyName = (string)r["Company_Name"],
                        CountryCode = (string)r["Country_Code"],
                        Location = (string)r["Location"],
                        JobTitle = (string)r["Job_Title"],
                        JobDescription = (string)r["Job_Description"],
                        StartMonth = (short)r["Start_Month"],
                        StartYear = (int)r["Start_Year"],
                        EndMonth = (short)r["End_Month"],
                        EndYear = (int)r["End_Year"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                    }
                    );
                }
                return applicantSkills;
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"delete from [dbo].[Applicant_Work_History]
                                                     where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantWorkHistoryPoco p in items)
                {
                    cmd.CommandText = @"update Applicant_Work_History
                                      set Applicant = @Applicant,
                                        Company_Name = @CompanyName,
                                        Country_Code = @CountryCode,
                                        Location = @Location,
                                        Job_Title = @JobTitle,
                                        Job_Description = @JobDescription,
                                        Start_Month = @StartMonth,
                                        Start_Year = @StartYear,
                                        End_Month = @EndMonth,
                                        End_Year = @EndYear
                                         where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Applicant", p.Applicant);
                    cmd.Parameters.AddWithValue("@CompanyName", p.CompanyName);
                    cmd.Parameters.AddWithValue("@CountryCode", p.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", p.Location);
                    cmd.Parameters.AddWithValue("@JobTitle", p.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", p.JobDescription);
                    cmd.Parameters.AddWithValue("@StartMonth", p.StartMonth);
                    cmd.Parameters.AddWithValue("@StartYear", p.StartYear);
                    cmd.Parameters.AddWithValue("@EndMonth", p.EndMonth);
                    cmd.Parameters.AddWithValue("@EndYear", p.EndYear);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
