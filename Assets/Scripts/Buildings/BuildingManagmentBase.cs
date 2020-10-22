using Assets.Scripts.Buildings.BuildingSystemHelper;
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
            get => listOfChildren; 
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
                Destroy(this.gameObject);
                }
            }
        }
    }
