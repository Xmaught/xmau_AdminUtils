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
        //Debug.WriteLine(file);
        public SavesServer()
        {
            EventHandlers["vorp:spos"] += new Action<string,Vector3>(SposServer);
            //EventHandlers["vorp:showpos"] += new Action<Player>(showposServer);
            //EventHandlers["vorp:sban"] += new Action<>(sban);


            //using (StreamWriter sw = File.CreateText(file));
            //{
            //    sw.WriteLine("pene");
            //}
        }

        //private void showposServer([FromSource]Player player)
        //{
            
        //    List<string> savedPos = new List<string>();
        //    using (StreamReader sr = File.OpenText(file))
        //    {
        //        string s = "";
        //        while((s = sr.ReadLine()) != null)
        //        {
        //            savedPos.Add(s);
        //        }
        //    }
        //    player.TriggerEvent("vorp:showposserve", savedPos);
        //}

        private void SposServer(string name, Vector3 coords)
        {
            
            Debug.WriteLine(file);
            using (StreamWriter sw = new StreamWriter(file,true))
            {
                sw.WriteLine($"{name},{coords.X},{coords.Y},{coords.Z}");
            }
        }


    }
}
