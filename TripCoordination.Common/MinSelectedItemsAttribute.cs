using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TripCoordination.Common
{
    public class MinSelectedItemsAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var list = value as List<int>;
            return list != null && list.Any();
        }
    }
}
