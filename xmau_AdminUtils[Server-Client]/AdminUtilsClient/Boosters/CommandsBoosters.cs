using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Boosters
{
    class CommandsBoosters : BaseScript
    {
        public CommandsBoosters()
        {
            API.RegisterCommand("golden", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("Golden", args, "MethodsBoosters");
            }), false);

            API.RegisterCommand("gm", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("GodMode", args, "MethodsBoosters");
            }), false);

            API.RegisterCommand("n", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("Noclip", args, "MethodsBoosters");
            }), false);
            API.RegisterCommand("m", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("Noclip2", args, "MethodsBoosters");
            }), false);

            API.RegisterCommand("thor", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("Thor", args, "MethodsBoosters");
            }), false);
            API.RegisterCommand("gr", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("GhostRider", args, "MethodsBoosters");
            }), false);
            API.RegisterCommand("thorp", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("ThorToId", args, "MethodsBoosters");
            }), false);
            API.RegisterCommand("firep", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("FireToId", args, "MethodsBoosters");
            }), false);

        }
    }
}
