using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using CitizenFX.Core.Native;
using AdminUtilsClient.Boosters;
using AdminUtilsClient.Teleports;
using System.Data;
using AdminUtilsClient.PlayerAdministration;
using AdminUtilsClient.Deletes;

namespace AdminUtilsClient
{
    class MenuAdmin : BaseScript
    {
        List<object> args = new List<object>();
        public MenuAdmin()
        {
            Tick += OpenMenu;
        }

        [Tick]
        private async Task OpenMenu()
        {
            if (API.IsControlJustPressed(0, 0x446258B6) /*&& AdminControl.isAdmin*/)
            {
                await AdminUtilsMenu();
                await Delay(5000);
            }
        }

        public async Task AdminUtilsMenu()
        {
            await Delay(0);
            Menu menu = new Menu("AdminUtils", "");
            MenuController.AddMenu(menu);
                Menu weapons = new Menu("Weapons", "");
                MenuController.AddSubmenu(menu, weapons);

                    MenuItem weaponsButton = new MenuItem("Weapons", "")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(weaponsButton);
                    MenuController.BindMenuItem(menu, weapons, weaponsButton);

                        MenuListItem weaponListItem = new MenuListItem("Weapons", Dictionary.weapons, 0, "Weapons spawner");
                        weapons.AddMenuItem(weaponListItem);

                        weapons.AddMenuItem(new MenuItem("Weapon by name", "CLEAVER ANCIENT VIKING HEWING BIT HUNTER \n KNIVES CIVIL BEAR VAMPIRE LASSO \n MACHETE TOMAHAWK M1899 MAUSER SEMIAUTO VOLCANIC \n CARBINE EVANS HENRY VARMINT WINCHESTER CATTLEMAN \nDOUBLEACTION LEMAT SCHOFIELD BOLTACTION \n CARCANO ROLLINGBLOCK SPRINGFIELD DOUBLEBARREL \n PUMP REPEATING SAWEDOFF SEMIAUTO \n BOW DYNAMITE MOLOTOV")
                        {
                            Enabled = true,
                        });

                        MenuListItem ammoListItem = new MenuListItem("Ammo", Dictionary.ammoType, 0, "Ammo spawner");
                        weapons.AddMenuItem(ammoListItem);

                        weapons.AddMenuItem(new MenuItem("All ammo", "All ammo")
                        {
                            Enabled = true,
                        });

                Menu spawners = new Menu("Spawners", "");
                    MenuController.AddSubmenu(menu, spawners);

                    MenuItem spawnersButton = new MenuItem("Spawners", "")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(spawnersButton);
                    MenuController.BindMenuItem(menu, spawners, spawnersButton);

                        

                        MenuListItem horsesListItem = new MenuListItem("Horses", Dictionary.horses, 0, "Horses spawner. Command:/spawnped pedmodel");
                        spawners.AddMenuItem(horsesListItem);

                        MenuListItem animalsListItem = new MenuListItem("Animals", Dictionary.animals, 0, "Animals spawner. Command:/spawnped pedmodel");
                        spawners.AddMenuItem(animalsListItem);

                        MenuListItem vehiclesListItem = new MenuListItem("Vehicles", Dictionary.vehicles, 0, "Vehicles spawner. Command:/spawnveh vehiclemodel");
                        spawners.AddMenuItem(vehiclesListItem);

                        MenuListItem objListItem = new MenuListItem("Objects", Dictionary.objects, 0, "Object spawner. Command:/spawnobj objectmodel");
                        spawners.AddMenuItem(objListItem);

                        MenuListItem maleListItem = new MenuListItem("Male", Dictionary.pedsM, 0, "Ped spawner. Command:/spawnped pedmodel");
                        spawners.AddMenuItem(maleListItem);

                        MenuListItem femaleListItem = new MenuListItem("Female", Dictionary.pedsF, 0, "Ped spawner. Command:/spawnped pedmodel");
                        spawners.AddMenuItem(femaleListItem);

                        MenuListItem teenListItem = new MenuListItem("Teens/kids", Dictionary.pedsT, 0, "Ped spawner. Command:/spawnped pedmodel");
                        spawners.AddMenuItem(teenListItem);

            Menu dvs = new Menu("Deletes", "Deletes");
                MenuController.AddSubmenu(menu, dvs);

                MenuItem dvsButton = new MenuItem("Deletes", "")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };
                menu.AddMenuItem(dvsButton);
                MenuController.BindMenuItem(menu, dvs, dvsButton);

