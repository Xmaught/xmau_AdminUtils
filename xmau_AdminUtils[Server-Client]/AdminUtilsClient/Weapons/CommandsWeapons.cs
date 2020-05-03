using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Weapons
{
    class CommandsWeapons : BaseScript
    {
        public CommandsWeapons()
        {
            API.RegisterCommand("weap", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("Weap", args, "Methods");
            }), false);

            API.RegisterCommand("ammo", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("Ammo", args, "MethodsSpawners");
            }), false);
        }
    }
}
