using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpShapesLib {
    public enum Shape { Circle, Square, Triangle, Diamond }

    public class Player {
        public string Name { get; }
        public Shape Shape { get; }

        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }

        public int X { get; set; }
        public int Y { get; set; }

        public Player (string name, Shape shape, byte red, byte green, byte blue) {
            Name = name;
            Shape = shape;

            Red = red;
            Green = green;
            Blue = blue;
        }
        public Player (BinaryReader reader, string name) {
            Name = name;
            Shape = (Shape) reader.ReadByte ();

            Red = reader.ReadByte ();
            Green = reader.ReadByte ();
            Blue = reader.ReadByte ();

            X = reader.ReadInt32 ();
            Y = reader.ReadInt32 ();
        }

        public void Move (BinaryReader reader) {
            X = reader.ReadInt32 ();
            Y = reader.ReadInt32 ();
        }

        public void Draw (Graphics g, int red_col, int green_col,
            int blue_col, int width, int height,
            int multiplier) {
            Brush brush = new SolidBrush (Color.FromArgb (red_col, green_col, blue_col));

            g.ResetTransform ();
            g.TranslateTransform (X, Y);
            if (Shape == Shape.Circle)
                g.FillEllipse (brush, 0, 0, width, height);
            else if (Shape == Shape.Square)
                g.FillRectangle (brush, 0, 0, width, height);
            else if (Shape == Shape.Triangle)
                g.FillPolygon (brush, new[] { new Point (25 * multiplier, 0), new Point (50 * multiplier, 50 * multiplier), new Point (0, 50 * multiplier) });
            else if (Shape == Shape.Diamond)
                g.FillPolygon (brush, new[] { new Point (25 * multiplier, 0), new Point (50 * multiplier, 25 * multiplier), new Point (25 * multiplier, 50 * multiplier), new Point (0, 25 * multiplier) });
        }

        public byte[] EnterMessage () {
            MemoryStream stream = new MemoryStream ();
            BinaryWriter writer = new BinaryWriter (stream);
            writer.Write (Message.Enter);
            writer.Write (Name);
            writer.Write ((byte) Shape);
            writer.Write (Red);
            writer.Write (Green);
            writer.Write (Blue);
            writer.Write (X);
            writer.Write (Y);
            return stream.ToArray ();
        }

        public byte[] ExistMessage () {
            MemoryStream stream = new MemoryStream ();
            BinaryWriter writer = new BinaryWriter (stream);
            writer.Write (Message.Exist);
            writer.Write (Name);
            writer.Write ((byte) Shape);
            writer.Write (Red);
            writer.Write (Green);
            writer.Write (Blue);
            writer.Write (X);
            writer.Write (Y);
            return stream.ToArray ();
        }

        public byte[] MoveMessage () {
            MemoryStream stream = new MemoryStream ();
            BinaryWriter writer = new BinaryWriter (stream);
            writer.Write (Message.Move);
            writer.Write (Name);
            writer.Write (X);
            writer.Write (Y);
            return stream.ToArray ();
        }

        public byte[] LeaveMessage () {
            MemoryStream stream = new MemoryStream ();
            BinaryWriter writer = new BinaryWriter (stream);
            writer.Write (Message.Leave);
            writer.Write (Name);
            return stream.ToArray ();
        }

        public bool Contains (Point point) =>
            point.X >= X && point.X < X + 50 &&
            point.Y >= Y && point.Y < Y + 50;
    }
}
