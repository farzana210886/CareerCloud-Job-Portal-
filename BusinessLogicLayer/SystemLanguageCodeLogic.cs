﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {

        }

        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            Add(pocos);
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            Update(pocos);
        }

        public void Get(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            Get(pocos);
        }

        public void GetAll(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            Get(pocos);
        }


        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            Update(pocos);
        }

        protected void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, $"Language ID {poco.LanguageID} cannot be null or empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(1001, $"Name {poco.Name} cannot be null or empty"));
                }
                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, $"Native Name {poco.NativeName} cannot be null or empty"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

        }
    }
}
