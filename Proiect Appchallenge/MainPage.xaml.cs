
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
        uint width, height;
        WriteableBitmap currentBitmap;
        BitmapImage originalBitmap;

        WriteableBitmap currentBitmapE1;
        WriteableBitmap currentBitmapE2;
        WriteableBitmap currentBitmapE3;
        WriteableBitmap currentBitmapE4;
        WriteableBitmap currentBitmapE5;

        void makePreview()
        {            
            aplicaEfect4();
            aplicaEfect5();
            aplicaEfect1();
            aplicaEfect2();
            aplicaEfect3();
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

        private async void aplicaEfect2()
        {
            byte[] grayscaled = new byte[sourcePixels.Length];

            try
            {
                grayscaled = raiseContrast(sourcePixels, 10);
                grayscaled = tint(grayscaled, 50, 5, 10);
                currentBitmapE2 = new WriteableBitmap((int)width, (int)height);

                //grayscaled = sourcePixels;

                using (Stream stream = currentBitmapE2.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(grayscaled, 0, grayscaled.Length);
                    efect2.Source = currentBitmapE2;
                }
            }
            catch
            {
            }
        }

        private async void aplicaEfect3()
        {
            byte[] grayscaled = new byte[sourcePixels.Length];

            try
            {
                grayscaled = raiseContrast(sourcePixels, 10);
                grayscaled = tint(grayscaled, 10, 0, 0);
                currentBitmapE3 = new WriteableBitmap((int)width, (int)height);

                //grayscaled = sourcePixels;

                using (Stream stream = currentBitmapE3.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(grayscaled, 0, grayscaled.Length);
                    efect3.Source = currentBitmapE3;
                }
            }
            catch
            {
            } 
        }

        private async void aplicaEfect1()
        {
            byte[] grayscaled = new byte[sourcePixels.Length];

            try
            {
                grayscaled = raiseContrast(sourcePixels, 5);
                grayscaled = tint(grayscaled, 25, 25, 0);
                currentBitmapE1 = new WriteableBitmap((int)width, (int)height);

                //grayscaled = sourcePixels;

                using (Stream stream = currentBitmapE1.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(grayscaled, 0, grayscaled.Length);
                    efect1.Source = currentBitmapE1;
                }
            }
            catch
            {
            } 
        }

        private async void aplicaEfect4()
        {
            byte[] grayscaled = new byte[sourcePixels.Length];

            try
            {
                grayscaled = raiseContrast(sourcePixels, 0);
                grayscaled = tint(grayscaled, 40, 20, 15);
                currentBitmapE4 = new WriteableBitmap((int)width, (int)height);

                //grayscaled = sourcePixels;

                using (Stream stream = currentBitmapE4.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(grayscaled, 0, grayscaled.Length);
                    efect4.Source = currentBitmapE4;
                }
            }
            catch
            {
            }
        }

        private async void aplicaEfect5()
        {
            byte[] grayscaled = new byte[sourcePixels.Length];

            try
            {
                grayscaled = raiseContrast(sourcePixels, 5);
                grayscaled = tint(grayscaled, 0, 0, 20);
                currentBitmapE5 = new WriteableBitmap((int)width, (int)height);

                //grayscaled = sourcePixels;

                using (Stream stream = currentBitmapE5.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(grayscaled, 0, grayscaled.Length);
                    efect5.Source = currentBitmapE5;
                }
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

                originalBitmap = new BitmapImage();

                originalBitmap.SetSource(img);
                width = Convert.ToUInt32(originalBitmap.PixelWidth);
                height = Convert.ToUInt32(originalBitmap.PixelHeight);

               // mainContainer.Source = originalBitmap; // show original image in MainPage                

                /*efect1.Source = originalBitmap; // show original image in MainPage
                efect2.Source = originalBitmap;
                efect3.Source = originalBitmap;
                efect4.Source = originalBitmap;
                efect5.Source = originalBitmap;
                */
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
                    switch (imgFile.FileType)
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
                    mainContainer.Source = currentBitmap;

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
                    switch (file.FileType)
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
            currentBitmap = currentBitmapE1;
            mainContainer.Source = currentBitmap;
        }

        private void efect2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            currentBitmap = currentBitmapE2;
            mainContainer.Source = currentBitmap;
        }

        private void efect3_PointerPressed_1(object sender, PointerRoutedEventArgs e)
        {
            currentBitmap = currentBitmapE3;
            mainContainer.Source = currentBitmap;
        }

        private void efect4_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            currentBitmap = currentBitmapE4;
            mainContainer.Source = currentBitmap;
        }

        private async void efect5_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            currentBitmap = currentBitmapE5;
            var v = await ResizeImage(currentBitmapE5, 300, 300);
            mainContainer.Source = v;
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
                ScaledHeight = height
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
    }
}
