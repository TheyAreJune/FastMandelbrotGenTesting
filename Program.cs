using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace FastMandelbrotGenTesting
{
    class RoughDraft
    {
        static int resx = 350;
        static int resy = 200;
        
        static void Main(string[] args)
        {
            using (Image<Rgba32> image = new Image<Rgba32>(resx, resy))
            {
                //Define Other Variables
                float x0;
                float y0;
                float a;
                float b;
                float atemp;
                int iter;
                int max_iter = 64; //ITERATION COUNT. VERY IMPORTANT!!

                // Color Switcher
                float grayscale;

                for (int y = 0; y < image.Height; y++)
                {
                    Span<Rgba32> pixelRowSpan = image.GetPixelRowSpan(y);
                    for (int x = 0; x < image.Width; x++)
                    {
                        /*
                        *  if (Math.Round(Math.Sin(x) * resy, 0) == y)
                        *  {
                        *      pixelRowSpan[x] = new Rgba32(255.0f, 255.0f, 255.0f, 255.0f);
                        *  }
                        *  else
                        *  {
                        *      pixelRowSpan[x] = new Rgba32(0.0f, 0.0f, 0.0f, 255.0f);
                        *  }
                        */

                        x0 = (x - (resx / 2)) * (2.5f / (resx / 2));
                        y0 = (x - (resy / 2)) * (2 / resy);
                        a = 0.0f;
                        b = 0.0f;
                        iter = 0;

                        while ((a * a) + (b * b) <= 2 * 2 & iter <= max_iter)
                        {
                            atemp = (a * a) - (b * b) + x0;
                            b = 2 * a * b + y0;
                            a = atemp;
                            iter++;
                        }


                        grayscale = (iter / max_iter) * 255;
                        // Output pixel with appropriate color
                        pixelRowSpan[x] = new Rgba32(grayscale, grayscale, grayscale, 255.0f);

                    }
                }   
                image.Save("1a.png");
            }
        }
    }
}