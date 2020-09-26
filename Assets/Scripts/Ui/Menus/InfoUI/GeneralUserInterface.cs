﻿using UnityEngine;
namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class GeneralUserInterface : MonoBehaviour
        {
        public GameObject selectedGameobject;
        internal string ObjectName = "";
        private void Start()
            {
            FindObjectOfType<GeneralUserInterfaceManagment>().ShortInfoPanelToggeled += GetGameObject;
            }
        private void LateUpdate()
            {
            if (selectedGameobject != null)
                {
                if (this.gameObject.activeSelf)
                    {
                    FindObjectOfType<GeneralUserInterfaceManagment>().ShortInfoPanelToggeled += GetGameObject;
                    }
                }
            }

        private void GetGameObject(GameObject gameObject)
            {
            selectedGameobject = gameObject;
            }
        internal string GetObjectName(string name)
            {
            string[] BuildingNameArray = name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }


        }
    }
