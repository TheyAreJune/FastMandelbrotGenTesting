/* June P - Mandelbrot Fractal Generator Without Tutorial
 * 
 * Version 1.0 - 2:28 AM 6-07-2021
 *   - Currently generates fractals and zooms at good perceptive rate
 *   - Iteration control needs more work
 *   - SINGLE THREADED
 *   - No anti-aliasing
 *   - Only single-spectrum color management
 *   
 * Version 1.1 - 11:57 AM 6-08-2021
 *   - Multi-threaded
 *     - Can set limit on maximum thread count
 *     - File stream is locked so only one file can be written at a time, no overwrites
 *   - Iteration control is better, needs improvement
 *   - Color spectrum control needed, makes iteration control irrelevant
 *   - No errors in the for loop skipping numbers now
 * 
 * *Desired Features* for Version 1.2
 *   - Benchmarked testing for finding optimal thread counts
 *   - Multi-color spectrum, test zooms for noisy flashes
 *   - Anti-Aliasing or Denoising if possible (Likely not possible)
 */

using System;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
// using SixLabors.ImageSharp.Processing; (Possible use for anti-aliasing later on)

namespace FastMandelbrotGenTesting
{
    class Functioning
    {       

        private static readonly object locker = new object();

        static void Main(string[] args)

        {
            // Gives script stronger power, faster speeds. Instantly crashes PC too.
            // Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            // Console.WriteLine("X Dimension: ");
            // int xsize = Convert.ToInt32(Console.ReadLine());
            // Console.WriteLine("Y Dimension ");
            // int ysize = Convert.ToInt32(Console.ReadLine());
            int xsize = 1920;
            int ysize = 1080;

            int counter = 0;

            int threaders = 0;
            int max_threaders = 4;

            Console.WriteLine("How Many Frames? ");
            int framecount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i <= framecount; i++)
            {
                checker:
                    if (threaders < max_threaders)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            threaders++;
                            string name = counter.ToString();
                            // Faster speeds, better, instantly crashes pc lmao
                            // Thread.CurrentThread.Priority = ThreadPriority.Highest;

                            double zeem = 5.0 / Math.Pow(1.05, i);
                            float it = (float) Math.Round(8.0 * Math.Pow(1.015, i), 0);
                                
                            Mandelbrot(xsize, ysize, 0.251, 0.00005, it, zeem, counter);
                            // Mandelbrot(xsize, ysize, 0.179, 0.56001, it, zeem, counter);
                            Console.WriteLine("Frame " + name + " Complete.");
                            threaders--;
                        });
                        Thread.Sleep(0010);
                    }
                    else
                    {
                        goto checker;
                    }
                
                    counter++;
            }

            Console.ReadKey();
            
        }
        static void Mandelbrot(int resx, int resy, double originx, double originy, float max_iter, double zoom, int frame)
        {
            using (Image<Rgba32> image = new Image<Rgba32>(resx, resy))
            {
                //Define Other Variables
                double x0;
                double y0;
                double a;
                double b;
                double atemp;
                float iter;

                for (int y = 0; y < image.Height; y++)
                {
                    Span<Rgba32> pixelRowSpan = image.GetPixelRowSpan(y);
                    for (int x = 0; x < image.Width; x++)
                    {
                        // Set initial values using current pixel, scale and transform to custom coordinate system.
                        x0 = (x - (resx / 2.0)) * (zoom / resy) + originx;
                        y0 = (y - (resy / 2.0)) * (zoom / resy) + originy;

                        // Initialize values for new pixel.
                        a = 0.0;
                        b = 0.0;
                        iter = 0.0f;

                        // While the iteration result is within the 2r circle, and the iterations are below the limit.
                        while ((a * a) + (b * b) <= 4.0 & iter <= max_iter)
                        {
                            // Funky Math Stuff
                            atemp = (a * a) - (b * b) + x0;
                            b = 2.0 * a * b + y0;
                            a = atemp;
                            iter++;
                        }

                        // Make center set black, vary color for all else
                        if (iter - 1 == max_iter)
                        {
                            pixelRowSpan[x] = new Rgba32(0.0f, 0.0f, 0.0f, 255.0f);
                        }
                        else
                        {
                            float shade = iter / max_iter;
                            pixelRowSpan[x] = new Rgba32(shade, shade, shade, 255.0f);
                        }

                    }
                }
                lock(locker)
                {
                    // long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    // string name = milliseconds.ToString();
                    // Random r = new Random();
                    image.Save("frame_" + frame + ".png");
                }
            }
        }
    }
}