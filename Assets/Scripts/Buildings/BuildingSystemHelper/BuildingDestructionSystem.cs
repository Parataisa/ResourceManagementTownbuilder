using Assets.Scripts.Ui.Menus.InfoUI;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    class BuildingDestructionSystem : MonoBehaviour
        {
        //[SerializeField] private GameObject OnClickBuilding;
        [SerializeField] private UserInterfaceManagment GeneralUserInterfaceManagment;
        [SerializeField] private GameObject ResourceBuildingInterfaceOnClick;
        [SerializeField] private GameObject SocialBuildingInterfaceOnClick;
        //public void Awake()
        //    {
        //    GeneralUserInterfaceManagment.OnClickInfoPanelToggled += GetOnClickInterface;
        //    }
        public void DestroyMainBuidling()
            {
            Destroy(UserInterfaceManagment.CurrentSelectedGameObject);
            SocialBuildingInterfaceOnClick.SetActive(false);
            ResourceBuildingInterfaceOnClick.SetActive(false);
            }
        //private void GetOnClickInterface(GameObject onClickBuilding)
        //    {
        //    OnClickBuilding = onClickBuilding;
        //    }
        }
    }
