using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace GCSArduino.Interface
{
    public interface IMaintenance
    {
        ObservableCollection<IVehicle> Vehicles { get; }
    }
}
