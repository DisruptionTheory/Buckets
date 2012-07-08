using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Plot3D;


namespace Buckets
{
    public static class HashMatrix
    {
        private static int width = 600;
        private static int height = 600;
        private static long[,] pixelMatrix = new long[width, height];

        //3D properties
        private static Surface3DRenderer renderer3d;
        private static bool isMouseLeftButtonDown = false;
        private static bool isMouseRightButtonDown = false;
        private static double zoom = 0.5;
        private static Point mouseLocation;
        private static Point screenXY = new Point(0, 0);
        private static Point3D observableXYZ = new Point3D(width / 3, height / 3, 50);

        public static long KeyCount
        {
            get
            {
                return width * height * LoadMultiplier;
            }
        }

        public static int LoadMultiplier
        {
            get;
            set;
        }

        public static long HighVal
        {
            get;
            private set;
        }

        private static bool threading;
        public static bool MultiThreaded
        {
            get
            {
                return threading;
            }
            set
            {
                if (renderer3d != null) renderer3d.MultiThreaded = value;
                threading = value;
            }
        }

        public static bool BlackAndWhite
        {
            get;
            set;
        }

        public static short AdjustmentValue
        {
            get;
            set;
        }

        public static bool CapturingMouse_3DSurface
        {
            get;
            set;
        }
            

        static HashMatrix()
        {
            MultiThreaded = false;
            LoadMultiplier = 1;
            BlackAndWhite = false;
            CapturingMouse_3DSurface = false;
            HighVal = 0;
        }

        /// <summary>
        /// Fill the internal pixel matrix according to the specified hash function. using Randomly Generated strings.
        /// </summary>
        /// <param name="function">The hash function to fill according to.</param>
        public static void ApplyHashRandomString(int stringSize, Func<string, uint, uint> function)
        {
            pixelMatrix = new long[width, height];
            long range = width * height;

            //fill pixel matrix with number of times a bucket gets a value
            if (MultiThreaded)
            {
                ParallelPlus.StridingFor(0, range * LoadMultiplier, i =>
                {
                    long position = function(Generator.RandomAlphaNumeric(stringSize, true), (uint)range);
                    long y = position / height;
                    long x = position % width;
                    pixelMatrix[x, y] += 1;
                });
            }
            else
            {
                for (int i = 0; i < range * LoadMultiplier; i++)
                {
                    long position = function(Generator.RandomAlphaNumeric(stringSize), (uint)range);
                    long y = position / height;
                    long x = position % width;
                    pixelMatrix[x, y] += 1;
                }
            }
            FindHighVal();
        }

        /// <summary>
        /// Fill the internal pixel matrix according to the specified hash function. using Randomly Generated alpha numeric strings with special characters.
        /// </summary>
        /// <param name="function">The hash function to fill according to.</param>
        public static void ApplyHashRandomStringSpecial(int stringSize, Func<string, uint, uint> function)
        {
            pixelMatrix = new long[width, height];
            long range = width * height;

            //fill pixel matrix with number of times a bucket gets a value
            if (MultiThreaded)
            {
                ParallelPlus.StridingFor(0, range * LoadMultiplier, i =>
                {
                    long position = function(Generator.RandomAlphaNumericSpecial(stringSize, true), (uint)range);
                    long y = position / height;
                    long x = position % width;
                    pixelMatrix[x, y] += 1;
                });
            }
            else
            {
                for (int i = 0; i < range * LoadMultiplier; i++)
                {
                    long position = function(Generator.RandomAlphaNumericSpecial(stringSize), (uint)range);
                    long y = position / height;
                    long x = position % width;
                    pixelMatrix[x, y] += 1;
                }
            }
            FindHighVal();
        }

