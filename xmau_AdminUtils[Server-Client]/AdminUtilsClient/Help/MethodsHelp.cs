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
            Debug.WriteLine("spawnobj objectModel ----> Spawn object");
            Debug.WriteLine("spawnped pedModel ----> Spawn ped");
            Debug.WriteLine("spawnveh vechicleModel ----> Spawn vechicle");

            Debug.WriteLine("tpwayp  ----> Teleport to a waypoint(Mark a waypoint before)");
            Debug.WriteLine("tpcoords [cooordX] [coordY]  ----> Teleport to coord");
            Debug.WriteLine("tpplayer idPlayer ----> Teleport to player");
            Debug.WriteLine("tpbring idPlayer ----> Bring player to your position");

            Debug.WriteLine("golden ----> You and you horse become full gold");
        }
    }
}
