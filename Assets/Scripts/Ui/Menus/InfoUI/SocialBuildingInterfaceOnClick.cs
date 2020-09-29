using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class SocialBuildingInterfaceOnClick : MonoBehaviour
        {
        public GameObject selectedGameobject;
        internal GameObject savedeGameObject;
        internal string ObjectName = "";

        private void Start()
            {
            savedeGameObject = selectedGameobject;
            }
        private void Update()
            {
            if (this.enabled)
                {
                if (selectedGameobject == null)
                    {
                    return;
                    }
                if (ObjectName.Length == 0)
                    {
                    ObjectName = GetObjectName(selectedGameobject.transform.name);
                    this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                    if (selectedGameobject != savedeGameObject)
                        {
                        ObjectName = GetObjectName(selectedGameobject.transform.name);
                        this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                        }
                    }
                }
            }
        private string GetObjectName(string name)
            {
            string[] BuildingNameArray = name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }

        private void GetGameObject(GameObject gameObject)
            {
            selectedGameobject = gameObject;
            }
        }
    }