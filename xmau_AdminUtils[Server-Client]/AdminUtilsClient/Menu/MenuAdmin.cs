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
            if (API.IsControlJustPressed(0, 0x446258B6))
            {
                await AdminUtilsMenu();
            }
        }

        public async Task AdminUtilsMenu()
        {
            await Delay(0);
            Menu menu = new Menu("AdminUtils", "Administration Menu");
            MenuController.AddMenu(menu);


                Menu spawners = new Menu("Spawners", "Spawners");
                MenuController.AddSubmenu(menu, spawners);

                    MenuItem spawnersButton = new MenuItem("Spawners", "This button is bound to a submenu. Clicking it will take you to the submenu.")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(spawnersButton);
                    MenuController.BindMenuItem(menu, spawners, spawnersButton);

                        MenuListItem pedListItem = new MenuListItem("Peds", Dictionary.peds, 0, "Ped spawner. Command:/spawnped pedmodel");
                        spawners.AddMenuItem(pedListItem);

                        MenuListItem horsesListItem = new MenuListItem("Horses", Dictionary.horses, 0, "Horses spawner. Command:/spawnped pedmodel");
                        spawners.AddMenuItem(horsesListItem);

                        MenuListItem animalsListItem = new MenuListItem("Animals", Dictionary.animals, 0, "Animals spawner. Command:/spawnped pedmodel");
                        spawners.AddMenuItem(animalsListItem);

                        MenuListItem vehiclesListItem = new MenuListItem("Vehicles", Dictionary.vehicles, 0, "Vehicles spawner. Command:/spawnveh vehiclemodel");
                        spawners.AddMenuItem(vehiclesListItem);

                        MenuListItem objListItem = new MenuListItem("Objects", Dictionary.objects, 0, "Object spawner. Command:/spawnobj objectmodel");
                        spawners.AddMenuItem(objListItem);

                        MenuListItem weaponListItem = new MenuListItem("Weapons", Dictionary.weapons, 0, "Weapons spawner");
                        spawners.AddMenuItem(weaponListItem);

                        spawners.AddMenuItem(new MenuItem("Weapon by name", "CLEAVER ANCIENT VIKING HEWING BIT HUNTER \n KNIVES CIVIL BEAR VAMPIRE LASSO \n MACHETE TOMAHAWK M1899 MAUSER SEMIAUTO VOLCANIC \n CARBINE EVANS HENRY VARMINT WINCHESTER CATTLEMAN \nDOUBLEACTION LEMAT SCHOFIELD BOLTACTION \n CARCANO ROLLINGBLOCK SPRINGFIELD DOUBLEBARREL \n PUMP REPEATING SAWEDOFF SEMIAUTO \n BOW DYNAMITE MOLOTOV")
                        {
                            Enabled = true,
                        });

                        MenuListItem ammoListItem = new MenuListItem("Ammo", Dictionary.ammoType, 0, "Ammo spawner");
                        spawners.AddMenuItem(ammoListItem);

                        spawners.AddMenuItem(new MenuItem("All ammo", "All ammo")
                        {
                            Enabled = true,
                        });

            Menu teleports = new Menu("Teleports", "Teleports");
                MenuController.AddSubmenu(menu, teleports);

                    MenuItem teleportButton = new MenuItem("Teleports", "Teleports Menu")
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
                                    positions.AddMenuItem(new MenuItem("Add a new position", "Press here to save coords whit name. Command:/spos positionname")
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
                                        Debug.WriteLine(s);

                                        positions.AddMenuItem(new MenuItem(sPos[0], "Press here to teleport to this saved coords")
                                        {
                                            Enabled = true,
                                        });
                                    }
            


            Menu boosters = new Menu("Boosters", "Boosters");
                MenuController.AddSubmenu(menu, boosters);

                    MenuItem boostersButton = new MenuItem("Boosters", "Boosters for you own empowering.")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(boostersButton);
                    MenuController.BindMenuItem(menu, boosters, boostersButton);

                        boosters.AddMenuItem(new MenuItem("Golden", "Be Gold! Command:/golden")
                        {
                            Enabled = true,
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("Godmode", "Be god! Command:/gm", MethodsBoosters.godmodeON)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("Thor", "Be lightning! Command:/thor. After activate it use mouse3 to throw lightnings", MethodsBoosters.thorON)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("GhostRider", "Be fire! Command:/gr", MethodsBoosters.ghostRiderON)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("Noclip", "Be weightless! Command:/n. W,A,S,D,Z,X,UpArrow,DownArrow,C", MethodsBoosters.noclip)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });
            Menu peds = new Menu("Peds", "Peds");
                MenuController.AddSubmenu(menu, peds);

                    MenuItem pedButton = new MenuItem("Peds", "Menu to change your ped(Most of them cant use weapons).")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(pedButton);
                    MenuController.BindMenuItem(menu, peds, pedButton);

                        MenuListItem pedHumanListItem = new MenuListItem("Human", Dictionary.peds, 0, "Press here to change your ped to another human. Command:/changeped pedmodel");
                        peds.AddMenuItem(pedHumanListItem);

                        MenuListItem pedAnimalListItem = new MenuListItem("Animal", Dictionary.animals, 0, "Press here to change your ped to an animal. Command:/changeped pedmodel");
                        peds.AddMenuItem(pedAnimalListItem);


            Menu administration = new Menu("Administration", "Administration");
                MenuController.AddSubmenu(menu, administration);

                    MenuItem administrationButton = new MenuItem("Administration", "Administration menu")
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

                            

                        Menu bansList = new Menu("Bans", "Bans");
                        MenuController.AddSubmenu(administration, bansList);

                        MenuItem bansButton = new MenuItem("Bans", "Bans Menu")
                        {
                            RightIcon = MenuItem.Icon.ARROW_RIGHT
                        };
                        administration.AddMenuItem(bansButton);
                        MenuController.BindMenuItem(administration, bansList, bansButton);

                            bansList.AddMenuItem(new MenuItem("Fast ban", "Fast ban. Command:/ban id reason.")
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
                                Debug.WriteLine(s);

                                bansList.AddMenuItem(new MenuItem(sBan[0], sBan[1])
                                {
                                    Enabled = true,
                                });
                            }

                                
            Menu notifications = new Menu("Notifications", "Notifications");
                MenuController.AddSubmenu(menu, notifications);

                    MenuItem notificationButton = new MenuItem("Notifications", "Notification menu")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(notificationButton);
                    MenuController.BindMenuItem(menu, notifications, notificationButton);

                        notifications.AddMenuItem(new MenuItem("Pm", "Press here to send a private message")
                        {
                            Enabled = true,
                        });
                        notifications.AddMenuItem(new MenuItem("Bc", "Press here to send a broadcast message")
                        {
                            Enabled = true,
                        });

            menu.OpenMenu();

            //SPAWNER MENU CALLS

            spawners.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                Debug.WriteLine($"OnListItemSelect: [{_menu}, {_listItem}, {_listIndex}, {_itemIndex}]");
                if(_itemIndex == 0)
                {
                    List<object> pedsList = new List<object>();
                    pedsList.Add(Dictionary.peds[_listIndex]);
                    Debug.WriteLine(pedsList[0].ToString());
                    AdminControl.executeAdminCommand("Spawnped",pedsList,"MethodsSpawners");
                }
                else if (_itemIndex == 1)
                {
                    List<object> horsesList = new List<object>();
                    horsesList.Add(Dictionary.horses[_listIndex]);
                    Debug.WriteLine(horsesList[0].ToString());
                    AdminControl.executeAdminCommand("Spawnped", horsesList, "MethodsSpawners");
                }
                else if (_itemIndex == 2)
                {
                    List<object> animalsList = new List<object>();
                    animalsList.Add(Dictionary.animals[_listIndex]);
                    Debug.WriteLine(animalsList[0].ToString());
                    AdminControl.executeAdminCommand("Spawnped", animalsList, "MethodsSpawners");
                }
                else if (_itemIndex == 3)
                {
                    List<object> vehiclesList = new List<object>();
                    vehiclesList.Add(Dictionary.vehicles[_listIndex]);
                    Debug.WriteLine(vehiclesList[0].ToString());
                    AdminControl.executeAdminCommand("Spawnveh", vehiclesList, "MethodsSpawners");
                }
                else if (_itemIndex == 4)
                {
                    List<object> objectsList = new List<object>();
                    objectsList.Add(Dictionary.objects[_listIndex]);
                    Debug.WriteLine(objectsList[0].ToString());
                    AdminControl.executeAdminCommand("Spawnobj", objectsList, "MethodsSpawners");
                }
                else if (_itemIndex == 5)
                {
                    List<object> weaponList = new List<object>();
                    weaponList.Add(Dictionary.weapons[_listIndex]);
                    Debug.WriteLine(weaponList[0].ToString());
                    weaponList.Add(200);
                    AdminControl.executeAdminCommand("Weap", weaponList, "MethodsSpawners");
                    foreach (string am in Dictionary.ammoType)
                    {
                        if (weaponList[0].ToString().Contains(am))
                        {
                            List<object> ammoList = new List<object>();
                            ammoList.Add(am);
                            ammoList.Add(200);
                            AdminControl.executeAdminCommand("WeapAmmo", ammoList, "MethodsSpawners");
                        }
                    }
                }
                else if (_itemIndex == 7)
                {
                    List<object> ammoList = new List<object>();
                    ammoList.Add(Dictionary.ammoType[_listIndex]);
                    Debug.WriteLine(ammoList[0].ToString());
                    ammoList.Add(200);
                    AdminControl.executeAdminCommand("WeapAmmo", ammoList, "MethodsSpawners");
                }
            };

            spawners.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 6)
                {
                    args = await Utils.GetOneByNUI(args, "Weapon name", "weapon name");
                    Debug.WriteLine(args[0].ToString());

                    string weap = Dictionary.weapons.FirstOrDefault(c => c.Contains(args[0].ToString()));
                    if(weap != null)
                    {
                        args.Clear();
                                Debug.WriteLine(weap);
                                args.Add(weap);
                                args.Add(200);
                                AdminControl.executeAdminCommand("Weap", args, "MethodsSpawners");
                    }
                    args.Clear();
                }
                else if (_index == 8)
                {
                    Debug.WriteLine("ammo");
                    AdminControl.executeAdminCommand("Ammo", args, "MethodsSpawners");
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
                    args = await Utils.GetCoordsByNUI(args);
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

                peds.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                Debug.WriteLine($"OnListItemSelect: [{_menu}, {_listItem}, {_listIndex}, {_itemIndex}]");
                if (_itemIndex == 0)
                {
                    List<object> pedList = new List<object>();
                    pedList.Add(Dictionary.peds[_listIndex]);
                    Debug.WriteLine(pedList[0].ToString());
                    AdminControl.executeAdminCommand("ChangeModel", pedList, "MethodsPeds");
                }
                else if (_itemIndex == 1)
                {
                    List<object> animalList = new List<object>();
                    animalList.Add(Dictionary.animals[_listIndex]);
                    Debug.WriteLine(animalList[0].ToString());
                    AdminControl.executeAdminCommand("ChangeModel", animalList, "MethodsPeds");
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
                    else
                    {
                        //view reason
                    }
                }
            };
        }
    }
}