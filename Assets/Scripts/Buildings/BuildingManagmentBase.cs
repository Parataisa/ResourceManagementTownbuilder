using Assets.Scripts.Buildings.BuildingSystemHelper;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings
    {
    class BuildingManagmentBase : MonoBehaviour, IBuildingManagment
        {
        internal List<GameObject> listOfChildren = new List<GameObject>();
        internal GameObject gameobjectPrefab;

        public GameObject GameobjectPrefab
            {
            get => gameobjectPrefab;
            set => gameobjectPrefab = value;
            }
        public List<GameObject> ListOfChildren 
            { 
            get => ReturnNullFreeListOfChildrend(); 
            }

        private List<GameObject> ReturnNullFreeListOfChildrend()
            {
            listOfChildren.RemoveAll(item => item == null);
            return listOfChildren;
            }

        protected virtual void  Start()
            {
            AddingChildsToList();
            }
        internal virtual void AddingChildsToList()
            {
            }
        private void LateUpdate()
            {
            if (this.transform.childCount == 0)
                {
                MainBuildingList.BuildingMain.Remove(this.gameObject);
                Destroy(this.gameObject);
                AvailableResouceManagment.AvailableManpower.UpdateWorkingPeopleCount();
                }
            }
        }
    }
