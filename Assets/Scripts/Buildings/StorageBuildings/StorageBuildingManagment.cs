﻿using Assets.Scripts.Buildings.ResourceBuildings;
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
            int childCount = transform.childCount;
            listOfChildren.Clear();
            if (childCount != 0)
                {
                for (int i = 0; i < childCount; i++)
                    {
                    if (listOfChildren.Contains(transform.GetChild(i).gameObject))
                        continue;
                    else
                        listOfChildren.Add(transform.GetChild(i).gameObject);
                    }
                }
            buildingTyp = transform.GetChild(0).GetComponent<BuildingBase>();
            WorkingPeopleCapacity = childCount * 10;
            }
        }
    }
