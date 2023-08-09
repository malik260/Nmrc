using System.Drawing;
using System.Drawing.Imaging;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Verification code tool
    public static class CaptchaHelper
    {
        // The first value of Tuple is the expression, the second value is the result of the expression
        // <returns></returns>
        public static Tuple<string, string> GetCaptchaCode()
        {
            int value = 0;
            char[] operators = { '+', '-', '*' };
            string randomCode = string.Empty;
            Random random = new Random();

            int first = random.Next() % 10;
            int second = random.Next() % 10;
            char operatorChar = operators[random.Next(0, operators.Length)];
            switch (operatorChar)
            {
                case '+': value = first + second; break;
                case '-':
                    // The first number is greater than the second number
                    if (first < second)
                    {
                        int temp = first;
                        first = second;
                        second = temp;
                    }
                    value = first - second;
                    break;
                case '*': value = first * second; break;
            }

            char code = (char)('0' + (char)first);
            randomCode += code;
            randomCode += operatorChar;
            code = (char)('0' + (char)second);
            randomCode += code;
            randomCode += "=?";
            return new Tuple<string, string>(randomCode, value.ToString());
        }

        // Generate a verification code image
        // <param name="randomCode"></param>
        // <returns></returns>
        public static byte[] CreateCaptchaImage(string randomCode)
        {
            const int randAngle = 45; //Random rotation angle
            int mapwidth = (int)(randomCode.Length * 16);
            Bitmap map = new Bitmap(mapwidth, 28); //Create image background
            Graphics graph = Graphics.FromImage(map);
            graph.Clear(Color.AliceBlue); //Clear the screen and fill the background

            Random random = new Random();
            // Background noise generation, in order to display on a white background, try to generate dark colors
            // int intRed = random.Next(256);
            // int intGreen = random.Next(256);
            // int intBlue = (intRed + intGreen > 400) ? 0 : 400 - intRed - intGreen;
            // intBlue = (intBlue > 255) ? 255 : intBlue;

            //Pen blackPen = new Pen(Color.FromArgb(intRed, intGreen, intBlue), 0);
            //for (int i = 0; i < 50; i++)
            //{
            // int x = random.Next(0, map.Width);
            // int y = random.Next(0, map.Height);
            // graph.DrawRectangle(blackPen, x, y, 1, 1);
            //}
            //Draw the interference curve
            for (int i = 0; i < 2; i++)
            {
                Point p1 = new Point(0, random.Next(map.Height));
                Point p2 = new Point(random.Next(map.Width), random.Next(map.Height));
                Point p3 = new Point(random.Next(map.Width), random.Next(map.Height));
                Point p4 = new Point(map.Width, random.Next(map.Height));
                Point[] p = { p1, p2, p3, p4 };
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    graph.DrawBeziers(pen, p);
                }
            }

            // in the text space
            using (StringFormat format = new StringFormat(StringFormatFlags.NoClip))
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                //define the color
                Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
                //define the font
                string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
                int cindex = random.Next(7);

                //The verification code rotates to prevent machine recognition
                char[] chars = randomCode.ToCharArray();//Disassemble the string into a single character array
                foreach (char t in chars)
                {
                    int findex = random.Next(5);
                    using (Font font = new Font(fonts[findex], 14, FontStyle.Bold))//Font style (parameter 2 is the font size)
                    {
                        using (Brush brush = new SolidBrush(c[cindex]))
                        {
                            Point dot = new Point(14, 14);
                            float angle = random.Next(-randAngle, randAngle);//The degree of rotation
                            if (t == '+' || t == '-' || t == '*')
                            {
                                //The addition, subtraction and multiplication operators do not rotate
                                graph.TranslateTransform(dot.X, dot.Y);//Move the cursor to the specified position
                                graph.DrawString(t.ToString(), font, brush, 1, 1, format);
                                graph.TranslateTransform(-2, -dot.Y);//Move the cursor to the specified position, each character is displayed compactly to avoid being recognized by the software
                            }
                            else
                            {
                                graph.TranslateTransform(dot.X, dot.Y);//Move the cursor to the specified position
                                graph.RotateTransform(angle);
                                graph.DrawString(t.ToString(), font, brush, 1, 1, format);
                                graph.RotateTransform(-angle);//Turn back
                                graph.TranslateTransform(-2, -dot.Y);//Move the cursor to the specified position, each character is displayed compactly to avoid being recognized by the software
                            }
                        }
                    }
                }
            }
            //generate image
            using (MemoryStream ms = new MemoryStream())
            {
                map.Save(ms, ImageFormat.Gif);

                graph.Dispose();
                map.Dispose();
                return ms.GetBuffer();
            }
        }
    }
}