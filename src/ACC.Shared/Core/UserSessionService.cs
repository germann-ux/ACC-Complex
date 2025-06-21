using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Core
{
    public class UserSessionService
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }

        public void Clear() 
        {
            UserId = string.Empty;
            UserName = string.Empty;
            IsAuthenticated = false;
        }
    }
}
