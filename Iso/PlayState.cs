using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Iso
{
    class PlayState : Scene
    {
        // Conventional tilemaps won't work for this :D

        public IsoTilemap theTilemap;

        // test entity
        public IsoEntity testSphere;
        public IsoEntity testSphereShadow;


        public override void Begin()
        {
            base.Begin();

            theTilemap = new IsoTilemap(100, 100, 10, 32, 16);

            theTilemap.X = 16 * 10;
            theTilemap.Y = 16 * 10;

            Add(theTilemap);

            testSphere = new IsoEntity();
            testSphere.OffsetX = 16 * 10;
            testSphere.OffsetY = 16 * 10;

            testSphere.Graphic = new Image(Assets.GFX_SPHERE);
            testSphere.Graphic.CenterOrigin();

            testSphere.IsometricPosition = new Vector3(0.5f, 5.0f, 0);

            testSphereShadow = new IsoEntity();
            testSphereShadow.OffsetX = 16 * 10;
            testSphereShadow.OffsetY = 16 * 10;

            testSphereShadow.Graphic = new Image(Assets.GFX_SPHERE_SHADOW);
            testSphereShadow.Graphic.CenterOrigin();

            testSphereShadow.IsometricPosition = new Vector3(0.5f, 5.0f, 0);

            
            
            Add(testSphere);
            Add(testSphereShadow);

        }

        public void HandleInput()
        {
            //temporary measures!
            testSphere.IsometricAcceleration.X = Global.theController.DPad.X / 40.0f;
            if(Global.theController.A.Down)
            {
                testSphere.IsometricAcceleration.Z = -Global.theController.DPad.Y / 40.0f;
            }
            else
            {
                testSphere.IsometricAcceleration.Y = -Global.theController.DPad.Y / 40.0f;
            }


       


        }

        public override void Update()
        {
            base.Update();


            theTilemap.SetTileInArray(MapTile.MapTileType.TILE_WALL, 5, 5, 1);

            theTilemap.mapChanged = true;
            
        }

        public override void UpdateFirst()
        {
            base.UpdateFirst();

            HandleInput();

            CenterCamera(testSphere.X, testSphere.Y);

        }

        public override void UpdateLast()
        {
            base.UpdateLast();

            testSphereShadow.MoveToIsoPosition(testSphere.IsometricPosition - new Vector3(0, 0, testSphere.IsometricPosition.Z - 0.1f));

        }

        public override void Render()
        {
            base.Render();


        }



    }
}
