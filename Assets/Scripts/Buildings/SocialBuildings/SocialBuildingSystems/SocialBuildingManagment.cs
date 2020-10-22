using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Buildings.SocialBuildings
    {
    class SocialBuildingManagment : BuildingManagmentBase
        {
        public string SocialBuildingType = "";
        public float BirthRate = 0.2f;
        public int People = 2;
        public int PeopleCapacity;
        public Action PersonBirth;

        protected override void Start()
            {
            base.Start();
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
        internal override void AddingChildsToList()
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