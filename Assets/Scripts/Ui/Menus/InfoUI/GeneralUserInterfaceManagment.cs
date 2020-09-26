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
        public EventSystem EventSystem;
        public event Action<GameObject> ShortInfoPanelToggeled;
        public event Action<GameObject> OnClickInfoPanelToggled;
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
                        if (Input.GetMouseButtonDown(0) && ResouceBuildingUserInterface.activeSelf &&  hitInfo.transform.gameObject.layer == 9)
                            {
                            ResourceBuildingInterfaceOnClick.SetActive(true);
                            OnClickInfoPanelToggled?.Invoke(parent);
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
                           // ResourceBuildingInterfaceOnClick.SetActive(true);
                           // OnClickInfoPanelToggled?.Invoke(parent);
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
