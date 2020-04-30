using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Peds
{
    class CommandsPeds : BaseScript
    {
        public CommandsPeds()
        {
            API.RegisterCommand("changeped", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("ChangeModel", args, "MethodsPeds");
            }), false);
        }
    }
}
