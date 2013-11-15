
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Proiect_Appchallenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        byte[] sourcePixels;
        byte[] thumbnailPixels;
        uint width, height;
        uint thumbnailWidth, thumbnailHeight;
        WriteableBitmap currentBitmap;
        BitmapImage originalBitmap;

        string openedFileType;

        WriteableBitmap[] thumbnailEffects = new WriteableBitmap[5];
        WriteableBitmap[] bigEffects = new WriteableBitmap[5];

        //WriteableBitmap currentBitmapE1;
        //WriteableBitmap currentBitmapE2;
        //WriteableBitmap currentBitmapE3;
        //WriteableBitmap currentBitmapE4;
        //WriteableBitmap currentBitmapE5;

        void makePreview()
        {
            aplicaEfect1(thumbnailEffects[0], thumbnailPixels, thumbnailWidth, thumbnailHeight, false);
            aplicaEfect2(thumbnailEffects[1], thumbnailPixels, thumbnailWidth, thumbnailHeight, false);
            aplicaEfect3(thumbnailEffects[2], thumbnailPixels, thumbnailWidth, thumbnailHeight, false);
            aplicaEfect4(thumbnailEffects[3], thumbnailPixels, thumbnailWidth, thumbnailHeight, false);
            aplicaEfect5(thumbnailEffects[4], thumbnailPixels, thumbnailWidth, thumbnailHeight, false);
            efect1.Source = thumbnailEffects[0];
            efect2.Source = thumbnailEffects[1];
            efect3.Source = thumbnailEffects[2];
            efect4.Source = thumbnailEffects[3];
            efect5.Source = thumbnailEffects[4];
        }


        byte[] raiseContrast(byte[] v, float value)
        {
            double contrastLevel = Math.Pow((100.0 + value) / 100.0, 2);

            double blue = 0;
            double green = 0;
            double red = 0;

            byte[] newImg = new byte[v.Length];

            for (int k = 0; k + 4 < v.Length; k += 4)
            {
                blue = ((((v[k] / 255.0) - 0.5) *
                            contrastLevel) + 0.5) * 255.0;


                green = ((((v[k + 1] / 255.0) - 0.5) *
                            contrastLevel) + 0.5) * 255.0;


                red = ((((v[k + 2] / 255.0) - 0.5) *
                            contrastLevel) + 0.5) * 255.0;


                if (blue > 255)
                { blue = 255; }
                else if (blue < 0)
                { blue = 0; }


                if (green > 255)
                { green = 255; }
                else if (green < 0)
                { green = 0; }


                if (red > 255)
                { red = 255; }
                else if (red < 0)
                { red = 0; }


                newImg[k] = (byte)blue;
                newImg[k + 1] = (byte)green;
                newImg[k + 2] = (byte)red;
                newImg[k + 3] = sourcePixels[k + 3];
            }

            return newImg;

        }

        byte[] tint(byte[] v, float blueTint, float greenTint, float redTint)
        {

            float blue = 0;
            float green = 0;
            float red = 0;

            blueTint = blueTint / 100.0f;
            greenTint = greenTint / 100.0f;
            redTint = redTint / 100.0f;

            byte[] newImg = new byte[v.Length];

            for (int k = 0; k + 4 < v.Length; k += 4)
            {
                
                
                blue = v[k] + (255 - v[k]) * blueTint;
                green = v[k + 1] + (255 - v[k + 1]) * greenTint;
                red = v[k + 2] + (255 - v[k + 2]) * redTint;


                if (blue > 255)
                { blue = 255; }


                if (green > 255)
                { green = 255; }


                if (red > 255)
                { red = 255; }


                newImg[k] = (byte)blue;
                newImg[k + 1] = (byte)green;
                newImg[k + 2] = (byte)red;
                newImg[k + 3] = v[k + 3];

            }

            return newImg;
        }

        private async void aplicaEfect1(WriteableBitmap sourceBitmap, byte[] sourcePixels, uint width, uint height, bool flag)
        {
            byte[] newPixels = new byte[sourcePixels.Length];
            try
            {
                newPixels = raiseContrast(sourcePixels, 5);
                newPixels = tint(newPixels, 25, 25, 0);
                sourceBitmap = new WriteableBitmap((int)width, (int)height);

                using (Stream stream = sourceBitmap.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(newPixels, 0, newPixels.Length);                    
                }
                if (flag)
                    bigEffects[0] = sourceBitmap;
                else thumbnailEffects[0] = sourceBitmap;

            }
            catch
            {
            }
        }

        private async void aplicaEfect2(WriteableBitmap sourceBitmap, byte[] sourcePixels, uint width, uint height, bool flag)
        {
            byte[] newPixels = new byte[sourcePixels.Length];
            try
            {
                newPixels = raiseContrast(sourcePixels, 10);
                newPixels = tint(newPixels, 50, 5, 10);
                sourceBitmap = new WriteableBitmap((int)width, (int)height);

                using (Stream stream = sourceBitmap.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(newPixels, 0, newPixels.Length);                    
                }
                if (flag)
                    bigEffects[1] = sourceBitmap;
                else thumbnailEffects[1] = sourceBitmap;
            }
            catch
            {
            }
        }

        private async void aplicaEfect3(WriteableBitmap sourceBitmap, byte[] sourcePixels, uint width, uint height, bool flag)
        {
            byte[] newPixels = new byte[sourcePixels.Length];
            try
            {
                newPixels = raiseContrast(sourcePixels, 10);
                newPixels = tint(newPixels, 10, 0, 0);
                sourceBitmap = new WriteableBitmap((int)width, (int)height);

                using (Stream stream = sourceBitmap.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(newPixels, 0, newPixels.Length);                    
                }
                if (flag)
                    bigEffects[2] = sourceBitmap;
                else thumbnailEffects[2] = sourceBitmap;
            }
            catch
            {
            }
        }

        private async void aplicaEfect4(WriteableBitmap sourceBitmap, byte[] sourcePixels, uint width, uint height, bool flag)
        {
            byte[] newPixels = new byte[sourcePixels.Length];
            try
            {
                newPixels = raiseContrast(sourcePixels, 0);
                newPixels = tint(newPixels, 40, 20, 15);
                sourceBitmap = new WriteableBitmap((int)width, (int)height);

                using (Stream stream = sourceBitmap.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(newPixels, 0, newPixels.Length);                    
                }
                if (flag)
                    bigEffects[3] = sourceBitmap;
                else thumbnailEffects[3] = sourceBitmap;
            }
            catch
            {
            }
        }

        private async void aplicaEfect5(WriteableBitmap sourceBitmap, byte[] sourcePixels, uint width, uint height, bool flag)
        {
            byte[] newPixels = new byte[sourcePixels.Length];
            try
            {
                newPixels = raiseContrast(sourcePixels, 5);
                newPixels = tint(newPixels, 0, 0, 20);
                sourceBitmap = new WriteableBitmap((int)width, (int)height);

                using (Stream stream = sourceBitmap.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(newPixels, 0, newPixels.Length);                    
                }
                if (flag)
                    bigEffects[4] = sourceBitmap;
                else thumbnailEffects[4] = sourceBitmap;
            }
            catch
            {
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void loadButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker imgPicker = new FileOpenPicker();

            imgPicker.FileTypeFilter.Add(".bmp");
            imgPicker.FileTypeFilter.Add(".jpeg");
            imgPicker.FileTypeFilter.Add(".jpg");
            imgPicker.FileTypeFilter.Add(".gif");
            imgPicker.FileTypeFilter.Add(".png");

            imgPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            StorageFile imgFile = await imgPicker.PickSingleFileAsync();

            try
            {
                
                IRandomAccessStreamWithContentType img = await imgFile.OpenReadAsync();
                openedFileType = imgFile.FileType;


                for (int i = 0; i < bigEffects.Length; i++)
                    bigEffects[i] = null;
                
                originalBitmap = new BitmapImage();

                originalBitmap.SetSource(img);
                img.Dispose();
                width = Convert.ToUInt32(originalBitmap.PixelWidth);
                height = Convert.ToUInt32(originalBitmap.PixelHeight);

               // mainContainer.Source = originalBitmap; // show original image in MainPage                
             // Fetching pixel data

                using (IRandomAccessStream fileStream = await imgFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                                     
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);
                    // Scale image to appropriate size

                    BitmapTransform transform = new BitmapTransform()
                    {
                        ScaledWidth = width,
                        ScaledHeight = height
                    };


                    /// RGBA8 format:

                    /// Each pixel is store in 4 consecutive bytes:

                    /// 1st byte is Red (no offset)

                    /// 2nd byte is Green (+1)

                    /// 3rd byte is Blue (+2)

                    /// 4th byte is Alpha (+3)

                    PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                        BitmapPixelFormat.Bgra8,
                        BitmapAlphaMode.Straight,
                        transform,
                        ExifOrientationMode.IgnoreExifOrientation, // This sample ignores Exif orientation
                        ColorManagementMode.DoNotColorManage
                    );



                    // An array containing the decoded image data,

                    // which could be modified before being displayed

                    sourcePixels = pixelData.DetachPixelData();

                    // Approach 1 : Encoding the image buffer again:
                    // Encoding data
                    var inMemoryRandomStream = new InMemoryRandomAccessStream();
                    BitmapEncoder encoder;
                    switch (imgFile.FileType.ToLower())
                    {
                        case ".jpg":
                            encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream);
                            break;
                        case ".png":
                            encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, inMemoryRandomStream);
                            break;
                        case ".bmp":
                            encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, inMemoryRandomStream);
                            break;
                        case ".gif":
                            encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.GifEncoderId, inMemoryRandomStream);
                            break;
                        case ".jpeg":
                            encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream);
                            break;
                        default:
                            encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream);
                            break;
                    }                    
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, width, height, 96, 96, sourcePixels);
                    await encoder.FlushAsync();
                    inMemoryRandomStream.Seek(0);
                    // finally the resized writablebitmap
                    currentBitmap = new WriteableBitmap((int)width, (int)height);
                    await currentBitmap.SetSourceAsync(inMemoryRandomStream);
                    inMemoryRandomStream.Dispose();
                    mainContainer.Source = currentBitmap;


                    resBox.Text = currentBitmap.PixelHeight + " " + currentBitmap.PixelWidth;
                    heightBox.Text = mainContainer.ActualHeight.ToString();
                    widthBox.Text = mainContainer.ActualWidth.ToString();
                    //mainContainerGrid.Height = mainContainer.ActualHeight;
                    dimBox.Text = mainContainerGrid.Width.ToString() + " " + mainContainer.ActualWidth.ToString();

                    double ratio = (double)width / height;
                    if(width > height)//landscape
                    {
                        thumbnailWidth = (uint)efect1.ActualWidth;
                        thumbnailHeight = (uint)(efect1.ActualWidth / ratio);
                    }
                    else//portrait
                    {
                        thumbnailHeight = (uint)efect1.ActualHeight;
                        thumbnailWidth = (uint)(efect1.ActualHeight * ratio);
                    }
                    thumbnailHeight *= 2;
                    thumbnailWidth *= 2;

                    WriteableBitmap smallImage = await ResizeImage(currentBitmap, thumbnailWidth, thumbnailHeight);
                    Stream stream = smallImage.PixelBuffer.AsStream();
                    thumbnailPixels = new byte[(uint)stream.Length];
                    await stream.ReadAsync(thumbnailPixels, 0, thumbnailPixels.Length);



                    makePreview();
                }
            }
            catch
            {
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker picker = new FileSavePicker();
            picker.FileTypeChoices.Add("JPG File", new List<string>() { ".jpg" });
            picker.FileTypeChoices.Add("PNG File", new List<string>() { ".png" });
            picker.FileTypeChoices.Add("BMP File", new List<string>() { ".bmp" });
            picker.FileTypeChoices.Add("GIF File", new List<string>() { ".gif" });
            picker.FileTypeChoices.Add("JPEG File", new List<string>() { ".jpeg" });
            picker.FileTypeChoices.Add("Picture File", new List<string>() { ".jpg",".png",".bmp",".gif",".jpeg" });
            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder;
                    switch (file.FileType.ToLower())
                    {
                        case ".jpg":
                            encoder  = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                            break;
                        case ".png":
                             encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                            break;
                        case ".bmp" :
                             encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, stream);
                            break;
                        case ".gif" :
                            encoder  = await BitmapEncoder.CreateAsync(BitmapEncoder.GifEncoderId, stream);
                            break;
                        case ".jpeg" :
                             encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                            break; 
                        default :
                             encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                            break;
                    }
                    
                    using (Stream pixelStream = currentBitmap.PixelBuffer.AsStream())
                    {
                        byte[] pixels = new byte[pixelStream.Length];
                        await pixelStream.ReadAsync(pixels, 0, pixels.Length);

                        encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)currentBitmap.PixelWidth, (uint)currentBitmap.PixelHeight, 96.0, 96.0, pixels);
                        await encoder.FlushAsync();
                    }
                }
            }
        }

        private void efect1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (bigEffects[0] == null)
            {
                aplicaEfect1(bigEffects[0], sourcePixels, width, height, true);
            }         
            //mainContainer.Source = bigEffects[0];
            currentBitmap = bigEffects[0];
        }

        private void efect2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (bigEffects[1] == null)
            {
                aplicaEfect2(bigEffects[1], sourcePixels, width, height, true);
            }
            mainContainer.Source = bigEffects[1];
        }

        private void efect3_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (bigEffects[2] == null)
            {
                aplicaEfect3(bigEffects[2], sourcePixels, width, height, true);
            }
            mainContainer.Source = bigEffects[2];
        }

        private void efect4_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (bigEffects[3] == null)
            {
                aplicaEfect4(bigEffects[3], sourcePixels, width, height, true);
            }
            mainContainer.Source = bigEffects[3];
        }

        private void efect5_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (bigEffects[4] == null)
            {
                aplicaEfect5(bigEffects[4], sourcePixels, width, height, true);
            }
            mainContainer.Source = bigEffects[4];
        }

        private async Task<WriteableBitmap> ResizeImage(WriteableBitmap sourceWriteBitmap, uint width, uint height)
        {
            // Get the pixel buffer of the writable bitmap in bytes
            Stream stream = sourceWriteBitmap.PixelBuffer.AsStream();
            byte[] pixels = new byte[(uint)stream.Length];
            await stream.ReadAsync(pixels, 0, pixels.Length);
            //Encoding the data of the PixelBuffer we have from the writable bitmap
            var inMemoryRandomStream = new InMemoryRandomAccessStream();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream);
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)sourceWriteBitmap.PixelWidth, (uint)sourceWriteBitmap.PixelHeight, 96, 96, pixels);
            await encoder.FlushAsync();
            // At this point we have an encoded image in inMemoryRandomStream
            // We apply the transform and decode
            var transform = new BitmapTransform
            {
                ScaledWidth = width,
                ScaledHeight = height,
                InterpolationMode = BitmapInterpolationMode.Cubic
            };
            //inMemoryRandomStream.Seek(0);
            var decoder = await BitmapDecoder.CreateAsync(inMemoryRandomStream);
            var pixelData = await decoder.GetPixelDataAsync(
                            BitmapPixelFormat.Bgra8,
                            BitmapAlphaMode.Straight,
                            transform,
                            ExifOrientationMode.IgnoreExifOrientation,
                            ColorManagementMode.DoNotColorManage);
            // An array containing the decoded image data
            var sourceDecodedPixels = pixelData.DetachPixelData();
            // Approach 1 : Encoding the image buffer again:
            // Encoding data
            var inMemoryRandomStream2 = new InMemoryRandomAccessStream();
            var encoder2 = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream2);
            encoder2.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, width, height, 96, 96, sourceDecodedPixels);
            await encoder2.FlushAsync();
            inMemoryRandomStream2.Seek(0);
            // finally the resized writablebitmap
            var bitmap = new WriteableBitmap((int)width, (int)height);
            await bitmap.SetSourceAsync(inMemoryRandomStream2);
            return bitmap;
        }

        private async Task<WriteableBitmap> rotateImage(WriteableBitmap sourceWriteBitmap, int rotateParam)
        {
            // Get the pixel buffer of the writable bitmap in bytes
            Stream stream = sourceWriteBitmap.PixelBuffer.AsStream();
            byte[] pixels = new byte[(uint)stream.Length];
            await stream.ReadAsync(pixels, 0, pixels.Length);

            //Encoding the data of the PixelBuffer we have from the writable bitmap
            var inMemoryRandomStream = new InMemoryRandomAccessStream();

            BitmapEncoder encoder;
            switch (openedFileType.ToLower())
            {
                case ".jpg":
                    encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream);
                    break;
                case ".png":
                    encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, inMemoryRandomStream);
                    break;
                case ".bmp":
                    encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, inMemoryRandomStream);
                    break;
                case ".gif":
                    encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.GifEncoderId, inMemoryRandomStream);
                    break;
                case ".jpeg":
                    encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream);
                    break;
                default:
                    encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream);
                    break;
            }

            //var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream);
            encoder.SetPixelData(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Straight,
                (uint)sourceWriteBitmap.PixelWidth,
                (uint)sourceWriteBitmap.PixelHeight,
                96, 96, pixels);
            await encoder.FlushAsync();

            // At this point we have an encoded image in inMemoryRandomStream
            // We apply the transform and decode

            BitmapRotation r;
            switch (rotateParam)
            {
                case (1):
                    r = BitmapRotation.Clockwise180Degrees;
                    break;

                case (2):
                    r = BitmapRotation.Clockwise270Degrees;
                    break;

                case (3):
                    r = BitmapRotation.Clockwise90Degrees;
                    break;

                default:
                    r = BitmapRotation.None;
                    break;


            }

            var transform = new BitmapTransform
            {
                Rotation = r
            };

            //inMemoryRandomStream.Seek(0);

            var decoder = await BitmapDecoder.CreateAsync(inMemoryRandomStream);
            var pixelData = await decoder.GetPixelDataAsync(
                            BitmapPixelFormat.Bgra8,
                            BitmapAlphaMode.Straight,
                            transform,
                            ExifOrientationMode.IgnoreExifOrientation,
                            ColorManagementMode.DoNotColorManage);

            // An array containing the decoded image data
            var sourceDecodedPixels = pixelData.DetachPixelData();

            // Approach 1 : Encoding the image buffer again:
            // Encoding data
            var inMemoryRandomStream2 = new InMemoryRandomAccessStream();

            BitmapEncoder encoder2;
            switch (openedFileType.ToLower())
            {
                case ".jpg":
                    encoder2 = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream2);
                    break;
                case ".png":
                    encoder2 = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, inMemoryRandomStream2);
                    break;
                case ".bmp":
                    encoder2 = await BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, inMemoryRandomStream2);
                    break;
                case ".gif":
                    encoder2 = await BitmapEncoder.CreateAsync(BitmapEncoder.GifEncoderId, inMemoryRandomStream2);
                    break;
                case ".jpeg":
                    encoder2 = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream2);
                    break;
                default:
                    encoder2 = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, inMemoryRandomStream2);
                    break;
            }

            encoder2.SetPixelData(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Ignore,
                width, height, 96, 96,
                sourceDecodedPixels
                );

            await encoder2.FlushAsync();
            inMemoryRandomStream2.Seek(0);
            // finally the resized writablebitmap
            var bitmap = new WriteableBitmap((int)width, (int)height);
            await bitmap.SetSourceAsync(inMemoryRandomStream2);

            mainContainer.Source = bitmap;
            return bitmap;
        }


        private void efect1_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            r1.Visibility = Visibility.Visible;
        }

        private void efect1_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            r1.Visibility = Visibility.Collapsed;
        }

        private void efect2_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            r2.Visibility = Visibility.Visible;

        }

        private void efect3_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            r3.Visibility = Visibility.Visible;

        }

        private void efect4_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            r4.Visibility = Visibility.Visible;

        }

        private void efect5_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            r5.Visibility = Visibility.Visible;

        }

        private void efect2_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            r2.Visibility = Visibility.Collapsed;

        }

        private void efect3_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            r3.Visibility = Visibility.Collapsed;

        }

        private void efect4_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            r4.Visibility = Visibility.Collapsed;

        }

        private void efect5_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            r5.Visibility = Visibility.Collapsed;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            rotateImage(currentBitmap, 1);
        }

        bool pressedOnce = false;
        bool pressedTwice = false;
        bool pressedThrice = false;

        Point firstPoint;
        Point secondPoint;

        private void mainContainer_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (!pressedOnce)
            {
                pressedOnce = true;
                PointerPoint pt = e.GetCurrentPoint(mainContainer);
                firstPoint = pt.Position;
                resBox.Text = firstPoint.Y + " " + firstPoint.X;
            }
            else if (!pressedTwice)
            {
                pressedTwice = true;
                Point p = new Point();
                PointerPoint pt = e.GetCurrentPoint(mainContainer);
                secondPoint = pt.Position;

                try
                {
                    selectionBox.Height = Math.Abs(firstPoint.Y - secondPoint.Y);
                    selectionBox.Width = Math.Abs(firstPoint.X - secondPoint.X);
                }
                catch
                {
                }

                double smallestX = Math.Min(firstPoint.X, secondPoint.X);
                double smallestY = Math.Min(firstPoint.Y, secondPoint.Y);


                selectionBox.Margin = new Thickness((mainContainerGrid.Width - mainContainer.ActualWidth) / 2 + smallestX,
                    (mainContainerGrid.Height - mainContainer.ActualHeight) / 2 + smallestY,
                    0, 0);

                selectionBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else if (!pressedThrice)
            {
                pressedOnce = false;
                pressedTwice = false;
                Point p = new Point(0, 0);
                firstPoint = secondPoint = p;
                selectionBox.Height = 0;
                selectionBox.Width = 0;
                selectionBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void mainContainer_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (pressedOnce && !pressedTwice)
            {
                PointerPoint pt = e.GetCurrentPoint(mainContainer);
                Point currentPoint = pt.Position;

                double smallestX = Math.Min(firstPoint.X, currentPoint.X);
                double smallestY = Math.Min(firstPoint.Y, currentPoint.Y);


                selectionBox.Height = Math.Abs(firstPoint.Y - currentPoint.Y);
                selectionBox.Width = Math.Abs(firstPoint.X - currentPoint.X);
                

                selectionBox.Margin = new Thickness((mainContainerGrid.Width - mainContainer.ActualWidth) / 2 + smallestX,
                    (mainContainerGrid.Height - mainContainer.ActualHeight) / 2 + smallestY,
                    0, 0);

                selectionBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            
            }
        }

      
}
}
