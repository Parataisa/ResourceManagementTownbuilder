using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class BuildingInterfaceOnClick : MonoBehaviour
        {
        public GameObject selectedGameobject;
        internal string ObjectName = "";
        private void Start()
            {
            FindObjectOfType<GeneralUserInterfaceManagment>().OnClickInfoPanelToggled += GetGameObject;
            }

        private void GetGameObject(GameObject gameObject)
            {
            selectedGameobject = gameObject;
            }
        }
    }
