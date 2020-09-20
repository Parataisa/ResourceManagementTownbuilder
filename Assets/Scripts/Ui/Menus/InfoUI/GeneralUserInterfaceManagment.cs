using System;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class GeneralUserInterfaceManagment : MonoBehaviour
        {
        public new Camera camera;
        public GameObject ResoucePatchUserInterface;
        public GameObject ResouceBuildingUserInterface;
        public GameObject SocialBuildingUserInterface;
        public event Action<GameObject> PanelToggeled;
        public void Start()
            {
            camera = Camera.main;
            }
        public void LateUpdate()
            {
            Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo))
                {
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
                        ResouceBuildingUserInterface.SetActive(true);
                        ResoucePatchUserInterface.SetActive(false);
                        SocialBuildingUserInterface.SetActive(false);
                        PanelToggeled?.Invoke(parent);
                        if (Input.GetMouseButtonDown(0))
                            {
                            //ToDo: Opens a extra Menu when clicked.
                            }
                        }
                    else if (parent.name.Contains("(SocialBuildingMain)-"))
                        {
                        SocialBuildingUserInterface.SetActive(true);
                        ResoucePatchUserInterface.SetActive(false);
                        ResouceBuildingUserInterface.SetActive(false);
                        if (Input.GetMouseButtonDown(0))
                            {

                            }
                        PanelToggeled?.Invoke(parent);
                        }
                    }
                }
            }
        }
    }
