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
        static int resx = 2560;
        static int resy = 1440;
        
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
                float iter;
                float max_iter = 16; //ITERATION COUNT. VERY IMPORTANT!!

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

                        x0 = (float) (x - (resx / 2.0f)) * (2.5f / (resx / 2.0f));
                        y0 = (float) (y - (resy / 2.0f)) * (2.0f / resy);
                        a = 0.0f;
                        b = 0.0f;
                        iter = 0.0f;

                        while ((a * a) + (b * b) <= 2.0f * 2.0f & iter <= max_iter)
                        {
                            atemp = (a * a) - (b * b) + x0;
                            b = 2.0f * a * b + y0;
                            a = atemp;
                            iter++;
                        }

                        // float shade = iter;
                        // Output pixel with appropriate color
                        // pixelRowSpan[x] = new Rgba32(shade, shade, shade, 255.0f);

                        if (iter == max_iter | iter - 1 == max_iter)
                        {
                            pixelRowSpan[x] = new Rgba32(0.0f, 0.0f, 0.0f, 255.0f);
                        }
                        else
                        {
                            pixelRowSpan[x] = new Rgba32((float)iter / max_iter, 0.0f, 0.0f, 255.0f);
                        }

                    }
                }
                image.Save("1p.png");
            }
        }
    }
}