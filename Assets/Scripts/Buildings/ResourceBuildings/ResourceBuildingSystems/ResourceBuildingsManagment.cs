using ResourceGeneration.ResourceVariationen;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingsManagment : BuildingManagmentBase
        {
        private ResourceBuildingBase buildingTyp;
        private int gatheredResourcesOverall = 0;
        private Dictionary<string, int> storedResources;
        private ResourceBuildingData buildingData;
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
        internal ResourceBuildingBase BuildingTyp
            {
            get => buildingTyp;
            set => buildingTyp = value;
            }

        public event Action<GameObject> UpdateResouces;
        public event Action<ResourceBuildingsManagment> ResourceQuantityDecrease;

        protected override void Start()
            {
            buildingData = new ResourceBuildingData();
            StoredResources = new Dictionary<string, int>();
            base.Start();
            StartCoroutine(UpdateResoucesMethode());
            }
        private void Update()
            {
            if (listOfChildren.Count.Equals(null))
                return;
            if (!(transform.childCount == listOfChildren.Count))
                AddingChildsToList();
            }
        public void IncreaseGatherResource(int numberOfIncrices, IResources resourceTyp)
            {
            StoredResources[resourceTyp.ResourceName] += numberOfIncrices;
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
            buildingTyp = transform.GetChild(0).GetComponent<ResourceBuildingBase>();
            for (int i = 0; i < buildingTyp.ResourceToGather.Count; i++)
                {
                if (!this.storedResources.ContainsKey(buildingTyp.ResourceToGather[i]))
                    {
                    this.storedResources.Add(buildingTyp.ResourceToGather[i], 0);
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


