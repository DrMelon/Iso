using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Iso 
{
    class MapTile : Entity
    {
        // Map tiles know what floor type they are.
        public enum MapTileType
        {
            TILE_EMPTY,
            TILE_FLOOR,
            TILE_WALL
        }

        public MapTileType myType;


        // They also know where they're positioned in the isometric world.
        public int XPos, YPos, ZPos;

        // They also know if they've got anything in them or not
        public bool IsSolid {
            set
            {
                if (Graphic != null)
                {
                    Graphic.Visible = value;
                }

            }
            get
            {
                return IsSolid;
            }
        }
        

        public MapTile(MapTileType type, int Xp, int Yp, int Zp)
        {
            XPos = Xp;
            YPos = Yp;
            ZPos = Zp;
            myType = type;

            if(myType == MapTileType.TILE_FLOOR)
            {
                Graphic = new Image(Assets.GFX_TEST_TILE);
            }
            if(myType == MapTileType.TILE_WALL)
            {
                Graphic = new Image(Assets.GFX_TEST_WALL);
            }
            
        }

        public void ChangeType(MapTileType newType)
        {
            myType = newType;

            if (myType == MapTileType.TILE_FLOOR)
            {
                Graphic = new Image(Assets.GFX_TEST_TILE);
            }
            if (myType == MapTileType.TILE_WALL)
            {
                Graphic = new Image(Assets.GFX_TEST_WALL);
            }

            if(myType == MapTileType.TILE_EMPTY)
            {
                IsSolid = false;
            }
            else
            {
                IsSolid = true;
            }
        }

    }
}
