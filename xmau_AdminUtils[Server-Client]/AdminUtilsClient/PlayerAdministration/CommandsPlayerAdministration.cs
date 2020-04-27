using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.PlayerAdministration
{
    class CommandsPlayerAdministration : BaseScript
    {
        public CommandsPlayerAdministration()
        {
            API.RegisterCommand("spec", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("Spec", args, "MethodsPlayerAdministration");
            }), false);

            API.RegisterCommand("sspec", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("SSpec", args, "MethodsPlayerAdministration");
            }), false);

            API.RegisterCommand("stop", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("StopPlayer", args, "MethodsPlayerAdministration");
            }), false);

            API.RegisterCommand("slap", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("Slap", args, "MethodsPlayerAdministration");
            }), false);

            API.RegisterCommand("k", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("Kick", args, "MethodsPlayerAdministration");
            }), false);
        }
    }
}
