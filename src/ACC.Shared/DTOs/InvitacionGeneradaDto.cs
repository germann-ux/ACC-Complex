using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs; 

public class InvitacionGeneradaDto
{
    public string Token { get; set; } = null!;
    public string LinkInvitacion { get; set; } = null!;
    public DateTime? ExpiraEn { get; set; }
}