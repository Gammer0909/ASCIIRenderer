using System;
using System.Text;
using Color = System.ConsoleColor;

namespace Gammer0909.ASCIIRenderer;

public class AsciiImage {

    public char[,] asciiImage;
    public Color[,] pixelColors;

    public int width;
    public int height;

    public AsciiImage(char[,] asciiImage, Color[,] pixelColors, int width, int height) {

        this.asciiImage = asciiImage;
        this.pixelColors = pixelColors;

        this.width = width;
        this.height = height;

    }

}