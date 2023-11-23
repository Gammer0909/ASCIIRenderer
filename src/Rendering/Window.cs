using System;
using Color = System.ConsoleColor;

namespace Gammer0909.ASCIIRenderer.Rendering;

class Window {

    string Title { get; set; }

    Color WindowColor { get; set; }


    public Window(string title, Color windowColor = Color.Black) {
        Title = title;
        WindowColor = windowColor;

        Console.Title = Title;
        Console.BackgroundColor = WindowColor;
        Console.Clear();
    }

    public void Redraw() {
        Console.ResetColor();
        Console.BackgroundColor = WindowColor;
        Console.Clear();
    }


}