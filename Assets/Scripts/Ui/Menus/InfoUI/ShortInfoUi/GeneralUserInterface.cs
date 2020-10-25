using UnityEngine;
namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class GeneralUserInterface : MonoBehaviour
        {
        internal GameObject selectedGameobject;
        internal string ObjectName = "";
        internal virtual int Layer { get; }

        private void Start()
            {
            selectedGameobject = UserInterfaceManagment.CurrentSelectedGameObject;
            UserInterfaceManagment.ShortInfoPanelToggeled += GetGameObject;
            }
        private void LateUpdate()
            {
            if (selectedGameobject != null)
                {
                if (this.gameObject.activeSelf)
                    {
                    UserInterfaceManagment.ShortInfoPanelToggeled += GetGameObject;
                    }
                }
            }
        private void OnDisable()
            {
            UserInterfaceManagment.ShortInfoPanelToggeled -= GetGameObject;
            }
        private void OnEnable()
            {
            UserInterfaceManagment.ShortInfoPanelToggeled += GetGameObject;
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
        internal void CloseSelf()
            {
            this.gameObject.SetActive(false);
            }
        }
    }
