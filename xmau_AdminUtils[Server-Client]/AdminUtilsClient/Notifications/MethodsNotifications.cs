using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Notifications
{
    class MethodsNotifications : BaseScript
    {
        public MethodsNotifications()
        {

        }

        public void PrivateMessage(List<object> args)
        {
            string message = "";
            int id = int.Parse(args[0].ToString());
            for (int i = 1; i < args.Count; i++)
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
