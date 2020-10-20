using Assets.Scripts.AvailableResouceManagment;
using Assets.Scripts.Buildings.ResourceBuildings;
using Assets.Scripts.Buildings.SocialBuildings;
using UnityEngine;

namespace Assets.Scripts.Buildings
    {
    public static class BuildingComponentTypAdder
        {
        public static void AddBuildingTyp(GameObject building)
            {
            string buildingName = building.name.Split('-')[0];
            switch (buildingName)
                {
                case "(ResourceBuildingMain)":
                    building.AddComponent<ResourceBuildingAccountant>();
                    building.AddComponent<ResourceBuildingsManagment>();
                    break;
                case "(SocialBuildingMain)":
                    building.AddComponent<SocialBuildingManagment>();
                    AvailableManpower.SocialBuildingList.Add(building);
                    break;
                case "(StorageBuildingMain)":
                    //building.AddComponent<StorageBuildingManagment>();
                    break;
                default:
                    break;
                }
            }
        }
    }
