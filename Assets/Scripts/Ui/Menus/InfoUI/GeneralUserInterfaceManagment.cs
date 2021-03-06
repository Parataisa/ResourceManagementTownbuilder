﻿using System;
using System.Collections.Generic;
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
                if (ResourceBuildingInterfaceOnClick.activeSelf == true && Input.GetMouseButtonDown(0) && !EventSystem.IsPointerOverGameObject())
                    {
                    ResourceBuildingInterfaceOnClick.SetActive(false);
                    }
                if (SocialBuildingInterfaceOnClick.activeSelf == true && Input.GetMouseButtonDown(0) && !EventSystem.IsPointerOverGameObject())
                    {
                    SocialBuildingInterfaceOnClick.SetActive(false);
                    }
                if (hitInfo.transform.gameObject.layer == 8 || hitInfo.transform.gameObject.layer == 9 || hitInfo.transform.gameObject.layer == 10)
                    {
                    GameObject parent = hitInfo.transform.parent.gameObject;
                    if (parent.name.Contains("(ResoucePatch)-"))
                        {
                        ResoucePatchUserInterface.SetActive(true);
                        ResouceBuildingUserInterface.SetActive(false);
                        SocialBuildingUserInterface.SetActive(false);
                        ShortInfoPanelToggeled?.Invoke(parent);
                        }
                    else if (parent.name.Contains("(ResouceBuildingMain)-"))
                        {
                        if (Input.GetMouseButtonDown(0) && ResouceBuildingUserInterface.activeSelf && hitInfo.transform.gameObject.layer == 9)
                            {
                            SocialBuildingInterfaceOnClick.SetActive(false);
                            ResourceBuildingInterfaceOnClick.SetActive(true);
                            OnClickInfoPanelToggled?.Invoke(hitInfo.transform.gameObject);
                            OnClickInfoPanelTextUpdate?.Invoke();
                            }
                        ResouceBuildingUserInterface.SetActive(true);
                        ResoucePatchUserInterface.SetActive(false);
                        SocialBuildingUserInterface.SetActive(false);
                        ShortInfoPanelToggeled?.Invoke(parent);
                        }
                    else if (parent.name.Contains("(SocialBuildingMain)-"))
                        {
                        if (Input.GetMouseButtonDown(0) && SocialBuildingUserInterface.activeSelf && hitInfo.transform.gameObject.layer == 8)
                            {
                            ResourceBuildingInterfaceOnClick.SetActive(false);
                            SocialBuildingInterfaceOnClick.SetActive(true);
                            OnClickInfoPanelToggled?.Invoke(hitInfo.transform.gameObject);
                            }
                        SocialBuildingUserInterface.SetActive(true);
                        ResoucePatchUserInterface.SetActive(false);
                        ResouceBuildingUserInterface.SetActive(false);
                        ShortInfoPanelToggeled?.Invoke(parent);
                        }
                    }
                else if (hitInfo.transform.gameObject.layer == 11)
                    {
                    ResouceBuildingUserInterface.SetActive(false);
                    ResoucePatchUserInterface.SetActive(false);
                    SocialBuildingUserInterface.SetActive(false);
                    }
                }
            }
        }
    }
