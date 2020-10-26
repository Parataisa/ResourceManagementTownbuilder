using Assets.Scripts.Ui.Menus.InfoUI;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    class BuildingDestructionSystem : MonoBehaviour
        {
        [SerializeField] private UserInterfaceManagment UserInterfaceManagment;

        public void DestroyBuidling()
            {
            Destroy(UserInterfaceManagment.CurrentSelectedGameObject);
            UserInterfaceManagment.CloseOnClickUi(null);
            }
        }
    }
