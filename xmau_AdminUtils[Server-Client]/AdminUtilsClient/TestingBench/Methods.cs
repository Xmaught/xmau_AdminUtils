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

        
        public void Weap(List<object> args)
        {
            int playerPed = API.PlayerPedId();
            int HashModel = API.GetHashKey(args[0].ToString());
            int ammoQuantity = int.Parse(args[1].ToString());
             
            API.GiveDelayedWeaponToPed(playerPed, (uint)HashModel, ammoQuantity, true, 2);
            API.SetPedAmmo(playerPed, (uint)HashModel, ammoQuantity);
        }

        public void WeapAmmo(List<object> args)
        {
            int playerPed = API.PlayerPedId();
            string ammo = args[0].ToString();
            int ammoQuantity = int.Parse(args[1].ToString());
            foreach(string am in Dictionary.ammo[ammo])
            {
                int ammoType = API.GetHashKey(am);
                API.SetPedAmmoByType(playerPed, ammoType, ammoQuantity);
            }
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
