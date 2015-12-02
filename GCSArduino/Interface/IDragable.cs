using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCSArduino.Interface
{
    public interface IDragable
    {
        Type DataType { get; }
        void Remove(object i);
        void Add(object uIElement, object dataContext);
    }
}
