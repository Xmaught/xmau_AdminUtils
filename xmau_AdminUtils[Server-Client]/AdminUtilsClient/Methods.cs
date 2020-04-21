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
        public static Vector3 lastTpCoords = new Vector3(0.0F, 0.0F, 0.0F);
        static bool godmodeON = false;
        int spectateCam;
        bool handcuffed = false;
        Vector3 pCCoords;
        bool noclip = false;
        float heading;
        float speed = 1.28F;
        int playerPed;

        

        public Methods()
        {

            Tick += Noc;

            EventHandlers["vorp:sendCoordsToDestinyBring"] += new Action<Vector3>(Bring);
            EventHandlers["vorp:askForCoords"] += new Action<string>(ResponseCoords);
            EventHandlers["vorp:coordsToStart"] += new Action<Vector3>(TpToPlayerDone);
            EventHandlers["vorp:slapback"] += new Action(SlapDone);
            EventHandlers["vorp:stopit"] += new Action(StopIt);
        }

        



        //public static void BridgeToExecute(string command, List<object> args)
        //{
        //    executeAdminCommand(command, args);
        //}

        public static void executeAdminCommand(string command, List<object> args)
        {
            Type type = typeof(Methods);
            MethodInfo mi = type.GetMethod(command);
            Methods meth = new Methods();
            mi.Invoke(meth, new Object[] { args });
        }



        public async void Com(List<object> args)
        {
            Debug.WriteLine("spawnobj objectModel ----> Spawn object");
            Debug.WriteLine("spawnped pedModel ----> Spawn ped");
            Debug.WriteLine("spawnveh vechicleModel ----> Spawn vechicle");

            Debug.WriteLine("tpwayp  ----> Teleport to a waypoint(Mark a waypoint before)");
            Debug.WriteLine("tpcoords [cooordX] [coordY]  ----> Teleport to coord");
            Debug.WriteLine("tpplayer idPlayer ----> Teleport to player");
            Debug.WriteLine("tpbring idPlayer ----> Bring player to your position");

            Debug.WriteLine("golden ----> You and you horse become full gold");
        }




        /// <summary>
        /// Method that create an object in front or the character
        /// </summary>
        /// <param name="args"> object model </param>
        public async void Spawnobj(List<object> args)
        {
            string objeto = args[0].ToString();
            int HashObjeto = API.GetHashKey(objeto);
            Vector3 coords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            await Utils.LoadModel(HashObjeto);
            int cosa = API.CreateObject((uint)HashObjeto, coords.X + 0.5f, coords.Y + 0.5f, coords.Z + 1.0f, true, true, false, true, true);
            API.PlaceObjectOnGroundProperly(cosa, 1);
        }

        /// <summary>
        /// Method that create a ped in front or the character, in case that the created ped
        /// is an horse the character is setted mounting it
        /// </summary>
        /// <param name="args"> ped model </param>
        public async void Spawnped(List<object> args)
        {
            string ped = args[0].ToString();
            int HashPed = API.GetHashKey(ped);
            Vector3 coords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            await Utils.LoadModel(HashPed);
            int pedCreated = API.CreatePed((uint)HashPed, coords.X + 1, coords.Y, coords.Z, 0, true, true, true, true);
            //Spawn
            Function.Call((Hash)0x283978A15512B2FE, pedCreated, true);
            //SetPedIntoVehicle
            Function.Call((Hash)0x028F76B6E78246EB, API.PlayerPedId(), pedCreated, -1, false);
        }

        /// <summary>
        /// Method that create a vehicle and the character is setted mounting it
        /// </summary>
        /// <param name="args"> vehicle model </param>
        public async void Spawnveh(List<object> args)
        {
            string veh = args[0].ToString();
            int HashVeh = API.GetHashKey(veh);
            Vector3 coords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            await Utils.LoadModel(HashVeh);
            int vehCreated = API.CreateVehicle((uint)HashVeh, coords.X + 1, coords.Y, coords.Z, 0, true, true, true, true);
            //Spawn
            Function.Call((Hash)0x283978A15512B2FE, vehCreated, true);
            //TaskWanderStandard
            Function.Call((Hash)0xBB9CE077274F6A1B, 10, 10);
            //SetPedIntoVehicle
            Function.Call((Hash)0x23f74c2fda6e7c61, API.PlayerPedId(), vehCreated, -1, false);
        }


        //Tps

        /// <summary>
        /// Method that teleport player to waypoint
        /// </summary>
        /// <param name="args">None</param>
        public void TpToWaypoint(List<object> args)
        {
            Vector3 waypointCoords = API.GetWaypointCoords();
            lastTpCoords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            Utils.CreateBlip();
            
            Utils.TeleportAndFoundGroundAsync(waypointCoords);
        }

        /// <summary>
        /// Method that teleport player to coords in ground
        /// </summary>
        /// <param name="args"> string x, string y </param>

        public void TpToCoords(List<object> args)
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
                    Utils.TeleportAndFoundGroundAsync(chosenCoords);
                
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
            Utils.TeleportAndFoundGroundAsync(bringCoords);
        }


        /// <summary>
        /// Method that request coords from the player destiny
        /// </summary>
        /// <param name="args"></param>
        public void TpToPlayer(List<object> args)
        {
            lastTpCoords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            Utils.CreateBlip();
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
        private void TpToPlayerDone(Vector3 coordsToTp)
        {
            Utils.TeleportAndFoundGroundAsync(coordsToTp);
        }

        public void TpBack(List<object> args)
        {
            if (Utils.blip != -1)
            {
                API.RemoveBlip(ref Utils.blip);
                Utils.blip = -1;
                Utils.TeleportAndFoundGroundAsync(lastTpCoords);
            } 
        }

        public void DelBack(List<object> args)
        {
            API.RemoveBlip(ref Utils.blip);
            Utils.blip = -1;
            lastTpCoords = new Vector3(0.0F, 0.0F, 0.0F);
        }



        public async void Golden(List<object> args)
        {
            int pPedId = API.PlayerPedId();
            //Jugador cores
            Function.Call((Hash)0xC6258F41D86676E0, pPedId, 0, 100);
            Function.Call((Hash)0xC6258F41D86676E0, pPedId, 1, 100);
            Function.Call((Hash)0xC6258F41D86676E0, pPedId, 2, 100);
            //Jugador circles                   
            Function.Call((Hash)0x4AF5A4C7B9157D14, pPedId, 0, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, pPedId, 1, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, pPedId, 2, 5000.0);

            Function.Call((Hash)0xF6A7C08DF2E28B28, pPedId, 1, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, pPedId, 2, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, pPedId, 0, 5000.0);


            int entity = Function.Call<int>(Hash.GET_ENTITY_ATTACHED_TO, pPedId);


            Function.Call((Hash)0x09A59688C26D88DF, entity, 0, 1100);
            Function.Call((Hash)0x09A59688C26D88DF, entity, 1, 1100);
            Function.Call((Hash)0x09A59688C26D88DF, entity, 2, 1100);

            Function.Call((Hash)0x75415EE0CB583760, entity, 0, 1100);
            Function.Call((Hash)0x75415EE0CB583760, entity, 1, 1100);
            Function.Call((Hash)0x75415EE0CB583760, entity, 2, 1100);

            Function.Call((Hash)0x5DA12E025D47D4E5, entity, 0, 10);
            Function.Call((Hash)0x5DA12E025D47D4E5, entity, 1, 10);
            Function.Call((Hash)0x5DA12E025D47D4E5, entity, 2, 10);

            Function.Call((Hash)0x920F9488BD115EFB, entity, 0, 10);
            Function.Call((Hash)0x920F9488BD115EFB, entity, 1, 10);
            Function.Call((Hash)0x920F9488BD115EFB, entity, 2, 10);

            Function.Call((Hash)0xF6A7C08DF2E28B28, entity, 0, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, entity, 1, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, entity, 2, 5000.0);

            Function.Call((Hash)0x4AF5A4C7B9157D14, entity, 0, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, entity, 1, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, entity, 2, 5000.0);
        }


        public void GodMode(List<object> args)
        {
            
            if (!godmodeON)
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), true);
                godmodeON = true;
            } else
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), false);
                godmodeON = false;
            }
        }

        public void Noclip(List<object> args)
        {


            int playerPed = API.PlayerPedId();
            heading = API.GetEntityHeading(playerPed);

            if (!noclip)
            {
                API.FreezeEntityPosition(playerPed,true);
                //Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), true);
                noclip = true;
                Noc();
            } else
            {
                API.FreezeEntityPosition(playerPed, false);
                //Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), false);
                noclip = false;
                
            }
        }

        [Tick]
        private async Task Noc()
        {
            while (noclip)
            {
                int playerPed = API.PlayerPedId();
                API.SetEntityHeading(playerPed, heading);
                await Delay(0);
                if (API.IsControlPressed(0, 0x8FD015D8)) //W
                {
                    Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, speed, -1.0F);
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0xD27782E3)) //S
                {
                    Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, -speed, -1.0F);
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0x7065027D)) //A
                {
                    Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, -speed, 0.0F, -1.0F);
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0xB4E465B4)) //D
                {
                    Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, speed, 0.0F, -1.0F);
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0x26E9DC00)) //Z
                {
                    Vector3 c = new Vector3();
                    if (speed > 1.0F)
                    {
                        c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, -speed * 2);
                    }
                    else
                    {
                        c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, -speed - 1.0F);
                    }
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0x8CC9CD42)) //X
                {
                    Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, speed - 1.0F);
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0x6319DB71)) //UP
                {
                    if (speed > 0.5F)
                    {
                        speed = speed + 0.5F;
                    }
                }
                if (API.IsControlPressed(0, 0x05CA7C52)) //DOWN
                {
                    if (speed > 0.5)
                    {
                        speed = speed - 0.5F;
                    }
                }
                if (API.IsControlPressed(0, 0x9959A6F0)) //C
                {
                    speed = 1.28F;
                }
                if (API.IsControlPressed(0, 0xDE794E3E)) //Q
                {
                    heading = heading + 2.0F;
                }
                if (API.IsControlPressed(0, 0xCEFD9220)) //E
                {
                    heading = heading - 2.0F;
                }

            }
        }
        


        public void Spec(List<object> args)
        {
            int destinataryId = int.Parse(args[0].ToString());
            pCCoords = API.GetGameplayCamCoord();
            Vector3 playerCoords = API.GetEntityCoords(API.GetPlayerPed(API.GetPlayerFromServerId(destinataryId)),true,true);
            
            spectateCam = API.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", playerCoords.X, playerCoords.Y, playerCoords.Z, -9.622695f, 0.0f, -86.08074f, 40.00f, false, 0);
            API.SetCamActive(spectateCam, true);
            API.RenderScriptCams(true, true, 1000, true, true, 0);
        }

        public void SSpec(List<object> args)
        {
            API.SetCamActive(spectateCam, false);
            API.DestroyCam(spectateCam, true);
            spectateCam = API.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", pCCoords.X, pCCoords.Y, pCCoords.Z, -9.622695f, 0.0f, -86.08074f, 40.00f, false, 0);
            API.SetCamActive(spectateCam, true);
            API.RenderScriptCams(true, true, 1000, true, true, 0);
        }

        public void StopPlayer(List<object> args)
        {
            int idPlayer = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:stopplayer", idPlayer);            
        }

        public void StopIt()
        {
            if (!handcuffed)
            {
                API.FreezeEntityPosition(API.PlayerPedId(), true);
                Function.Call((Hash)0xDF1AF8B5D56542FA,API.PlayerPedId(), true);
                handcuffed = true;
            }
            else
            {
                API.FreezeEntityPosition(API.PlayerPedId(), false);
                Function.Call((Hash)0xDF1AF8B5D56542FA, API.PlayerPedId(), false);
                handcuffed = false;
            }
        }

        public void Slap(List<object> args)
        {
            int destinataryID = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:slap", destinataryID);
        }

        private void SlapDone()
        {
            Vector3 idCoords = API.GetEntityCoords(API.PlayerPedId(),true,true);
            Utils.TeleportToCoords(idCoords.X, idCoords.Y, idCoords.Z + 1000.0F);
        }

        public void Kick(List<object> args)
        {
            int id = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:kick", id);
        }



        public void PrivateMessage(List<object> args)
        {
            string message = "";
            int id = int.Parse(args[0].ToString());
            for ( int i = 1; i < args.Count; i++)
            {
                message += args[i].ToString() + " ";
                
            }
            
            TriggerServerEvent("vorp:privateMesage", id, message);
        }

        public void BroadCast(List<object> args)
        {
            string message = "";
            for (int i = 0; i < args.Count; i++)
            {
                message += args[i].ToString() + " ";

            }

            TriggerServerEvent("vorp:broadCastMessage", message);
        }
    }
}
