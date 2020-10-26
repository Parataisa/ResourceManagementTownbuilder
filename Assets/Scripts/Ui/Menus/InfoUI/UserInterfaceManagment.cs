﻿using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Ui.Menus.TogglePanelUis;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class UserInterfaceManagment : MonoBehaviour
        {
        public static event Action<GameObject> ShortInfoPanelToggeled;
        public static event Action<GameObject> OnClickInfoPanelToggled;
        public static GameObject CurrentSelectedGameObject;

        private new Camera camera;
        private readonly List<GeneralUserInterface> generalUserInterfaceBasesList = new List<GeneralUserInterface>();
        private readonly List<OnClickInterfaceBase> onClickInterfaceBasesList = new List<OnClickInterfaceBase>();
        private readonly List<TogglePanelBase> toggleInterfaceBasesList = new List<TogglePanelBase>();
        [SerializeField] private EventSystem EventSystem;

        public void Start()
            {
            camera = Camera.main;
            onClickInterfaceBasesList.AddRange(transform.GetComponentsInChildren<OnClickInterfaceBase>(true));
            generalUserInterfaceBasesList.AddRange(transform.GetComponentsInChildren<GeneralUserInterface>(true));
            toggleInterfaceBasesList.AddRange(transform.GetComponentsInChildren<TogglePanelBase>(true));
            }
        public void Update()
            {
            Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo))
                {
                if (Input.GetMouseButtonDown(0) && !EventSystem.IsPointerOverGameObject())
                    {
                    CloseOnClickUi(null);
                    CloseToggleUi();
                    }
                if (LayerClass.GetSolitObjectLayer().Contains(hitInfo.transform.gameObject.layer) && !EventSystem.IsPointerOverGameObject())
                    {
                    //Open the different Uis
                    GameObject parent = hitInfo.transform.parent.gameObject;
                    OpenUiForBuildingType(hitInfo, parent);
                    }
                //Close shortUiInfo
                else if (hitInfo.transform.gameObject.layer == LayerClass.Ground)
                    {
                    CloseGeneralUi(null);
                    }
                }
            }

        private void OpenUiForBuildingType(RaycastHit hitInfo, GameObject parent)
            {
            var hitObjectLayer = generalUserInterfaceBasesList.FirstOrDefault(x => x.Layer == hitInfo.transform.gameObject.layer);
            CloseGeneralUi(hitObjectLayer);
            ShortInfoPanelToggeled?.Invoke(parent);
            if (Input.GetMouseButtonDown(0) && LayerClass.GetBuildingLayers().Contains(hitInfo.transform.gameObject.layer))
                {
                OpenOnClickUi(hitInfo);
                }
            }

        private void OpenOnClickUi(RaycastHit hitInfo)
            {
            var hitObjectLayer = onClickInterfaceBasesList.FirstOrDefault(x => x.Layer == hitInfo.transform.gameObject.layer);
            CloseOnClickUi(hitObjectLayer);
            OnClickInfoPanelToggled?.Invoke(hitInfo.transform.gameObject);
            FindObjectOfType<BuildingMenuToggle>().panel.SetActive(false);
            }

        public void CloseOnClickUi(OnClickInterfaceBase self)
            {
            foreach (var onClickUi in onClickInterfaceBasesList)
                {
                if (onClickUi == self)
                    {
                    onClickUi.gameObject.SetActive(true);
                    continue;
                    }
                onClickUi.CloseSelf();
                }
            }
        private void CloseGeneralUi(GeneralUserInterface self)
            {
            foreach (var generalUi in generalUserInterfaceBasesList)
                {
                if (generalUi == self)
                    {
                    generalUi.gameObject.SetActive(true);
                    continue;
                    }
                generalUi.CloseSelf();
                }
            }
        public void CloseToggleUi()
            {
            foreach (var toggleUi in toggleInterfaceBasesList)
                {
                toggleUi.CloseSelf();
                }
            }
        }
    }