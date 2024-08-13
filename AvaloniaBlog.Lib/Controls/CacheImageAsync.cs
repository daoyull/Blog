using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Common.Lib.Helpers;
using Common.Lib.Ioc;
using Markdig.Avalonia.Controls;

namespace AvaloniaBlog.Lib.Controls;

public class CacheImageAsync : ImageAsync
{
    protected override void HandleUrlChanged(ImageAsync image, AvaloniaPropertyChangedEventArgs args)
    {
        if (args.NewValue is not string url || string.IsNullOrEmpty(url))
        {
            return;
        }

        ThreadPool.QueueUserWorkItem(LoadImage);

        async void LoadImage(object? callback)
        {
            try
            {
                var uri = new Uri(url);
                byte[] fileBytes;
                Bitmap? bitmap = null;
                if (uri.IsFile)
                {
                    fileBytes = await File.ReadAllBytesAsync(url);
                    bitmap = new Bitmap(new MemoryStream(fileBytes));
                    SetImage(bitmap);
                    return;
                }

                var cacheDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Cache");
                if (!Directory.Exists(cacheDirectory))
                {
                    Directory.CreateDirectory(cacheDirectory);
                }


                var extension = Path.GetExtension(url);
                var fileName = "cache" + Md5Helper.Md5(url) +
                               (string.IsNullOrEmpty(extension) ? "cache" : extension);
                var filePath = Path.Combine(cacheDirectory, fileName);
                // 是否已经缓存
                if (!File.Exists(filePath))
                {
                    using var httpClient = new HttpClient();
                    fileBytes = await httpClient.GetByteArrayAsync(uri);
                    await using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    // 将字节数组写入文件
                    await fileStream.WriteAsync(fileBytes, 0, fileBytes.Length);
                }
                else
                {
                    fileBytes = await File.ReadAllBytesAsync(filePath);
                }

                bitmap = new Bitmap(new MemoryStream(fileBytes));
                SetImage(bitmap);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void SetImage(Bitmap? bitmap)
        {
            if (bitmap != null)
            {
                Dispatcher.UIThread.Post(() => { image.Source = bitmap; });
            }
        }
    }
}