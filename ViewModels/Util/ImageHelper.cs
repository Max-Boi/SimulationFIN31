using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace SimulationFIN31.ViewModels.Util;

public static class  ImageHelper
{
    
        public static Bitmap LoadFromResource(Uri resourceUri)
        {
            return new Bitmap(AssetLoader.Open(resourceUri));
        }
        public static Bitmap LoadFromResource(string path)
        {
            return new Bitmap(AssetLoader.Open(new Uri(path)));
        }
}