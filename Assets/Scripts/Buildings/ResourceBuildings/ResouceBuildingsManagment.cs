using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResouceBuildingsManagment : MonoBehaviour
        {
        private List<GameObject> ListOfChildren = new List<GameObject>();
        public int GatheredResourcesOverall = 0;
        public int StortedResources = 0;
        public float ProduktionSpeed;
        public int WorkingPeopleCapacity;
        public int WorkingPeople;
        public event Action<GameObject> UpdateResouces;

        private void Start()
            {
            AddingChildsToList();
            InvokeRepeating("UpdateResoucesMethode", 0.2f, 1f / ProduktionSpeed);

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
                }
            }
        }
    }


