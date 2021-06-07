/* June P - Mandelbrot Fractal Generator Without Tutorial
 * 
 * Version 1.0 - 2:28 AM 6-07-2021
 *   - Currently generates fractals and zooms at good perceptive rate
 *   - Iteration control needs more work
 *   - SINGLE THREADED
 *   - No anti-aliasing
 *   - Only single-spectrum color management
 *   
 * *Desired Features* for Version 1.1
 *   - Multithreaded process for speed optimization
 *   - TEST FOR FOR-LOOP ERRORS
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace FastMandelbrotGenTesting
{
    class Functioning
    {       
        static void Main(string[] args)

        {
            // Console.WriteLine("X Dimension: ");
            // int xsize = Convert.ToInt32(Console.ReadLine());
            // Console.WriteLine("Y Dimension ");
            // int ysize = Convert.ToInt32(Console.ReadLine());
            int xsize = 1280;
            int ysize = 720;

            Console.WriteLine("How Many Frames? ");
            int framecount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i <= framecount; i++)
            {
                double zeem = 5.0 / Math.Pow(1.05, i); //PAY ATTENTION. SWITCH BACK TO i.
                float it = (float) Math.Round(16.0 * Math.Pow(1.02, i), 0);
                // Mandelbrot(xsize, ysize, 0.251, 0.00005, 32 + 2 * i, zeem, i.ToString());
                Mandelbrot(xsize, ysize, 0.179, 0.56001, it, zeem, i.ToString());
                Console.WriteLine("Frame " + i.ToString() + " Complete.");
            }
            
        }
        static void Mandelbrot(int resx, int resy, double originx, double originy, float max_iter, double zoom, string fileName)
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
                image.Save("frame_" + fileName + ".png");
            }
        }
    }
}