using Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems;
using Assets.Scripts.Buildings.StorageBuildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingTransportationSystem
    {
    class TransportUnitBehaviour : MonoBehaviour
        {
        private Dictionary<string, int> transportedResource;
        private float moveSpeed = 4.5f;
        private int carryingCapacity = 5;
        private GameObject origenBuilding;
        private GameObject targetBuilding;
        private bool targetReacht = false;
        private bool haveResources = false;

        private void Start()
            {
            StartCoroutine(GoingToTargetBuilding(targetBuilding));
            }

        private void Update()
            {
            if (transform.localPosition == targetBuilding.transform.GetChild(0).transform.localPosition)
                {
                targetReacht = true;
                StopAllCoroutines();
                CollectResources();
                }
            if (transform.localPosition == origenBuilding.transform.GetChild(0).transform.localPosition && haveResources)
                {
                targetReacht = false;
                StopAllCoroutines();
                origenBuilding.GetComponent<StorageBuildingManagment>().AddResources(transportedResource);
                haveResources = false;
                transportedResource.Clear();
                targetBuilding = origenBuilding.GetComponent<StorageBuildingManagment>().TargetBuilding;
                StartCoroutine(GoingToTargetBuilding(targetBuilding));
                }
            }

        private void CollectResources()
            {
            transportedResource = new Dictionary<string, int>();
            var targetBuildingStortedResources = targetBuilding.GetComponent<ResourceHandlingBuildingBase>();
            int totalResources = 0;
            foreach (var resource in targetBuildingStortedResources.StoredResources)
                {
                totalResources += resource.Value;
                }
            if (targetBuildingStortedResources.ResourceCollectionAtTheTime || totalResources == 0)
                {
                StartCoroutine(WaitForResources(totalResources));
                }
            else
                {
                string resourceToCollect = "";
                targetBuildingStortedResources.ResourceCollectionAtTheTime = true;
                foreach (var targetResource in targetBuildingStortedResources.StoredResources)
                    {
                    if (targetResource.Value == 0)
                        {
                        continue;
                        }
                    else
                        {
                        resourceToCollect = targetResource.Key;
                        break;
                        }
                    }
                transportedResource.Add(resourceToCollect, targetBuildingStortedResources.StoredResources[resourceToCollect]);
                targetBuildingStortedResources.StoredResources[resourceToCollect] = 0;
                targetBuildingStortedResources.ResourceCollectionAtTheTime = false;
                haveResources = true;
                MoveBackToOrigenBuilding();
                }
            }

        private IEnumerator WaitForResources(int targetBuildingStortedResources)
            {
            while (targetBuildingStortedResources == 0)
                {
                yield return new WaitForSeconds(0.5f);
                }
            }

        private void MoveBackToOrigenBuilding()
            {
            targetReacht = false;
            StartCoroutine(GoingToTargetBuilding(origenBuilding));
            }

        private IEnumerator GoingToTargetBuilding(GameObject target)
            {
            while (!targetReacht)
                {
                Vector3 targetPosition = target.transform.GetChild(0).position;
                gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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
