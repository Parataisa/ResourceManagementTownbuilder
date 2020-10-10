using Assets.Scripts.Buildings.SocialBuildings;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AvailableResouceManagment
    {
    class AvailableManpower
        {
        private List<GameObject> SocialBuildingList = SocialBuildingManagment.SocialBuildingMain;
        public int AvailablePeople;

        private void NewSocialBuildingListIteration()
            {
            foreach (var MainBuildings in SocialBuildingList)
                {
                AvailablePeople += MainBuildings.GetComponent<SocialBuildingManagment>().People;
                }
            }
        private void AddedToSocialBuildingList(GameObject NewSocialBuidling)
            {
            AvailablePeople += NewSocialBuidling.GetComponent<SocialBuildingManagment>().People;
            }
        }
    }
