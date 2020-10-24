using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class OnClickInterfaceBase : MonoBehaviour, IOnClickInterface
        {
        internal string objectName = "";
        internal GameObject savedGameObject;
        internal GameObject selectedGameobject;

        public string ObjectName { get => objectName; set => objectName = value; }
        public GameObject SavedGameObject { get => savedGameObject; set => savedGameObject = value; }
        public GameObject SelectedGameobject { get => selectedGameobject; set => selectedGameobject = value; }
        protected virtual void Start()
            {        
            }
        internal void GetGameObject(GameObject gameObject)
            {
            selectedGameobject = gameObject;
            }
        private void OnEnable()
            {
            GeneralUserInterfaceManagment.OnClickInfoPanelToggled += GetGameObject;
            }
        internal string GetObjectName(string name)
            {
            string[] BuildingNameArray = name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }
        internal void CloseAllUIExceptThis()
            {
            var onClickInterfaces = FindObjectsOfType<OnClickInterfaceBase>();
            foreach (var Ui in onClickInterfaces)
                {
                if (Ui != this)
                    {
                    Ui.gameObject.SetActive(false);
                    }
                else
                    {
                    Ui.gameObject.SetActive(true);
                    continue;
                    }
                }
            }
        internal void CloseSelf()
            {
            this.gameObject.SetActive(false);
            }
        }
    }
