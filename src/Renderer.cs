using System;
using Color = System.ConsoleColor;
using System.Text;
using System.Collections.Generic;
using SkiaSharp;
using System.Security.Cryptography.X509Certificates;

namespace Gammer0909.ASCIIRenderer;

public class Renderer {

    static string ASCIIChars = "`.-':_,^=;><+!rc*/z?sLTv)J7(|Fi{C}fI31tlu[neoZ5Yxjya]2ESwqkP6h9d4VpOGbUAKXHm8RD#$Bg0MNWQ%&@";

    private static readonly (int, int, int)[] colorValuesInt = new (int, int, int)[] {
        (0, 0, 255), (0, 255, 255), (0, 0, 128), (0, 139, 139), (169, 169, 169), (0, 100, 0), (139, 0, 139), (139, 0, 0), (139, 139, 0), (128, 128, 128), (0, 128, 0), (255, 0, 255), (255, 0, 0), (255, 255, 255), (255, 255, 0)
    };

    public void PrintASCII(AsciiImage asciiImage) {

        // Loop through the ASCII image
        for (int y = 0; y < asciiImage.pixelColors.GetLength(0); y++) {

            for (int x = 0; x < asciiImage.pixelColors.GetLength(1); x++) {

                // Get the char
                char c = asciiImage.asciiImage[x, y];
                // Get the color
                Color color = asciiImage.pixelColors[x, y];

                // Draw the pixel
                DrawPixel(c, color);

            }

            // Append a new line
            Console.WriteLine();

        }

    }

    public AsciiImage ConvertAndScaleAscii(SKBitmap image, int pixelsPerCharacter = 4, bool color = false) {
            
            // Convert the image to ASCII
            var asciiImage = ConvertToASCII(image, color);

            // Now that it's converted, we can scale it
            // Create a new list of chars
            char[,] scaledAsciiImage = new char[asciiImage.width / pixelsPerCharacter, asciiImage.height / pixelsPerCharacter];
            // Create a new list of colors
            Color[,] scaledPixelColors = new Color[asciiImage.width / pixelsPerCharacter, asciiImage.height / pixelsPerCharacter];

            List<SKColor> pixelsInChunk = new List<SKColor>();

            // Loop through the image, but every pixelsPerCharacter pixels, we average out the color and char
            for (int y = 0; y < asciiImage.height - 1; y++) {

                for (int x = 0; x < asciiImage.width - 1; x++) {

                    int averageBrightness = 0;

                    if (x != 0 && x % pixelsPerCharacter == 0) {

                        // We're at the end of the 'chunk', so we can average out the color and char
                        // Get the average color
                        var averageColor = GetAverageColor(pixelsInChunk);

                        // Get the average char
                        for (int i = 0; i < pixelsInChunk.Count; i++) {

                            var pixel = pixelsInChunk[i];

                            averageBrightness += GetBrightness(pixel);

                        }

                        averageBrightness /= pixelsInChunk.Count;

                        // Get the char of the pixel
                        var character = GetChar(averageBrightness);

                        // Add the char to the list
                        try {
                            int scaledX = x / pixelsPerCharacter;
                            int scaledY = y / pixelsPerCharacter;
                            scaledAsciiImage[scaledX, scaledY] = character;
                            scaledPixelColors[scaledX, scaledY] = averageColor;
                        } catch (IndexOutOfRangeException e) {

                            Console.WriteLine($"Index was out of range. Index: {x / pixelsPerCharacter}, {y / pixelsPerCharacter}\n{e.Message}");
                            
                        }

                        // Clear the list
                        pixelsInChunk.Clear();

                    } else {
                            
                        // We're not at the end of the 'chunk', so we can just add the pixel to the list
                        pixelsInChunk.Add(image.GetPixel(x, y));
    
                    }

                }

            }



            return new AsciiImage(scaledAsciiImage, scaledPixelColors, asciiImage.width / pixelsPerCharacter, asciiImage.height / pixelsPerCharacter);
    }

    
    public AsciiImage ConvertToASCII(SKBitmap image, bool color = false) {

        // Create a new list of chars
        char[,] asciiImage = new char[image.Width, image.Height];
        // Create a new list of colors
        Color[,] pixelColors = new Color[image.Width, image.Height];

        // Loop through the image
        for (int y = 0; y < image.Height; y++) {

            for (int x = 0; x < image.Width; x++) {

                // Get the pixel
                var pixel = image.GetPixel(x, y);

                // Get the color of the pixel
                var c = GetColor(pixel);

                // Get the char of the pixel
                var character = GetChar(pixel);

                // Add the char to the list
                asciiImage[x, y] = character;
                // Add the color to the list
                pixelColors[x, y] = c;

            }

        }        

        // Return the ASCII image
        return new AsciiImage(asciiImage, pixelColors, image.Width, image.Height);

    }

    private int GetBrightness(SKColor pixel) {
            
            // Get the brightness of the pixel
            int brightness = (pixel.Red + pixel.Green + pixel.Blue) / 3;
    
            return brightness;
    }

    private char GetChar(int brightness) {
            
        // Get the index of the char
        int charIndex = (int)Math.Round((brightness / 255.0) * (ASCIIChars.Length - 1));

        // Get the char
        char c = ASCIIChars[charIndex];

        return c;
    }

    private char GetChar(SKColor pixel) {

        // First get the brightness of the pixel
        int brightness = (pixel.Red + pixel.Green + pixel.Blue) / 3;

        // Get the index of the char
        int charIndex = (int)Math.Round((brightness / 255.0) * (ASCIIChars.Length - 1));

        // Get the char
        char c = ASCIIChars[charIndex];

        return c;
        
    }

    private Color GetAverageColor(List<SKColor> colors) {

        // Loop through the list and add all the r, g and B values
        int r = 0;
        int g = 0;
        int b = 0;

        foreach (var color in colors) {

            r += color.Red;
            g += color.Green;
            b += color.Blue;

        }

        byte averageR = (byte)(r / colors.Count);
        byte averageG = (byte)(g / colors.Count);
        byte averageB = (byte)(b / colors.Count);

        return GetColor(new SKColor(averageR, averageG, averageB));

    }

    private Color GetColor(SKColor pixel) {

        // Make a lamda square function
        Func<int, int> square = (int x) => x * x;

        Color[] consoleColors = new[] {
            Color.Blue, Color.Cyan, Color.DarkBlue, Color.DarkCyan, Color.DarkGray, Color.DarkGreen, Color.DarkMagenta, Color.DarkRed, Color.DarkYellow, Color.Gray, Color.Green, Color.Magenta, Color.Red, Color.White, Color.Yellow
        };

        Color best = Color.Black;

        int r = pixel.Red;
        int g = pixel.Green;
        int b = pixel.Blue;

        int bestSquareDistance = int.MaxValue;
        for (int i = 0; i < consoleColors.Length; i++) {

            Color c = consoleColors[i];
            (int, int, int) colorValues = colorValuesInt[i];

            int squareDistance = square(r - colorValues.Item1) + square(g - colorValues.Item2) + square(b - colorValues.Item3);

            if (squareDistance < bestSquareDistance) {
                best = c;
                bestSquareDistance = squareDistance;
            }

        }

        return best;

    }

    private static void DrawPixel(char c, Color fg) {
        Console.ForegroundColor = fg;
        Console.Write(c);
        Console.ResetColor();
    }


}