using System;
using System.Drawing;
using System.Security.Cryptography;

public class ImageHasher {
    public Bitmap HashData(int squareSize, byte[] data) {
        if (squareSize < 1) throw new ArgumentOutOfRangeException("squareSize", squareSize, "Must be greater than or equal to 1");
        byte[] hash = new SHA512Managed().ComputeHash(data);

        Bitmap image = new Bitmap(squareSize * 8, squareSize * 8);
        Graphics graphics = Graphics.FromImage(image);

        for (int i = 0; i < 64; i++) {
            double hue = 360 * ((double)hash[i] / 255);
            graphics.FillRectangle(new SolidBrush(this.ColorFromHSV(hue, 1, 1)),  squareSize * (i % 8), squareSize * (i / 8), squareSize, squareSize);
        }

		return image;
    }

	private Color ColorFromHSV(double hue, double saturation, double val)
	{
		int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
		double f = hue / 60 - Math.Floor(hue / 60);

		val = val * 255;
		int v = Convert.ToInt32(val);
		int p = Convert.ToInt32(val * (1 - saturation));
		int q = Convert.ToInt32(val * (1 - f * saturation));
		int t = Convert.ToInt32(val * (1 - (1 - f) * saturation));

		switch (hi) {
			case 1:
				return Color.FromArgb(255, q, v, p);
			case 2:
				return Color.FromArgb(255, p, v, t);
			case 3:
				return Color.FromArgb(255, p, q, v);
			case 4:
				return Color.FromArgb(255, t, p, v);
			case 5:
				return Color.FromArgb(255, v, p, q);
			default:
				return Color.FromArgb(255, v, t, p);
		}
	}
}
