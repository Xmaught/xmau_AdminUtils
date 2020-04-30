using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Teleports
{

    /// <summary>
    /// COMENTAR CON OSMANY LO DEL new Action<int, List<object>, string, string>((source, args, cl, raw)
    /// </summary>
    class CommandsTeleports :BaseScript
    {
        public CommandsTeleports()
        {
            API.RegisterCommand("tpwayp", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("TpToWaypoint", args, "MethodsTeleports");
            }), false);

            API.RegisterCommand("tpcoords", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("TpToCoords", args, "MethodsTeleports");
            }), false);

            API.RegisterCommand("tpplayer", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("TpToPlayer", args, "MethodsTeleports");
            }), false);

            API.RegisterCommand("tpbring", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("TpBring", args, "MethodsTeleports");
            }), false);

            API.RegisterCommand("tpback", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("TpBack", args, "MethodsTeleports");
            }), false);

            API.RegisterCommand("delback", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("DelBack", args, "MethodsTeleports");
            }), false);

            API.RegisterCommand("guarma", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("Guarma", args, "MethodsTeleports");
            }), false);

            API.RegisterCommand("tpview", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("TpView", args, "MethodsTeleports");
            }), false);

            API.RegisterCommand("spos", new Action<int, List<object>, string>((source, args, raw) =>
            {
                AdminControl.executeAdminCommand("Spos", args, "MethodsTeleports");
            }), false);

            //API.RegisterCommand("showpos", new Action<int, List<object>, string>((source, args, raw) =>
            //{
            //    AdminControl.executeAdminCommand("ShowPos", args, "MethodsTeleports");
            //}), false);

            //API.RegisterCommand("tppos", new Action<int, List<object>, string>((source, args, raw) =>
            //{
            //    AdminControl.executeAdminCommand("TpPos", args, "MethodsTeleports");
            //}), false);

            //API.RegisterCommand("delpos", new Action<int, List<object>, string>((source, args, raw) =>
            //{
            //    AdminControl.executeAdminCommand("DelPos", args, "MethodsTeleports");
            //}), false);
        }
    }
}
