using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tiled_Monogame_example
{
    public class Map
    {
        private List<Layer> MapLayers;
        private float height;
        private float width;
        public float Height => height;
        public float Width => width;

        private float tileheight;
        private float tilewidth;
        public float TileHeight => tileheight;
        public float TileWidth => tilewidth;

        public Map(string MapPath, Game1 game, int tileSize, int ScreenScaler = 1)
        {
            MapLayers = new List<Layer>();
            using (StreamReader JsonFile = File.OpenText(MapPath))
            {
                JObject MapJson = (JObject)JToken.ReadFrom(new JsonTextReader(JsonFile));
                JArray LayersJson = (JArray)MapJson["layers"];
                JArray LayerJsonData = (JArray)MapJson["tilesets"];


                foreach (JObject Layers in LayersJson)
                {
                    string Json = "{\n" + $"data:{Layers["data"].ToString()},\nname:'{Layers["name"].ToString()}'," + "\n}";
                    Layer layer = JsonConvert.DeserializeObject<Layer>(Json);
                    layer.LoadTileset(game, tileSize);
                    MapLayers.Add(layer);
                }
                int i = 0;
                foreach (JObject LayersData in LayerJsonData)
                {
                    string Json2 = "{\n" + $"firstgid:{LayersData["firstgid"].ToString()},"+"\n"+"}";
                    Layer tempLayer = JsonConvert.DeserializeObject<Layer>(Json2);
                    MapLayers[i].firstgid = tempLayer.firstgid;
                    MapLayers[i].setDataValues();
                    i += 1;
                }
            }
        }

        private void getMapData(JObject MapJson)
        {
            string MapData = 
                "{\n"
                +$"height:{MapJson["height"].ToString()},\ntileheight:{MapJson["tileheight"].ToString()},\ntilewidth:{MapJson["tilewidth"].ToString()},\nwidth:{MapJson["width"].ToString()},"+
                "\n}";
                System.Console.WriteLine(MapData);
                string[] data = new string[4];
                data[0] = JsonConvert.DeserializeObject<string>(MapData);
                data[1] = JsonConvert.DeserializeObject<string>(MapData);
                data[2] = JsonConvert.DeserializeObject<string>(MapData);
                data[3] = JsonConvert.DeserializeObject<string>(MapData);
                // Map tempMap = JsonConvert.DeserializeObject<Map>(MapData);
                System.Console.WriteLine(Height + " " + Width);
                System.Console.WriteLine(TileHeight + " " + TileWidth);
        }

        public void DrawMap(SpriteBatch spriteBatch, int ScreenScaler = 1)
        {
            foreach (Layer layer in MapLayers)
            {
                layer.DrawLayer(spriteBatch, ScreenScaler);
            }
        }

    }
}
