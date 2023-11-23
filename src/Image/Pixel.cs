using System;
using Color = System.ConsoleColor;

namespace Gammer0909.ASCIIRenderer.Image;

/*

Blue: #0000FF
Cyan: #00FFFF
DarkBlue: #000080
DarkCyan: #008B8B
DarkGray: #A9A9A9
DarkGreen: #006400
DarkMagenta: #8B008B
DarkRed: #8B0000
DarkYellow: #8B8B00
Gray: #808080
Green: #008000
Magenta: #FF00FF
Red: #FF0000
White: #FFFFFF
Yellow: #FFFF00

*/

/// <summary>
/// A class that represents a single pixel in an image
/// </summary>
public class Pixel {

    private static readonly string[] colorHexValues = new string[] {
        "0000FF", "00FFFF", "000080", "008B8B", "A9A9A9", "006400", "8B008B", "8B0000", "8B8B00", "808080", "008000", "FF00FF", "FF0000", "FFFFFF", "FFFF00"
    };

    int r { get; set; }
    int g { get; set; }
    int b { get; set; }

    Color color { get; set; }


    /// <summary>
    /// Creates a new pixel with the specified color
    /// </summary>
    /// <param name="color">The color of the pixel</param>
    /// <returns>A new pixel with the specified color</returns>
    public Pixel(Color color) {
        this.color = color;
    }

    /// <summary>
    /// Creates a new pixel with the specified color
    /// </summary>
    /// <param name="r">The red value of the pixel</param>
    /// <param name="g">The green value of the pixel</param>
    /// <param name="b">The blue value of the pixel</param>
    /// <returns>A new pixel with the specified color</returns>
    public Pixel(int r, int g, int b) {
        this.r = r;
        this.g = g;
        this.b = b;

        // We need to convert the RGB values to a ConsoleColor, so we can use it in the DrawPixel method
        this.color = RGBToConsoleColor(r, g, b);

    }


    /// <summary>
    /// Converts the RGB values to a ConsoleColor
    /// </summary>
    /// <param name="r">The red value of the pixel</param>
    /// <param name="g">The green value of the pixel</param>
    /// <param name="b">The blue value of the pixel</param>
    /// <returns>A ConsoleColor that represents the RGB values</returns>
    public static Color RGBToConsoleColor(int r, int g, int b) {
 
        // Conver the red green and blue values to a hex
        string hexValue = r.ToString("X2") + g.ToString("X2") + b.ToString("X2");

        // Loop through all the hex values
        for (int i = 0; i < colorHexValues.Length; i++) {

            // If the hex value is equal to the current hex value
            if (hexValue == colorHexValues[i]) {

                // Return the ConsoleColor
                return (Color)i;

            }

        }

        // If the hex value is not equal to any of the hex values, return the default color
        return Color.Black;

    }

}