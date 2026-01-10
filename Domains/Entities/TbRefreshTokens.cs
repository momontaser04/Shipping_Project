using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entities
{
    public class TbRefreshTokens:BaseTable
    {
        public string Token { get; set; }

        public string UserId { get; set; }

        public DateTime Expires { get; set; }
    }
}
