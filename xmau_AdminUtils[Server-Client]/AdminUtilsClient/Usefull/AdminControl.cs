using AdminUtilsClient.Boosters;
using AdminUtilsClient.Deletes;
using AdminUtilsClient.Help;
using AdminUtilsClient.Notifications;
using AdminUtilsClient.Peds;
using AdminUtilsClient.PlayerAdministration;
using AdminUtilsClient.Spawners;
using AdminUtilsClient.Teleports;
using AdminUtilsClient.Weapons;
using AdminUtilsClient.WeatherTime;
using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace AdminUtilsClient
{
    public class AdminControl : BaseScript
    {
        public AdminControl()
        {
            EventHandlers["vorp:setAdmin"] += new Action<bool>(setAdmin);
        }

        public static bool isAdmin = false;
        private void setAdmin(bool adm)
        {
            isAdmin = adm;
        }

        static Type type;
        static MethodsHelp methHelp;
        static MethodsSpawners methSpawners;
        static MethodsTeleports methTeleports;
        static MethodsBoosters methBoosters;
        static MethodsPeds methPeds;
        static MethodsNotifications methNotifications;
        static MethodsPlayerAdministration methPlayerAdministration;
        static Methods meth;
        static MethodsDeletes methDeletes;
        static MethodsWeapons methWeapons;
        static MethodsWeatherTime methWeatherTime;

        public static void executeAdminCommand(string command, List<object> args, string cl)
        {
            /*if (!isAdmin)
            {
                return;
            }*/
            if(cl == "MethodsHelp")
            {
                type = typeof(MethodsHelp);
                MethodInfo mi = type.GetMethod(command);
                methHelp = new MethodsHelp();
                mi.Invoke(methHelp, new Object[] { args });
            }
            else if (cl == "MethodsWeapons")
            {
                type = typeof(MethodsWeapons);
                MethodInfo mi = type.GetMethod(command);
                methWeapons = new MethodsWeapons();
                mi.Invoke(methWeapons, new Object[] { args });
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
                methPlayerAdministration = new MethodsPlayerAdministration();
                mi.Invoke(methPlayerAdministration, new Object[] { args });
            }
            else if (cl == "Methods")
            {
                type = typeof(Methods);
                MethodInfo mi = type.GetMethod(command);
                meth = new Methods();
                mi.Invoke(meth, new Object[] { args });
            }
            else if (cl == "MethodsDeletes")
            {
                type = typeof(MethodsDeletes);
                MethodInfo mi = type.GetMethod(command);
                methDeletes = new MethodsDeletes();
                mi.Invoke(methDeletes, new Object[] { args });
            }
            else if (cl == "MethodsWeatherTime")
            {
                type = typeof(MethodsWeatherTime);
                MethodInfo mi = type.GetMethod(command);
                methWeatherTime = new MethodsWeatherTime();
                mi.Invoke(methWeatherTime, new Object[] { args });
            }
        }
    }
}
