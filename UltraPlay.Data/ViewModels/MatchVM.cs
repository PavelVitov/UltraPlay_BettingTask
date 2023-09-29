using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraPlay.Data.ViewModels
{
    public class MatchVM : AbstractVM
    {
        public DateTime StartDate { get;  set; }
        public IList<BetVM> ActiveBets { get; set; }
        public List<BetVM> InactiveBets { get; set; }
    }
}
