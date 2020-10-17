using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    interface IOnClickInterface
        {
        string ObjectName { get; set; }
        GameObject SavedeGameObject { get; set; }
        GameObject SelectedGameobject { get; set; }
        }
    }
