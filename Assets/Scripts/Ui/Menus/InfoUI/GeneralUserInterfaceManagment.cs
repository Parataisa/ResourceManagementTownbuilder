﻿using Assets.Scripts.Buildings.BuildingSystemHelper;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class GeneralUserInterfaceManagment : MonoBehaviour
        {
        public new Camera camera;
        public GameObject ResoucePatchUserInterface;
        public GameObject ResouceBuildingUserInterface;
        public GameObject SocialBuildingUserInterface;
        public GameObject ResourceBuildingInterfaceOnClick;
        public GameObject SocialBuildingInterfaceOnClick;
        public EventSystem EventSystem;
        public static event Action<GameObject> ShortInfoPanelToggeled;
        public static event Action<GameObject> OnClickInfoPanelToggled;
        public event Action OnClickInfoPanelTextUpdate;
        public static GameObject CurrentOnClickGameObject;

        public void Start()
            {
            camera = Camera.main;
            }
        public void Update()
            {
            Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo))
                {
                if (Input.GetMouseButtonDown(0) && !EventSystem.IsPointerOverGameObject())
                    {
                    ResourceBuildingInterfaceOnClick.SetActive(false);
                    SocialBuildingInterfaceOnClick.SetActive(false);
                    }
                if (LayerClass.GetSolitObjectLayer().Contains(hitInfo.transform.gameObject.layer) && !EventSystem.IsPointerOverGameObject())
                    {
                    GameObject parent = hitInfo.transform.parent.gameObject;
                    if (hitInfo.transform.gameObject.layer == LayerClass.ResourcePatch)
                        {
                        ResoucePatchUserInterface.SetActive(true);
                        ResouceBuildingUserInterface.SetActive(false);
                        SocialBuildingUserInterface.SetActive(false);
                        ShortInfoPanelToggeled?.Invoke(parent);
                        }
                    else if (hitInfo.transform.gameObject.layer == LayerClass.ResourceBuildings)
                        {
                        if (Input.GetMouseButtonDown(0) && ResouceBuildingUserInterface.activeSelf && hitInfo.transform.gameObject.layer == LayerClass.ResourceBuildings)
                            {
                            SocialBuildingInterfaceOnClick.SetActive(false);
                            ResourceBuildingInterfaceOnClick.SetActive(true);
                            FindObjectOfType<BuildingMenuToggle>().panel.SetActive(false);
                            OnClickInfoPanelToggled?.Invoke(hitInfo.transform.gameObject);
                            OnClickInfoPanelTextUpdate?.Invoke();
                            }
                        ResouceBuildingUserInterface.SetActive(true);
                        ResoucePatchUserInterface.SetActive(false);
                        SocialBuildingUserInterface.SetActive(false);
                        ShortInfoPanelToggeled?.Invoke(parent);
                        }
                    else if (hitInfo.transform.gameObject.layer == LayerClass.SocialBuildings)
                        {
                        if (Input.GetMouseButtonDown(0) && SocialBuildingUserInterface.activeSelf && hitInfo.transform.gameObject.layer == LayerClass.SocialBuildings)
                            {
                            ResourceBuildingInterfaceOnClick.SetActive(false);
                            SocialBuildingInterfaceOnClick.SetActive(true);
                            FindObjectOfType<BuildingMenuToggle>().panel.SetActive(false);
                            OnClickInfoPanelToggled?.Invoke(hitInfo.transform.gameObject);
                            }
                        SocialBuildingUserInterface.SetActive(true);
                        ResoucePatchUserInterface.SetActive(false);
                        ResouceBuildingUserInterface.SetActive(false);
                        ShortInfoPanelToggeled?.Invoke(parent);
                        }
                    }
                else if (hitInfo.transform.gameObject.layer == LayerClass.Ground)
                    {
                    ResouceBuildingUserInterface.SetActive(false);
                    ResoucePatchUserInterface.SetActive(false);
                    SocialBuildingUserInterface.SetActive(false);
                    }
                }
            }

        public void CloseOnClickUi()
            {
            ResourceBuildingInterfaceOnClick.SetActive(false);
            SocialBuildingInterfaceOnClick.SetActive(false);
            }
        }
    }
