using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities; 

public class InvitacionAula
{
    public int Id { get; set; }
    public int AulaId { get; set; }
    public string Token { get; set; } = null!;
    public bool Activa { get; set; } = true;
    public DateTime? ExpiraEn { get; set; } // UTC
    public DateTime CreatedAt { get; set; } // UTC
    public int NumUsos { get; set; } = 0; 
    public Aula Aula { get; set; } = null!;
}
