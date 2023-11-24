using System;
using Color = System.ConsoleColor;
using System.Text;
using SkiaSharp;

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

    
    public AsciiImage ConvertToASCII(SKBitmap image, int pixelsPerCharacter = 1, bool color = false) {

        // Create a new list of chars
        char[,] asciiImage = new char[image.Width, image.Height];
        // Create a new list of colors
        Color[,] pixelColors = new Color[image.Width, image.Height];
        SKColor[,] sKColors = new SKColor[image.Width, image.Height];

        // Loop through the image
        for (int y = 0; y < image.Height; y++) {
            int startX = 0;

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
                // Add the SKColor for averaging
                sKColors[x, y] = pixel;

                if (startX == pixelsPerCharacter) {
                    // Average out the last {pixelsPerCharacter} pixels, then cram them into one char
                    // Get the average color
                    SKColor[] colors = new SKColor[pixelsPerCharacter];
                    // We're gonna have to loop from where x started to where x is now
                    // but to do this, we need to know where x started
                    int startX2 = x - pixelsPerCharacter; // But what if this returns a negative number?
                    // If it does, we need to make it 0
                    if (startX2 < 0) {
                        startX2 = 0;
                    }

                    // Now we can loop from startX2 to x
                    for (int i = startX2; i < x; i++) {
                        // Add the color to the list
                        colors[i] = sKColors[i, y];
                    }

                    // Get the average color
                    Color averageColor = GetAverageColor(colors);

                    // Next we also need to do this with the pixel

                } else {
                    startX++;
                }

            }

        }        

        // Return the ASCII image
        return new AsciiImage(asciiImage, pixelColors, image.Width, image.Height);

    }

    private Color GetAverageColor(SKColor[] colors) {



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