using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Dtos
{
    public class UserInfoDto
    {

        public string FName { get; set; }

        public string LName { get; set; }

        public string Gender { get; set; }
        public DateTime? BrithDate { get; set; }
        public string Photo { get; set; }
    }
}
