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
            if(API.IsControlJustPressed(0, 0x446258B6))
            {
                AdminUtilsMenu();
                await Delay(500);
            }
            await Delay(0);
        }

        public void AdminUtilsMenu()
        {
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

                        boosters.AddMenuItem(new MenuItem("Golden", "This is a simple button with a simple description. Scroll down for more button types!")
                        {
                            Enabled = true,
                            LeftIcon = MenuItem.Icon.TICK

                        });

                        boosters.AddMenuItem(new MenuCheckboxItem("Godmode", "This checkbox does nothing right now.", MethodsBoosters.godmodeON)
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



            //buttons/methods

            //SPAWNERS

            spawners.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                Debug.WriteLine($"OnListItemSelect: [{_menu}, {_listItem}, {_listIndex}, {_itemIndex}]");
                if (_itemIndex == 0)
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
                else if (_itemIndex == 4)
                {
                    List<object> pedList = new List<object>();
                    pedList.Add(Dictionary.peds[_listIndex]);
                    Debug.WriteLine(pedList[0].ToString());
                    AdminControl.executeAdminCommand("ChangeModel", pedList,"MethodsPeds");
                }

            };

            //BOOSTERS
        /*    boosters.OnItemSelect += (_menu, _item, _index) =>
            {
                switch (_index)
                {
                    case 0:
                        AdminControl.executeAdminCommand("Golden", args);
                        break;
                }
                // Code in here would get executed whenever an item is pressed.
                Debug.WriteLine($"OnItemSelect: [{_menu}, {_item}, {_index}]");
            };

            boosters.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if(_index == 1)
                {
                    AdminControl.executeAdminCommand("GodMode", args);
                }
            };*/

    
            //NOTIFICATIONS






                menu.OpenMenu();
        }
    }
}
