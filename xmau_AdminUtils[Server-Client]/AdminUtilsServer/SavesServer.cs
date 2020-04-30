using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CitizenFX.Core.Native;

namespace AdminUtilsServer
{
    class SavesServer : BaseScript
    {
        public static string file = $"{API.GetResourcePath(API.GetCurrentResourceName())}/spos.txt";
        public static string fileban = $"{API.GetResourcePath(API.GetCurrentResourceName())}/bans.txt";
        public static List<string> savedPos = new List<string>();
        public static List<string> savedBans = new List<string>();


        //Debug.WriteLine(file);
        public SavesServer()
        {
            EventHandlers["vorp:spos"] += new Action<string,Vector3>(SposServer);
            EventHandlers["vorp:callpos"] += new Action<Player>(CallPosServer);
            EventHandlers["vorp:deletepos"] += new Action<int>(DeletePosServer);

            LoadPos();

            EventHandlers["vorp:sbans"] += new Action<Player,int,string>(BansServer);
            EventHandlers["vorp:callbans"] += new Action<Player>(CallBansServer);
            EventHandlers["vorp:deletebans"] += new Action<int>(DeleteBansServer);

            LoadBans();

            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
        }

        private async void OnPlayerConnecting([FromSource]Player player, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            deferrals.defer();

            await Delay(0);

            var steamIdentifier = player.Identifiers["steam"];
            



            if (savedBans.Any(c=> c.StartsWith(steamIdentifier)))
            {
                deferrals.done("You are banned from this server. Reason: " + savedBans.FirstOrDefault(c => c.Contains(steamIdentifier)).Split(',')[1]);
                Debug.WriteLine("banned");
            } 
            else
            {
                deferrals.done();
                Debug.WriteLine("not banned");
            }

        }

    

    private void CallPosServer([FromSource]Player player)
        {
            TriggerClientEvent(player, "vorp:servepos",savedPos);
        }

        private void LoadPos()
        {
            savedPos = File.ReadAllLines(file).ToList();

            
        }

        private void DeletePosServer(int name)
        {
            savedPos.RemoveAt(name);
            SavePos();
        }

        private void SavePos() 
        {
            File.WriteAllLines(file, savedPos);
            LoadPos();
        }
        

        private void SposServer(string name, Vector3 coords)
        {
            savedPos.Add($"{name},{coords.X},{coords.Y},{coords.Z}");
            SavePos();
            
        }



        private void CallBansServer([FromSource]Player player)
        {
            TriggerClientEvent(player, "vorp:servebans", savedBans);
        }

        private void LoadBans()
        {
            savedBans = File.ReadAllLines(fileban).ToList();
        }

        private void DeleteBansServer(int name)
        {
            savedBans.RemoveAt(name);
            SaveBans();
        }

        private void SaveBans()
        {
            File.WriteAllLines(fileban, savedBans);
            LoadBans();
        }


        private void BansServer([FromSource]Player player,int id,string reason)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[id];
            if (p.Identifiers != null)
            {
                savedBans.Add($"{p.Identifiers["steam"]},{reason}");
                p.Drop("You have been banned from this server. Reason: " + reason);
                SaveBans();
            }
            else
            {

            }
        }
    }
}
