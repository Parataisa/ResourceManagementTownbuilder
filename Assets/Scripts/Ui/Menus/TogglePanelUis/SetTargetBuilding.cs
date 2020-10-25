using Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems;
using Assets.Scripts.Ui.Menus.InfoUI;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.Building
    {
    class SetTargetBuilding : MonoBehaviour
        {
        public void SetTarget()
            {
            FindObjectOfType<OnClickInterfaceBase>().SelectedGameobject.transform.parent.GetComponent<ResourceHandlingBuildingBase>().TargetBuilding = this.gameObject.GetComponent<TargetButtonData>().MainBuilding;
            }
        }
    }
