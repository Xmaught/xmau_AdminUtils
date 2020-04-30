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

                        MenuListItem pedListItem = new MenuListItem("Peds", Dictionary.peds, 0, "Ped spawner");
                        spawners.AddMenuItem(pedListItem);

                        MenuListItem horsesListItem = new MenuListItem("Horses", Dictionary.horses, 0, "Horses spawner");
                        spawners.AddMenuItem(horsesListItem);

                        MenuListItem animalsListItem = new MenuListItem("Animals", Dictionary.animals, 0, "Animals spawner");
                        spawners.AddMenuItem(animalsListItem);

                        MenuListItem vehiclesListItem = new MenuListItem("Vehicles", Dictionary.vehicles, 0, "Vehicles spawner");
                        spawners.AddMenuItem(vehiclesListItem);

                        MenuListItem objListItem = new MenuListItem("Objects", Dictionary.objects, 0, "Object spawner");
                        spawners.AddMenuItem(objListItem);

                        MenuListItem weaponListItem = new MenuListItem("Weapons", Dictionary.weapons, 0, "Weapons spawner");
                        spawners.AddMenuItem(weaponListItem);

                        spawners.AddMenuItem(new MenuItem("Weapon by name", "Weapon by name")
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


            Menu boosters = new Menu("Boosters", "Boosters");
                MenuController.AddSubmenu(menu, boosters);

                    MenuItem boostersButton = new MenuItem("Boosters", "This button is bound to a submenu. Clicking it will take you to the submenu.")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(boostersButton);
                    MenuController.BindMenuItem(menu, boosters, boostersButton);

                        boosters.AddMenuItem(new MenuItem("Golden", "Be Gold!")
                        {
                            Enabled = true,
                            LeftIcon = MenuItem.Icon.TICK

                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("Godmode", "Be god!", MethodsBoosters.godmodeON)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("Thor", "Be lightning!", MethodsBoosters.thorON)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("GhostRider", "Be fire!", MethodsBoosters.ghostRiderON)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("Noclip", "Be weightless!", MethodsBoosters.noclip)
                        {
                            Style = MenuCheckboxItem.CheckboxStyle.Tick
                        });
            Menu peds = new Menu("Peds", "Peds");
                MenuController.AddSubmenu(menu, peds);

                    MenuItem pedButton = new MenuItem("Peds", "This button is bound to a submenu. Clicking it will take you to the submenu.")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(pedButton);
                    MenuController.BindMenuItem(menu, peds, pedButton);

                        MenuListItem pedHumanListItem = new MenuListItem("Peds", Dictionary.peds, 0, "Ped spawner");
                        peds.AddMenuItem(pedHumanListItem);

                        MenuListItem pedAnimalListItem = new MenuListItem("Peds", Dictionary.animals, 0, "Ped spawner");
                        peds.AddMenuItem(pedAnimalListItem);


            Menu administration = new Menu("Administration", "Administration");
                MenuController.AddSubmenu(menu, administration);

                    MenuItem administrationButton = new MenuItem("Administration", "This button is bound to a submenu. Clicking it will take you to the submenu.")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(administrationButton);
                    MenuController.BindMenuItem(menu, administration, administrationButton);


            Menu notifications = new Menu("Notifications", "Notifications");
                MenuController.AddSubmenu(menu, notifications);

                    MenuItem notificationButton = new MenuItem("Notifications", "This button is bound to a submenu. Clicking it will take you to the submenu.")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(notificationButton);
                    MenuController.BindMenuItem(menu, notifications, notificationButton);

                        notifications.AddMenuItem(new MenuItem("Pm", "This is a simple button with a simple description. Scroll down for more button types!")
                        {
                            Enabled = true,
                            LeftIcon = MenuItem.Icon.TICK

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
        }
    }
}
