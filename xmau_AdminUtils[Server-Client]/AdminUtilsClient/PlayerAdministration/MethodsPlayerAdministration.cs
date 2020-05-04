using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.PlayerAdministration
{
    class MethodsPlayerAdministration : BaseScript
    {
        static int spectateCam;
        static bool handcuffed = false;
        public static bool deleteOn = false;
        public static List<string> savedbans = new List<string>();
        public static List<int> playerList = new List<int>();
        static List<int> blipsList = new List<int>();
        static bool playersFollow = false;
        //static Vector3 pCCoords;
        public MethodsPlayerAdministration()
        {

            Tick += freezeAnim;
            //Tick += PlayersFollow;


            EventHandlers["vorp:slapback"] += new Action(SlapDone);
            EventHandlers["vorp:stopit"] += new Action(StopIt);
            EventHandlers["vorp:requestPlayerToSpectate2"] += new Action<string>(Spectate2);
            EventHandlers["vorp:requestPlayerToSpectate4"] += new Action<int>(Spectate4);
            EventHandlers["vorp:servebans"] += new Action<dynamic>(ServeBans);
        }

        public void Spec(List<object> args)
        {
            int destinataryId = int.Parse(args[0].ToString());

            TriggerServerEvent("vorp:requestPlayerToSpectate1", destinataryId);
        }


        private void Spectate2(string idPlayer)
        {
            int pPed = API.PlayerPedId();
            int dest = int.Parse(idPlayer);
            TriggerServerEvent("vorp:requestPlayerToSpectate3", dest, pPed);
        }

        private void Spectate4(int spectatedPlayer)
        {
            API.SetEntityVisible(API.PlayerPedId(), false);
            spectateCam = API.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", API.GetEntityCoords(API.PlayerPedId(),true,true).X, API.GetEntityCoords(API.PlayerPedId(), true, true).Y, API.GetEntityCoords(API.PlayerPedId(), true, true).Z, -9.622695f, 0.0f, -86.08074f, 40.00f, false, 0);
            API.AttachCamToEntity(spectateCam,spectatedPlayer,0.0F,0.0F,10.0F,true);
            API.SetCamActive(spectateCam, true);
            API.RenderScriptCams(true, true, 1000, true, true, 0);
        }

        public void SSpec(List<object> args)
        {
            int destinataryId = int.Parse(args[0].ToString());
            API.SetEntityVisible(API.PlayerPedId(), true);
            API.NetworkSetInSpectatorMode(false, 1);
            API.SetCamActive(spectateCam, false);
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
                API.DisableAllControlActions(-1);
                Function.Call((Hash)0xDF1AF8B5D56542FA, API.PlayerPedId(), true);
                handcuffed = true;
            }
            else
            {
                API.FreezeEntityPosition(API.PlayerPedId(), false);
                Function.Call((Hash)0xDF1AF8B5D56542FA, API.PlayerPedId(), false);
                handcuffed = false;
            }
        }

        [Tick]
        private async Task freezeAnim()
        {
            if (handcuffed)
            {
                await Delay(0);
                API.ClearPedTasksImmediately(API.PlayerPedId(), 1, 1);
            }
        }

        public void Slap(List<object> args)
        {
            int destinataryID = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:slap", destinataryID);
        }

        private void SlapDone()
        {
            Vector3 idCoords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            Utils.TeleportToCoords(idCoords.X, idCoords.Y, idCoords.Z + 1000.0F);
        }

        public void Kick(List<object> args)
        {
            int id = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:kick", id);
        }

        public void Sbans(List<object> args)
        {
            int playerid = int.Parse(args[0].ToString());
            string reason = args[1].ToString();

            
            TriggerServerEvent("vorp:sbans", playerid,reason);
        }
                
        public void DeleteBans(List<object> args)
        {
            TriggerServerEvent("vorp:deletebans", args[0]);
        }

        private void ServeBans(dynamic savedbansserve)
        {
            savedbans.Clear();
            foreach (var v in savedbansserve)
            {
                savedbans.Add(v);
            }
        }

        public void PlayerList(List<object> args)
        {
            List<int> playerList = new List<int>();
            foreach (var i in API.GetActivePlayers())
            {
                playerList.Add(i);
            }
        }
        
        public void PlayerBlips(List<object> args)
        {
            if (playersFollow)
            {
                playersFollow = false;
                ClearBlips();
            }
            else
            {
                playersFollow = true;
                CreateBlips();
            }
        }
        //Mañana muertos con blip
        private async Task CreateBlips()
        {
            foreach (var i in API.GetActivePlayers())
            {
                await Delay(10);
                Vector3 coords = API.GetEntityCoords(API.GetPlayerPed(i), true, true);
                int _blip = Function.Call<int>((Hash)0x23F74C2FDA6E7C61, 1664425300, API.GetPlayerPed(i));
                Function.Call((Hash)0x74F74D3207ED525C, _blip, -1580514024, 1);
                Function.Call((Hash)0xD38744167B2FA257, _blip, 0.2F);
                Function.Call((Hash)0x9CB1A1623062F402, _blip, $"{API.GetPlayerName(i)} id: {API.GetPlayerServerId(i)}");
                blipsList.Add(_blip);
            }
        }

        private async Task ClearBlips()
        {
            foreach (int b in blipsList)
            {
                int actualBlip = b;
                API.RemoveBlip(ref actualBlip);
            }
            blipsList.Clear();
            await Delay(1);
        }
    }
}
