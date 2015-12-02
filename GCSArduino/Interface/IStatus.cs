using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCSArduino.Interface
{
    public interface IStatus
    {
        double Text00 { get; set; }
        double Text01 { get; set; }
        double Text02 { get; set; }
        double Text03 { get; set; }
        double Text04 { get; set; }
        double Text05 { get; set; }
        double Text06 { get; set; }

        double Text10 { get; set; }
        double Text11 { get; set; }
        double Text12 { get; set; }
        double Text13 { get; set; }
        double Text14 { get; set; }
        double Text15 { get; set; }
        double Text16 { get; set; }
        
    }
}
