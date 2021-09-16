# Mongoame_Tiled
<p align="center">
<img src="https://dashboard.snapcraft.io/site_media/appmedia/2018/03/tiled-logo-256.png" width="200">
<img src="https://www.pinclipart.com/picdir/big/528-5284520_hand-png-icon-shaking-hands-logo-png-clipart.png" width="150">
<img src = "https://avatars.githubusercontent.com/u/4772066?s=280&v=4" width="200">
</p>

# How to Use
1. Add to the ***"Content"*** folder a new folder called ***"Assets"***

2. In the Assets folder, create two new folders. ***"Json"*** and ***"Tilesets"***

3. Import your Tilesets used for each of the layers in your world. Remember to use one tileset per each layer and to name them acourding to the layer name.

   Example

   |Layer Name|Tileset Name|
   |----------|------------|
   |Ground|Ground.png|
   |Houses|Houses.png|
4. Build imported items with Monogame MGCB.
5. Add **Map.cs** and **Layer.cs** scripts to your project.
6. Create new private ***Map*** variable in **Game1.cs**.
7. In the Initialize function, instantiate the map with a new object of type **Map**, give the path for the json file, the current Game1 class and the tileset width size.

       protected override void Initialize()
        {
            map = new Map("Content/Assets/Json/TiledMap.json",this,TilesetBoxSize);
            base.Initialize();
        }
        
8. Add in the Draw function of the Game1.cs file, the map DrawFunction and add a parameter of type spritebatch.

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            map.DrawMap(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

For any questions, check the template example at <a href=Tiled_Monogame_example>**Tiled_Monogame_example**</a>