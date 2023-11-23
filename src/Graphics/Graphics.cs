using System;
using Color = System.ConsoleColor;

namespace Gammer0909.ASCIIRenderer.Graphics;

/// <summary>
/// A simple graphics library for rendering ASCII art.
/// </summary>
public static class Graphics {
    
    /// <summary>
    /// Draws a pixel to the console, with the specified color and character
    /// </summary>
    /// <param name="pixel">Character to draw to the console</param>
    /// <param name="fg"></param>
    public static void DrawPixel(char pixel = ' ', Color fg = Color.White) {
        Console.ForegroundColor = fg;
        Console.Write(pixel);
        Console.ResetColor();
    }

    /// <summary>
    /// Draws a pixel to the console, with the specified foreground and background colors
    /// </summary>
    /// <param name="pixel">Character to draw to the console</param>
    /// <param name="fg">The Foreground color of the pixel</param>
    /// <param name="bg">The Background Color of the Pixel</param>
    public static void DrawPixel(char pixel = ' ', Color fg = Color.White, Color bg = Color.Black) {
        Console.ForegroundColor = fg;
        Console.BackgroundColor = bg;
        Console.Write(pixel);
        Console.ResetColor();
    }


    // This should be all I need for now, but I'll add more as I need it.

}

