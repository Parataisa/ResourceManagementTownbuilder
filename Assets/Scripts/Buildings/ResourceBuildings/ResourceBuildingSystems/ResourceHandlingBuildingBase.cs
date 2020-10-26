using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems
    {
    class ResourceHandlingBuildingBase : BuildingManagmentBase
        {
        internal ResourceBuildingHandlingData buildingData;
        internal Dictionary<string, int> storedResources;
        public bool CoroutinRunning { get; set; }
        public GameObject TargetBuilding;
        public bool ResourceCollectionAtTheTime = false;

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

        protected override void Start()
            {
            base.Start();
            }

        public virtual void UpdateWorkingPeople()
            {
            buildingData.ProduktionSpeed = buildingData.WorkingPeople;
            }

        }
    }
