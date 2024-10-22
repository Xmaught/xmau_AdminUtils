﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Spawners
{
    class MethodsSpawners : BaseScript
    {

        public async void Spawnobj(List<object> args)
        {
            string objeto = args[0].ToString();
            int HashObjeto = API.GetHashKey(objeto);
            Vector3 coords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            await Utils.LoadModel(HashObjeto);
            int cosa = API.CreateObject((uint)HashObjeto, coords.X + 0.5f, coords.Y + 0.5f, coords.Z + 1.0f, true, true, false, true, true);
            API.PlaceObjectOnGroundProperly(cosa, 1);
            API.SetModelAsNoLongerNeeded((uint)HashObjeto);
        }


        public async void Spawnped(List<object> args)
        {
            string ped = args[0].ToString();
            int HashPed = API.GetHashKey(ped);
            Vector3 coords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            await Utils.LoadModel(HashPed);
            int pedCreated = API.CreatePed((uint)HashPed, coords.X + 1, coords.Y, coords.Z, 0, true, true, true, true);
            //Spawn
            Function.Call((Hash)0x283978A15512B2FE, pedCreated, true);
            //SetPedIntoVehicle
            Function.Call((Hash)0x028F76B6E78246EB, API.PlayerPedId(), pedCreated, -1, false);
            API.SetModelAsNoLongerNeeded((uint)HashPed);
        }


        public async void Spawnveh(List<object> args)
        {
            string veh = args[0].ToString();
            int HashVeh = API.GetHashKey(veh);
            Vector3 coords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            await Utils.LoadModel(HashVeh);
            int vehCreated = API.CreateVehicle((uint)HashVeh, coords.X + 1, coords.Y, coords.Z, 0, true, true, false, false);
            //Spawn
            Function.Call((Hash)0x283978A15512B2FE, vehCreated, true);
            //TaskWanderStandard
            Function.Call((Hash)0xBB9CE077274F6A1B,vehCreated, 10, 10);
            //SetPedIntoVehicle
            Function.Call((Hash)0xF75B0D629E1C063D, API.PlayerPedId(), vehCreated, -1, false);
            API.SetModelAsNoLongerNeeded((uint)HashVeh);
        }

        

    }
}
