using Assets.Scripts.Buildings;
using Assets.Scripts.Ui.Menus.Building;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus
    {
    class TargetListToggle : MonoBehaviour
        {
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject scrollViewContent;
        [SerializeField] private GameObject ItemPrefab;
        private List<GameObject> mainBuidlingList = MainBuildingList.BuildingMain;
        public void PanelToggel()
            {
            if (panel != null)
                {
                bool isActive = panel.activeSelf;
                UpdateList();
                panel.SetActive(!isActive);
                }
            }

        private void UpdateList()
            {
            foreach (Transform child in scrollViewContent.transform)
                {
                Destroy(child.gameObject);
                }
            foreach (var mainBuilding in mainBuidlingList)
                {
                var newElement = Instantiate(ItemPrefab, scrollViewContent.transform) as GameObject;
                newElement.GetComponentInChildren<TextMeshProUGUI>().SetText(GetName(mainBuilding));
                newElement.GetComponentInChildren<TargetButtonData>().MainBuilding = mainBuilding;
                }
            }

        private string GetName(GameObject mainBuilding)
            {
            return mainBuilding.name.Split('-')[1].Split('(')[0]; 
            }
        }
    }
