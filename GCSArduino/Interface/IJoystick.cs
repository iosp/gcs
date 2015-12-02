using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCSArduino.Model;

namespace GCSArduino.Interface
{
    public interface IJoystick
    {
        ObservableCollection<Equaliser> Equalisers { get; set; }
        void Start(long idx);
        void Stop();
    }
}
