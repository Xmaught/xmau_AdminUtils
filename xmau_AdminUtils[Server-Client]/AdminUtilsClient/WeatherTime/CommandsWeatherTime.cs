using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.WeatherTime
{
    class CommandsWeatherTime : BaseScript
    {
        public CommandsWeatherTime()
        {
            API.RegisterCommand("nextweather", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("NextWeather", args, "MethodsWeatherTime");
            }), false);
            API.RegisterCommand("weather", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("Weather", args, "MethodsWeatherTime");
            }), false);
            API.RegisterCommand("weatherauto", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("WeatherAuto", args, "MethodsWeatherTime");
            }), false);
            API.RegisterCommand("weatherfrozen", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("WeatherFrozen", args, "MethodsWeatherTime");
            }), false);
        }

    }
}
