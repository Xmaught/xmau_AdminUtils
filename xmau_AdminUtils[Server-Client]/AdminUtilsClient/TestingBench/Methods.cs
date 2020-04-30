using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient
{
    public class Methods : BaseScript
    {
        static uint weatherType1;
        static uint weatherType2;
        static float percentWeather2;
        

        public Methods()
        {
            

        }



        //API.TaskPlayAnim(API.PlayerPedId(), API.GetHashKey("amb_work@world_human_stand_fishing@male_b@base"), API.GetHashKey("base_fishingpole"), 8, -8, 10000, 0, 0, 1, 0, 0, 0, 0);

        public void Scenario(List<object> args)
        {
            Function.Call((Hash)0x524B54361229154F,API.PlayerPedId(), API.GetHashKey("PROP_HUMAN_SEAT_CHAIR_FISHING_ROD"), 7000, true, 0, 0, false);
            
        }

        //prueba manos arriba
        public void HandUp(List<object> args)
        {
            //API.TaskHandsUp(API.PlayerPedId(),5000, API.PlayerPedId(),0,false);
            API.SetSwimMultiplierForPlayer(API.PlayerPedId(), 1000000.0F);
            int outEntity = API.PlayerPedId();
            int playerPed= API.FindFirstPed(ref outEntity);
        }

        

        public void Weather(List<object> args)
        {
            // Function.Call(Hash._we, 0x54A69840, 0x54A69840, 0.5F,true);
            

            API.GetWeatherTypeTransition(ref weatherType1,ref weatherType2, ref percentWeather2);
            Debug.WriteLine(weatherType1.ToString());
            Debug.WriteLine(weatherType2.ToString());
            Debug.WriteLine(percentWeather2.ToString());
        }

        //public void SetWeather(List<object> args)
        //{
        //    int tipe1 = 885599005;
        //    Function.Call(Hash._SET_WEATHER_TYPE_TRANSITION, API.GetHashKey(tipe1.ToString()), API.GetHashKey(tipe1.ToString()), 1.0F,true);
        //    API.GetWeatherTypeTransition(ref weatherType1, ref weatherType2, ref percentWeather2);
        //    Debug.WriteLine(API.GetHashKey(tipe1.ToString()).ToString());
        //    Debug.WriteLine(API.GetHashKey(weatherType2.ToString()).ToString());
        //    Debug.WriteLine(percentWeather2.ToString());
        //}
    }
}
