using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Weapons
{
    class MethodsWeapons : BaseScript
    {
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
            foreach (string am in Dictionary.ammo[ammo])
            {
                int ammoType = API.GetHashKey(am);
                API.SetPedAmmoByType(playerPed, ammoType, ammoQuantity);
            }
        }

        public void Ammo(List<object> args)
        {
            int playerPed = API.PlayerPedId();
            foreach (string ammo in Dictionary.ammoType)
            {
                foreach (string am in Dictionary.ammo[ammo])
                {
                    int ammoType = API.GetHashKey(am);
                    API.SetPedAmmoByType(playerPed, ammoType, 200);
                }
            }
        }
    }
}
