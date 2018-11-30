﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopUI.Infrastructure.Attributes
{
    public class RequiredCollectionAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            bool isValid = base.IsValid(value);

            if (isValid)
            {
                ICollection collection = value as ICollection;
                if (collection != null)
                {
                    isValid = collection.Count != 0;
                }
            }
            return isValid;
        }
    }
}
