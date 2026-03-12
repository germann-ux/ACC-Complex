using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Core
{
    [Obsolete("Use ServiceEndpointsOptions from configuration instead of hardcoded URLs.")]
    // Legacy URLs kept temporarily for compatibility with historical branches.
    public static class ServiceRoots
    {
        public const string ACC_API_Url = "";
        public const string ACC_WEBAPP_Url = "";
        public const string ACC_COMPILER_Url = "";

        public const string ACC_API_CAPITULOS = ACC_API_Url + "Capitulos/"; 
        public const string ACC_API_BIBLIOTECA = ACC_API_Url + "Biblioteca/";
        public const string ACC_API_MODULO = ACC_API_Url + "Modulo/";
        public const string ACC_API_EXAMENES = ACC_API_Url + "Examenes/";
    }
}
