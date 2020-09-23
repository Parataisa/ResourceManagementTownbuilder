using UnityEngine;
namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class GeneralUserInterface : MonoBehaviour
        {
        public GameObject selectedGameobject;
        private void Start()
            {
            FindObjectOfType<GeneralUserInterfaceManagment>().PanelToggeled += GetGameObject;
            }
        private void LateUpdate()
            {
            if (selectedGameobject != null)
                {
                if (this.gameObject.activeSelf)
                    {
                    FindObjectOfType<GeneralUserInterfaceManagment>().PanelToggeled += GetGameObject;
                    }
                }
            }

        private void GetGameObject(GameObject gameObject)
            {
            selectedGameobject = gameObject;
            }
        }
    }
