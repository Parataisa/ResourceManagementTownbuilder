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
        //ToDo: split up this mess
        private List<GameObject> listOfChildren = new List<GameObject>();
        private ResourceBuildingBase childBuildingTyp;
        private int gatheredResourcesOverall = 0;
        private Dictionary<string, int> storedResources;
        private GameObject gameobjectPrefab;
        private ResourceBuildingData buildingData;
        public List<GameObject> ListOfChildren { get => listOfChildren; }
        public bool CoroutinRunning { get; set; }
        public int WorkingPeople
            {
            get => buildingData.WorkingPeople;
            set => buildingData.WorkingPeople = value;
            }
        public int WorkingPeopleCapacity
            {
            get => buildingData.WorkingPeopleCapacity;
            set => buildingData.WorkingPeopleCapacity = value;
            }
        public float ProduktionSpeed
            {
            get => buildingData.ProduktionSpeed;
            set => buildingData.ProduktionSpeed = value;
            }
        public Dictionary<string, int> StoredResources
            {
            get => storedResources;
            set => storedResources = value;
            }
        public int GatheredResourcesOverall
            {
            get => gatheredResourcesOverall;
            set => gatheredResourcesOverall = value;
            }
        internal ResourceBuildingBase ChildBuildingTyp
            {
            get => childBuildingTyp;
            set => childBuildingTyp = value;
            }
        public GameObject GameobjectPrefab
            {
            get => gameobjectPrefab;
            set => gameobjectPrefab = value;
            }

        public event Action<GameObject> UpdateResouces;
        public event Action<ResourceBuildingsManagment> ResourceQuantityDecrease;

        private void Start()
            {
            buildingData = new ResourceBuildingData();
            StoredResources = new Dictionary<string, int>();
            AddingChildsToList(StoredResources);
            StartCoroutine(UpdateResoucesMethode());
            }
        private void Update()
            {
            if (listOfChildren.Count.Equals(null))
                return;
            if (!(transform.childCount == listOfChildren.Count))
                AddingChildsToList(StoredResources);
            }
        public void IncreaseGatherResource(int numberOfIncrices, IResources resourceTyp)
            {
            StoredResources[resourceTyp.ResourceName] += numberOfIncrices;
            }
        private void AddingChildsToList(Dictionary<string, int> storedResources)
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
            ChildBuildingTyp = transform.GetChild(0).GetComponent<ResourceBuildingBase>();
            for (int i = 0; i < ChildBuildingTyp.ResourceToGather.Count; i++)
                {
                if (!storedResources.ContainsKey(ChildBuildingTyp.ResourceToGather[i]))
                    {
                    storedResources.Add(ChildBuildingTyp.ResourceToGather[i], 0);
                    }
                }
            buildingData.WorkingPeopleCapacity = childCount * 10;
            UpdateWorkingPeople();
            }

        public void UpdateWorkingPeople()
            {
            buildingData.ProduktionSpeed = buildingData.WorkingPeople;
            if (!CoroutinRunning)
                StartCoroutine(UpdateResoucesMethode());
            }
        private IEnumerator UpdateResoucesMethode()
            {
            if (buildingData.ProduktionSpeed != 0)
                {
                CoroutinRunning = true;
                float gatherTimer = 0;
                while (gatherTimer < 1)
                    {
                    gatherTimer += 0.1f;
                    yield return new WaitForSeconds(2 / buildingData.ProduktionSpeed);
                    }
                UpdateResouces?.Invoke(this.gameObject);
                ResourceQuantityDecrease?.Invoke(this);
                StartCoroutine(UpdateResoucesMethode());
                }
            }
        }
    }


