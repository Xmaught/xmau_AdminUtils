using AdminUtilsClient.Teleports;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace AdminUtilsClient
{
    public class Utils : BaseScript
    {

        public static int blip = -1;

        public Utils()
        {
            EventHandlers["onResourceStop"] += new Action<string>(OnResourceStop);
        }
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
            heading = API.GetEntityHeading(playerPedId);
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
                    await BaseScript.Delay(100);
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        public static void CreateBlip()
        {
            blip = Function.Call<int>((Hash)0x554D9D53F696D002, 203020899, MethodsTeleports.lastTpCoords.X, MethodsTeleports.lastTpCoords.Y, MethodsTeleports.lastTpCoords.Z);
            Function.Call((Hash)0x74F74D3207ED525C, blip, -1546805641, 1);
            Function.Call((Hash)0xD38744167B2FA257, blip, 0.2F);
            Function.Call((Hash)0x9CB1A1623062F402, blip, "LastPosition");
        }

        private void OnResourceStop(string resourceName)
        {
            if (API.GetCurrentResourceName() != resourceName) return;

                API.RemoveBlip(ref blip);
        }

            public static Vector3 GetCoordsFromCam(float distance)
        {
            Vector3 rot = API.GetGameplayCamRot(2);
            Vector3 coord = API.GetGameplayCamCoord();

            float tZ = rot.Z * 0.0174532924F;
            float tX = rot.X * 0.0174532924F;




            float num = (float)Math.Abs(Math.Cos(tX));

            float newCoordX = coord.X + (float)(-Math.Sin(tZ)) * (num + distance);
            float newCoordY = coord.Y + (float)(Math.Cos(tZ)) * (num + distance);
            float newCoordZ = coord.Z + (float)(Math.Sin(tX)) * (num + distance);

            return new Vector3(newCoordX, newCoordY, newCoordZ);
        }

        public static async Task<List<object>> GetTwoByNUI(List<object> args,string label, string hint, string label2, string hint2)
        {
            string V1 = null;
            string V2 = null;
            TriggerEvent("vorpinputs:getInput", label, hint, new Action<dynamic>((value1) =>
            {
                V1 = value1;
                args.Add(V1);
            }));

            while (V1 == null)
            {
                await Delay(1000);
            }

            TriggerEvent("vorpinputs:getInput", label2, hint2, new Action<dynamic>((value2) =>
            {
                V2 = value2;
                args.Add(V2);
            }));

            while (V2 == null)
            {
                await Delay(1000);
            }

            return args;
        }

        public async static Task<List<object>> GetOneByNUI(List<object> args,string title, string hint)
        {
            string postValue = null;
            TriggerEvent("vorpinputs:getInput", title, hint, new Action<dynamic>((value) =>
            {
                postValue = value;
                args.Add(value);
            }));

            while (postValue == null)
            {
                await Delay(1000);
            }
            return args;
        }

        public void getNearestPlayers()
        {
            float closestDistance = 5.0F;
            int localPed = API.PlayerPedId();
            Vector3 coords = API.GetEntityCoords(localPed, true, true);
            List<int> closestPlayers = new List<int>();
            List<int> players = new List<int>();
            foreach (var player in API.GetActivePlayers())
            {
                players.Add(player);
            }

            foreach (var player in players)
            {
                int target = API.GetPlayerPed(player);
                if (target != localPed)
                {
                    Vector3 targetCoords = API.GetEntityCoords(target, true, true);
                    float distance = API.GetDistanceBetweenCoords(targetCoords.X, targetCoords.Y, targetCoords.Z,
                        coords.X, coords.Y, coords.Z, false);

                    if (closestDistance > distance)
                    {
                        closestPlayers.Add(player);
                    }
                }
            }

            foreach (var VARIABLE in closestPlayers)
            {
                // Debug.WriteLine(VARIABLE.ToString());
                // Debug.WriteLine(API.GetPlayerPed(VARIABLE).ToString());
                // Debug.WriteLine(localPed.ToString());
                //Debug.WriteLine(API.GetPlayerName(VARIABLE));
                //Debug.WriteLine(API.GetPlayerServerId(VARIABLE).ToString());
            }



        }
    }
}
