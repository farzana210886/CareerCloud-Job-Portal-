using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
    {
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {

        }

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            Add(pocos);
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            Update(pocos);
        }

        public void Delete(SystemCountryCodePoco[] pocos) { 
        Verify(pocos);
        Update(pocos);
        }

        public void Get(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);  
            Get(pocos); 
        }

        public void GetAll(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            Get(pocos);
        }

        protected void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            if (pocos != null)
            {
                foreach (var poco in pocos)
                {
                    if (string.IsNullOrEmpty(poco.Code))
                    {
                        exceptions.Add(new ValidationException(900, $"Code {poco.Code} cannot be null or empty"));
                    }
                    if (string.IsNullOrEmpty(poco.Name))
                    {
                        exceptions.Add(new ValidationException(901, $"Name {poco.Name} cannot be null or empty"));
                    }

                }
            }
            else
            {
                exceptions.Add(new ValidationException(900, $"Country Code info cannot be null or empty"));
            }
                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }

            }
        }
    }

