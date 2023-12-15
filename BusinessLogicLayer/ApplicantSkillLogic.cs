using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {

        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.StartMonth.ToString()))
                {
                    exceptions.Add(new ValidationException(101, $"Start Month {poco.StartMonth} cannot be null or empty"));
                }
                else if (poco.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(101, $"Start Month {poco.StartMonth} cannot be greater than 12"));
                }

                if (string.IsNullOrEmpty(poco.EndMonth.ToString()))
                {
                    exceptions.Add(new ValidationException(102, $"End Month {poco.EndMonth} cannot be null or empty"));
                }
                else if (poco.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102, $"End Month {poco.EndMonth} cannot be greater than 12"));
                }

                if (string.IsNullOrEmpty(poco.StartYear.ToString()))
                {
                    exceptions.Add(new ValidationException(103, $"Start Year {poco.StartYear} cannot be null or empty"));
                }
                else if (poco.StartYear < 1900)
                {
                    exceptions.Add(new ValidationException(103, $"Start Year {poco.StartYear} cannot be less than 1900"));
                }

                if (string.IsNullOrEmpty(poco.EndYear.ToString()))
                {
                    exceptions.Add(new ValidationException(104, $"End Year {poco.EndYear} cannot be null or empty"));
                }
                else if (poco.EndYear < poco.StartYear)
                {
                    exceptions.Add(new ValidationException(104, $"End Year {poco.EndYear} cannot be less than Start Year {poco.StartYear}"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

        }
    }
}
