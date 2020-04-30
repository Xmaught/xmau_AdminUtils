using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient
{
    class Commands : BaseScript
    {

        public Commands()
        {

            



            API.RegisterCommand("weather", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("Weather", args, "Methods");
            }), false);

            API.RegisterCommand("scenario", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("Scenario", args, "Methods");
            }), false);

            API.RegisterCommand("handup", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("HandUp", args, "Methods");
            }), false);

            //API.RegisterCommand("setweather", new Action<int, List<object>, string>((source, args, raw) =>
            //{
            //    AdminControl.executeAdminCommand("SetWeather", args, "Methods");
            //}), false);
        }
    }
}
