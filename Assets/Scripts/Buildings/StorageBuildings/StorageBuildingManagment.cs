using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Buildings.ResourceBuildings;
using Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.StorageBuildings
    {
    class StorageBuildingManagment : ResourceHandlingBuildingBase
        {
        private BuildingBase buildingTyp;
        public int MaxAmountOfStorableResources = 100000;
        public int CurrentAmountOfStoredResources;
        public bool WarehouseFull = false;

        public BuildingBase BuildingTyp
            {
            get => buildingTyp;
            set => buildingTyp = value;
            }
        protected override void Start()
            {
            buildingData = new ResourceBuildingHandlingData();
            StoredResources = new Dictionary<string, int>();
            base.Start();
            }


        private void Update()
            {
            if (StoredResources.Values.Count != CurrentAmountOfStoredResources)
                {
                CurrentAmountOfStoredResources = StoredResources.Values.Count;
                }
            if (listOfChildren.Count.Equals(null))
                return;
            if (!(transform.GetComponentsInChildren<BuildingBase>().Length == listOfChildren.Count))
                AddingChildsToList();
            }

        public void AddResources(Dictionary<string, int> resourcesToAdd)
            {
            if (CurrentAmountOfStoredResources < MaxAmountOfStorableResources)
                {
                foreach (var resource in resourcesToAdd)
                    {
                    if (StoredResources.ContainsKey(resource.Key))
                        {
                        StoredResources[resource.Key] += resource.Value;
                        }
                    else
                        {
                        StoredResources.Add(resource.Key, resource.Value);
                        }
                    }
                }
            else
                {
                WarehouseFull = true;
                Debug.Log("Warehouse is full");
                }
            }
        public void RemoveResources(int amountToRemove, string ResouceNameOfResouceToRemove)
            {
            if (StoredResources.ContainsKey(ResouceNameOfResouceToRemove))
                {
                StoredResources[ResouceNameOfResouceToRemove] -= amountToRemove;
                }
            else
                {
                Debug.Log("Resource not in storage");
                }
            }
        internal override void AddingChildsToList()
            {
            int childCount = transform.GetComponentsInChildren<BuildingBase>().Length;
            if (childCount != 0)
                {
                foreach (Transform ChildBuilding in GetComponentInChildren<Transform>())
                    {
                        {
                        if (listOfChildren.Contains(ChildBuilding.gameObject))
                            continue;
                        else if (ChildBuilding.gameObject.layer == LayerClass.StorageBuildings)
                            listOfChildren.Add(ChildBuilding.gameObject);
                        else
                            continue;
                        }
                    }
                }
            buildingTyp = transform.GetChild(0).GetComponent<BuildingBase>();
            buildingData.WorkingPeopleCapacity = listOfChildren.Count * 10;
            }
        }
    }
