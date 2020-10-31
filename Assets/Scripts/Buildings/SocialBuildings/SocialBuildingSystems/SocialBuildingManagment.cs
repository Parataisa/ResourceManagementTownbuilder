﻿using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Buildings.SocialBuildings
    {
    class SocialBuildingManagment : BuildingManagmentBase
        {
        public string SocialBuildingType = "";
        public float BirthRate = 0.0f;
        public int People = 2;
        public int PeopleCapacity;
        public Action PersonBirth;
        public float BirthProgress = 0;
        protected override void Start()
            {
            base.Start();
            SocialBuildingType = GetSocialBuildingName();
            StartCoroutine(BirthTimer());
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
        internal override void AddingChildsToList()
            {
            int childCount = transform.childCount;
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
                        BirthRate += 0.2f;
                        }
                    }
                }
            }

        private string GetSocialBuildingName()
            {
            string[] BuildingNameArray = listOfChildren[0].gameObject.name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }
        IEnumerator BirthTimer()
            {
            if (People != PeopleCapacity)
                {
                float birthTime = 0;
                while (birthTime < 1)
                    {
                    birthTime += 0.001f;
                    BirthProgress += 0.001f;
                    yield return new WaitForSeconds(0.05f / BirthRate);
                    }
                BirthProgress = 0;
                People += 1;
                PersonBirth?.Invoke();
                StartCoroutine(BirthTimer());
                }
            }
        }
    }