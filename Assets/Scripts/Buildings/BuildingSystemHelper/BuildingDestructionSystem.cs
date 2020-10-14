using Assets.Scripts.Buildings.ResourceBuildings;
using Assets.Scripts.Ui.Menus.InfoUI;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    class BuildingDestructionSystem : MonoBehaviour
        {
        [SerializeField] private ResourceBuildingInterfaceOnClick ResourceBuidlingOnClickUi;
        [SerializeField] private GameObject ResourceBuildingInterfaceOnClick;
        [SerializeField] private GameObject SocialBuildingInterfaceOnClick;

        public void DestroyMainBuidling()
            {
            Destroy(GetSelectedBuilding());
            SocialBuildingInterfaceOnClick.SetActive(false);
            ResourceBuildingInterfaceOnClick.SetActive(false);
            }
        private GameObject GetSelectedBuilding()
            {
            return ResourceBuidlingOnClickUi.SelectedGameobject.transform.parent.gameObject;
            }
        }
    }
