using Assets.Scripts.AvailableResouceManagment;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AvailablePeopleUiUpdater : MonoBehaviour
{
    //ToDo: Check if something has changed and than change the value 
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().SetText("Available People: " + AvailableManpower.BusyPeople.ToString() + "/" + AvailableManpower.AvailablePeople.ToString());  
    }
}
