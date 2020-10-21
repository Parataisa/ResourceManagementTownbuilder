using Assets.Scripts.AvailableResouceManagment;
using TMPro;
using UnityEngine;

public class AvailablePeopleUiUpdater : MonoBehaviour
    {
    private int savedcurrendPeople = AvailableManpower.BusyPeople;
    private int savedMaxPeople = AvailableManpower.AvailablePeople;

    void Update()
        {
        if (savedcurrendPeople != AvailableManpower.BusyPeople || savedMaxPeople != AvailableManpower.AvailablePeople)
            {
            savedcurrendPeople = AvailableManpower.BusyPeople;
            savedMaxPeople = AvailableManpower.AvailablePeople;
            this.GetComponent<TextMeshProUGUI>().SetText("Available People: " + savedcurrendPeople.ToString() + "/" + savedMaxPeople.ToString());
            }
        else
            {
            return;
            }
        }
    }
