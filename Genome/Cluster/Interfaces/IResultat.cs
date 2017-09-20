using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Interfaces
{
    public interface IResultat
    {
         int Id { get; set; }
        bool HasValue { get; set; }
    }
}
