using Assets.Scripts.Buildings.BuildingSystemHelper;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.SocialBuildings
    {
    class SocialBuildingManagment : MonoBehaviour, IBuildingManagment
        {
        readonly List<GameObject> ListOfChildren = new List<GameObject>();
        public static List<GameObject> SocialBuildingMain = new List<GameObject>();
        public string SocialBuildingType = "";
        public float BirthRate;
        public int People;
        public int PeopleCapacity;
        public GameObject GameobjectPrefab;

        List<GameObject> IBuildingManagment.ListOfChildren { get => ListOfChildren; }

        private void Start()
            {
            AddingChildsToList();
            this.SocialBuildingType = GetSocialBuildingName();
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
                this.PeopleCapacity = ListOfChildren.Count * 10;
                //ToDO: For now the building is at full capacity
                this.People = PeopleCapacity;
                this.BirthRate = People / 10;
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