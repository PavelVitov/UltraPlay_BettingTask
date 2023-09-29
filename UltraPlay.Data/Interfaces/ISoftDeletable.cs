using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraPlay.Data.Interfaces
{
    public interface ISoftDeletable : IKeyedEntity
    {
        public DateTime? DeletedAt { get; set; }
    }
}
