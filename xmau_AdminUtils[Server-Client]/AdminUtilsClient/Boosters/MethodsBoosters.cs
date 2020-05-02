﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Boosters
{
    class MethodsBoosters : BaseScript
    {
        static float heading;
        public static bool godmodeON = false;
        public static bool noclip = false;
        float speed = 1.28F;

        public static bool thorON = false;
        public static bool ghostRiderON = false;
        static int entity;
        static int pedCreated = 0;
        public MethodsBoosters()
        {
            EventHandlers["vorp:thordone"] += new Action<Vector3>(ThorDone);

            Tick += Noc;
            Tick += OnClick;
            Tick += OnLight;
            Tick += OnFire;
        }

        

        public void Golden(List<object> args)
        {
            int pPedId = API.PlayerPedId();
            //Jugador cores
            Function.Call((Hash)0xC6258F41D86676E0, pPedId, 0, 100);
            Function.Call((Hash)0xC6258F41D86676E0, pPedId, 1, 100);
            Function.Call((Hash)0xC6258F41D86676E0, pPedId, 2, 100);
            //Jugador circles                   
            Function.Call((Hash)0x4AF5A4C7B9157D14, pPedId, 0, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, pPedId, 1, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, pPedId, 2, 5000.0);

            Function.Call((Hash)0xF6A7C08DF2E28B28, pPedId, 1, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, pPedId, 2, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, pPedId, 0, 5000.0);


            int entity = Function.Call<int>(Hash.GET_ENTITY_ATTACHED_TO, pPedId);


            Function.Call((Hash)0x09A59688C26D88DF, entity, 0, 1100);
            Function.Call((Hash)0x09A59688C26D88DF, entity, 1, 1100);
            Function.Call((Hash)0x09A59688C26D88DF, entity, 2, 1100);

            Function.Call((Hash)0x75415EE0CB583760, entity, 0, 1100);
            Function.Call((Hash)0x75415EE0CB583760, entity, 1, 1100);
            Function.Call((Hash)0x75415EE0CB583760, entity, 2, 1100);

            Function.Call((Hash)0x5DA12E025D47D4E5, entity, 0, 10);
            Function.Call((Hash)0x5DA12E025D47D4E5, entity, 1, 10);
            Function.Call((Hash)0x5DA12E025D47D4E5, entity, 2, 10);

            Function.Call((Hash)0x920F9488BD115EFB, entity, 0, 10);
            Function.Call((Hash)0x920F9488BD115EFB, entity, 1, 10);
            Function.Call((Hash)0x920F9488BD115EFB, entity, 2, 10);

            Function.Call((Hash)0xF6A7C08DF2E28B28, entity, 0, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, entity, 1, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, entity, 2, 5000.0);

            Function.Call((Hash)0x4AF5A4C7B9157D14, entity, 0, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, entity, 1, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, entity, 2, 5000.0);
        }


        public void GodMode(List<object> args)
        {

            if (!godmodeON)
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), true);
                godmodeON = true;
            }
            else
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), false);
                godmodeON = false;
            }
        }

        public void Noclip(List<object> args)
        {


            int playerPed = API.PlayerPedId();
            heading = API.GetEntityHeading(playerPed);

            if (!noclip)
            {
                API.FreezeEntityPosition(playerPed, true);
                //Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), true);
                noclip = true;
            }
            else
            {
                API.FreezeEntityPosition(playerPed, false);
                //Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), false);
                noclip = false;

            }
        }

        [Tick]
        private async Task Noc()
        {
            await Delay(0);
            if (noclip)
            {
                int playerPed = API.PlayerPedId();
                API.SetEntityHeading(playerPed, heading);
                if (API.IsControlPressed(0, 0x8FD015D8)) //W
                {
                    Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, speed, -1.0F);
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0xD27782E3)) //S
                {
                    Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, -speed, -1.0F);
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0x7065027D)) //A
                {
                    Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, -speed, 0.0F, -1.0F);
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0xB4E465B4)) //D
                {
                    Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, speed, 0.0F, -1.0F);
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0x26E9DC00)) //Z
                {
                    Vector3 c = new Vector3();
                    if (speed > 1.0F)
                    {
                        c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, -speed * 2);
                    }
                    else
                    {
                        c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, -speed - 1.0F);
                    }
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0x8CC9CD42)) //X
                {
                    Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, speed - 1.0F);
                    API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                }

                if (API.IsControlPressed(0, 0x6319DB71)) //UP
                {
                    if (speed > 0.5F)
                    {
                        speed = speed + 0.5F;
                    }
                }
                if (API.IsControlPressed(0, 0x05CA7C52)) //DOWN
                {
                    if (speed > 0.5)
                    {
                        speed = speed - 0.5F;
                    }
                }
                if (API.IsControlPressed(0, 0x9959A6F0)) //C
                {
                    speed = 1.28F;
                }
                if (API.IsControlPressed(0, 0xDE794E3E)) //Q
                {
                    heading = heading + 2.0F;
                }
                if (API.IsControlPressed(0, 0xCEFD9220)) //E
                {
                    heading = heading - 2.0F;
                }

            }
        }

        public async void GhostRider(List<object> args)
        {
            if (ghostRiderON)
            {
                API.RemoveParticleFxFromEntity(entity);
                API.RemoveParticleFxFromEntity(pedCreated);
                //API.StartEntityFire(entity, 0, 0, 1000);

                ghostRiderON = false;
            }
            else
            {
                if (API.GetEntityAttachedTo(entity) == 0)
                {
                    int HashPed = API.GetHashKey("A_C_Horse_Arabian_Black");
                    Vector3 coords = API.GetEntityCoords(API.PlayerPedId(), true, true);
                    await Utils.LoadModel(HashPed);
                    pedCreated = API.CreatePed((uint)HashPed, coords.X + 1, coords.Y, coords.Z, 0, true, true, true, true);


                    //Spawn
                    Function.Call((Hash)0x283978A15512B2FE, pedCreated, true);
                    //SetPedIntoVehicle
                    Function.Call((Hash)0x028F76B6E78246EB, API.PlayerPedId(), pedCreated, -1, false);
                   

                    API.SetEntityInvincible(pedCreated, true);
                }
                else
                {
                    API.SetEntityInvincible(API.GetEntityAttachedTo(entity), true);
                }
                ghostRiderON = true;


            }
        }

        public async Task OnFire()
        {
            await Delay(500);
            if (ghostRiderON)
            {
                entity = API.PlayerPedId();
                API.TaskAnimalUnalerted(pedCreated, -1, 0, 0, 0);

                //API.StartEntityFire(entity, 0, 0, 0);

                API.SetEntityHealth(entity, API.GetEntityMaxHealth(entity, 0), 0);
                Vector3 horseCoords = API.GetEntityCoords(pedCreated, true, true);

                API.RequestNamedPtfxAsset((uint)API.GetHashKey("core"));
                API.UseParticleFxAsset("core");
                API.StartParticleFxLoopedOnEntity("ent_amb_generic_fire_field", entity, 0.0F, 0.3F, -1F, 0.0F, 0.0F, 0.0F, 1.5F, true, true, true);
                API.RequestNamedPtfxAsset((uint)API.GetHashKey("core"));
                API.UseParticleFxAsset("core");
                API.StartParticleFxLoopedOnEntity("ent_amb_generic_fire_field", pedCreated, 0.0F, 0.3F, -1F, 0.0F, 0.0F, 0.0F, 2.5F, true, true, true);
            }
        }




        [Tick]
        public async Task OnLight()
        {
            int entity = 0;
            bool hit = false;
            Vector3 endCoord = new Vector3();
            Vector3 surfaceNormal = new Vector3();
            Vector3 camCoords = API.GetGameplayCamCoord();
            Vector3 sourceCoords = Utils.GetCoordsFromCam(1000.0F);
            int rayHandle = API.StartShapeTestRay(camCoords.X, camCoords.Y, camCoords.Z, sourceCoords.X, sourceCoords.Y, sourceCoords.Z, -1, API.PlayerPedId(), 0);
            API.GetShapeTestResult(rayHandle, ref hit, ref endCoord, ref surfaceNormal, ref entity);
            if (thorON)
            {
                //API.DrawLightWithRange(endCoord.X, endCoord.Y, endCoord.Z, 255, 255, 255, 2.0F, 200000000.0F);
                Function.Call((Hash)0x2A32FAA57B937173, -1795314153, endCoord.X, endCoord.Y, endCoord.Z, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.5F, 0.5F, 50.0F, 255, 255, 0, 155, false, false, 2, false, 0, 0, false);
            }
        }


        [Tick]
        public async Task OnClick()
        {
            await Delay(0);
            int entity = 0;
            bool hit = false;
            Vector3 endCoord = new Vector3();
            Vector3 surfaceNormal = new Vector3();
            Vector3 camCoords = API.GetGameplayCamCoord();
            Vector3 sourceCoords = Utils.GetCoordsFromCam(1000.0F);
            int rayHandle = API.StartShapeTestRay(camCoords.X, camCoords.Y, camCoords.Z, sourceCoords.X, sourceCoords.Y, sourceCoords.Z, -1, API.PlayerPedId(), 0);
            API.GetShapeTestResult(rayHandle, ref hit, ref endCoord, ref surfaceNormal, ref entity);



            if (API.IsControlJustPressed(0, 0xCEE12B50) && thorON)
            {
                TriggerServerEvent("vorp:thor", endCoord);
                
            }
        }

        private void ThorDone(Vector3 endCooord)
        {
            Debug.WriteLine("vuelve cliente");
            API.ForceLightningFlashAtCoords(endCooord.X, endCooord.Y, endCooord.Z);
        }


        public void Thor(List<object> args)
        {
            if (thorON)
            {
                thorON = false;
            }
            else
            {
                thorON = true;
            }
            //API.GetShapeTestResult();
            //API.StartEntityFire
        }

        
    }
}