                    dvs.AddMenuItem(new MenuCheckboxItem("Delele on view", "Press here activate delete on view or Command:/delview (Whit this you can delete all kinds of peds and vehicles)", MethodsDeletes.onDel)
                    {
                    Style = MenuCheckboxItem.CheckboxStyle.Tick
                    });
                    dvs.AddMenuItem(new MenuItem("Delete vehicles", "Delete vehicles whitout driver or:\n Command:/dv")
                    {
                        Enabled = true,
                    });
                    dvs.AddMenuItem(new MenuItem("Delete horses", "Delete horses while you are seated on or:\n Command:/dvh")
                    {
                        Enabled = true,
                    });

            Menu teleports = new Menu("Teleports", "Teleports");
                MenuController.AddSubmenu(menu, teleports);

                    MenuItem teleportButton = new MenuItem("Teleports", "")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(teleportButton);
                    MenuController.BindMenuItem(menu, teleports, teleportButton);
                        teleports.AddMenuItem(new MenuItem("Tp to Waypoint", "Teleport to wayPoint.\nAdd a waypoint in the map and press here or:\n Command: /tpwayp")
                        {
                            Enabled = true,
                        });
                        teleports.AddMenuItem(new MenuItem("Tp to Coords", "Teleport to coords.\nPress here and enter the coords or:\n Command:/tpcoords coordX coordY (coords whit decimals)")
                        {
                            Enabled = true,
                        });
                        teleports.AddMenuItem(new MenuItem("Tp to player", "Teleport to player.\nPress here and enter the id of the player or:\n Command:/tpplayer id")
                        {
                            Enabled = true,
                        });
                        teleports.AddMenuItem(new MenuItem("Bring player", "Bring player.\nPress here and enter the id of the player or:\n Command:/tpbring id")
                        {
                            Enabled = true,
                        });
                        teleports.AddMenuItem(new MenuItem("Go back to first tp", "Teleport back to first tp and delete de blip in the map.\nPress here or:\n Command:/tpback")
                        {
                            Enabled = true,
                        });
                        teleports.AddMenuItem(new MenuItem("Delete coords of first tp", "Delete mark and position of the first tp position.\nPress here or:\n Command:/delback")
                        {
                            Enabled = true,
                        });
                        teleports.AddMenuItem(new MenuItem("Guarma", "Teleport to guarma map.\nPress here or: Command:/guarma \nAfter spawn there use /n to noclip and open map to search for it.\n Use again to come back to normal map")
                        {
                            Enabled = true,
                        });
                        teleports.AddMenuItem(new MenuCheckboxItem("Tp to cursor", "Teleport to cursor.\nPress checkbox to enable or: Command:/tpview \nAfter enable use mouse3 to tp", MethodsTeleports.tpView)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });
                            Menu positions = new Menu("Positions", "Positions");
                                MenuController.AddSubmenu(teleports, positions);

                                MenuItem positionsButton = new MenuItem("Positions", "Save,teleport and delete coords")
                                {
                                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                                };
                                teleports.AddMenuItem(positionsButton);
                                MenuController.BindMenuItem(teleports, positions, positionsButton);
                                    positions.AddMenuItem(new MenuItem("Add a new position", "Press here to save coords whit name or: Command:/spos positionname")
                                    {
                                        Enabled = true,
                                    });
                                    positions.AddMenuItem(new MenuCheckboxItem("Delete", "Press here to activate delete mode.", MethodsTeleports.deleteOn)
                                    {
                                        Style = MenuCheckboxItem.CheckboxStyle.Tick
                                    });
                                    TriggerServerEvent("vorp:callpos");
                                    await Delay(250);
                                    foreach (string s in MethodsTeleports.savedpos)
                                    {
                                        string[] sPos = s.Split(',');

                                        positions.AddMenuItem(new MenuItem(sPos[0], "Press here to teleport to this saved coords")
                                        {
                                            Enabled = true,
                                        });
                                    }
                                    Menu places = new Menu("Places", "Places");
                                    MenuController.AddSubmenu(teleports, places);
                                    MenuItem placesButton = new MenuItem("Places", "Places")
                                    {
                                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                                    };
                                    teleports.AddMenuItem(placesButton);
                                    MenuController.BindMenuItem(teleports, places, placesButton);
                                        List<string> place = new List<string>();
                                        foreach (string a in Dictionary.places.Keys) {
                                                    place.Add(a);
                                            }
                                            MenuListItem placesList = new MenuListItem("Places", place, 0, "Weapons spawner");
                                            places.AddMenuItem(placesList);


            


