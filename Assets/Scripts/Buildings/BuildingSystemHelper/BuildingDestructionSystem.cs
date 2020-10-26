using Assets.Scripts.Ui.Menus.InfoUI;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    class BuildingDestructionSystem : MonoBehaviour
        {
        [SerializeField] private UserInterfaceManagment GeneralUserInterfaceManagment;
        [SerializeField] private GameObject ResourceBuildingInterfaceOnClick;
        [SerializeField] private GameObject SocialBuildingInterfaceOnClick;

        public void DestroyMainBuidling()
            {
            Destroy(UserInterfaceManagment.CurrentSelectedGameObject);
            SocialBuildingInterfaceOnClick.SetActive(false);
            ResourceBuildingInterfaceOnClick.SetActive(false);
            }

        }
    }
