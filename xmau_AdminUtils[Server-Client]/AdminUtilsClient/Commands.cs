using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient
{
    class Commands : BaseScript
    {

        public Commands()
        {
            API.RegisterCommand("com", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("Com", args);
            }), false);

            //Spawners

            /// <see cref="Spawnobj(List{object})"/>
            API.RegisterCommand("spawnobj", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("Spawnobj", args);
            }), false);

            /// <see cref="Spawnped(List{object})"/>
            API.RegisterCommand("spawnped", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("Spawnped", args);
            }), false);

            /// <see cref="Spawnveh(List{object})"/>
            API.RegisterCommand("spawnveh", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("Spawnveh", args);
            }), false);



            //Tps\\

            API.RegisterCommand("tpwayp", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("TpToWaypoint", args);
            }), false);

            API.RegisterCommand("tpcoords", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("TpToCoords", args);
            }), false);

            API.RegisterCommand("tpplayer", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("TpToPlayer", args);
            }), false);

            API.RegisterCommand("tpbring", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("TpBring", args);
            }), false);

            API.RegisterCommand("tpbring", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("TpBring", args);
            }), false);

            API.RegisterCommand("tpback", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("TpBack", args);
            }), false);

            API.RegisterCommand("delback", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("DelBack", args);
            }), false);




            //Advantages\\

            API.RegisterCommand("golden", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("Golden", args);
            }), false);

            API.RegisterCommand("gm", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("GodMode", args);
            }), false);

            API.RegisterCommand("n", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("Noclip", args);
            }), false);



            API.RegisterCommand("spec", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("Spec", args);
            }), false);

            API.RegisterCommand("sspec", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("SSpec", args);
            }), false);

            API.RegisterCommand("stop", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("StopPlayer", args);
            }), false);

            API.RegisterCommand("slap", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("Slap", args);
            }), false);

            API.RegisterCommand("k", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("Kick", args);
            }), false);

            API.RegisterCommand("pm", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("PrivateMessage", args);
            }), false);
           
            API.RegisterCommand("bc", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Methods.executeAdminCommand("BroadCast", args);
            }), false);
        }
    }
}
