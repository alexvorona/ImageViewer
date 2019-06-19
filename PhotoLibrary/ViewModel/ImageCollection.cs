using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using PhotoLibrary.Model;

namespace PhotoLibrary.ViewModel
{
    public class ImageCollection : ViewModelBase
    {    
        public ImageCollection()
        {
            Images = new ObservableCollection<Photo>();
        }
       
        public ObservableCollection<Photo> Images { get; }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged();
            }
        }

        private Photo _selectedImage;
        public Photo SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                RaisePropertyChanged();
            }
        }
       
        public void Clear()
        {
            foreach (Photo m in Images)
                m.Dispose();

            Images.Clear();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            SelectedImage = null;
        }

        private void ClearSelectedImage()
        {
            _selectedImage?.Dispose();
            GC.Collect();
        }
        public void Add(Photo photo)
        {
            Images.Add(photo);
        }

        public void Up()
        {
            if (SelectedIndex > 0)
            {
                ClearSelectedImage();
                SelectedIndex--;
                SelectedImage = Images[SelectedIndex];
            }
        }

        public void Down()
        {
            if (SelectedIndex < Images.Count - 1)
            {
                ClearSelectedImage();
                SelectedIndex++;
                SelectedImage = Images[SelectedIndex];
            }
        }

    }
}
