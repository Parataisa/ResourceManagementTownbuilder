using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingTransportationSystem
    {
    class TransportUnitBehaviour : MonoBehaviour
        {
        private Dictionary<string, int> transportedResource;
        private float moveSpeed = 1.5f;
        private int carryingCapacity = 5;
        private GameObject origenBuilding;
        private GameObject targetBuilding;
        private bool targetReacht = false;
        private void Start()
            {
            StartCoroutine(GoingToTargetBuilding());
            }

        private void Update()
            {
            if (transform.localPosition == targetBuilding.transform.localPosition)
                {
                targetReacht = true;
                StopAllCoroutines();
                }
            }
        private IEnumerator GoingToTargetBuilding()
            {
            while (!targetReacht)
                {
                gameObject.transform.position = Vector3.MoveTowards(transform.position, targetBuilding.transform.GetChild(0).position + Vector3.forward + Vector3.right, moveSpeed * Time.deltaTime);
                yield return null;
                }
            }

        public void SetBuildingData(GameObject origen, GameObject target)
            {
            GetOrigenBuilding(origen);
            GetTargetBuilding(target);
            }
        private void GetOrigenBuilding(GameObject origen)
            {
            origenBuilding = origen;
            }
        private void GetTargetBuilding(GameObject target)
            {
            targetBuilding = target;
            }

        }
    }
