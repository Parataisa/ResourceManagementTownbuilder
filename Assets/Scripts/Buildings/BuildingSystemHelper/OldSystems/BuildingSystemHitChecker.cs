using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {

    public static class BuildingSystemHitChecker
        {
        public static bool WestHitCheck(GameObject h, Vector3 MouseCurserWorldPosition)
            {
            return h.transform.position.x > MouseCurserWorldPosition.x && Enumerable.Range((int)(h.transform.position.z + (h.transform.lossyScale.z / 2)), (int)(h.transform.position.z - h.transform.lossyScale.z / 2)).Contains(Convert.ToInt32(MouseCurserWorldPosition.z));
            }

        public static bool SouthHitCheck(GameObject h, Vector3 MouseCurserWorldPosition)
            {
            return h.transform.position.z > MouseCurserWorldPosition.z && Enumerable.Range((int)(h.transform.position.x - (h.transform.lossyScale.x / 2)), (int)(h.transform.position.x + h.transform.lossyScale.x / 2)).Contains(Convert.ToInt32(MouseCurserWorldPosition.x));
            }

        public static bool EastHitCheck(GameObject h, Vector3 MouseCurserWorldPosition)
            {
            return h.transform.position.x < MouseCurserWorldPosition.x && Enumerable.Range((int)(h.transform.position.z - (h.transform.lossyScale.z / 2)), (int)(h.transform.position.z + h.transform.lossyScale.z / 2)).Contains(Convert.ToInt32(MouseCurserWorldPosition.z));
            }

        public static bool NorthHitCheck(GameObject h, Vector3 MouseCurserWorldPosition)
            {
            return h.transform.position.z < MouseCurserWorldPosition.z && Enumerable.Range((int)(h.transform.position.x + (h.transform.lossyScale.x / 2)), (int)(h.transform.position.x - h.transform.lossyScale.x / 2)).Contains(Convert.ToInt32(MouseCurserWorldPosition.x));
            }
        }
    }
