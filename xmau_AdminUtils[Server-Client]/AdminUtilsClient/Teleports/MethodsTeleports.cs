using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Teleports
{
    class MethodsTeleports : BaseScript
    {
        public static Vector3 lastTpCoords = new Vector3(0.0F, 0.0F, 0.0F);
        static bool guarma = false;

        public MethodsTeleports()
        {
            EventHandlers["vorp:sendCoordsToDestinyBring"] += new Action<Vector3>(Bring);
            EventHandlers["vorp:askForCoords"] += new Action<string>(ResponseCoords);
            EventHandlers["vorp:coordsToStart"] += new Action<Vector3>(TpToPlayerDone);
        }

        public async void TpToWaypoint(List<object> args)
        {
            Vector3 waypointCoords = API.GetWaypointCoords();
            if (Utils.blip == -1)
            {
                lastTpCoords = API.GetEntityCoords(API.PlayerPedId(), true, true);
                Utils.CreateBlip();
            }

            await Utils.TeleportAndFoundGroundAsync(waypointCoords);
        }

        /// <summary>
        /// Method that teleport player to coords in ground
        /// </summary>
        /// <param name="args"> string x, string y </param>

        public async void TpToCoords(List<object> args)
        {
            try
            {
                if (Utils.blip == -1)
                {
                    lastTpCoords = API.GetEntityCoords(API.PlayerPedId(), true, true);
                    Utils.CreateBlip();
                }
                float XCoord = float.Parse(args[0].ToString());
                float YCoord = float.Parse(args[1].ToString());
                float ZCoord = 0.0f;
                Vector3 chosenCoords = new Vector3(XCoord, YCoord, ZCoord);
                await Utils.TeleportAndFoundGroundAsync(chosenCoords);

            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        /// <summary>
        /// Method that send player coords through server to bring player to own coords
        /// </summary>
        /// <param name="args">None</param>
        public void TpBring(List<object> args)
        {
            int destinataryID = int.Parse(args[0].ToString());
            Vector3 ownCoords = API.GetEntityCoords(API.PlayerPedId(), true, true);

            TriggerServerEvent("vorp:ownerCoordsToBring", ownCoords, destinataryID);
        }

        /// <summary>
        /// Method that receive coords from the server to make an teleport requested by admin
        /// </summary>
        /// <param name="bringCoords"></param>
        public async void Bring(Vector3 bringCoords)
        {
            Debug.WriteLine(bringCoords.X.ToString());
            await Utils.TeleportAndFoundGroundAsync(bringCoords);
        }


        /// <summary>
        /// Method that request coords from the player destiny
        /// </summary>
        /// <param name="args"></param>
        public void TpToPlayer(List<object> args)
        {
            if (Utils.blip == -1)
            {
                lastTpCoords = API.GetEntityCoords(API.PlayerPedId(), true, true);
                Utils.CreateBlip();
            }
            Debug.WriteLine("1");
            int destinataryID = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:askCoordsToTPPlayerDestiny", destinataryID);
        }

        /// <summary>
        /// Method that response to the petition of coords made by TpToPlayer through server
        /// </summary>
        /// <param name="sourceID"></param>
        private void ResponseCoords(string sourceID)
        {
            Vector3 responseCoords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            TriggerServerEvent("vorp:callbackCoords", sourceID, responseCoords);
        }


        /// <summary>
        /// Method that teleport the source to player destinatary
        /// </summary>
        /// <param name="coordsToTp"></param>
        private async void TpToPlayerDone(Vector3 coordsToTp)
        {
            await Utils.TeleportAndFoundGroundAsync(coordsToTp);
        }

        public async void TpBack(List<object> args)
        {
            if (Utils.blip != -1)
            {
                API.RemoveBlip(ref Utils.blip);
                Utils.blip = -1;
                await Utils.TeleportAndFoundGroundAsync(lastTpCoords);
            }
        }

        public void DelBack(List<object> args)
        {
            API.RemoveBlip(ref Utils.blip);
            Utils.blip = -1;
            lastTpCoords = new Vector3(0.0F, 0.0F, 0.0F);
        }

        public void Guarma(List<object> args)
        {

            if (!guarma)
            {
                API.SetEntityCoords(API.PlayerPedId(), 1606.34F, -4096.1F, 89.68F,false,false,false,false);
                Function.Call((Hash)0xA657EC9DBC6CC900, 1935063277);
                Function.Call((Hash)0xE8770EE02AEE45C2, 1);
                Function.Call((Hash)0x74E2261D2A66849A, true);
                guarma = true;
            }
            else
            {
                API.SetEntityCoords(API.PlayerPedId(), 1606.34F, -4096.1F, 89.68F, false, false, false, false);
                Function.Call((Hash)0xA657EC9DBC6CC900, -1868977180);
                Function.Call((Hash)0xE8770EE02AEE45C2, 0);
                Function.Call((Hash)0x74E2261D2A66849A, false);
                guarma = false;
            }
            
        }



    }
}
