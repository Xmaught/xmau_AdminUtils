using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Notifications
{
    class CommandsNotifications : BaseScript
    {
        public CommandsNotifications()
        {
            API.RegisterCommand("pm", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("PrivateMessage", args, "MethodsNotifications");
            }), false);

            API.RegisterCommand("bc", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AdminControl.executeAdminCommand("BroadCast", args, "MethodsNotifications");
            }), false);
        }
    }
}
