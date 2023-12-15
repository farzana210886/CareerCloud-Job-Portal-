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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        private readonly string connectionString;
        public CompanyJobSkillRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
              
                foreach (CompanyJobSkillPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Company_Job_Skills(Id, Job, Skill_Level, Skill, Importance) " +
                                      "Values (@Id, @Job, @SkillLevel, @Skill, @Importance)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Job", p.Job);
                    cmd.Parameters.AddWithValue("@SkillLevel", p.SkillLevel);
                    cmd.Parameters.AddWithValue("@Skill", p.Skill);
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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Company_Job_Skills", conn).ExecuteReader();

                var companyJobSkills = new List<CompanyJobSkillPoco>();

                while (r.Read())
                {
                    companyJobSkills.Add(new CompanyJobSkillPoco()
                    {
                        Id = (Guid)r["Id"],
                        Job = (Guid)r["Job"],
                        SkillLevel = (string)r["Skill_Level"],
                        Skill = (string)r["Skill"],
                        Importance = (int)r["Importance"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                    }
                    );
                }
                return companyJobSkills;
            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Job_Skills]
                                                     WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }


        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyJobSkillPoco p in items)
                {
                    cmd.CommandText = @"update Company_Job_Skills
                                       set Job = @Job
                                          ,Skill = @Skill
                                          ,Skill_Level = @SkillLevel
                                          ,Importance = @Importance
                                       where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Job", p.Job);
                    cmd.Parameters.AddWithValue("@Skill", p.Skill);
                    cmd.Parameters.AddWithValue("@SkillLevel", p.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", p.Importance);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

