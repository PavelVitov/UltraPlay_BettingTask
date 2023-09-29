using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraPlay.Services.Interfaces
{
    public interface ISportsXMLProvider
    {
        Task<string> GetData(CancellationToken stoppingToken);
    }
}
