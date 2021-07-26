using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MonogameTrain
{
    class Map
    {
        private List<Layer> MapLayers;
        public Map(string MapPath,Game1 game,int tileSize,int ScreenScaler = 1)
        {
            MapLayers = new List<Layer>();
            using (StreamReader JsonFile = File.OpenText(MapPath)) 
            {
                JObject MapJson = (JObject)JToken.ReadFrom(new JsonTextReader(JsonFile));
                JArray LayersJson = (JArray)MapJson["layers"];
                foreach (JObject Layers in LayersJson)
                {
                    string Json = "{\n"+$"data:{Layers["data"].ToString()},\nname:'{Layers["name"].ToString()}',"+"\n}";
                    Layer layer = JsonConvert.DeserializeObject<Layer>(Json);
                    layer.LoadTileset(game,tileSize);
                    MapLayers.Add(layer);                    
                }                
            }
        }

        public void DrawMap(SpriteBatch spriteBatch, int ScreenScaler = 1) 
        {
            foreach (Layer layer in MapLayers)
            {
                layer.DrawLayer(spriteBatch,ScreenScaler);
            }
        }

    }
}
