using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Annotations;
using GCSArduino.Interface;
using GalaSoft.MvvmLight.Command;

namespace GCSArduino.ViewModel
{
    public class MenuViewModel : IMenuViewModel , INotifyPropertyChanged
    {
        private bool _isOpenCameraWindows;
        private bool _cameraVisibillit;
        public RelayCommand WindowMenuOpenCamerasCommand { get; set; }
        public RelayCommand WindowMenuOpenQuadPropertyCommand { get; set; }
        public RelayCommand FileMenuCommand { get; set; }
        public RelayCommand LoadMissionMenuCommand { get; set; }
        public RelayCommand DeleteMissionMenuCommand { get; set; }
        public RelayCommand ToolsMenuCommand { get; set; }


        public bool CameraVisibillit
        {
            get { return _cameraVisibillit; }
            set { _cameraVisibillit = value; OnPropertyChanged();}
        }

        public bool IsOpenCameraWindows
        {
            get { return _isOpenCameraWindows; }
            set
            {
                if (value.Equals(_isOpenCameraWindows)) return;
                _isOpenCameraWindows = value;
                OnPropertyChanged();
            }
        }

        MultimediaWindow MultimediaWindow { get; set; }
        QuadPropertyWindow QuadPropertyWindow { get; set; }
        public MenuViewModel()
        {
            WindowMenuOpenCamerasCommand = new RelayCommand(WindowMenuOpenCamerasCommandAction);
            WindowMenuOpenQuadPropertyCommand = new RelayCommand(WindowMenuOpenQuadPropertyCommandAction);
            IsOpenCameraWindows = false;
        }

        private void WindowMenuOpenQuadPropertyCommandAction()
        {
            if (QuadPropertyWindow != null)
                QuadPropertyWindow.Close();
            QuadPropertyWindow = new QuadPropertyWindow();
            QuadPropertyWindow.Show();

        }

        private void WindowMenuOpenCamerasCommandAction()
        {
            if (!IsOpenCameraWindows)
            {
                MultimediaWindow = new MultimediaWindow();
                MultimediaWindow.Show();
                IsOpenCameraWindows = true;
            }
            else
            {
                MultimediaWindow.Hide();
                MultimediaWindow = null;
                IsOpenCameraWindows = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
