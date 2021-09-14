using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace Tiled_Monogame_example
{
    class Layer
    {
        public int[] data { get; set; }
        public string name { get; set; }
        public int firstgid { get; set; }

        public Texture2D Tileset;
        public int tileSize;

        public Layer(string name, int[] data)
        {
            this.name = name;
            this.data = data;
        }

        public void LoadTileset(Game1 game, int tileSize)
        {
            this.tileSize = tileSize;
            Tileset = game.Content.Load<Texture2D>($"Assets/Tilesets/{name}");
        }
        public void setDataValues()
        {
            if (firstgid != 1)
            {
                System.Console.WriteLine(name);
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] != 0)
                        data[i] -= firstgid-1;
                }
            }
        }

        public void DrawLayer(SpriteBatch spriteBatch, int ScreenScaler)
        {
            Vector2 Pos = new Vector2(0, 0);

            for (int i = 1; i < data.Length + 1; i++)
            {
                int imageID = data[i - 1];
                if (imageID == 0)
                {
                    if (i % 25 == 0)
                    {
                        Pos = new Vector2(0, Pos.Y + tileSize * ScreenScaler);
                    }
                    else
                    {
                        Pos = new Vector2(Pos.X + tileSize * ScreenScaler, Pos.Y);
                    }
                    continue;
                }
                int counter = 1;
                Point TilesetDrawingPosition = Point.Zero;
                while (counter != imageID)
                {
                    if (TilesetDrawingPosition.X >= Tileset.Width - tileSize)
                    {
                        TilesetDrawingPosition = new Point(0, TilesetDrawingPosition.Y + tileSize);
                    }
                    else
                    {
                        TilesetDrawingPosition = new Point(TilesetDrawingPosition.X + tileSize, TilesetDrawingPosition.Y);
                    }
                    counter += 1;
                }

                spriteBatch.Draw(
                    Tileset,
                    Pos,
                    new Rectangle(TilesetDrawingPosition, new Point(tileSize, tileSize)),
                    Color.White,
                    0,
                    Vector2.Zero,
                    ScreenScaler,
                    SpriteEffects.None,
                    0);

                if (i % 25 == 0)
                {
                    Pos = new Vector2(0, Pos.Y + tileSize * ScreenScaler);
                }
                else
                {
                    Pos = new Vector2(Pos.X + tileSize * ScreenScaler, Pos.Y);
                }
            }
        }
    }
}
