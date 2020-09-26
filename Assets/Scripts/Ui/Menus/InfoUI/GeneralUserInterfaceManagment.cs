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
        public GameObject BuildingInterfaceOnClick;
        public EventSystem EventSystem;
        public event Action<GameObject> PanelToggeled;

        public void Start()
            {
            camera = Camera.main;
            }
        public void Update()
            {
            Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo))
                {
                if (BuildingInterfaceOnClick.activeSelf == true && Input.GetMouseButtonDown(0) && !EventSystem.IsPointerOverGameObject()) 
                    {
                    BuildingInterfaceOnClick.SetActive(false);
                    }
                if (hitInfo.transform.gameObject.layer == 8 || hitInfo.transform.gameObject.layer == 9 || hitInfo.transform.gameObject.layer == 10)
                    {
                    GameObject parent = hitInfo.transform.parent.gameObject;
                    if (parent.name.Contains("(ResoucePatch)-"))
                        {
                        ResoucePatchUserInterface.SetActive(true);
                        ResouceBuildingUserInterface.SetActive(false);
                        SocialBuildingUserInterface.SetActive(false);
                        PanelToggeled?.Invoke(parent);
                        }
                    else if (parent.name.Contains("(ResouceBuildingMain)-"))
                        {
                        if (Input.GetMouseButtonDown(0) && ResouceBuildingUserInterface.activeSelf)
                            {
                            BuildingInterfaceOnClick.SetActive(true);
                            }
                        ResouceBuildingUserInterface.SetActive(true);
                        ResoucePatchUserInterface.SetActive(false);
                        SocialBuildingUserInterface.SetActive(false);
                        PanelToggeled?.Invoke(parent);
                        }
                    else if (parent.name.Contains("(SocialBuildingMain)-"))
                        {
                        if (Input.GetMouseButtonDown(0) && SocialBuildingUserInterface.activeSelf)
                            {
                            BuildingInterfaceOnClick.SetActive(true);
                            }
                        SocialBuildingUserInterface.SetActive(true);
                        ResoucePatchUserInterface.SetActive(false);
                        ResouceBuildingUserInterface.SetActive(false);
                        PanelToggeled?.Invoke(parent);
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
