using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using PhotoLibrary.Model;

namespace PhotoLibrary.ViewModel
{
    public partial class ImageGalleryViewModel
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private RelayCommand<MouseButtonEventArgs> _dblClickCommand;
        private RelayCommand<DragEventArgs> _dropCommand;
        private RelayCommand _moveUpCommand;
        private RelayCommand _moveDownCommand;
        private RelayCommand _blureCommand;
        
        private RelayCommand<KeyEventArgs> _keyDownCommand;

        public RelayCommand<MouseButtonEventArgs> DblClickCommand => _dblClickCommand ?? (_dblClickCommand = new RelayCommand<MouseButtonEventArgs>(ShowDetail));
        public RelayCommand<DragEventArgs> DropCommand => _dropCommand ?? (_dropCommand = new RelayCommand<DragEventArgs>(DropAsync));
        public RelayCommand MoveUpCommand => _moveUpCommand ?? (_moveUpCommand = new RelayCommand(Up));
        public RelayCommand MoveDownCommand => _moveDownCommand ?? (_moveDownCommand = new RelayCommand(Down));
        public RelayCommand BlureCommand => _blureCommand ?? (_blureCommand = new RelayCommand(BlurToggle));
        
        public RelayCommand<KeyEventArgs> KeyDownCommand => _keyDownCommand ?? (_keyDownCommand = new RelayCommand<KeyEventArgs>(KeyDown));

        private void KeyDown(KeyEventArgs e)
        {
            if (e == null) return;
            if (e.Key == Key.Escape)
                ShowGallery();
            if (e.Key == Key.Left)
                Up();
            if (e.Key == Key.Right)
                Down();

        }
        private void ShowDetail(MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Image)
                CurrentView = _detailView;
        }
        public void ShowGallery()
        {
            CurrentView = _galleryView;
        }
       
        private void Up()
        {
            ImagesCollection.Up();
        }
        private void Down()
        {
            ImagesCollection.Down();
        }

        private void BlurToggle()
        {
            BlurMode = !BlurMode;
        }

        private async void DropAsync(DragEventArgs e)
        {
            string[] droppedFiles = null;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                droppedFiles = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            }

            if ((null == droppedFiles) || (!droppedFiles.Any()))
            {
                return;
            }

            await AddImages(droppedFiles.ToList());
        }
        private async Task AddImages(List<string> list)
        {
            foreach (var file in list)
            {
                try
                {
                    logger.Info($"File {file}");
                    var image = new Photo(file);
                    var result = ImageLoader.GetThumbnailAsync(file);
                    await result.ContinueWith(t =>
                    {
                        image.Thumbnail = t.Result;
                    });
                    ImagesCollection.Add(image);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Image Add");
                }
            }

        }
    }
}