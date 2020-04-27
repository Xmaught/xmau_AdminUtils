using AdminUtilsClient.Boosters;
using AdminUtilsClient.Help;
using AdminUtilsClient.Notifications;
using AdminUtilsClient.Peds;
using AdminUtilsClient.PlayerAdministration;
using AdminUtilsClient.Spawners;
using AdminUtilsClient.Teleports;
using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace AdminUtilsClient
{
    public class AdminControl
    {
        static Type type;
        static MethodsHelp methHelp;
        static MethodsSpawners methSpawners;
        static MethodsTeleports methTeleports;
        static MethodsBoosters methBoosters;
        static MethodsPeds methPeds;
        static MethodsNotifications methNotifications;
        static MethodsPlayerAdministration methodsPlayerAdministration;

        public static void executeAdminCommand(string command, List<object> args, string cl)
        {
            if(cl == "MethodsHelp")
            {
                type = typeof(MethodsHelp);
                MethodInfo mi = type.GetMethod(command);
                methHelp = new MethodsHelp();
                mi.Invoke(methHelp, new Object[] { args });
            }
            else if(cl == "MethodsSpawners")
            {
                type = typeof(MethodsSpawners);
                MethodInfo mi = type.GetMethod(command);
                methSpawners = new MethodsSpawners();
                mi.Invoke(methSpawners, new Object[] { args });
            }
            else if (cl == "MethodsTeleports")
            {
                type = typeof(MethodsTeleports);
                MethodInfo mi = type.GetMethod(command);
                methTeleports = new MethodsTeleports();
                mi.Invoke(methTeleports, new Object[] { args });
            }
            else if (cl == "MethodsBoosters")
            {
                type = typeof(MethodsBoosters);
                MethodInfo mi = type.GetMethod(command);
                methBoosters = new MethodsBoosters();
                mi.Invoke(methBoosters, new Object[] { args });
            }
            else if (cl == "MethodsPeds")
            {
                type = typeof(MethodsPeds);
                MethodInfo mi = type.GetMethod(command);
                methPeds = new MethodsPeds();
                mi.Invoke(methPeds, new Object[] { args });
            }
            else if (cl == "MethodsNotifications")
            {
                type = typeof(MethodsNotifications);
                MethodInfo mi = type.GetMethod(command);
                methNotifications = new MethodsNotifications();
                mi.Invoke(methNotifications, new Object[] { args });
            }
            else if (cl == "MethodsPlayerAdministration")
            {
                type = typeof(MethodsPlayerAdministration);
                MethodInfo mi = type.GetMethod(command);
                methodsPlayerAdministration = new MethodsPlayerAdministration();
                mi.Invoke(methodsPlayerAdministration, new Object[] { args });
            }
        }
    }
}
