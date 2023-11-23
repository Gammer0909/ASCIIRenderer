using System;
using Color = System.ConsoleColor;

namespace Gammer0909.ASCIIRenderer.Rendering;

/// <summary>
/// A simple graphics library for rendering ASCII art.
/// </summary>
public class Graphics {
    
    /// <summary>
    /// Draws a pixel to the console, with the specified color and character
    /// </summary>
    /// <param name="pixel">Character to draw to the console</param>
    /// <param name="fg"></param>
    public void DrawPixel(char pixel = ' ', Color fg = Color.White) {
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
    public void DrawPixel(char pixel = ' ', Color fg = Color.White, Color bg = Color.Black) {
        Console.ForegroundColor = fg;
        Console.BackgroundColor = bg;
        Console.Write(pixel);
        Console.ResetColor();
    }

    /// <summary>
    /// Draws a pixel to the console, with the specified position, color, and character
    /// </summary>
    /// <param name="x">The X value that the pixel should be drawn at</param>
    /// <param name="y">The Y value that the pixel should be drawn at</param>
    /// <param name="pixel">the character that should be drawn to the console, default is a space</param>
    /// <param name="fg">the foreground color of the pixel</param>
    public void DrawPixel(int x, int y, char pixel = ' ', Color fg = Color.White) {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = fg;
        Console.Write(pixel);
        Console.ResetColor();
    }

    /// <summary>
    /// Draws a pixel to the console, with the specified position, color, and character
    /// </summary>
    /// <param name="x">The X value that the pixel should be drawn at</param>
    /// <param name="y">The Y value that the pixel should be drawn at</param>
    /// <param name="pixel">the character that should be drawn to the console, default is a space</param>
    /// <param name="fg">the foreground color of the pixel</param>
    /// <param name="bg">the background color of the pixel</param>
    public void DrawPixel(int x, int y, char pixel = ' ', Color fg = Color.White, Color bg = Color.Black) {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = fg;
        Console.BackgroundColor = bg;
        Console.Write(pixel);
        Console.ResetColor();
    }

}

