using Assets.Scripts.Buildings.BuildingSystemHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.SocialBuildings
    {
    class SocialBuildingManagment : MonoBehaviour, IBuildingManagment
        {
        private readonly List<GameObject> ListOfChildren = new List<GameObject>();
        public static List<GameObject> SocialBuildingMain = new List<GameObject>();
        public string SocialBuildingType = "";
        public float BirthRate = 0.2f;
        public int People = 2;
        public int PeopleCapacity;
        public GameObject GameobjectPrefab;
        public Action PersonBirth;

        List<GameObject> IBuildingManagment.ListOfChildren { get => ListOfChildren; }

        private void Start()
            {
            AddingChildsToList();
            SocialBuildingType = GetSocialBuildingName();
            StartCoroutine(BirthTimer(BirthRate));

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
                PeopleCapacity = ListOfChildren.Count * 10;
                }
            else if (transform.childCount == ListOfChildren.Count && PeopleCapacity == ListOfChildren.Count * 10)
                {
                return;
                }
            }

        private void AddingChildsToList()
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
                        PeopleCapacity += 10;
                        }
                    }
                }
            }

        private string GetSocialBuildingName()
            {
            string[] BuildingNameArray = ListOfChildren[0].gameObject.name.Split('-');
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