        /// <summary>
        /// Fill the internal pixel matrix according to the specified hash function. using numbers 0 to load limit converted to alpha numeric strings
        /// </summary>
        /// <param name="function">The hash function to fill according to.</param>
        public static void ApplyHashIncrementalString(Func<string, uint, uint> function)
        {
            pixelMatrix = new long[width, height];
            long range = width * height;

            //fill pixel matrix with number of times a bucket gets a value
            if (MultiThreaded)
            {
                ParallelPlus.StridingFor(0, range * LoadMultiplier, i =>
                {
                    long position = function(Generator.NumberToTextAlphaNumeric(i), (uint)range);
                    long y = position / height;
                    long x = position % width;
                    pixelMatrix[x, y] += 1;
                });
            }
            else
            {
                for (int i = 0; i < range * LoadMultiplier; i++)
                {
                    string o = Generator.NumberToTextAlphaNumeric(i);
                    long position = function(Generator.NumberToTextAlphaNumeric(i), (uint)range);
                    long y = position / height;
                    long x = position % width;
                    pixelMatrix[x, y] += 1;
                }
            }
            FindHighVal();
        }

        /// <summary>
        /// Fill the internal pixel matrix according to the specified hash function. using numbers 0 to load limit converted to random alpha numeric strings with special characters.
        /// </summary>
        /// <param name="function">The hash function to fill according to.</param>
        public static void ApplyHashIncrementalStringSpecial(Func<string, uint, uint> function)
        {
            pixelMatrix = new long[width, height];
            long range = width * height;

            //fill pixel matrix with number of times a bucket gets a value
            if (MultiThreaded)
            {
                ParallelPlus.StridingFor(0, range * LoadMultiplier, i =>
                {
                    long position = function(Generator.NumberToTextAlphaNumericSpecial(i), (uint)range);
                    long y = position / height;
                    long x = position % width;
                    pixelMatrix[x, y] += 1;
                });
            }
            else
            {
                for (int i = 0; i < range * LoadMultiplier; i++)
                {
                    string o = Generator.NumberToTextAlphaNumeric(i);
                    long position = function(Generator.NumberToTextAlphaNumericSpecial(i), (uint)range);
                    long y = position / height;
                    long x = position % width;
                    pixelMatrix[x, y] += 1;
                }
            }
            FindHighVal();
        }


        /// <summary>
        /// Fill the internal pixel matrix according to the specified hash function. using numbers 0 to load limit as strings.
        /// </summary>
        /// <param name="function">The hash function to fill according to.</param>
        public static void ApplyHashIncrementalNumerics(Func<string, uint, uint> function)
        {
            pixelMatrix = new long[width, height];
            long range = width * height;

            //fill pixel matrix with number of times a bucket gets a value
            if (MultiThreaded)
            {
                ParallelPlus.StridingFor(0, range * LoadMultiplier, i =>
                {
                    long position = function(i.ToString(), (uint)range);
                    long y = position / height;
                    long x = position % width;
                    pixelMatrix[x, y] += 1;
                });
            }
            else
            {
                for (int i = 0; i < range * LoadMultiplier; i++)
                {
                    long position = function(i.ToString(), (uint)range);
                    long y = position / height;
                    long x = position % width;
                    pixelMatrix[x, y] += 1;
                }
            }
            FindHighVal();
        }

