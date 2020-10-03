using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class OnClickInterfaceBase : MonoBehaviour, IOnClickInterface
        {
        internal string ObjectName = "";
        internal GameObject savedeGameObject;
        public GameObject selectedGameobject;
        internal GeneralUserInterfaceManagment generalUi;

        string IOnClickInterface.ObjectName { get => ObjectName; set => ObjectName = value; }
        public GameObject SavedeGameObject { get => savedeGameObject; set => savedeGameObject = value; }
        public GameObject SelectedGameobject { get => selectedGameobject; set => selectedGameobject = value; }
        }
    }
