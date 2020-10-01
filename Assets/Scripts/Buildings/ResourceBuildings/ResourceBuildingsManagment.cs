using ResourceGeneration.ResourceVariationen;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingsManagment : MonoBehaviour
        {
        private List<GameObject> ListOfChildren = new List<GameObject>();
        public ResourceBuildingBase ChildBuildingTyp;
        public int GatheredResourcesOverall = 0;
        public Dictionary<string, int> StortedResources = new Dictionary<string, int>();
        public float ProduktionSpeed;
        public int WorkingPeopleCapacity;
        public int WorkingPeople;
        public event Action<GameObject> UpdateResouces;
        public event Action<ResourceBuildingsManagment> ResourceQuantityDecrease;

        private void Start()
            {
            AddingChildsToList();
            ChildBuildingTyp = transform.GetChild(0).GetComponent<ResourceBuildingBase>();
            for (int i = 0; i < ChildBuildingTyp.ResourceToGather.Count; i++)
                {
                StortedResources.Add(ChildBuildingTyp.ResourceToGather[i], 0);
                }
            InvokeRepeating("UpdateResoucesMethode", 0.2f, 1f / ProduktionSpeed);
            }

        public void IncreaseGatherResource(int numberOfIncrices, ResourceBase resourceTyp)
            {
            StortedResources[resourceTyp.ResourceName] += numberOfIncrices;
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


