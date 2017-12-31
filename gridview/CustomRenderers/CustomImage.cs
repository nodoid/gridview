﻿using Xamarin.Forms;

namespace gridview
{
    public class CustomImage : Image
    {
        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create("ImageSource", typeof(string), typeof(CustomImage), default(string), propertyChanged: (bindable, oldValue, newValue) =>
                {
                    if (Device.RuntimePlatform != Device.Android)
                    {
                        var image = (CustomImage)bindable;

                        var baseImage = (Image)bindable;
                        baseImage.Source = image.ImageSource;
                    }
                });

        public string ImageSource
        {
            get { return GetValue(ImageSourceProperty) as string; }
            set { SetValue(ImageSourceProperty, value); }
        }

        public CustomImage()
        {
        }
    }
}


