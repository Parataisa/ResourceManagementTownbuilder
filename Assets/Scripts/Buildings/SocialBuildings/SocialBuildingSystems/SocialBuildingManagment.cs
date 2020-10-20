using Assets.Scripts.Buildings.BuildingSystemHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.SocialBuildings
    {
    class SocialBuildingManagment : MonoBehaviour, IBuildingManagment
        {
        private readonly List<GameObject> listOfChildren = new List<GameObject>();
        public string SocialBuildingType = "";
        public float BirthRate = 0.2f;
        public int People = 2;
        public int PeopleCapacity;
        private GameObject gameobjectPrefab;
        public Action PersonBirth;

        public List<GameObject> ListOfChildren { get => listOfChildren; }
        public GameObject GameobjectPrefab
            {
            get => gameobjectPrefab;
            set => gameobjectPrefab = value;
            }

        private void Start()
            {
            AddingChildsToList();
            SocialBuildingType = GetSocialBuildingName();
            StartCoroutine(BirthTimer(BirthRate));

            }
        private void Update()
            {
            if (listOfChildren.Count.Equals(null))
                {
                return;
                }
            if (!(transform.childCount == listOfChildren.Count))
                {
                AddingChildsToList();
                PeopleCapacity = listOfChildren.Count * 10;
                }
            else if (transform.childCount == listOfChildren.Count && PeopleCapacity == listOfChildren.Count * 10)
                {
                return;
                }
            }

        private void AddingChildsToList()
            {
            int childCount = transform.childCount;
            listOfChildren.Clear();
            if (childCount != 0)
                {
                for (int i = 0; i < childCount; i++)
                    {
                    if (listOfChildren.Contains(transform.GetChild(i).gameObject))
                        {
                        continue;
                        }
                    else
                        {
                        listOfChildren.Add(transform.GetChild(i).gameObject);
                        PeopleCapacity += 10;
                        }
                    }
                }
            }

        private string GetSocialBuildingName()
            {
            string[] BuildingNameArray = listOfChildren[0].gameObject.name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }
        IEnumerator BirthTimer(float birthRate)
            {
            if (People != PeopleCapacity)
                {
                float birthTime = 0;
                while (birthTime < 1)
                    {
                    birthTime += birthRate;
                    yield return new WaitForSeconds(1f);
                    }
                People += 1;
                PersonBirth?.Invoke();
                StartCoroutine(BirthTimer(BirthRate));
                }
            }
        }
    }