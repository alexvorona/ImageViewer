using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;

namespace PhotoLibrary.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ImageGalleryViewModel _currentViewModel;

        public ImageGalleryViewModel CurrentViewModel {
            get
            {
                return _currentViewModel;
            }
        }
       
        public MainViewModel(ImageGalleryViewModel currentViewModel)
        {
            _currentViewModel = currentViewModel;
        }
    }
}