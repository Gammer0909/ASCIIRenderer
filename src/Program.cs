using System;
using Gammer0909.ASCIIRenderer.Rendering;
using Gammer0909.ASCIIRenderer.Image;
using Color = System.ConsoleColor;

namespace Gammer0909.ASCIIRenderer;

class Program
{
    static void Main()
    {
        // Your RGB values
        int red = 255;
        int green = 0;
        int blue = 0;

        Color c = Pixel.RGBToConsoleColor(red, green, blue);
        Graphics g = new();

        // Draw the pixel
        g.DrawPixel('A', c);

        Console.ReadKey();

    }

}
