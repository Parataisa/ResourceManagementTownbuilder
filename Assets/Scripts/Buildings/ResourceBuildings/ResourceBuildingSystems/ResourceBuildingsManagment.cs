﻿using Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems;
using ResourceGeneration.ResourceVariationen;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingsManagment : ResourceHandlingBuildingBase
        {
        private ResourceBuildingBase buildingTyp;
        private int gatheredResourcesOverall = 0;
        public float GatherProgress = 0;

        public int GatheredResourcesOverall
            {
            get => gatheredResourcesOverall;
            set => gatheredResourcesOverall = value;
            }
        public ResourceBuildingBase BuildingTyp
            {
            get => buildingTyp;
            set => buildingTyp = value;
            }

        public event Action<GameObject> UpdateResouces;
        public event Action<ResourceBuildingsManagment> ResourceQuantityDecrease;

        protected override void Start()
            {
            buildingData = new ResourceBuildingHandlingData();
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

        public override void UpdateWorkingPeople()
            {
            base.UpdateWorkingPeople();
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
                    GatherProgress += 0.001f;
                    gatherTimer += 0.001f;
                    yield return new WaitForSeconds(0.25f / buildingData.ProduktionSpeed);
                    }
                GatherProgress = 0;
                UpdateResouces?.Invoke(this.gameObject);
                ResourceQuantityDecrease?.Invoke(this);
                StartCoroutine(UpdateResoucesMethode());
                }
            }
        }
    }


