using Assets.Scripts.Buildings.BuildingSystemHelper;
using ResourceGeneration.ResourceVariationen;
using System;
using System.Collections;
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
        public GameObject GameobjectPrefab;
        public event Action<GameObject> UpdateResouces;
        public event Action<ResourceBuildingsManagment> ResourceQuantityDecrease;
        public bool CoroutinRunning;

        private void Start()
            {
            StoredResources = new Dictionary<string, int>();
            AddingChildsToList(StoredResources);
            StartCoroutine(UpdateResoucesMethode());
            }

        public void IncreaseGatherResource(int numberOfIncrices, ResourceBase resourceTyp)
            {
            StoredResources[resourceTyp.ResourceName] += numberOfIncrices;
            }
        private void AddingChildsToList(Dictionary<string, int> storedResources)
            {
            int childCount = transform.childCount;
            ListOfChildren.Clear();
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
            WorkingPeopleCapacity = childCount * 10;
            UpdateWorkingPeople();
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
                }
            }
        public void UpdateWorkingPeople()
            {
            ProduktionSpeed = WorkingPeople;
            if (!CoroutinRunning)
                {
                StartCoroutine(UpdateResoucesMethode());
                }
            }
        private IEnumerator UpdateResoucesMethode()
            {
            if (ProduktionSpeed != 0)
                {
                CoroutinRunning = true;
                float gatherTimer = 0;
                while (gatherTimer < 1)
                    {
                    gatherTimer += 0.1f;
                    yield return new WaitForSeconds(2 / ProduktionSpeed);
                    }
                UpdateResouces?.Invoke(this.gameObject);
                ResourceQuantityDecrease?.Invoke(this);
                StartCoroutine(UpdateResoucesMethode());
                }
            }
        }
    }


