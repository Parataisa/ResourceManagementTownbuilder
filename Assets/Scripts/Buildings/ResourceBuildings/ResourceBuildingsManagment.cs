using ResourceGeneration.ResourceVariationen;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingsManagment : MonoBehaviour
        {
        public List<GameObject> ListOfChildren = new List<GameObject>();
        public string ResourceBuildingType = "";
        public int GatheredResourcesOverall = 0;
        public Dictionary<string, int> StortedResources = new Dictionary<string, int>();
        public List<string> GatherableResourcesForThisBuilding = new List<string>(); // ToDo:!!!!!
        public float ProduktionSpeed;
        public int WorkingPeopleCapacity;
        public int WorkingPeople;
        public event Action<GameObject> UpdateResouces;
        public event Action<ResourceBuildingsManagment> ResourceQuantityDecrease;

        private void Start()
            {
            AddingChildsToList();
            this.ResourceBuildingType = GetResourceBuildingName();
            InvokeRepeating("UpdateResoucesMethode", 0.2f, 1f / ProduktionSpeed);
            }

        public void IncreaseGatherResource(int numberOfIncrices, ResourceBase resourceTyp)
            {
            if (StortedResources.Count == 0)
                {
                StortedResources.Add(resourceTyp.ResourceName, 0);
                GatherableResourcesForThisBuilding.Add(resourceTyp.ResourceName);
                }
                StortedResources[resourceTyp.ResourceName] += numberOfIncrices;
            }

        private string GetResourceBuildingName()
            {
            string[] BuildingNameArray = this.ListOfChildren[0].gameObject.name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }

        private void AddingChildsToList()
            {
            int childCount = transform.childCount;
            ListOfChildren.Clear();
            if (childCount != 0)
                {
                for (int i = 0; i < childCount; i++)
                    {
                    this.ListOfChildren.Add(transform.GetChild(i).gameObject);
                    }
                }
            for (int x = 0; x < childCount; x++)
                {
                this.WorkingPeopleCapacity += 10;
                this.WorkingPeople = WorkingPeopleCapacity;
                this.ProduktionSpeed = WorkingPeople;
                }
            }

        private void Update()
            {
            if (ListOfChildren.Count.Equals(null))
                {
                return;
                }
            if (!(transform.childCount == ListOfChildren.Count))
                {
                AddingChildsToList();
                this.WorkingPeopleCapacity = ListOfChildren.Count * 10;
                //ToDO: For now the building is at full capacity
                this.WorkingPeople = WorkingPeopleCapacity;
                this.ProduktionSpeed = WorkingPeople;
                CancelInvoke();
                InvokeRepeating("UpdateResoucesMethode", 0.2f, 1f / ProduktionSpeed);
                }
            else if (transform.childCount == ListOfChildren.Count && WorkingPeopleCapacity == ListOfChildren.Count * 10)
                {
                return;
                }
            }
        private void UpdateResoucesMethode()
            {
            foreach (var child in ListOfChildren)
                {
                UpdateResouces?.Invoke(child);
                ResourceQuantityDecrease?.Invoke(this);
                }
            }
        }
    }


