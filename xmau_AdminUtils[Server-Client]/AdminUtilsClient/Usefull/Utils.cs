﻿using AdminUtilsClient.Teleports;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient
{
    public class Utils : BaseScript
    {
        public static int blip = -1;

        public static async Task TeleportAndFoundGroundAsync(Vector3 tpCoords)
        {
            float groundZ = 0.0F;
            Vector3 normal = new Vector3(1.0f, 1.0f, 1.0f);
            bool foundGround = false;


            for (int i = 1; i < 1000.0; i++)
            {
                API.SetEntityCoords(API.PlayerPedId(), tpCoords.X, tpCoords.Y, (float)i, true, true, true, false);
                foundGround = API.GetGroundZAndNormalFor_3dCoord(tpCoords.X, tpCoords.Y, (float)i, ref groundZ, ref normal);
                await Delay(1);
                if (foundGround == true)
                {
                    API.SetEntityCoords(API.PlayerPedId(), tpCoords.X, tpCoords.Y, (float)i, true, true, true, false);
                    break;
                }
            }
        }

        public static void TeleportToCoords(float x, float y, float z, float heading = 0.0f)
        {
            int playerPedId = API.PlayerPedId();
            API.SetEntityCoords(playerPedId, x, y, z, true, true, true, false);
            API.SetEntityHeading(playerPedId, heading);
        }

        public static async Task<bool> LoadModel(int hash)
        {
            if (Function.Call<bool>(Hash.IS_MODEL_VALID, hash))
            {
                Function.Call(Hash.REQUEST_MODEL, hash);
                while (!Function.Call<bool>(Hash.HAS_MODEL_LOADED, hash))
                {
                    Debug.WriteLine($"Waiting for model {hash} load!");
                    await BaseScript.Delay(100);
                }
                return true;
            }
            else
            {
                Debug.WriteLine($"Model {hash} is not valid!");
                return false;
            }
        }

        public static void CreateBlip()
        {
            blip = Function.Call<int>((Hash)0x554D9D53F696D002, 203020899, MethodsTeleports.lastTpCoords.X, MethodsTeleports.lastTpCoords.Y, MethodsTeleports.lastTpCoords.Z);
            Debug.WriteLine(blip.ToString());
            Function.Call((Hash)0x74F74D3207ED525C, blip, -1546805641, 1);
            Function.Call((Hash)0xD38744167B2FA257, blip, 0.2F);
            Function.Call((Hash)0x9CB1A1623062F402, blip, "LastPosition");
        }
    }
}