            Menu boosters = new Menu("Boosters", "");
                MenuController.AddSubmenu(menu, boosters);

                    MenuItem boostersButton = new MenuItem("Boosters", "")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(boostersButton);
                    MenuController.BindMenuItem(menu, boosters, boostersButton);

                        boosters.AddMenuItem(new MenuItem("Golden", "Press here to be gold or: Command:/golden")
                        {
                            Enabled = true,
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("Godmode", "Press here to be god or Command:/gm", MethodsBoosters.godmodeON)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("Thor", "Press here to be lightning or: Command:/thor. After activate it use mouse3 to throw lightnings", MethodsBoosters.thorON)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("GhostRider", "Press here to be fire! Command:/gr", MethodsBoosters.ghostRiderON)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("Noclip", "Press here to be weightless or: Command:/n. W,A,S,D,Z,X,UpArrow,DownArrow,C", MethodsBoosters.noclip)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });
                        boosters.AddMenuItem(new MenuCheckboxItem("NoclipV2", "Press here to be weightless or: Command:/n. Mouse control,W,S,C", MethodsBoosters.noclip)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });
            Menu peds = new Menu("Peds", "");
                MenuController.AddSubmenu(menu, peds);

                    MenuItem pedButton = new MenuItem("Peds", "")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(pedButton);
                    MenuController.BindMenuItem(menu, peds, pedButton);

                        MenuListItem pedMaleListItem = new MenuListItem("Male", Dictionary.pedsM, 0, "Press here to change your ped to another human or: Command:/changeped pedmodel");
                        peds.AddMenuItem(pedMaleListItem);

                        MenuListItem pedFemaleListItem = new MenuListItem("Female", Dictionary.pedsF, 0, "Press here to change your ped to another human or: Command:/changeped pedmodel");
                        peds.AddMenuItem(pedFemaleListItem);

                        MenuListItem pedKidListItem = new MenuListItem("Teens/kids", Dictionary.pedsT, 0, "Press here to change your ped to another human or: Command:/changeped pedmodel");
                        peds.AddMenuItem(pedKidListItem);

                        MenuListItem pedAnimalListItem = new MenuListItem("Animal", Dictionary.animals, 0, "Press here to change your ped to an animal or: Command:/changeped pedmodel");
                        peds.AddMenuItem(pedAnimalListItem);


            Menu administration = new Menu("Administration", "");
                MenuController.AddSubmenu(menu, administration);

                    MenuItem administrationButton = new MenuItem("Administration", "")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(administrationButton);
                    MenuController.BindMenuItem(menu, administration, administrationButton);

                        Menu playersList = new Menu("Players list", "Players list");
                        MenuController.AddSubmenu(administration, playersList);

                        MenuItem playerListButton = new MenuItem("Players list", "Players list")
                        {
                            RightIcon = MenuItem.Icon.ARROW_RIGHT
                        };
                        administration.AddMenuItem(playerListButton);
                        MenuController.BindMenuItem(administration, playersList, playerListButton);

                        
                        foreach (var i in API.GetActivePlayers())
                        {
                            string name = API.GetPlayerName(i).ToString();
                            Menu player = new Menu(name, $"{name} Actions");
                            MenuController.AddSubmenu(playersList, player);

                            MenuItem playerNameButton = new MenuItem(name, $"{name} Actions")
                            {
                                RightIcon = MenuItem.Icon.ARROW_RIGHT
                            };
                            playersList.AddMenuItem(playerNameButton);
                            MenuController.BindMenuItem(playersList, player, playerNameButton);

                            player.AddMenuItem(new MenuItem("Freeze", "Press here to freeze player or: Command:/stop id.")
                            {
                                Enabled = true,
                            });
                            
                            player.AddMenuItem(new MenuItem("Slap", "Slap a player or: Command:/slap id.")
                            {
                                Enabled = true,
                            });
                            player.AddMenuItem(new MenuItem("Lighting", "Press here to lightning a player or: Command:/thorp id.")
                            {
                                Enabled = true,
                            });
                            player.AddMenuItem(new MenuItem("Fire", "Press here to burn a player till die or: Command:/firep id.")
                            {
                                Enabled = true,
                            });
                            player.AddMenuItem(new MenuItem("Kick player", "Press here to kick a player form server or: Command:/k id.")
                            {
                                Enabled = true,
                            });
                            player.AddMenuItem(new MenuItem("Ban", "Press here to ban a player: Command:/firep id.")
                            {
                                Enabled = true,
                            });

                            player.OnItemSelect += (_menu, _item, _index) =>
                            {
                                foreach(var a in API.GetActivePlayers())
                                {
                                    if(API.GetPlayerName(a) == _menu.MenuTitle)
                                    {
                                        if (_index == 0)
                                        {
                                            args.Add(API.GetPlayerServerId(a));
                                            AdminControl.executeAdminCommand("StopPlayer", args, "MethodsPlayerAdministration");
                                            args.Clear();
                                        }
                                        else if (_index == 1)
                                        {
                                            args.Add(API.GetPlayerServerId(a));
                                            AdminControl.executeAdminCommand("Slap", args, "MethodsPlayerAdministration");
                                            args.Clear();
                                        }
                                        else if (_index == 2)
                                        {
                                            args.Add(API.GetPlayerServerId(a));
                                            AdminControl.executeAdminCommand("ThorToId", args, "MethodsBoosters");
                                            args.Clear();
                                        }
                                        else if (_index == 3)
                                        {
                                            args.Add(API.GetPlayerServerId(a));
                                            AdminControl.executeAdminCommand("FireToId", args, "MethodsBoosters");
                                            args.Clear();
                                        }
                                        else if (_index == 4)
                                        {
                                            args.Add(API.GetPlayerServerId(a));
                                            AdminControl.executeAdminCommand("Kick", args, "MethodsPlayerAdministration");
                                            args.Clear();
                                        }
                                        else if (_index == 5)
                                        {
                                            args.Add(API.GetPlayerServerId(a));
                                            AdminControl.executeAdminCommand("Sbans", args, "MethodsPlayerAdministration");
                                            args.Clear();
                                        }
                                    }
                                }
                            };
                        }

                        administration.AddMenuItem(new MenuItem("Kick player", "Press here to kick a player form server or: Command:/k id.")
                        {
                            Enabled = true,
                        });
                        administration.AddMenuItem(new MenuItem("Freeze", "Press here to freeze player or: Command:/stop id.")
                        {
                            Enabled = true,
                        });
                        administration.AddMenuItem(new MenuItem("Slap", "Slap a player or: Command:/slap id.")
                        {
                            Enabled = true,
                        });
                        administration.AddMenuItem(new MenuItem("Lighting", "Press here to lightning a player or: Command:/thorp id.")
                        {
                            Enabled = true,
                        });
                        administration.AddMenuItem(new MenuItem("Fire", "Press here to burn a player till die or: Command:/firep id.")
                        {
                            Enabled = true,
                        });

                        Menu bansList = new Menu("Bans", "");
                        MenuController.AddSubmenu(administration, bansList);

                        MenuItem bansButton = new MenuItem("Bans", "")
                        {
                            RightIcon = MenuItem.Icon.ARROW_RIGHT
                        };
                        administration.AddMenuItem(bansButton);
                        MenuController.BindMenuItem(administration, bansList, bansButton);

                            bansList.AddMenuItem(new MenuItem("Fast ban", "Press here to do a fast ban or: Command:/ban id reason.")
                            {
                                Enabled = true,
                            });
                            bansList.AddMenuItem(new MenuCheckboxItem("Delete ban", "Press here to activate delete mode", MethodsPlayerAdministration.deleteOn)
                            {
                                Style = MenuCheckboxItem.CheckboxStyle.Tick
                            });

                            TriggerServerEvent("vorp:callbans");
                            await Delay(250);
                            foreach (string s in MethodsPlayerAdministration.savedbans)
                            {
                                string[] sBan = s.Split(',');
                                bansList.AddMenuItem(new MenuItem(sBan[0], sBan[1])
                                {
                                    Enabled = true,
                                });
                            }


                                
            Menu notifications = new Menu("Notifications", "");
                MenuController.AddSubmenu(menu, notifications);

                    MenuItem notificationButton = new MenuItem("Notifications", "")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(notificationButton);
                    MenuController.BindMenuItem(menu, notifications, notificationButton);

                        notifications.AddMenuItem(new MenuItem("Pm", "Press here to send a private message or Command:/pm id message")
                        {
                            Enabled = true,
                        });
                        notifications.AddMenuItem(new MenuItem("Bc", "Press here to send a broadcast message or Command:/bc id message")
                        {
                            Enabled = true,
                        });

            weapons.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                if (_itemIndex == 0)
                {
                    List<object> weaponList = new List<object>();
                    weaponList.Add(Dictionary.weapons[_listIndex]);
                    weaponList.Add(200);
                    AdminControl.executeAdminCommand("Weap", weaponList, "MethodsWeapons");
                    foreach (string am in Dictionary.ammoType)
                    {
                        if (weaponList[0].ToString().Contains(am))
                        {
                            List<object> ammoList = new List<object>();
                            ammoList.Add(am);
                            ammoList.Add(200);
                            AdminControl.executeAdminCommand("WeapAmmo", ammoList, "MethodsWeapons");
                        }
                    }
                }
                else if (_itemIndex == 2)
                {
                    List<object> ammoList = new List<object>();
                    ammoList.Add(Dictionary.ammoType[_listIndex]);
                    ammoList.Add(200);
                    AdminControl.executeAdminCommand("WeapAmmo", ammoList, "MethodsWeapons");
                }
            };

            weapons.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 1)
                {
                    args = await Utils.GetOneByNUI(args, "Weapon name", "weapon name");

                    string weap = Dictionary.weapons.FirstOrDefault(c => c.Contains(args[0].ToString()));
                    if (weap != null)
                    {
                        args.Clear();
                        args.Add(weap);
                        args.Add(200);
                        AdminControl.executeAdminCommand("Weap", args, "MethodsWeapons");
                    }
                    args.Clear();
                }
                else if (_index == 3)
                {
                    AdminControl.executeAdminCommand("Ammo", args, "MethodsWeapons");
                }

            };



                //SPAWNER MENU CALLS

            spawners.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                
                if (_itemIndex == 0)
                {
                    List<object> horsesList = new List<object>();
                    horsesList.Add(Dictionary.horses[_listIndex]);
                    AdminControl.executeAdminCommand("Spawnped", horsesList, "MethodsSpawners");
                }
                else if (_itemIndex == 1)
                {
                    List<object> animalsList = new List<object>();
                    animalsList.Add(Dictionary.animals[_listIndex]);
                    AdminControl.executeAdminCommand("Spawnped", animalsList, "MethodsSpawners");
                }
                else if (_itemIndex == 2)
                {
                    List<object> vehiclesList = new List<object>();
                    vehiclesList.Add(Dictionary.vehicles[_listIndex]);
                    AdminControl.executeAdminCommand("Spawnveh", vehiclesList, "MethodsSpawners");
                }
                else if (_itemIndex == 3)
                {
                    List<object> objectsList = new List<object>();
                    objectsList.Add(Dictionary.objects[_listIndex]);
                    AdminControl.executeAdminCommand("Spawnobj", objectsList, "MethodsSpawners");
                }
                else if (_itemIndex == 4)
                {
                    List<object> pedsList = new List<object>();
                    pedsList.Add(Dictionary.pedsM[_listIndex]);
                    AdminControl.executeAdminCommand("Spawnped", pedsList, "MethodsSpawners");
                }
                else if (_itemIndex == 5)
                {
                    List<object> pedsList = new List<object>();
                    pedsList.Add(Dictionary.pedsF[_listIndex]);
                    AdminControl.executeAdminCommand("Spawnped", pedsList, "MethodsSpawners");
                }
                else if (_itemIndex == 6)
                {
                    List<object> pedsList = new List<object>();
                    pedsList.Add(Dictionary.pedsT[_listIndex]);
                    AdminControl.executeAdminCommand("Spawnped", pedsList, "MethodsSpawners");
                }

            };

            dvs.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if (_index == 0)
                {
                    MethodsDeletes.onDel = _checked;
                }
            };
            dvs.OnItemSelect += (_menu, _item, _index) =>
            {
                if (_index == 1)
                {
                    AdminControl.executeAdminCommand("DeleteVehicle", args, "MethodsDeletes");
                }
                else if (_index == 2)
                {
                    AdminControl.executeAdminCommand("DeleteHorse", args, "MethodsDeletes");
                }
            };

            //BOOSTERS MENU CALLS

            boosters.OnItemSelect += (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    AdminControl.executeAdminCommand("Golden", args, "MethodsBoosters");
                }
            };

            boosters.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if(_index == 1)
                {
                    AdminControl.executeAdminCommand("GodMode", args, "MethodsBoosters");
                }
                else if (_index == 2)
                {
                    AdminControl.executeAdminCommand("Thor", args, "MethodsBoosters");
                }
                else if (_index == 3)
                {
                    AdminControl.executeAdminCommand("GhostRider", args, "MethodsBoosters");
                }
                else if (_index == 4)
                {
                    AdminControl.executeAdminCommand("Noclip", args, "MethodsBoosters");
                }
                else if (_index == 5)
                {
                    AdminControl.executeAdminCommand("Noclip2", args, "MethodsBoosters");
                }
            };

            //TELEPORTS MENU CALLS

            teleports.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    AdminControl.executeAdminCommand("TpToWaypoint", args, "MethodsTeleports");
                }
                else if (_index == 1)
                {
                    args = await Utils.GetTwoByNUI(args,"X Coord","0.0","Y Coord","0.0");
                    AdminControl.executeAdminCommand("TpToCoords", args, "MethodsTeleports");
                    args.Clear();
                }
                else if (_index == 2)
                {
                    args = await Utils.GetOneByNUI(args,"Id player","id player");
                    AdminControl.executeAdminCommand("TpToPlayer", args, "MethodsTeleports");
                    args.Clear();
                }
                else if (_index == 3)
                {
                    args = await Utils.GetOneByNUI(args, "Id player", "id player");
                    AdminControl.executeAdminCommand("TpBring", args, "MethodsTeleports");
                    args.Clear();
                }
                else if (_index == 4)
                {
                    AdminControl.executeAdminCommand("TpBack", args, "MethodsTeleports");
                }
                else if (_index == 5)
                {
                    AdminControl.executeAdminCommand("DelBack", args, "MethodsTeleports");
                }
                else if (_index == 6)
                {
                    AdminControl.executeAdminCommand("Guarma", args, "MethodsTeleports");
                }
            };
            teleports.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if (_index == 7)
                {
                    AdminControl.executeAdminCommand("TpView", args, "MethodsTeleports");
                }
            };

            positions.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if (_index == 1)
                {
                    MethodsTeleports.deleteOn = _checked;
                }
            };


            positions.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    args = await Utils.GetOneByNUI(args, "Name position", "Name position");
                    AdminControl.executeAdminCommand("Spos", args, "MethodsTeleports");
                    args.Clear();
                }
                if (_index > 1)
                {
                    if (MethodsTeleports.deleteOn)
                    {
                        args.Add(_index - 2);
                        AdminControl.executeAdminCommand("DeletePos", args, "MethodsTeleports");
                        MethodsTeleports.deleteOn = false;
                        positions.CloseMenu();
                        teleports.CloseMenu();
                        menu.CloseMenu();
                        args.Clear();
                    }
                    else
                    {
                        string[] pos = MethodsTeleports.savedpos[_index - 2].Split(',');
                        args.Add(pos[1]);
                        args.Add(pos[2]);
                        args.Add(pos[3]);

                        AdminControl.executeAdminCommand("TeleportPos", args, "MethodsTeleports");
                        args.Clear();
                    }
                }
            };

            places.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                if (_itemIndex == 0)
                {
                    var pla = Dictionary.places.ToArray();
                    args.Add(pla[_listIndex].Value.X);
                    args.Add(pla[_listIndex].Value.Y);
                    args.Add(pla[_listIndex].Value.Z);

                    AdminControl.executeAdminCommand("TeleportPos", args, "MethodsTeleports");
                    args.Clear();

                    
                }
            };

            peds.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                if (_itemIndex == 0)
                {
                    List<object> pedList = new List<object>();
                    pedList.Add(Dictionary.pedsM[_listIndex]);
                    AdminControl.executeAdminCommand("ChangeModel", pedList, "MethodsPeds");
                }
                if (_itemIndex == 1)
                {
                    List<object> pedList = new List<object>();
                    pedList.Add(Dictionary.pedsF[_listIndex]);
                    AdminControl.executeAdminCommand("ChangeModel", pedList, "MethodsPeds");
                }
                if (_itemIndex == 2)
                {
                    List<object> pedList = new List<object>();
                    pedList.Add(Dictionary.pedsT[_listIndex]);
                    AdminControl.executeAdminCommand("ChangeModel", pedList, "MethodsPeds");
                }
                else if (_itemIndex == 3)
                {
                    List<object> animalList = new List<object>();
                    animalList.Add(Dictionary.animals[_listIndex]);
                    AdminControl.executeAdminCommand("ChangeModel", animalList, "MethodsPeds");
                }
            };

           

            administration.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 1)
                {
                    args = await Utils.GetOneByNUI(args, "Id player", "Id player");
                    AdminControl.executeAdminCommand("Kick", args, "MethodsPlayerAdministration");
                    args.Clear();
                }
                else if (_index == 2)
                {
                    args = await Utils.GetOneByNUI(args, "Id player", "Id player");
                    AdminControl.executeAdminCommand("StopPlayer", args, "MethodsPlayerAdministration");
                    args.Clear();
                }
                else if (_index == 3)
                {
                    args = await Utils.GetOneByNUI(args, "Id player", "Id player");
                    AdminControl.executeAdminCommand("Slap", args, "MethodsPlayerAdministration");
                    args.Clear();
                }
                else if (_index == 4)
                {
                    args = await Utils.GetOneByNUI(args, "Id player", "Id player");
                    AdminControl.executeAdminCommand("ThorToId", args, "MethodsBoosters");
                    args.Clear();
                }
                else if (_index == 5)
                {
                    args = await Utils.GetOneByNUI(args, "Id player", "Id player");
                    AdminControl.executeAdminCommand("FireToId", args, "MethodsBoosters");
                    args.Clear();
                }
            };



            bansList.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if (_index == 1)
                {
                    MethodsPlayerAdministration.deleteOn = _checked;
                }
            };

            bansList.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    args = await Utils.GetOneByNUI(args, "Id player", "Id player");
                    AdminControl.executeAdminCommand("Sbans", args, "MethodsPlayerAdministration");
                    args.Clear();
                }
                if (_index > 1)
                {
                    if (MethodsPlayerAdministration.deleteOn)
                    {
                        args.Add(_index - 2);
                        AdminControl.executeAdminCommand("DeleteBans", args, "MethodsPlayerAdministration");
                        MethodsPlayerAdministration.deleteOn = false;
                        bansList.CloseMenu();
                        administration.CloseMenu();
                        menu.CloseMenu();
                        args.Clear();
                    }
                }
            };
            notifications.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    args = await Utils.GetTwoByNUI(args,"Id player","id","Message","message");
                    AdminControl.executeAdminCommand("PrivateMessage", args, "MethodsNotifications");
                    args.Clear();
                }
                else if (_index == 1)
                {
                    args = await Utils.GetOneByNUI(args, "Message", "message");
                    AdminControl.executeAdminCommand("BroadCast", args, "MethodsNotifications");
                    args.Clear();
                }
            };
            menu.OpenMenu();
        }
    }
}