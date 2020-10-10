using Assets.Scripts.Buildings.BuildingSystemHelper;
using ResourceGeneration.ResourceVariationen;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingsManagment : MonoBehaviour, IBuildingManagment
        {
        private List<GameObject> ListOfChildren = new List<GameObject>();
        List<GameObject> IBuildingManagment.ListOfChildren { get => ListOfChildren; }

        public static List<GameObject> ResourceBuildingMain = new List<GameObject>();
        public ResourceBuildingBase ChildBuildingTyp;
        public int GatheredResourcesOverall = 0;
        public Dictionary<string, int> StoredResources;
        public float ProduktionSpeed;
        public int WorkingPeopleCapacity;
        public int WorkingPeople;
        public event Action<GameObject> UpdateResouces;
        public event Action<ResourceBuildingsManagment> ResourceQuantityDecrease;
        public GameObject GameobjectPrefab;

        private void Start()
            {
            StoredResources = new Dictionary<string, int>();
            AddingChildsToList(StoredResources);
            InvokeRepeating("UpdateResoucesMethode", 0.02f, 1f / ProduktionSpeed);
            // Adding to eatch Building its own StartCoroutine or invoke
            }

        public void IncreaseGatherResource(int numberOfIncrices, ResourceBase resourceTyp)
            {
            StoredResources[resourceTyp.ResourceName] += numberOfIncrices;
            }
        private void AddingChildsToList(Dictionary<string, int> storedResources)
            {
            int childCount = transform.childCount;
            if (childCount != 0)
                {
                for (int i = 0; i < childCount; i++)
                    {
                    if (ListOfChildren.Contains(transform.GetChild(i).gameObject))
                        {
                        continue;
                        }
                    else
                        {
                        ListOfChildren.Add(transform.GetChild(i).gameObject);
                        }
                    }
                }
            ChildBuildingTyp = transform.GetChild(0).GetComponent<ResourceBuildingBase>();
            for (int i = 0; i < ChildBuildingTyp.ResourceToGather.Count; i++)
                {
                if (!storedResources.ContainsKey(ChildBuildingTyp.ResourceToGather[i]))
                    {
                    storedResources.Add(ChildBuildingTyp.ResourceToGather[i], 0);
                    }
                }
            WorkingPeopleCapacity = 0;
            for (int x = 0; x < childCount; x++)
                {
                WorkingPeopleCapacity += 10;
                WorkingPeople = WorkingPeopleCapacity;
                ProduktionSpeed = WorkingPeople;
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
                AddingChildsToList(StoredResources);
                WorkingPeopleCapacity = ListOfChildren.Count * 10;
                //ToDO: For now the building is at full capacity
                WorkingPeople = WorkingPeopleCapacity;
                ProduktionSpeed = WorkingPeople;
                CancelInvoke();
                InvokeRepeating("UpdateResoucesMethode", 0.02f, 4f / ProduktionSpeed);
                }
            else if (transform.childCount == ListOfChildren.Count && WorkingPeopleCapacity == ListOfChildren.Count * 10)
                {
                return;
                }
            }
        private void UpdateResoucesMethode()
            {
            UpdateResouces?.Invoke(this.gameObject);
            ResourceQuantityDecrease?.Invoke(this);
            }
        }
    }