        private static void FindHighVal()
        {
            //find highest value bucket to adjust color proportions accordingly.
            HighVal = 0;
            if (MultiThreaded)
            {
                ParallelPlus.StridingFor(0, width, x =>
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (pixelMatrix[x, y] > HighVal) HighVal = pixelMatrix[x, y];
                    }
                });
            }
            else
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (pixelMatrix[x, y] > HighVal) HighVal = pixelMatrix[x, y];
                    }
                }
            }
        }

        public static void Draw2DGradiant(PictureBox pBox)
        {
            //steal back mouse controls
            if (CapturingMouse_3DSurface) CapturingMouse_3DSurface = false;

            if (BlackAndWhite)
            {
                double valueProportion = 255 / HighVal;
                //Create and fill a bitmap
                Bitmap image = new Bitmap(width, height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        double pixelValue = pixelMatrix[x, y] * valueProportion;
                        int roundedValue = (int)Math.Floor(pixelValue);
                        image.SetPixel(x, y, Color.FromArgb(roundedValue, roundedValue, roundedValue));
                    }
                }
                //set image on picturebox
                pBox.Image = image;
            }
            else
            {
                double valueProportion = int.MaxValue / HighVal;
                //Create and fill a bitmap
                Bitmap image = new Bitmap(width, height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        double pixelValue = pixelMatrix[x, y] * valueProportion;
                        int roundedValue = (int)Math.Floor(pixelValue);
                        image.SetPixel(x, y, Color.FromArgb(roundedValue));
                    }
                }
                //set image on picturebox
                pBox.Image = image;
            }
            pBox.Invalidate();
        }

        public static void Draw3DSurface(PictureBox pBox)
        {
            //set up objects 
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            renderer3d = new Surface3DRenderer(observableXYZ.x, observableXYZ.y, observableXYZ.z, screenXY.X, screenXY.Y, width, height, zoom, 0, 0);
            renderer3d.MultiThreaded = MultiThreaded;

            //set mouse events
            pBox.Paint += Draw3DSurface_Paint;
            pBox.MouseWheel += Draw3DSurface_MouseWheel;
            pBox.MouseDown += Draw3DSurface_MouseDown;
            pBox.MouseUp += Draw3DSurface_MouseUp;
            pBox.MouseMove += Draw3DSurface_MouseMove;
            CapturingMouse_3DSurface = true;

            if (BlackAndWhite) renderer3d.ColorSchema = ColorSchema.Greyscale();
            else renderer3d.ColorSchema = ColorSchema.Hsv;

            //render surface
            renderer3d.RenderSurface(graphics, pixelMatrix, AdjustmentValue);
            graphics.Dispose();
            pBox.Image = bitmap;
        }

        public static void Draw3DSurface_Paint(Object sender, PaintEventArgs e)
        {
            PictureBox pBox = (PictureBox)sender;

            //remove any attached mouse events if we are no longer using the 3d surface
            if (!CapturingMouse_3DSurface)
            {
                pBox.Paint -= Draw3DSurface_Paint;
                pBox.MouseWheel -= Draw3DSurface_MouseWheel;
                pBox.MouseDown -= Draw3DSurface_MouseDown;
                pBox.MouseUp -= Draw3DSurface_MouseUp;
                pBox.MouseMove -= Draw3DSurface_MouseMove;
                return;
            }
            e.Graphics.Clear(pBox.BackColor);
            renderer3d.RenderSurface(e.Graphics, pixelMatrix, AdjustmentValue);
        }

        public static void Draw3DSurface_MouseWheel(Object sender, MouseEventArgs e)
        {
            zoom += (e.Delta > 0 ? 1 : -1) * 0.04;
            renderer3d.ReCalculateTransformationsCoeficients(observableXYZ.x, observableXYZ.y, observableXYZ.z, screenXY.X, screenXY.Y, width, height, zoom, 0, 0);
            ((PictureBox)sender).Invalidate();
        }

        public static void Draw3DSurface_MouseDown(Object sender, MouseEventArgs e)
        {
            mouseLocation = e.Location;
            if (e.Button == MouseButtons.Left) isMouseLeftButtonDown = true;
            if (e.Button == MouseButtons.Right) isMouseRightButtonDown = true;
        }

        public static void Draw3DSurface_MouseUp(Object sender, MouseEventArgs e)
        {
            if (isMouseLeftButtonDown && (e.Button == MouseButtons.Left)) isMouseLeftButtonDown = false;
            if (isMouseRightButtonDown && (e.Button == MouseButtons.Right)) isMouseRightButtonDown = false;
        }

        public static void Draw3DSurface_MouseMove(Object sender, MouseEventArgs e)
        {
            if (isMouseLeftButtonDown)
            {
                screenXY.X += e.Location.X - mouseLocation.X;
                screenXY.Y += e.Location.Y - mouseLocation.Y;
            }
            if (isMouseRightButtonDown)
            {
                double xDelta = e.Location.X - mouseLocation.X;
                double yDelta = e.Location.Y - mouseLocation.Y;
                observableXYZ.y -= xDelta;
                observableXYZ.z += yDelta;
            }
            mouseLocation = e.Location;
            if (isMouseLeftButtonDown || isMouseRightButtonDown)
            {
                renderer3d.ReCalculateTransformationsCoeficients(observableXYZ.x, observableXYZ.y, observableXYZ.z, screenXY.X, screenXY.Y, width, height, zoom, 0, 0);
                ((PictureBox)sender).Invalidate();
            }
        }
    }

    
}
