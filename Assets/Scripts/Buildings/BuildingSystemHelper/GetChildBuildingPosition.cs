using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    public class GetChildBuildingPosition
        {
        public static Vector3 GetPosition(int couplingPosition, GameObject selectedGameobject)
            {
            //North
            if (couplingPosition == 1)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z + selectedGameobject.transform.lossyScale.z);
                if (IsPositionFree(newPosition, selectedGameobject))
                    return ReturnLogic(selectedGameobject, newPosition);
                return selectedGameobject.transform.position;
                }
            //East
            else if (couplingPosition == 2)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x + selectedGameobject.transform.lossyScale.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z);
                if (IsPositionFree(newPosition, selectedGameobject))
                    return ReturnLogic(selectedGameobject, newPosition);
                return selectedGameobject.transform.position;
                }
            //South
            else if (couplingPosition == 3)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z - selectedGameobject.transform.lossyScale.z);
                if (IsPositionFree(newPosition, selectedGameobject))
                    return ReturnLogic(selectedGameobject, newPosition);
                return selectedGameobject.transform.position;
                }
            //West
            else if (couplingPosition == 4)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x - selectedGameobject.transform.lossyScale.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z);
                if (IsPositionFree(newPosition, selectedGameobject))
                    return ReturnLogic(selectedGameobject, newPosition);
                return selectedGameobject.transform.position;
                }
            return selectedGameobject.transform.position;
            }

        private static Vector3 ReturnLogic(GameObject selectedGameobject, Vector3 newPosition)
            {
            if (HaveNeightbourAtPosition(newPosition, selectedGameobject.transform.parent.GetComponent<IBuildingManagment>().ListOfChildren))
                {
                return selectedGameobject.transform.position;
                }
            return newPosition;
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
        private static bool IsPositionFree(Vector3 position, GameObject selectedGameobject)
            {
            Collider[] collidersInArea = new Collider[20];
            _ = Physics.OverlapSphereNonAlloc(position, 0.8f, collidersInArea);
            foreach (Collider collider in collidersInArea)
                {
                if (collider == null)
                    {
                    break;
                    }
                if (collider.transform.parent == selectedGameobject.transform.parent)
                    {
                    continue;
                    }
                else if (LayerClass.GetSolitObjectLayer().Contains(collider.gameObject.layer))
                    {
                    return false;
                    }
                }
            return true;
            }
        }
    }
