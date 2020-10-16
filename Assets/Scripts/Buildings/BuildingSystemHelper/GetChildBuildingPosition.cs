using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    public class GetChildBuildingPosition
        {
        public static  Vector3 GetPosition(int couplingPosition, GameObject selectedGameobject)
            {
            //North
            if (couplingPosition == 1)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z + selectedGameobject.transform.lossyScale.z);
                if (HaveNeightbourAtPosition(newPosition, selectedGameobject.transform.parent.GetComponent<IBuildingManagment>().ListOfChildren))
                    {
                    return selectedGameobject.transform.position;
                    }
                return newPosition;
                }
            //East
            else if (couplingPosition == 2)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x + selectedGameobject.transform.lossyScale.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z);
                if (HaveNeightbourAtPosition(newPosition, selectedGameobject.transform.parent.GetComponent<IBuildingManagment>().ListOfChildren))
                    {
                    return selectedGameobject.transform.position;
                    }
                return newPosition;
                }
            //South
            else if (couplingPosition == 3)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z - selectedGameobject.transform.lossyScale.z);
                if (HaveNeightbourAtPosition(newPosition, selectedGameobject.transform.parent.GetComponent<IBuildingManagment>().ListOfChildren))
                    {
                    return selectedGameobject.transform.position;
                    }
                return newPosition;
                }
            //West
            else if (couplingPosition == 4)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x - selectedGameobject.transform.lossyScale.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z);
                if (HaveNeightbourAtPosition(newPosition, selectedGameobject.transform.parent.GetComponent<IBuildingManagment>().ListOfChildren))
                    {
                    return selectedGameobject.transform.position;
                    }
                return newPosition;
                }
            return selectedGameobject.transform.position;
            }
        private static bool HaveNeightbourAtPosition(Vector3 newPosition, List<GameObject> neighbourPosititions)
            {
            foreach (var neighbour in neighbourPosititions)
                {
                if (neighbour.transform.position == newPosition)
                    {
                    return true;
                    }
                }
            return false;
            }

        }
    }
