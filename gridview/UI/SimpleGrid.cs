using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace gridview
{
    public class SimpleGrid : ContentPage
    {
        public SimpleGrid()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.OS == TargetPlatform.iOS)
                Padding = new Thickness(0, 20, 0, 0);
            GenerateUi();
        }

        void GenerateUi()
        {
            var imageNames = new List<string>{ "chicken1.png", "chicken2.png", "chicken3.png", "chicken4.png", "chicken5.png", "chicken6.png" };
            var internalStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White,
                WidthRequest = App.ScreenSize.Width,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(6, 0, 6, 4),
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = App.ScreenSize.Width * .47 },
                    new ColumnDefinition { Width = App.ScreenSize.Width * .47 },
                },
            };

            int left = 0, top = 0;

            foreach (var pl in imageNames)
            {
                var imgProduct = new CustomImage
                {
                    VerticalOptions = LayoutOptions.Start,
                    Source = pl
                };       

                var lblRange = new Label
                {
                    Text = pl.Split('.')[0],
                    TextColor = Color.Blue,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 12
                };
                var stack = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    WidthRequest = App.ScreenSize.Width * .45,
                    HeightRequest = App.ScreenSize.Height * .35,
                    BackgroundColor = Color.White,
                    Spacing = 0.4,
                    Children =
                    { 
                        new StackLayout
                        {
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Children =
                            { imgProduct, 
                                new StackLayout
                                {
                                    Padding = new Thickness(0, -4, 0, 0),
                                    Children = { lblRange }
                                },
                            }
                        }

                    }
                };

                grid.Children.Add(stack, left, top);
                left++;
                if (left == 2)
                {
                    left = 0;
                    top++;
                }
            }

            internalStack.Children.Add(grid);

            Content = new StackLayout
            { 
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    internalStack
                }
            };
        }
    }
}


