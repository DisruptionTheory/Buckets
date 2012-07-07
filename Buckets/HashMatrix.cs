using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Buckets
{
    public static class HashMatrix
    {
        private static int width = 1000;
        private static int height = 1000;
        private static long[,] pixelMatrix = new long[width, height];
        private static int loadMultiplier = 100;
        private static long highVal = 0;

        public static bool MultiThreaded
        {
            get;
            set;
        }

        static HashMatrix()
        {
            MultiThreaded = true;
        }

        /// <summary>
        /// Fill the internal pixel matrix according to the specified hash function.
        /// </summary>
        /// <param name="function">The hash function to fill according to.</param>
        public static void ApplyHash(int stringSize, Func<string, uint, uint> function)
        {
            pixelMatrix = new long[1000, 1000];
            long range = pixelMatrix.GetLength(0) * pixelMatrix.GetLength(1);

            //fill pixel matrix with number of times a bucket gets a value
            if (MultiThreaded)
            {
                Parallel.For(0, pixelMatrix.GetLength(0) * pixelMatrix.GetLength(1) * loadMultiplier, i =>
                {
                    long position = function(Generator.RandomAlphaNumeric(stringSize), (uint)range);
                    long row = position / pixelMatrix.GetLength(1);
                    long column = position % pixelMatrix.GetLength(0);
                    pixelMatrix[row, column] += 1;
                });
            }
            else
            {
                for (int i = 0; i < pixelMatrix.GetLength(0) * pixelMatrix.GetLength(1) * loadMultiplier; i++)
                {
                    long position = (int)function(Generator.RandomAlphaNumeric(stringSize), (uint)range);
                    long x = position % pixelMatrix.GetLength(1);
                    long y = position / pixelMatrix.GetLength(0);
                    pixelMatrix[x, y] += 1;
                }
            }

            //find highest value bucket to adjust color proportions accordingly.
            highVal = 0;
            if (MultiThreaded)
            {
                Parallel.For(0, pixelMatrix.GetLength(0), x =>
                {
                    Parallel.For(0, pixelMatrix.GetLength(1), y =>
                    {
                        if (pixelMatrix[x, y] > highVal) Interlocked.Exchange(ref highVal, pixelMatrix[x, y]); 
                    });
                });                
            }
            else
            {
                for (int x = 0; x < pixelMatrix.GetLength(0); x++)
                {
                    for (int y = 0; y < pixelMatrix.GetLength(1); y++)
                    {
                        if (pixelMatrix[x, y] > highVal) highVal = pixelMatrix[x, y];
                    }
                }
            }
        }

        public static void DrawImage(PictureBox pBox)
        {
            double valueProportion = int.MaxValue / highVal;
            //Create and fill a bitmap
            Bitmap image = new Bitmap(width, height);
            for (int x = 0; x < pixelMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < pixelMatrix.GetLength(1); y++)
                {
                    double pixelValue = pixelMatrix[x,y] * valueProportion;
                    int roundedValue = (int)Math.Floor(pixelValue);
                    image.SetPixel(x,y, Color.FromArgb(roundedValue));
                }
            }
            //set image on picturebox
            pBox.Image = image;
        }

    }
}
