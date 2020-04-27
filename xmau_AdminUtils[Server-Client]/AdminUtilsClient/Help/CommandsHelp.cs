using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Help
{
    class CommandsHelp : BaseScript
    {
        public CommandsHelp()
        {
            API.RegisterCommand("com", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
                {
                    AdminControl.executeAdminCommand("Com", args, "MethodsHelp");
                }), false);
        }
    }
}
