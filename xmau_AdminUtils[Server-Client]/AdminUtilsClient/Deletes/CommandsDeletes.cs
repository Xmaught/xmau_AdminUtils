using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Deletes
{
    class CommandsDeletes : BaseScript
    {
        public CommandsDeletes()
        {
            API.RegisterCommand("dv", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("DeleteVehicle", args, "MethodsDeletes");
            }), false);
            API.RegisterCommand("dvh", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("DeleteHorse", args, "MethodsDeletes");
            }), false);
            API.RegisterCommand("delview", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("DelView", args, "MethodsDeletes");
            }), false);
        }
    }
}
