using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.SocialBuildings
    {
    class SocialBuildingManagment : MonoBehaviour
        {
        readonly List<GameObject> ListOfChildren = new List<GameObject>();
        public string SocialBuildingType = "";
        public float BirthRate;
        public int People;
        public int PeopleCapacity;

        private void Start()
            {
            AddingChildsToList();
            this.SocialBuildingType = GetSocialBuildingName();
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
                this.PeopleCapacity += 10;
                this.People = PeopleCapacity;
                this.BirthRate = People / 10;
            }
            }

        private string GetSocialBuildingName()
            {
            string[] BuildingNameArray = this.ListOfChildren[0].gameObject.name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }
        }

    }