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
        static Vector3 pCCoords;
        public MethodsPlayerAdministration()
        {

            Tick += freezeAnim;


            EventHandlers["vorp:slapback"] += new Action(SlapDone);
            EventHandlers["vorp:stopit"] += new Action(StopIt);
            EventHandlers["vorp:requestPlayerToSpectate2"] += new Action<string>(Spectate2);
            EventHandlers["vorp:requestPlayerToSpectate4"] += new Action<int>(Spectate4);
        }

        public void Spec(List<object> args)
        {
            int destinataryId = int.Parse(args[0].ToString());

            TriggerServerEvent("vorp:requestPlayerToSpectate1", destinataryId);

            //pCCoords = API.GetGameplayCamCoord();
            //Vector3 playerCoords = API.GetEntityCoords(API.GetPlayerPed(API.GetPlayerFromServerId(destinataryId)),true,true);

            //spectateCam = API.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", playerCoords.X, playerCoords.Y, playerCoords.Z, -9.622695f, 0.0f, -86.08074f, 40.00f, false, 0);
            //API.SetCamActive(spectateCam, true);
            //API.RenderScriptCams(true, true, 1000, true, true, 0);
        }



        private void Spectate2(string idPlayer)
        {
            int pPed = API.PlayerPedId();
            TriggerServerEvent("vorp:requestPlayerToSpectate3", idPlayer, pPed);
        }

        private void Spectate4(int spectatedPlayer)
        {
            Debug.WriteLine(spectatedPlayer.ToString());
            API.NetworkSetInSpectatorMode(true, spectatedPlayer);
        }

        public void SSpec(List<object> args)
        {
            int destinataryId = int.Parse(args[0].ToString());
            API.NetworkSetInSpectatorMode(false, 1);
            //API.SetCamActive(spectateCam, false);
            //API.DestroyCam(spectateCam, true);
            //spectateCam = API.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", pCCoords.X, pCCoords.Y, pCCoords.Z, -9.622695f, 0.0f, -86.08074f, 40.00f, false, 0);
            //API.SetCamActive(spectateCam, true);
            //API.RenderScriptCams(true, true, 1000, true, true, 0);
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
    }
}
