/* Mandelbrot Fractal Generator Without Tutorial
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
 *   - Add Diagnostic Timer to time operation length
 *     - Possibly add a log file that logs frame number, iteration count, resolution, and time elapsed.
 *   - Benchmarked testing for finding optimal thread counts
 *   - Multi-color spectrum, test zooms for noisy flashes
 *   - Anti-Aliasing or Denoising if possible (Likely not possible)
 *   
 *   *NEW RANDOM EXTRA BULLSHIT*
 *   - MULTIBROTS WITH CUSTOM FILE NAMING
 *   - IT'S BULLSHIT. NOT MULTIBROTS ACTUALLY. FUCK IT.
 *   
 *   Update on early morning July 11 2021.
 *   - Complete reorganization into different classes
 *   - Added softened renders (take like 16x the time though, not including the resizing time)
 *   - 
 */

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Globalization;

using nom.tam.fits;
using nom.tam.image;
using nom.tam.util;

class Functioning
{
    public static readonly object locker = new object();
    static void Main(string[] args)
    {
        /* Use this to add prompts for resolution at program start. Make sure to remove the xsize and ysize variables below. */
        // Console.WriteLine("X Dimension: ");
        // int xsize = Convert.ToInt32(Console.ReadLine());
        // Console.WriteLine("Y Dimension ");
        // int ysize = Convert.ToInt32(Console.ReadLine());

        // int xsize = 640;
        // int ysize = 360;

        // Console.WriteLine("How Many Frames? ");
        // int framecount = Convert.ToInt32(Console.ReadLine());
        // int framecount = 120;

        // Summoners.ManSummoner(2560, 1440, -0.5, 0, 32, 12, 0, 1);
        // Summoners.ManSummoner(640, 360, -0.5, 0, 32, 12, 0, 0);
        // Summoners.MulSummoner(640, 360, 32, 128);

        // 3840*4, 1024
        Summoners.BudSummoner(2560, 1024, 69420);
        // Summoners.BudSummoner(2560, 2560, 2000, 12);
        // Summoners.BudSummoner(2560, 2560, 20000, 13);


        //for (int test = 0; test <= 192; test++)
        //{
        //    Summoners.BudSummoner(1080, 1080, (int) Math.Pow(2, (test + 12) / 12.0), test);
        //}

        Console.WriteLine("Complete.");
        Console.ReadKey();
    }
}
    
class Summoners
{
    public static void ManSummoner(int xsize, int ysize, double xcoord, double ycoord, int res, int framecount, byte animToggle, byte soft)
    {
        Console.WriteLine("Begin Render. . .");

        if (soft == 1)
        {
            if (animToggle == 1)
            {
                Stopwatch jimmy = new Stopwatch();

                // Initialize current thread count, limit to 4 threads max
                int threaders = 0;
                int max_threaders = 8; // SET THIS TO THE MAXIMUM PROCESSING CORES YOU CAN RUN

                jimmy.Start();
                for (int i = 0; i <= framecount; i++)
                {
                    checker:
                    if (threaders < max_threaders)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            // timmy.Start();
                            threaders++;
                            string name = i.ToString();

                            double zeem = 5.0 / Math.Pow(1.05, i);
                            // float it = (float) Math.Round(8.0 * Math.Pow(1.015, i), 0);

                            Generators.SoftMandelbrot(xsize, ysize, xcoord, ycoord, res, zeem, i);
                            // timmy.Stop();
                            // int ts = (int) timmy.ElapsedMilliseconds;

                            Console.WriteLine("Frame " + name + " Complete."); // + " RunTime " + ts.ToString() + " ms." );
                                                                               // timmy.Reset();
                            threaders--;
                        });
                        Thread.Sleep(0003);
                    }
                    else
                    {
                        goto checker;
                    }
                }
                jimmy.Stop();
                int js = (int)jimmy.ElapsedMilliseconds;

