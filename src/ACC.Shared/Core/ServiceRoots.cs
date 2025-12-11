using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Core
{
    // rutas de servicios apis etc
    public static class ServiceRoots
    {
        public const string ACC_API_Url = "https://localhost:7059/api/";
        public const string ACC_WEBAPP_Url = "https://localhost:7189/";
        public const string ACC_COMPILER_Url = "https://localhost:7023/api/compile/";

        public const string ACC_API_CAPITULOS = ACC_API_Url + "Capitulos/"; 
        public const string ACC_API_BIBLIOTECA = ACC_API_Url + "Biblioteca/";
        public const string ACC_API_MODULO = ACC_API_Url + "Modulo/";
        public const string ACC_API_EXAMENES = ACC_API_Url + "Examenes/";
    }
}
