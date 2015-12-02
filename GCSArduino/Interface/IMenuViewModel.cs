using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace GCSArduino.Interface
{
    public interface IMenuViewModel
    {
        RelayCommand WindowMenuOpenCamerasCommand { get; set; }
        RelayCommand WindowMenuOpenQuadPropertyCommand { get; set; }

        RelayCommand FileMenuCommand { get; set; }
        RelayCommand LoadMissionMenuCommand { get; set; }
        RelayCommand DeleteMissionMenuCommand { get; set; }
        RelayCommand ToolsMenuCommand { get; set; }

        bool IsOpenCameraWindows { get; set; }
        bool CameraVisibillit { get; set; }
    }
}
