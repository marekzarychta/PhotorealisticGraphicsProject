namespace Foto.Math;
using System;
using System.IO;

public class Image
    {
        public int Width { get; }
        public int Height { get; }
        
        private readonly RGB[] _pixels;

        public Image(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Szerokość i wysokość muszą być większe od 0.");

            Width = width;
            Height = height;
            _pixels = new RGB[width * height];
        }

        public void SetPixel(int x, int y, RGB color)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;

            _pixels[y * Width + x] = color;
        }

        public void SetPixel(int x, int y, float r, float g, float b)
        {
            byte rByte = (byte)(Math.Clamp(r, 0.0f, 1.0f) * 255.999f);
            byte gByte = (byte)(Math.Clamp(g, 0.0f, 1.0f) * 255.999f);
            byte bByte = (byte)(Math.Clamp(b, 0.0f, 1.0f) * 255.999f);

            SetPixel(x, y, new RGB(rByte, gByte, bByte));
        }

        public void SaveToPPM(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("P3");
                writer.WriteLine($"{Width} {Height}");
                writer.WriteLine("255");

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        RGB color = _pixels[y * Width + x];
                        writer.WriteLine($"{color.r} {color.g} {color.b}");
                    }
                }
            }
        }
    }