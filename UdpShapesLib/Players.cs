using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpShapesLib {
    public class Players {
        private IDictionary<string, Player> players = new Dictionary<string, Player> ();

        public event Action<Player> OnEnter;

        public Player this[string name] => players[name];

        public bool TryGet (string name, out Player player) => players.TryGetValue (name, out player);

        public void ProcessMessage (byte[] bytes, Graphics graphics) {
            MemoryStream stream = new MemoryStream (bytes);
            BinaryReader reader = new BinaryReader (stream);
            byte message = reader.ReadByte ();
            if (message == Message.Enter) {
                Player newPlayer = new Player (reader, reader.ReadString());
                players.Add (newPlayer.Name, newPlayer);
                OnEnter?.Invoke (newPlayer);
            }
            else if (message == Message.Exist) {
                Player newPlayer = new Player (reader, reader.ReadString());
                if (players.ContainsKey (newPlayer.Name))
                    return;
                players.Add (newPlayer.Name, newPlayer);
            }
            else if (message == Message.Move) {
                string name = reader.ReadString ();
                players[name].Move (reader);
            }
            else if (message == Message.Leave) {
                string name = reader.ReadString ();
                players.Remove (name);
            }
            else if (message == Message.color)
            {
                string red_col = reader.ReadString(), green_col = reader.ReadString(), blue_col = reader.ReadString(),
                    width = reader.ReadString(), height = reader.ReadString(), multiplier = reader.ReadString();
                new Player(reader, reader.ReadString());
                new Player(reader, reader.ReadString()).Draw(graphics, int.Parse(red_col), int.Parse(green_col),
                    int.Parse(blue_col), int.Parse(width), int.Parse(height),
                    int.Parse(multiplier));
            }

        }

        public void Draw (Graphics graphics, string red_col, string green_col,
            string blue_col, int width, int height,
            int multiplier) {
            foreach (Player player in players.Values) player.Draw(graphics, int.Parse(red_col), int.Parse(green_col),
                int.Parse(blue_col), width, height,
                multiplier);
        }
    }
}
