using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using System.ComponentModel;
using System.Linq;
using Android.Graphics.Drawables;
using gridview.Droid;
using gridview;

[assembly: ExportRenderer(typeof(CustomImage), typeof(CustomImageRenderer))]
namespace gridview.Droid
{
    public class CustomImageRenderer : ImageRenderer
    {
        GradientDrawable shape;

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

        }

        private bool isDecoded;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var largeImage = (CustomImage)Element;

            if ((Element.Width > 0 && Element.Height > 0 && !isDecoded) || (e.PropertyName == "ImageSource" && largeImage.ImageSource != null))
            {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InJustDecodeBounds = true;

                //Get the resource id for the image
                if (largeImage.ImageSource != null)
                {
                    var field = typeof(Resource.Drawable).GetField(largeImage.ImageSource.Split('.').First());
                    var value = (int)field.GetRawConstantValue();

                    BitmapFactory.DecodeResource(Context.Resources, value, options);

                    var width = (int)Element.Width;
                    var height = (int)Element.Height;
                    options.InSampleSize = CalculateInSampleSize(options, width, height);

                    options.InJustDecodeBounds = false;
                    var bitmap = BitmapFactory.DecodeResource(Context.Resources, value, options);

                    Control.SetImageBitmap(bitmap);

                    isDecoded = true;
                }
            }

        }

        int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            var height = options.OutHeight;
            var width = options.OutWidth;
            var inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                var halfHeight = (int)(height / 2);
                var halfWidth = (int)(width / 2);

                while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return (int)inSampleSize;
        }
    }
}

