using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Iso
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create game window.
            Global.theGame = new Game("Isometric Game", (int)(240.0f*(16.0f/9.0f)), 240, 60, false);
            Global.theGame.SetWindow(1280, 720, false, true);


            // Create (load?) player session
            Global.theSession = Global.theGame.AddSession("Player1");

            // Set up controller and control bindings for keyboard
            Global.theSession.Controller = new ControllerXbox360(0);
            Global.theController = Global.theSession.GetController<ControllerXbox360>();

            // Leftstick/Motion
            Global.theController.DPad.AddKeys(new Key[] { Otter.Key.Up, Otter.Key.Right, Otter.Key.Down, Otter.Key.Left });
            Global.theController.DPad.AddAxis(Global.theController.LeftStick);

            // Accept / Select
            Global.theController.A.AddKey(Key.Z);

            // Load Scene
            Global.theGame.AddScene(new PlayState());

            // Start Game
            Global.theGame.Start();


        }
    }
}
