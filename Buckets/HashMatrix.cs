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
        private static int width = 500;
        private static int height = 500;
        private static long[,] pixelMatrix = new long[width, height];
        private static long highVal = 0;

        //3D properties
        private static Surface3DRenderer renderer3d;
        private static bool isMouseLeftButtonDown = false;
        private static bool isMouseRightButtonDown = false;
        private static double zoom = 0.5;
        private static Point mouseLocation;
        private static Point screenXY = new Point(0, 0);
        private static Point3D observableXYZ = new Point3D(width / 3, height / 3, 50);

        public static int LoadMultiplier
        {
            get;
            set;
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
            MultiThreaded = true;
            LoadMultiplier = 1;
            BlackAndWhite = false;
            CapturingMouse_3DSurface = false;
        }

        /// <summary>
        /// Fill the internal pixel matrix according to the specified hash function.
        /// </summary>
        /// <param name="function">The hash function to fill according to.</param>
        public static void ApplyHash(int stringSize, Func<string, uint, uint> function)
        {
            pixelMatrix = new long[width, height];
            long range = pixelMatrix.GetLength(0) * pixelMatrix.GetLength(1);

            //fill pixel matrix with number of times a bucket gets a value
            if (MultiThreaded)
            {
                ParallelPlus.StridingFor(0, pixelMatrix.GetLength(0) * pixelMatrix.GetLength(1) * LoadMultiplier, 100000, i =>
                {
                    long position = function(Generator.RandomAlphaNumeric(stringSize), (uint)range);
                    long row = position / pixelMatrix.GetLength(1);
                    long column = position % pixelMatrix.GetLength(0);
                    pixelMatrix[row, column] += 1;
                });
            }
            else
            {
                for (int i = 0; i < pixelMatrix.GetLength(0) * pixelMatrix.GetLength(1) * LoadMultiplier; i++)
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
                ParallelPlus.StridingFor(0, pixelMatrix.GetLength(0), 100, x =>
                {
                    for(int y = 0; y < pixelMatrix.GetLength(1); y++)
                    {
                        if (pixelMatrix[x, y] > highVal) Interlocked.Exchange(ref highVal, pixelMatrix[x, y]); 
                    }
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

        public static void Draw2DGradiant(PictureBox pBox)
        {
            //steal back mouse controls
            if (CapturingMouse_3DSurface) CapturingMouse_3DSurface = false;

            if (BlackAndWhite)
            {
                double valueProportion = 255 / highVal;
                //Create and fill a bitmap
                Bitmap image = new Bitmap(width, height);
                for (int x = 0; x < pixelMatrix.GetLength(0); x++)
                {
                    for (int y = 0; y < pixelMatrix.GetLength(1); y++)
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
                double valueProportion = int.MaxValue / highVal;
                //Create and fill a bitmap
                Bitmap image = new Bitmap(width, height);
                for (int x = 0; x < pixelMatrix.GetLength(0); x++)
                {
                    for (int y = 0; y < pixelMatrix.GetLength(1); y++)
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
