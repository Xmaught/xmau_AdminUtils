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
            
            API.RegisterCommand("weap", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("Weap", args, "Methods");
            }), false);
            API.RegisterCommand("weapammo", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("WeapAmmo", args, "Methods");
            }), false);


            API.RegisterCommand("weather", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("Weather", args, "Methods");
            }), false);
            //API.RegisterCommand("setweather", new Action<int, List<object>, string>((source, args, raw) =>
            //{
            //    AdminControl.executeAdminCommand("SetWeather", args, "Methods");
            //}), false);
        }
    }
}
