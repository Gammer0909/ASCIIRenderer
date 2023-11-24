using System;
using System.Timers;
using Color = System.ConsoleColor;

using SkiaSharp;

namespace Gammer0909.ASCIIRenderer;

class Program {

    

    public static void Main() {

        

        // First get the image (./image.png) via SkiaSharp
        var image = SkiaSharp.SKBitmap.Decode("./github.png");

        // Create a new instance of the Renderer class
        var renderer = new Renderer();

        // Convert the image to ASCII
        var asciiImage = renderer.ConvertToASCII(image, 1, true);

        Console.WriteLine($"Width: {asciiImage.width} Height: {asciiImage.height}");

        // Print the ASCII image
        renderer.PrintASCII(asciiImage);

        // Wait for a key press
        Console.ReadKey(false);

        var smallIm = renderer.ConvertToASCII(image, 4, true);

        Console.WriteLine($"Width: {smallIm.width} Height: {smallIm.height}");

        // Print the ASCII image
        renderer.PrintASCII(smallIm);

        // Wait for a key press
        Console.ReadKey(false);


    }



}

