using ResourceGeneration.ResourceVariationen;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class ResourcePatchesUserInterface : GeneralUserInterface
        {
        private int PatchQuantity = 0;
        private int PatchSize = 0;
        private ResourceBase currentGameObjectScript;

        private void Update()
            {
            if (this.enabled)
                {
                if (selectedGameobject != null)
                    {
                    currentGameObjectScript = selectedGameobject.GetComponent<ResourceBase>();
                    }
                if (currentGameObjectScript != selectedGameobject.GetComponent<ResourceBase>())
                    {
                    currentGameObjectScript = selectedGameobject.GetComponent<ResourceBase>();
                    }
                else
                    {
                    if (!ObjectName.Equals(GetObjectName(selectedGameobject.name)))
                        {
                        ObjectName = currentGameObjectScript.ResourceName;
                        this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                        }
                    if (!PatchSize.Equals(currentGameObjectScript.SizeOfTheResource))
                        {
                        PatchSize = currentGameObjectScript.SizeOfTheResource;
                        this.transform.Find("PatchSize").GetComponent<TextMeshProUGUI>().SetText(PatchSize.ToString());
                        }
                    if (!PatchQuantity.Equals(currentGameObjectScript.QuantityOfTheResource))
                        {
                        PatchQuantity = currentGameObjectScript.QuantityOfTheResource;
                        this.transform.Find("PatchQuantity").GetComponent<TextMeshProUGUI>().SetText(PatchQuantity.ToString());
                        }

                    }
                }
            }

        }
    }

