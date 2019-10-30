using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Projeto.Services.Validations
{
    public class ValidationUtil
    {

        public static Hashtable GetErrors(ModelStateDictionary modelState)
        {
            var errors = new Hashtable();

            foreach (var item in modelState)
            {
                if (item.Value.Errors.Count > 0)
                {
                    errors[item.Key] = item.Value.Errors.Select(e => e.ErrorMessage).ToList();
                }
            }

            return errors;
        }
    }
}
