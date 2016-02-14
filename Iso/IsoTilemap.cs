using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Iso
{
    class IsoTilemap : Entity
    {

        public List<MapTile.MapTileType> tileTypeArray;
        public Dictionary<int, MapTile> visibleTiles;

        public int mapWidth;
        public int mapHeight;
        public int mapDepth;

        public int mapTileSizeX;
        public int mapTileSizeY;

        public bool mapChanged = false;



        public IsoTilemap(int width, int height, int depth, int tilesizex, int tilesizey)
        {
            // Set variables
            mapWidth = width;
            mapHeight = height;
            mapDepth = depth;
            mapTileSizeX = tilesizex;
            mapTileSizeY = tilesizey;
        }

        public override void Added()
        {
            base.Added();


            // Create blank map
            tileTypeArray = new List<MapTile.MapTileType>();
            visibleTiles = new Dictionary<int, MapTile>();

            for (int z = 0; z < mapDepth; z++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    for (int x = 0; x < mapWidth; x++)
                    {
                        /*
                        var thisTile = new MapTile(MapTile.MapTileType.TILE_FLOOR);
                        thisTile.Graphic = new Image(Assets.GFX_TEST_TILE);
                        thisTile.Graphic.CenterOrigin();
                        if(z > 0)
                        {
                            thisTile.IsSolid = false;
                        }
                        
                        thisTile.X = X + (x * (mapTileSizeX / 2) + y * (mapTileSizeX / 2));
                        thisTile.Y = Y - (z * (mapTileSizeY / 2) + y * (mapTileSizeY / 2) - x * (mapTileSizeY / 2)); 
                        thisTile.Layer = -(2*z) + y - (x/2);
                        visibleTiles.Add(thisTile);
                        this.Scene.Add(thisTile);
                        */
                        if(z > 0)
                        {
                            tileTypeArray.Add(MapTile.MapTileType.TILE_EMPTY);
                        }
                        else
                        {
                            tileTypeArray.Add(MapTile.MapTileType.TILE_FLOOR);
                        }
                        

                    }

                }


            }

        }

        public MapTile GetTile(int x, int y, int z)
        {
            MapTile currentTile = null;

            // Try!
            int index = 0;
            index |= x;
            index |= y << 24;
            index |= z << 48;
            visibleTiles.TryGetValue(index, out currentTile);

            return currentTile;
        }

        public MapTile.MapTileType GetTileArray(int x, int y, int z)
        {
            int index = x + y * mapDepth + z * mapDepth * mapHeight;

            if (index > tileTypeArray.Count)
            {
                Util.Log("Index out of range!");
                return MapTile.MapTileType.TILE_EMPTY;
            }

            return tileTypeArray[index];
        }

        

        public void SetTile(MapTile.MapTileType type, int x, int y, int z)
        {
            int index = 0;
            index |= x;
            index |= y << 24;
            index |= z << 48;
            MapTile haveCurrentTile;

            if (visibleTiles.TryGetValue(index, out haveCurrentTile))
            {
                if (type == MapTile.MapTileType.TILE_EMPTY)
                {
                    haveCurrentTile.RemoveSelf();
                    visibleTiles.Remove(index);
                }
                else
                {
                    haveCurrentTile.myType = type;
                }

            }
            else if (type != MapTile.MapTileType.TILE_EMPTY) 
            {
                haveCurrentTile = new MapTile(type, x, y, z);

                haveCurrentTile.Graphic = new Image(Assets.GFX_TEST_TILE);
                haveCurrentTile.Graphic.CenterOrigin();

                haveCurrentTile.X = X + (x * (mapTileSizeX / 2) + y * (mapTileSizeX / 2));
                haveCurrentTile.Y = Y - (z * (mapTileSizeY / 2) + y * (mapTileSizeY / 2) - x * (mapTileSizeY / 2));
                haveCurrentTile.Layer = -(2 * z) + y - (x / 2);


                visibleTiles.Add(index, haveCurrentTile);
                Scene.Add(haveCurrentTile);
            }




        }

        public void SetTileInArray(MapTile.MapTileType type, int x, int y, int z)
        {
            int index = x + (y * mapDepth) + (z * mapHeight * mapWidth);

            if(index > tileTypeArray.Count)
            {
                Util.Log("Index out of range!");
                
                return;
            }

            tileTypeArray[index] = type;

        }

        public override void Render()
        {
            base.Render();

        }

        public override void Update()
        {
            base.Update();

            if(mapChanged)
            {
                UpdateVisibleTiles();
                mapChanged = false;
            }
        }

        public void UpdateVisibleTiles()
        {
            for (int z = 0; z < mapDepth; z++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    for (int x = 0; x < mapWidth; x++)
                    {
                        int index = x + (y * mapDepth) + (z * mapHeight * mapWidth);

                        SetTile(tileTypeArray[index], x, y, z);



                    }

                }
            }
        }


    }
}
