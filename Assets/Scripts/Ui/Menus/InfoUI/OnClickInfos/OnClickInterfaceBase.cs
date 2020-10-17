using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class OnClickInterfaceBase : MonoBehaviour, IOnClickInterface
        {
        internal string ObjectName = "";
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

        string IOnClickInterface.ObjectName { get => ObjectName; set => ObjectName = value; }
        public GameObject SavedeGameObject { get => savedeGameObject; set => savedeGameObject = value; }
        public GameObject SelectedGameobject { get => selectedGameobject; set => selectedGameobject = value; }
        }
    }
