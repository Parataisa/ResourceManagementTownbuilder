using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class OnClickInterfaceBase : MonoBehaviour, IOnClickInterface
        {
        internal string objectName = "";
        internal GameObject savedeGameObject;
        internal GameObject selectedGameobject;

        protected virtual void Start()
            {
            savedeGameObject = SelectedGameobject;
            }
        internal void GetGameObject(GameObject gameObject)
            {
            SelectedGameobject = gameObject;
            }
        internal void OnEnable()
            {
            GeneralUserInterfaceManagment.OnClickInfoPanelToggled += GetGameObject;
            GeneralUserInterfaceManagment.CurrentOnClickGameObject = SelectedGameobject;
            }
        public string ObjectName { get => objectName; set => objectName = value; }
        public GameObject SavedeGameObject { get => savedeGameObject; set => savedeGameObject = value; }
        public GameObject SelectedGameobject { get => selectedGameobject; set => selectedGameobject = value; }

        internal string GetObjectName(string name)
            {
            string[] BuildingNameArray = name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }
        }
    }
