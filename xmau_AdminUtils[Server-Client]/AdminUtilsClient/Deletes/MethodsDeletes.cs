using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Deletes
{
    class MethodsDeletes : BaseScript
    {
        public static bool onDel = false;
        public static Vector3 coordStart = new Vector3();
        public MethodsDeletes()
        {

            Tick += OnDelToView;
            Tick += OnDelView;
        }

        public void DeleteVehicle(List<object> args)
        {
            DeleteAllVehicles();
        }

        public async Task DeleteAllVehicles()
        {
            Vector3 pCoords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            for (int i = 0; i < 20; i++)
            {
                int vehicle = API.GetClosestVehicle(pCoords.X, pCoords.Y, pCoords.Z, 20, 0, 467);
                bool isMyEntity = API.NetworkRequestControlOfEntity(vehicle);
                int ped = API.GetMount(vehicle);
                Debug.WriteLine(ped.ToString());
                API.SetEntityAsMissionEntity(vehicle, true, true);
                API.DeleteVehicle(ref vehicle);
                await Delay(300);
                Debug.WriteLine(vehicle.ToString());
            }
        }

        public void DeleteHorse(List<object> args)
        {
            DeleteThisHorse();
        }

        public async Task DeleteThisHorse()
        {
            int entity = API.PlayerPedId();
            int vehicle = API.GetEntityAttachedTo(entity);
            bool isMyEntity = API.NetworkRequestControlOfEntity(vehicle);
            API.SetEntityAsMissionEntity(vehicle,true,true);
            Debug.WriteLine(isMyEntity.ToString());
            API.DeletePed(ref vehicle);
            await Delay(500);
        }

        [Tick]
        public async Task OnDelToView()
        {
            await Delay(0);
            int entity = 0;
            bool hit = false;
            Vector3 endCoord = new Vector3();
            Vector3 surfaceNormal = new Vector3();
            Vector3 camCoords = API.GetGameplayCamCoord();
            Vector3 sourceCoords = Utils.GetCoordsFromCam(100000.0F);
            int rayHandle = API.StartShapeTestRay(camCoords.X, camCoords.Y, camCoords.Z, sourceCoords.X, sourceCoords.Y, sourceCoords.Z, -1, API.PlayerPedId(), 0);
            API.GetShapeTestResult(rayHandle, ref hit, ref endCoord, ref surfaceNormal, ref entity);



            if (API.IsControlPressed(0, 0xCEE12B50) && onDel && endCoord.X != 0.0)
            {
                coordStart = API.GetEntityCoords(API.PlayerPedId(),true,true);
                Utils.TeleportToCoords(endCoord.X, endCoord.Y, endCoord.Z);
                Utils.TeleportToCoords(coordStart.X, coordStart.Y, coordStart.Z-1.0F);
            }
        }



        [Tick]
        public async Task OnDelView()
        {
            int entity = 0;
            bool hit = false;
            Vector3 endCoord = new Vector3();
            Vector3 surfaceNormal = new Vector3();
            Vector3 camCoords = API.GetGameplayCamCoord();
            Vector3 sourceCoords = Utils.GetCoordsFromCam(1000.0F);
            int rayHandle = API.StartShapeTestRay(camCoords.X, camCoords.Y, camCoords.Z, sourceCoords.X, sourceCoords.Y, sourceCoords.Z, -1, API.PlayerPedId(), 0);
            API.GetShapeTestResult(rayHandle, ref hit, ref endCoord, ref surfaceNormal, ref entity);
            if (onDel)
            {
                Function.Call((Hash)0x2A32FAA57B937173, -1795314153, endCoord.X, endCoord.Y, endCoord.Z, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.5F, 0.5F, 50.0F, 255, 0, 0, 155, false, false, 2, false, 0, 0, false);
            }
        }


        public void DelView(List<object> args)
        {
            if (onDel)
            {
                onDel = false;
            }
            else
            {
                onDel = true;
            }
        }
    }
}
