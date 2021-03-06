﻿using PhotoCollageScreensaver.Enums;
using System;

namespace PhotoCollageScreensaver
{
    public class Configuration
    {
        public Configuration()
        {
            this.Directory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.IsGrayscale = false;
            this.IsRandom = true;
            this.MaximumRotation = 15;
            this.MaximumSize = 500;
            this.NumberOfPhotos = 10;
            this.Opacity = 1.0;
            this.PhotoBorderType = BorderType.Border;
            this.Speed = ScreensaverSpeed.Medium;
            this.UseVerboseLogging = false;
        }

        public string Directory { get; set; }
        public bool IsGrayscale { get; set; }
        public bool IsRandom { get; set; }
        public int MaximumRotation { get; set; }
        public int MaximumSize { get; set; }
        public int NumberOfPhotos { get; set; }
        public double Opacity { get; set; }
        public BorderType PhotoBorderType { get; set; }
        public ScreensaverSpeed Speed { get; set; }
        public bool UseVerboseLogging { get; set; }
    }
}
