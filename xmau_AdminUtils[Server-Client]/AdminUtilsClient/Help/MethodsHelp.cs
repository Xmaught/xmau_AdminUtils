using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Help
{
    class MethodsHelp :BaseScript
    {
        public MethodsHelp()
        {

        }

        public void Com(List<object> args)
        {

            Debug.WriteLine("----------------------------------------");
            Debug.WriteLine("-----------------COMMANDS---------------");
            Debug.WriteLine("----------------------------------------");

            Debug.WriteLine("spawnobj objectModel ----> Spawn object");
            Debug.WriteLine("spawnped pedModel ----> Spawn ped(animals and humans)");
            Debug.WriteLine("spawnveh vechicleModel ----> Spawn vechicle");
            Debug.WriteLine("changeped pedmodel ----> Change your ped");


            Debug.WriteLine("tpwayp  ----> Teleport to a waypoint(Mark a waypoint before)");
            Debug.WriteLine("tpcoords [cooordX] [coordY]  ----> Teleport to coord");
            Debug.WriteLine("tpplayer idPlayer ----> Teleport to player");
            Debug.WriteLine("tpbring idPlayer ----> Bring player to your position");
            Debug.WriteLine("tpback ----> Return to last tp position");

            Debug.WriteLine("golden ----> You and you horse become full gold");
            Debug.WriteLine("gm ----> Godmode");
            Debug.WriteLine("n ----> NoClip(W,A,S,D,Z-Up,X-Down,UpArrow-SpeedUp,DownArrow-SpeedDown,C-SpeedReset");

            Debug.WriteLine("pm id message ----> PrivateMessage");
            Debug.WriteLine("bc message ----> BroadcastMessage");

            Debug.WriteLine("spec id ----> BroadcastMessage(dont work)");
            Debug.WriteLine("sspec id ----> BroadcastMessage(dont work");
            Debug.WriteLine("stop id ----> Freeze player");
            Debug.WriteLine("slap id ----> Slap player");
            Debug.WriteLine("kick id ----> Kick player");

            Debug.WriteLine("thor ----> Be thro");
            Debug.WriteLine("gr ----> Be ghostrider");
        }
    }
}
