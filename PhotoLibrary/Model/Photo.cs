using System;
using System.Windows.Media.Imaging;

namespace PhotoLibrary.Model
{
    public class Photo  : IDisposable
    {       
     
        public Photo(string path)
        {
            _path = path;
        }

        private string _path;
        public string Path { get { return System.IO.Path.GetFileName(_path); } }

        public BitmapSource Thumbnail { get; set; }

        private BitmapSource _image;
        public BitmapSource Image
        {
            get
            {
                if (_image == null)
                    _image = ImageLoader.GetImage(_path);
                return _image;
            }
        }
        
        public void Dispose()
        {
            Thumbnail = null;
            _image = null;
        }
        
    }
}
