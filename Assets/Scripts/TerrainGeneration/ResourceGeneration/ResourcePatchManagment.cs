using Assets.Scripts.Buildings.ResourceBuildings;
using Assets.Scripts.TerrainGeneration.RecourceGeneration;
using ResourceGeneration.ResourceVariationen;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration
    {
    class ResourcePatchManagment : MonoBehaviour
        {
        private readonly List<ResourceBase> ResourceOnTheMap = new List<ResourceBase>();
        private readonly List<int> ResourceOnTheMapSpawningStats = new List<int>();
        private readonly List<int> FractionValuesForThePatches = new List<int>();
        private readonly List<ResourceBuildingsManagment> BuildingOnTheMap = new List<ResourceBuildingsManagment>();
        public static List<ResourceBuildingsManagment> NewBuildingOnTheMap = new List<ResourceBuildingsManagment>();
        private void Start()
            {
            FindObjectOfType<ResourceGenerator>().ResourceSuccessfullyGenerated += GetResourcePatchesOnTheMap;
            }

        private void UpdateResourcePatches(ResourceBuildingsManagment resourceBase)
            {
            int i = GetTheModifidePatch(resourceBase);
            if ((ResourceOnTheMapSpawningStats[i] - ResourceOnTheMap[i].quantityOfTheResource) % FractionValuesForThePatches[i] == 0)
                {
                if (ResourceOnTheMap[i].quantityOfTheResource <= 1)
                    {
                    Destroy((resourceBase.ListOfChildren[0].GetComponent<ResourceBuildingBase>().GatherableResouceInArea[0].transform.gameObject));
                    }
                Destroy(resourceBase.ListOfChildren[0].GetComponent<ResourceBuildingBase>().GatherableResouceInArea[0].transform.GetChild(0).gameObject);
                }
            }
        private void Update()
            {
            if (!(NewBuildingOnTheMap.Count == 0))
                {
                for (int i = 0; i < NewBuildingOnTheMap.Count; i++)
                    {
                    NewBuildingOnTheMap[i].ResourceQuantityDecrease += UpdateResourcePatches;
                    BuildingOnTheMap.Add(NewBuildingOnTheMap[i]);
                    NewBuildingOnTheMap.RemoveAt(i);

                    }
                }

            }

        private int GetTheModifidePatch(ResourceBuildingsManagment resourceBase)
            {
            int i = 0;
            int quantity = 0;
            foreach (var patch in ResourceOnTheMap)
                {
                if (patch == resourceBase.ListOfChildren[0].GetComponent<ResourceBuildingBase>().GatherableResouceInArea[0].GetComponent<ResourceBase>())
                    {
                    return quantity = i;
                    }
                i++;
                }
            return quantity;
            }

        private void GetResourcePatchesOnTheMap(ResourceBase generatedResource)
            {
            ResourceOnTheMap.Add(generatedResource);
            ResourceOnTheMapSpawningStats.Add(generatedResource.quantityOfTheResource);
            FractionValuesForThePatches.Add(generatedResource.quantityOfTheResource / generatedResource.sizeOfTheResource);
            }
        }
    }
