using Assets.Scripts.Ui.Menus.InfoUI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    class BuildingDestructionSystem : MonoBehaviour
        {
        [SerializeField] private UserInterfaceManagment UserInterfaceManagment;

        public void DestroyBuidling()
            {
            List<GameObject> listOfChildren = UserInterfaceManagment.CurrentSelectedGameObject.transform.parent.GetComponent<IBuildingManagment>().ListOfChildren;
            listOfChildren.Remove(UserInterfaceManagment.CurrentSelectedGameObject);
            Destroy(UserInterfaceManagment.CurrentSelectedGameObject);
            UserInterfaceManagment.CloseOnClickUi(null);
            }
        }
    }
