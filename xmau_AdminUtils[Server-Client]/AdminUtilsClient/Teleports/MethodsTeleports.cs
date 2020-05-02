using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Teleports
{
    class MethodsTeleports : BaseScript
    {
        public static Vector3 lastTpCoords = new Vector3(0.0F, 0.0F, 0.0F);
        static bool guarma = false;
        public static bool tpView;
        public static bool deleteOn = false;
        public static List<string> savedpos= new List<string>();

        public MethodsTeleports()
        {
            EventHandlers["vorp:sendCoordsToDestinyBring"] += new Action<Vector3>(Bring);
            EventHandlers["vorp:askForCoords"] += new Action<string>(ResponseCoords);
            EventHandlers["vorp:coordsToStart"] += new Action<Vector3>(TpToPlayerDone);
            EventHandlers["vorp:servepos"] += new Action<dynamic>(ServePos);
            

            Tick += OnView;
            Tick += OnTpView;
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
        public void Bring(Vector3 bringCoords)
        {
            Debug.WriteLine(bringCoords.X.ToString());
            Utils.TeleportToCoords(bringCoords.X, bringCoords.Y, bringCoords.Z);
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
            Utils.TeleportToCoords(coordsToTp.X,coordsToTp.Y,coordsToTp.Z);
        }

        public void TpBack(List<object> args)
        {
            if (Utils.blip != -1)
            {
                API.RemoveBlip(ref Utils.blip);
                Utils.blip = -1;
                Utils.TeleportToCoords(lastTpCoords.X,lastTpCoords.Y,lastTpCoords.Z);
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
                API.SetEntityCoords(API.PlayerPedId(), 1471.457F, -7128.961F, 75.80013F, false,false,false,false);
                Function.Call((Hash)0xA657EC9DBC6CC900, 1935063277);
                Function.Call((Hash)0xE8770EE02AEE45C2, 1);
                Function.Call((Hash)0x74E2261D2A66849A, true);
                guarma = true;
            }
            else
            {
                API.SetEntityCoords(API.PlayerPedId(), 318.1798F, -1296.762F, 44.15731F, false, false, false, false);
                Function.Call((Hash)0xA657EC9DBC6CC900, -1868977180);
                Function.Call((Hash)0xE8770EE02AEE45C2, 0);
                Function.Call((Hash)0x74E2261D2A66849A, false);
                guarma = false;
            }
            
        }

        [Tick]
        public async Task OnTpView()
        {
            await Delay(0);
            int entity = 0;
            bool hit = false;
            Vector3 endCoord = new Vector3();
            Vector3 surfaceNormal = new Vector3();
            Vector3 camCoords = API.GetGameplayCamCoord();
            Vector3 sourceCoords = Utils.GetCoordsFromCam(100000.0F);
            int rayHandle = API.StartShapeTestRay(camCoords.X, camCoords.Y, camCoords.Z, sourceCoords.X, sourceCoords.Y, sourceCoords.Z, -1, API.PlayerPedId(), 0);
            API.GetShapeTestResult(rayHandle, ref hit, ref endCoord, ref surfaceNormal, ref entity);



            if (API.IsControlJustPressed(0, 0xCEE12B50) && tpView && endCoord.X != 0.0)
            {
                Utils.TeleportToCoords(endCoord.X, endCoord.Y, endCoord.Z);
            }
        }



        [Tick]
        public async Task OnView()
        {
            int entity = 0;
            bool hit = false;
            Vector3 endCoord = new Vector3();
            Vector3 surfaceNormal = new Vector3();
            Vector3 camCoords = API.GetGameplayCamCoord();
            Vector3 sourceCoords = Utils.GetCoordsFromCam(1000.0F);
            int rayHandle = API.StartShapeTestRay(camCoords.X, camCoords.Y, camCoords.Z, sourceCoords.X, sourceCoords.Y, sourceCoords.Z, -1, API.PlayerPedId(), 0);
            API.GetShapeTestResult(rayHandle, ref hit, ref endCoord, ref surfaceNormal, ref entity);
            if (tpView)
            {
                //API.DrawLightWithRange(endCoord.X, endCoord.Y, endCoord.Z, 255, 255, 255, 2.0F, 200000000.0F);
                Function.Call((Hash)0x2A32FAA57B937173, -1795314153, endCoord.X, endCoord.Y, endCoord.Z, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.5F, 0.5F, 50.0F, 0, 255, 0, 155, false, false, 2, false, 0, 0, false);
            }
        }


        public void TpView(List<object> args)
        {
            if (tpView)
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), false);
                tpView = false;
            }
            else
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), true);
                tpView = true;
            }
            //API.GetShapeTestResult();
            //API.StartEntityFire
        }


        public void Spos(List<object> args)
        {
            string name = args[0].ToString();
           
            Vector3 actualPos = API.GetEntityCoords(API.PlayerPedId(),true,true);
            TriggerServerEvent("vorp:spos", name, actualPos);
        }

        public void TeleportPos(List<object> args)
        {
            float x = float.Parse(args[0].ToString());
            float y = float.Parse(args[1].ToString());
            float z = float.Parse(args[2].ToString());
            Utils.TeleportToCoords(x, y, z);
        }

        public void DeletePos(List<object> args)
        {
            TriggerServerEvent("vorp:deletepos",args[0]);
        }

        private void ServePos(dynamic savedposserve)
        {
            savedpos.Clear();
            foreach (var v in savedposserve)
            {
                savedpos.Add(v);
            }
        }
    }
}
