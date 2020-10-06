using UnityEngine;
namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class GeneralUserInterface : MonoBehaviour
        {
        public GameObject selectedGameobject;
        internal string ObjectName = "";
        public GeneralUserInterfaceManagment generalUserInterfaceManagment;
        private void Start()
            {
            generalUserInterfaceManagment.ShortInfoPanelToggeled += GetGameObject;
            }
        private void LateUpdate()
            {
            if (selectedGameobject != null)
                {
                if (this.gameObject.activeSelf)
                    {
                    generalUserInterfaceManagment.ShortInfoPanelToggeled += GetGameObject;
                    }
                }
            }
        private void OnDisable()
            {
            generalUserInterfaceManagment.ShortInfoPanelToggeled -= GetGameObject;
            }
        private void OnEnable()
            {
            generalUserInterfaceManagment.ShortInfoPanelToggeled += GetGameObject;
            }
        private void GetGameObject(GameObject gameObject)
            {
            selectedGameobject = gameObject;
            }
        internal string GetObjectName(string name)
            {
            string[] BuildingNameArray = name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }


        }
    }
