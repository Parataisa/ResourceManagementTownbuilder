using UnityEngine;

namespace Assets.Scripts.Ui.Menus.TogglePanelUis
    {
    public class TogglePanelBase : MonoBehaviour
        {
        internal void CloseSelf()
            {
            this.gameObject.SetActive(false);
            }
        }
    }
