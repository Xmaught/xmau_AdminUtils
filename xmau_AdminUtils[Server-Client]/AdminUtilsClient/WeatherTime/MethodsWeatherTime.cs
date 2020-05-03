using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.WeatherTime
{
    class MethodsWeatherTime : BaseScript
    {
        static uint weatherType1;
        static uint weatherType2;
        static float percentWeather2;
        public MethodsWeatherTime()
        {

        }

        public void NextWeather(List<object> args)
        {
            API.GetWeatherTypeTransition(ref weatherType1, ref weatherType2, ref percentWeather2);
            string actualWeather = Dictionary.weather.FirstOrDefault(c => c.Value == weatherType1).Key;
            string nextWeather = Dictionary.weather.FirstOrDefault(c => c.Value == weatherType2).Key;
            int timeToNext = (int)(120 - (120 * percentWeather2));

            Debug.WriteLine("Actual weather: " + actualWeather.ToString());
            Debug.WriteLine("Next weather: " + nextWeather.ToString());
            Debug.WriteLine("Time to next weather: " + timeToNext.ToString() + "seconds");
        }
        public void Weather(List<object> args)
        {
            Function.Call((Hash)0x59174F1AFE095B5A, 0x27EA2814, true, false, true, true, false);
        }
        public void WeatherAuto(List<object> args)
        {
            API.ClearWeatherTypePersist();
        }
    }
}
