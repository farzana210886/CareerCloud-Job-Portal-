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
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        private readonly string connectionString;
        public ApplicantSkillRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                
                foreach (ApplicantSkillPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Applicant_Skills (Id, Skill_Level, Start_Month, Start_Year, End_Month, End_Year, Applicant, Skill) " +
                                      "Values (@Id, @SkillLevel, @StartMonth, @StartYear, @EndMonth, @EndYear, @Applicant, @Skill)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@SkillLevel", p.SkillLevel);
                    cmd.Parameters.AddWithValue("@StartMonth", p.StartMonth);
                    cmd.Parameters.AddWithValue("@StartYear", p.StartYear);
                    cmd.Parameters.AddWithValue("@EndMonth", p.EndMonth);
                    cmd.Parameters.AddWithValue("@EndYear", p.EndYear);
                    cmd.Parameters.AddWithValue("@Applicant", p.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", p.Skill);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Applicant_Skills", conn).ExecuteReader();

                var applicantSkills = new List<ApplicantSkillPoco>();

                while (r.Read())
                {
                    applicantSkills.Add(new ApplicantSkillPoco()
                    {
                        Id = (Guid)r["Id"],
                        SkillLevel = (string)r["Skill_Level"],
                        StartMonth = (byte)r["Start_Month"],
                        StartYear = (int)r["Start_Year"],
                        EndMonth = (byte)r["End_Month"],
                        EndYear = (int)r["End_Year"],
                        TimeStamp = (byte[])r["Time_Stamp"],
                        Applicant = (Guid)r["Applicant"],
                        Skill = (string)r["Skill"]
                    }
                    );
                }
                return applicantSkills;
            }
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantSkillPoco p in items)
                {
                    cmd.CommandText = @"delete from [dbo].[Applicant_Skills]
                                                     where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantSkillPoco p in items)
                {
                    cmd.CommandText = @"update Applicant_Skills
                                       set Applicant = @Applicant,
                                          Skill = @Skill,
                                          Skill_Level = @SkillLevel,
                                          Start_Month = @StartMonth,
                                          Start_Year = @StartYear,
                                          End_Month = @EndMonth,
                                          [End_Year] = @EndYear
                                          where Id = @Id ";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Applicant", p.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", p.Skill);
                    cmd.Parameters.AddWithValue("@SkillLevel", p.SkillLevel);
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
    
