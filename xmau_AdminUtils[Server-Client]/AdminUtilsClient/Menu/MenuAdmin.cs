using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using CitizenFX.Core.Native;
using AdminUtilsClient.Boosters;

namespace AdminUtilsClient
{
    class MenuAdmin : BaseScript
    {
        List<object> args;
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

                        MenuListItem vehiclesListItem = new MenuListItem("Vechiles", Dictionary.vehicles, 0, "Vehicles spawner");
                        spawners.AddMenuItem(vehiclesListItem);

                        MenuListItem objListItem = new MenuListItem("Objects", Dictionary.objects, 0, "Object spawner");
                        spawners.AddMenuItem(objListItem);

                        MenuListItem pedChangeListItem = new MenuListItem("PedChange", Dictionary.peds, 0, "Ped changer");
                        spawners.AddMenuItem(pedChangeListItem);

                        MenuListItem weaponListItem = new MenuListItem("Weapons", Dictionary.weapons, 0, "Weapons spawner");
                        spawners.AddMenuItem(weaponListItem);

                        MenuListItem ammoListItem = new MenuListItem("Ammo", Dictionary.ammoType, 0, "Ammo spawner");
                        spawners.AddMenuItem(ammoListItem);


            Menu teleports = new Menu("Teleports", "Teleports");
                MenuController.AddSubmenu(menu, teleports);

                    MenuItem teleportButton = new MenuItem("Teleports", "This button is bound to a submenu. Clicking it will take you to the submenu.")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    menu.AddMenuItem(teleportButton);
                    MenuController.BindMenuItem(menu, teleports, teleportButton);


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
                    List<object> vehiclesList = new List<object>();
                    vehiclesList.Add(Dictionary.vehicles[_listIndex]);
                    Debug.WriteLine(vehiclesList[0].ToString());
                    AdminControl.executeAdminCommand("Spawnveh", vehiclesList, "MethodsSpawners");
                }
                else if (_itemIndex == 3)
                {
                    List<object> objectsList = new List<object>();
                    objectsList.Add(Dictionary.objects[_listIndex]);
                    Debug.WriteLine(objectsList[0].ToString());
                    AdminControl.executeAdminCommand("Spawnobj", objectsList, "MethodsSpawners");
                }
                else if (_itemIndex == 4)
                {
                    List<object> pedList = new List<object>();
                    pedList.Add(Dictionary.peds[_listIndex]);
                    Debug.WriteLine(pedList[0].ToString());
                    AdminControl.executeAdminCommand("ChangeModel", pedList,"MethodsPeds");
                }
                else if (_itemIndex == 5)
                {
                    List<object> weaponList = new List<object>();
                    weaponList.Add(Dictionary.weapons[_listIndex]);
                    Debug.WriteLine(weaponList[0].ToString());
                    weaponList.Add(200);
                    AdminControl.executeAdminCommand("Weap", weaponList, "Methods");
                    foreach (string am in Dictionary.ammoType)
                    {
                        if (weaponList[0].ToString().Contains(am))
                        {
                            List<object> ammoList = new List<object>();
                            ammoList.Add(am);
                            ammoList.Add(200);
                            AdminControl.executeAdminCommand("WeapAmmo", ammoList, "Methods");
                        }
                    }
                    
                }
                else if (_itemIndex == 6)
                {
                    List<object> ammoList = new List<object>();
                    ammoList.Add(Dictionary.ammoType[_listIndex]);
                    Debug.WriteLine(ammoList[0].ToString());
                    ammoList.Add(200);
                    AdminControl.executeAdminCommand("WeapAmmo", ammoList, "Methods");
                }
            };

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
        }
    }
}