                Console.WriteLine("Total Time Elaspsed: " + js.ToString());
            }
            else
            {
                double zeem = 5.0 / Math.Pow(1.05, framecount);

                Generators.SoftMandelbrot(xsize, ysize, xcoord, ycoord, res, zeem, framecount);

                Console.WriteLine("Single Frame Render Complete.");
            }
        }
        else
        {
            if (animToggle == 1)
            {
                Stopwatch jimmy = new Stopwatch();

                // Initialize current thread count, limit to 4 threads max
                int threaders = 0;
                int max_threaders = 8; // SET THIS TO THE MAXIMUM PROCESSING CORES YOU CAN RUN

                jimmy.Start();
                for (int i = 0; i <= framecount; i++)
                {
                    checker:
                    if (threaders < max_threaders)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            // timmy.Start();
                            threaders++;
                            string name = i.ToString();

                            double zeem = 5.0 / Math.Pow(1.05, i);
                            // float it = (float) Math.Round(8.0 * Math.Pow(1.015, i), 0);

                            Generators.Mandelbrot(xsize, ysize, xcoord, ycoord, res, zeem, i);
                            // timmy.Stop();
                            // int ts = (int) timmy.ElapsedMilliseconds;

                            Console.WriteLine("Frame " + name + " Complete."); // + " RunTime " + ts.ToString() + " ms." );
                                                                               // timmy.Reset();
                            threaders--;
                        });
                        Thread.Sleep(0003);
                    }
                    else
                    {
                        goto checker;
                    }
                }
                jimmy.Stop();
                int js = (int)jimmy.ElapsedMilliseconds;

                Console.WriteLine("Total Time Elaspsed: " + js.ToString());
            }
            else
            {
                double zeem = 5.0 / Math.Pow(1.05, framecount);

                Generators.Mandelbrot(xsize, ysize, xcoord, ycoord, res, zeem, framecount);

                Console.WriteLine("Single Frame Render Complete.");
            }
        }
    }

    public static void MulSummoner(int xsize, int ysize, int res, int framecount)
    {
        //Stopwatch jimmy = new Stopwatch();

        int threaders = 0;
        int max_threaders = 8;

        //jimmy.Start();
        for (int i = 0; i <= framecount; i++)
        {
            checker:
            if (threaders < max_threaders)
            {
                Task.Factory.StartNew(() =>
                {
                    threaders++;
                    string name = i.ToString();

                    // double zeem = 5.0 / Math.Pow(1.05, i);

                    int p = 2 * i;

                    Generators.Multibrot(xsize, ysize, 0.9, 0, res, 0.1125, i, p);

                    Console.WriteLine("Frame " + name + " Complete.");
                    threaders--;
                });
                Thread.Sleep(0075);
            }
            else
            {
                goto checker;
            }
        }
        //jimmy.Stop();
        //int js = (int)jimmy.ElapsedMilliseconds;

        //Console.WriteLine("Total Time Elaspsed: " + js.ToString());
    }

    public static void BudSummoner(int xsize, int res, int frame)
    {
        // Console.WriteLine("Begin Render. . .");
        Generators.Buddhabrot(xsize, xsize, 0, 0, res, frame);
    }
}

class Generators
{
    public static void Mandelbrot(int resx, int resy, double originx, double originy, float max_iter, double zoom, int frame)
    {
        using (Image<Rgba32> image = new Image<Rgba32>(resx, resy))
        {
            //Define Other Variables
            double x0;      // Initial x coord
            double y0;      // Iniital y coord
            double a;       // "x" in equations
            double b;       // "y" in equations
            double a2;
            double b2;
            float iter;     // Current iteration count. It's a float for now due to the variability needed. Will be an int again in the future


            // Goes through every pixel in render resolution, calculates the output and assigns it a color.
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

                    a2 = 0.0;
                    b2 = 0.0;

                    // While the iteration result is within the 2r circle, and the iterations are below the limit.
                    while (a2 + b2 <= 1 << 16 & iter <= max_iter)
                    {
                        // Funky Math Stuff

                        b = (a + a) * b + y0;
                        a = a2 - b2 + x0;

                        a2 = a * a;
                        b2 = b * b;
                        iter++;
                    }


                    // COLOR MANAGEMENT. NEEDS FUTURE WORK FOR MULTI-COLOR, ITERATION-INDEPENDENT RENDERS
                    // Make center set black, vary color for all else
                    if (iter <= max_iter)
                    {
                        float s = iter / max_iter;
                        pixelRowSpan[x] = new Rgba32(s, s, s, 255.0f);
                    }
                    else
                    {
                        pixelRowSpan[x] = new Rgba32(0.0f, 0.0f, 0.0f, 255.0f);
                    }
                }
            }

