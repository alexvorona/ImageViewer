using System;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using PhotoLibrary.View;

namespace PhotoLibrary.ViewModel
{
    public partial class ImageGalleryViewModel : ViewModelBase
    {      
        private bool _blurMode;
        private ImageDetailControl _detailView;
        private ImageGalleryControl _galleryView;
        private UserControl _currentView;


        public ImageGalleryViewModel()
        {
            ImagesCollection = new ImageCollection();
            
            _galleryView = new ImageGalleryControl {DataContext = this};
            _detailView = new ImageDetailControl {DataContext = this};
            CurrentView = _galleryView;
        }
  
        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChanged();
            }
        }

        public bool BlurMode
        {
            get
            {
                return _blurMode;
            }
            set
            {
                _blurMode = value;
                RaisePropertyChanged();
            }
        }
        public ImageCollection ImagesCollection { get; }
    }
}
