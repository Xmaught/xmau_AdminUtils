using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsServer
{
    public class AdminUtilsServer : BaseScript
    {
        public AdminUtilsServer()
        {
            EventHandlers["vorp:ownerCoordsToBring"] += new Action<Vector3, int>(CoordsToBringPlayer);
            EventHandlers["vorp:askCoordsToTPPlayerDestiny"] += new Action<Player, int>(CoordsToPlayerDestiny);
            EventHandlers["vorp:callbackCoords"] += new Action<string, Vector3>(CoordsToStart);
            EventHandlers["vorp:kick"] += new Action<Player,int>(Kick); 
            EventHandlers["vorp:privateMesage"] += new Action<Player, int, string>(PrivateMessage); 
            EventHandlers["vorp:broadCastMessage"] += new Action<Player, string>(BroadCastMessage);
            EventHandlers["vorp:slap"] += new Action<Player, int>(Slap); 
            EventHandlers["vorp:stopplayer"] += new Action<Player, int>(StopP);
            EventHandlers["vorp:thor"] += new Action<Vector3>(ThorServer);
            EventHandlers["vorp:requestPlayerToSpectate1"] += new Action<Player,int>(Spectate1);
            EventHandlers["vorp:requestPlayerToSpectate3"] += new Action<int, int>(Spectate3);
            EventHandlers["redemrp:playerLoaded"] += new Action<int, dynamic>(GetAdmin);
        }

        private void GetAdmin(int source, dynamic user)
        {
            bool admin = false;
            PlayerList pl = new PlayerList();
            Player p = pl[source];

            Debug.WriteLine(user.getGroup());
                if (user.getGroup() == "admin")
                {
                    admin = true;
                    p.TriggerEvent("vorp:setAdmin", admin);
                }
            
        }

        private void ThorServer(Vector3 thorCoords)
        {
            Debug.WriteLine("entra server");
            TriggerClientEvent("vorp:thordone", thorCoords);
        }

        private void Spectate3(int sourceId, int ped)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[sourceId];
            TriggerClientEvent(p, "vorp:requestPlayerToSpectate4", ped);
        }

        private void Spectate1([FromSource]Player player,int idDestinatary)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[idDestinatary];
            TriggerClientEvent(p, "vorp:requestPlayerToSpectate2",player.Handle);
        }




        
        private void CoordsToBringPlayer(Vector3 coordToSend, int destinataryID)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[destinataryID];
            TriggerClientEvent(p, "vorp:sendCoordsToDestinyBring", coordToSend);
        }

       
        private void CoordsToPlayerDestiny([FromSource]Player ply, int destinataryID)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[destinataryID];
            TriggerClientEvent(p, "vorp:askForCoords", ply.Handle);
        }

        
        private void CoordsToStart(string sourceID, Vector3 coordsDestiny)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[int.Parse(sourceID)];
            TriggerClientEvent(p, "vorp:coordsToStart", coordsDestiny);
        }

        private void StopP([FromSource]Player player, int id)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[id];
            TriggerClientEvent(p,"vorp:stopit");
        }

        private void Slap([FromSource]Player player, int idDestinatary)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[idDestinatary];
            p.TriggerEvent("vorp:slapback");
        }

        private void Kick([FromSource]Player player, int id)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[id];
            p.Drop("Kicked by Staff");
        }

        private void PrivateMessage([FromSource]Player player, int id, string message)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[id];
            TriggerClientEvent(p,"vorp:Tip", message, 8000);
            //Exports["redem_roleplay"].DisplayTopCenterNotification(0, message, 8000);
        }

        private void BroadCastMessage([FromSource]Player player, string message)
        {
            TriggerClientEvent("vorp:NotifyLeft", player.Name, message, "generic_textures", "tick", 12000);
        }
    }
}