            // image.Mutate(x => x.Resize(resx / 4, 0, KnownResamplers.Welch));

            lock (Functioning.locker)
            {
                image.Save("frame_" + frame + ".png");
            }
        }
    }

    public static void SoftMandelbrot(int resx, int resy, double originx, double originy, float max_iter, double zoom, int frame)
    {
        using (Image<Rgba32> image = new Image<Rgba32>(resx, resy))
        {
            //Define Other Variables
            double x0;      // Initial x coord
            double y0;      // Iniital y coord
            double a;       // "x" in equations
            double b;       // "y" in equations
            double a2;
            double b2;
            float iter;     // Current iteration count. It's a float for now due to the variability needed. Will be an int again in the future


            // Goes through every pixel in render resolution, calculates the output and assigns it a color.
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

                    a2 = 0.0;
                    b2 = 0.0;

                    // While the iteration result is within the 2r circle, and the iterations are below the limit.
                    while (a2 + b2 <= 1 << 16 & iter <= max_iter)
                    {
                        // Funky Math Stuff

                        b = (a + a) * b + y0;
                        a = a2 - b2 + x0;

                        a2 = a * a;
                        b2 = b * b;
                        iter++;
                    }


                    // COLOR MANAGEMENT. NEEDS FUTURE WORK FOR MULTI-COLOR, ITERATION-INDEPENDENT RENDERS
                    // Make center set black, vary color for all else
                    if (iter <= max_iter)
                    {
                        float s = iter / max_iter;
                        pixelRowSpan[x] = new Rgba32(s, s, s, 255.0f);
                    }
                    else
                    {
                        pixelRowSpan[x] = new Rgba32(0.0f, 0.0f, 0.0f, 255.0f);
                    }
                }
            }

            image.Mutate(x => x.Resize(resx / 4, 0, KnownResamplers.Welch));

            lock (Functioning.locker)
            {
                image.Save("softened_frame_" + frame + ".png");
            }
        }
    }

    public static void Multibrot(int resx, int resy, double originx, double originy, float max_iter, double zoom, int frame, int power)
    {
        using (Image<Rgba32> image = new Image<Rgba32>(resx, resy))
        {
            //Define Other Variables
            double x0;      // Initial x coord
            double y0;      // Iniital y coord
            double a;       // "x" in equations
            double b;       // "y" in equations
            double a2;
            double b2;
            float iter;     // Current iteration count. It's a float for now due to the variability needed. Will be an int again in the future


            // Goes through every pixel in render resolution, calculates the output and assigns it a color.
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

                    a2 = 0.0;
                    b2 = 0.0;

                    // While the iteration result is within the 2r circle, and the iterations are below the limit.
                    while (a2 + b2 <= 1 << 16 & iter <= max_iter)
                    {
                        // Funky Math Stuff

                        b = (a + a) * b + y0;
                        a = a2 - b2 + x0;

                        a2 = Math.Pow(a, power);
                        b2 = Math.Pow(b, power);
                        iter++;
                    }

                    if (iter <= max_iter)
                    {
                        float shade = iter / max_iter;
                        pixelRowSpan[x] = new Rgba32(shade, shade, shade, 255.0f);

                    }
                    else
                    {
                        pixelRowSpan[x] = new Rgba32(0.0f, 0.0f, 0.0f, 255.0f);
                    }
                }
            }
            lock (Functioning.locker)
            {
                image.Save("frame_mul_" + frame + ".png");
            }
        }
    }

    public static void Buddhabrot(double resx, double resy, double originx, double originy, double max_iter, int frame)
    {
        using (Image<Rgba32> bimage = new Image<Rgba32>((int) resx, (int) resy))
        {
            // Point-Interaction Counter or whatever
            int[][] hitTicker = new int[(int) resy][];

            for (int aa = 0; aa < resy; aa++)
            {
                hitTicker[aa] = new int[(int) resx];
            }

            //Define Other Variables
            double x0;      // Initial x coord
            double y0;      // Iniital y coord
            double a;       // "x" in equations
            double b;       // "y" in equations
            double a2;
            double b2;
            double iter;     // Current iteration count. It's a float for now due to the variability needed. Will be an int again in the future

            // Used for array saving... Hopefully.. 
            int xloc;
            int yloc;

            float shade;

            // Goes through every pixel in render resolution, calculates the output and assigns it a color.
            for (double y = 0; y < bimage.Height; y++)
            {
                for (double x = 0; x < bimage.Width; x++)
                {
                    // Set initial values using current pixel, scale and transform to custom coordinate system.
                    x0 = (double) (((double) x - ((double) resx / (double) 2.0)) * ((double) 3.5 / (double) resy) + (double) originx);
                    y0 = (double) (((double) y - ((double) resy / (double) 2.0)) * ((double) 3.5 / (double) resy) + (double) originy);

                    // Initialize values for new pixel.
                    a = (double) 0.0;
                    b = (double) 0.0;
                    iter = (double) 0.0;

                    a2 = (double) 0.0;
                    b2 = (double) 0.0;

                    // While the iteration result is within the 2r circle, and the iterations are below the limit.
                    while (a2 + b2 <= 4 & iter <= max_iter)
                    {
                        // Funky Math Stuff
                        b = (double) (a + a) * b + y0;
                        a = (double) a2 - b2 + x0;

                        a2 = (double) a * a;
                        b2 = (double) b * b;

                        xloc = (int) (((a - (double) originx) * (resy / (double) 3.5)) + (resx / (double) 2.0));
                        yloc = (int) (((b - (double) originy) * (resy / (double) 3.5)) + (resy / (double) 2.0));

                        if (xloc >= 0 & xloc < resx & yloc >= 0 & yloc < resy)
                        {
                            hitTicker[xloc][yloc] = (short) (hitTicker[xloc][yloc] + 2); //Center pixel, full value.

                            // Wider radius, may add fade later
                            if (xloc != resx - 1 & xloc != 0)
                            {
                                hitTicker[xloc - 1][yloc]++;
                                hitTicker[xloc + 1][yloc]++;
                            }
                            if (yloc != resy - 1 & yloc != 0)
                            {
                                hitTicker[xloc][yloc - 1]++;
                                hitTicker[xloc][yloc + 1]++;
                            }

                        }
                        iter++;
                    }
                }
            }

            int bmax = hitTicker[0][0];
            for (int k = 0; k < resy; k++)
            {
                for (int l = 0; l < resx; l++)
                {
                    if (hitTicker[k][l] > bmax)
                    {
                        bmax = hitTicker[k][l];
                    }
                }
            }

            Console.WriteLine("");
            Console.WriteLine("MAXIMUM = " + bmax);

            for (int i = 0; i < resy; i++)
            {
                Span<Rgba32> pixelRowSpan = bimage.GetPixelRowSpan(i);
                for (int j = 0; j < resx; j++)
                {
                    shade = (float) Math.Pow((double) hitTicker[i][j] / (double) bmax, 1.0 / 3.0);
                    // shade = (float) hitTicker[i, j] / (float) bmax;
                    pixelRowSpan[j] = new Rgba32(shade, shade, shade, 255.0f);
                }
            }

            bimage.Save("Buddhabrot-" + frame + "_" + max_iter + ".png");
            bimage.Save("recent-buhhdabrot.png");
            bimage.Mutate(x => x.Resize((int) resx / 4, 0, KnownResamplers.Welch));
            bimage.Save("Buddhabrot-" + frame + "_" + max_iter + "soft.png");

            // Console.WriteLine(hitTicker.Rank);
            // Console.WriteLine(hitTicker.GetLength(0));
            // Console.WriteLine(hitTicker.GetLength(1));

            Exporter.SaveFITS(hitTicker, (int) resx, (int) resy);

        }
    }
}

class Exporter
{
    public static void SaveFITS(int[][] loc, int resx, int resy)
    {
        Fits f = new Fits();
        ImageHDU hdu = (ImageHDU) Fits.MakeHDU(loc);
        //BasicHDU hdu = Fits.MakeHDU(loc);
        // hdu.AddValue("BITPIX", 16, null);  // set bit depth of 16 bit
        // hdu.AddValue("NAXIS", 2, null);    // 2D-image
        //hdu.AddValue("NAXIS1", resx);
        //hdu.AddValue("NAXIS2", resy, null);
        f.AddHDU(hdu);
        //fits.AddHDU(hdu);
        BufferedFile file = new BufferedFile("test.fits", FileAccess.ReadWrite, FileShare.ReadWrite);
        f.Write(file);
        file.Close();
    }
}