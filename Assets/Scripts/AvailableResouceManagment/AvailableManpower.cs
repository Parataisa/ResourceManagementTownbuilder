using Assets.Scripts.Buildings.SocialBuildings;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AvailableResouceManagment
    {
    class AvailableManpower : MonoBehaviour
        {
        private List<GameObject> SocialBuildingList = new List<GameObject>();
        private List<GameObject> SubscribedMainBuildings = new List<GameObject>();
        public int AvailablePeople;
        private bool fistRun = false;
        private int numberOfBuildingsInList = 0;

        public void Start()
            {
            SocialBuildingList = SocialBuildingManagment.SocialBuildingMain;
            }
        public void LateUpdate()
            {
            if (SocialBuildingList.Count == 0)
                {
                return;
                }
            else if (!fistRun)
                {
                NewSocialBuildingListIteration();
                fistRun = true;
                }
            else if (numberOfBuildingsInList != SocialBuildingList.Count)
                {
                foreach (var Building in SocialBuildingList)
                    {
                    if (SubscribedMainBuildings.Contains(Building))
                        {
                        continue;
                        }
                    else
                        {
                        AddSocialBuildingToList(Building);
                        }
                    }
                }
            }
        private void NewSocialBuildingListIteration()
            {
            foreach (var Building in SocialBuildingList)
                {
                AddSocialBuildingToList(Building);
                }
            }
        private void AddSocialBuildingToList(GameObject Building)
            {
            AvailablePeople += Building.GetComponent<SocialBuildingManagment>().People;
            numberOfBuildingsInList += 1;
            Building.GetComponent<SocialBuildingManagment>().PersonBirth += BirthTimer;
            SubscribedMainBuildings.Add(Building);
            }
        private void BirthTimer()
            {
            AvailablePeople += 1;
            }
        }
    }
