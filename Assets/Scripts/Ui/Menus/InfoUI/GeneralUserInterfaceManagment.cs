using Assets.Scripts.Buildings.BuildingSystemHelper;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class GeneralUserInterfaceManagment : MonoBehaviour
        {
        public new Camera camera;
        private readonly List<GeneralUserInterface> generalUserInterfaceBasesList = new List<GeneralUserInterface>();
        private readonly List<OnClickInterfaceBase> onClickInterfaceBasesList = new List<OnClickInterfaceBase>();
        public EventSystem EventSystem;
        public static event Action<GameObject> ShortInfoPanelToggeled;
        public static event Action<GameObject> OnClickInfoPanelToggled;
        public event Action OnClickInfoPanelTextUpdate;
        public static GameObject CurrentSelectedGameObject;

        public void Start()
            {
            camera = Camera.main;
            onClickInterfaceBasesList.AddRange(transform.GetComponentsInChildren<OnClickInterfaceBase>(true));
            generalUserInterfaceBasesList.AddRange(transform.GetComponentsInChildren<GeneralUserInterface>(true));
            }
        public void Update()
            {
            Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo))
                {
                if (Input.GetMouseButtonDown(0) && !EventSystem.IsPointerOverGameObject())
                    {
                    CloseOnClickUi(null);
                    }
                if (LayerClass.GetSolitObjectLayer().Contains(hitInfo.transform.gameObject.layer) && !EventSystem.IsPointerOverGameObject())
                    {
                    //ResourcePatches
                    GameObject parent = hitInfo.transform.parent.gameObject;
                    if (hitInfo.transform.gameObject.layer == LayerClass.ResourcePatch)
                        {
                        CloseGeneralUi(FindObjectOfType<ResourcePatchesUserInterface>(true));
                        ShortInfoPanelToggeled?.Invoke(parent);
                        }
                    //Resourcebuildings
                    else if (hitInfo.transform.gameObject.layer == LayerClass.ResourceBuildings)
                        {
                        CloseGeneralUi(FindObjectOfType<ResourceBuildingUserInterface>(true));
                        ShortInfoPanelToggeled?.Invoke(parent);
                        if (Input.GetMouseButtonDown(0) && hitInfo.transform.gameObject.layer == LayerClass.ResourceBuildings)
                            {
                            CloseOnClickUi(FindObjectOfType<ResourceBuildingInterfaceOnClick>(true));
                            OnClickInfoPanelToggled?.Invoke(hitInfo.transform.gameObject);
                            OnClickInfoPanelTextUpdate?.Invoke();
                            FindObjectOfType<BuildingMenuToggle>().panel.SetActive(false);
                            }
                        }
                    //SocialBuildings
                    else if (hitInfo.transform.gameObject.layer == LayerClass.SocialBuildings)
                        {
                        CloseGeneralUi(FindObjectOfType<SocialBuildingUserInterface>(true));
                        ShortInfoPanelToggeled?.Invoke(parent);
                        if (Input.GetMouseButtonDown(0) && hitInfo.transform.gameObject.layer == LayerClass.SocialBuildings)
                            {
                            CloseOnClickUi(FindObjectOfType<SocialBuildingInterfaceOnClick>(true));
                            OnClickInfoPanelToggled?.Invoke(hitInfo.transform.gameObject);
                            FindObjectOfType<BuildingMenuToggle>().panel.SetActive(false);
                            }
                        }
                    }
                //Close shortUiInfo
                else if (hitInfo.transform.gameObject.layer == LayerClass.Ground)
                    {
                    CloseGeneralUi(null);
                    }
                }
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
        }
    }
