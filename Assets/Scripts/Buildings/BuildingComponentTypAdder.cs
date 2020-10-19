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
                case "(ResouceBuildingMain)":
                    building.AddComponent<ResourceBuildingAccountant>();
                    building.AddComponent<ResourceBuildingsManagment>();
                    break;
                case "(SocialBuildingMain)":
                    building.AddComponent<SocialBuildingManagment>();
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
