using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.StorageBuildings
    {
    class StorageSystem : BuildingBase
        {
        public Dictionary<string, int> StoredResources = new Dictionary<string, int>();
        public int MaxAmountOfWorker;
        public int CurrentWorkers;
        public int MaxAmountOfStorableResources;
        public int CurrentAmountOfStoredResources;

        private void Update()
            {
            if (StoredResources.Values.Count != CurrentAmountOfStoredResources)
                {
                CurrentAmountOfStoredResources = StoredResources.Values.Count;
                }
            }
        public void AddResources(int amountToAdd, string ResouceNameOfAddedResource)
            {
            if (CurrentAmountOfStoredResources < MaxAmountOfStorableResources)
                {
                if (StoredResources.ContainsKey(ResouceNameOfAddedResource))
                    {
                    StoredResources[ResouceNameOfAddedResource] += amountToAdd;
                    }
                else
                    {
                    StoredResources.Add(ResouceNameOfAddedResource, amountToAdd);
                    }
                }
            else
                {
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
        }
    }
