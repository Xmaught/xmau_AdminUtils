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
    }
}
