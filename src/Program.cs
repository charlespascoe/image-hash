using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

public static class Program {
    public static void Main(string[] args) {
        ImageHasher ih = new ImageHasher();

        foreach (string str in args) {
            Console.WriteLine(str);
        }

        Bitmap image = ih.HashData(64, Encoding.UTF8.GetBytes(args[0]));

        image.Save("out.png", ImageFormat.Png);
    }
}
