using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Spawners
{
    class CommandsSpawners :BaseScript
    {
        public CommandsSpawners()
        {

            API.RegisterCommand("spawnobj", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("Spawnobj", args, "MethodsSpawners");
            }), false);

            API.RegisterCommand("spawnped", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("Spawnped", args, "MethodsSpawners");
            }), false);

            API.RegisterCommand("spawnveh", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("Spawnveh", args, "MethodsSpawners");
            }), false);
        }
    }
}
