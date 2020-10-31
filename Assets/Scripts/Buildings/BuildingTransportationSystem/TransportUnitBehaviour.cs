using Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems;
using Assets.Scripts.Buildings.StorageBuildings.StorageBuildingSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingTransportationSystem
    {
    class TransportUnitBehaviour : MonoBehaviour
        {
        IEnumerator currentMoveCoroutine;
        IEnumerator waitTimer;
        private Dictionary<string, int> transportedResource;
        private float moveSpeed = 4.5f;
        private int carryingCapacity = 5;
        private GameObject origenBuilding;
        private GameObject targetBuilding;
        private bool targetReacht = false;
        private bool haveResources = false;
        private bool noResourcesToGatherAtTheTime = false;
        private bool waitTimerIsRunning = false;
        private void Start()
            {
            waitTimer = WaitForResources();
            currentMoveCoroutine = GoingToTargetBuilding(targetBuilding);
            StartCoroutine(currentMoveCoroutine);
            }

        private void Update()
            {
            //Target position
            if (transform.localPosition == targetBuilding.transform.localPosition)
                {
                targetReacht = true;
                StopCoroutine(currentMoveCoroutine);
                if (!waitTimerIsRunning)
                    {
                    CollectResources();
                    }
                }
            // Home building position
            if (transform.localPosition == origenBuilding.transform.localPosition)
                {
                targetReacht = false;
                StopCoroutine(currentMoveCoroutine);
                GameObject[] newTargetPosition = origenBuilding.GetComponentInParent<StorageBuildingTransportSystem>().OnWorkerReturn(transportedResource, haveResources);
                targetBuilding = newTargetPosition[0];
                origenBuilding = newTargetPosition[1];
                haveResources = false;
                transportedResource.Clear();
                if (GetComponentInParent<StorageBuildingTransportSystem>().DestoryWorkingPeople())
                    {
                    //Destory the Worker when true(SendWorker > WorkingPeople
                    Destroy(this.gameObject);
                    }
                currentMoveCoroutine = GoingToTargetBuilding(targetBuilding);
                StartCoroutine(currentMoveCoroutine);
                }
            }

        private void CollectResources()
            {
            transportedResource = new Dictionary<string, int>();
            var target = targetBuilding.GetComponentInParent<ResourceHandlingBuildingBase>();
            int totalResources = 0;
            foreach (var resource in target.StoredResources)
                {
                totalResources += resource.Value;
                }
            if ((target.ResourceCollectionAtTheTime || totalResources == 0) && !noResourcesToGatherAtTheTime)
                {
                StartCoroutine(waitTimer);
                }
            else
                {
                if (noResourcesToGatherAtTheTime)
                    {
                    haveResources = false;
                    MoveBackToOrigenBuilding();
                    }
                else
                    {
                    string resourceToCollect = "";
                    target.ResourceCollectionAtTheTime = true;
                    foreach (var targetResource in target.StoredResources)
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
                    transportedResource.Add(resourceToCollect, 0);
                    int resourceAmountCollected = target.StoredResources[resourceToCollect] > carryingCapacity ? carryingCapacity : target.StoredResources[resourceToCollect];
                    target.StoredResources[resourceToCollect] -= resourceAmountCollected;
                    transportedResource[resourceToCollect] += resourceAmountCollected;
                    target.ResourceCollectionAtTheTime = false;
                    haveResources = true;
                    MoveBackToOrigenBuilding();
                    }
                }
            }

        private IEnumerator WaitForResources()
            {
            float timer = 0.0f;
            while (timer < 2f)
                {
                waitTimerIsRunning = true;
                timer += 0.2f;
                yield return new WaitForSeconds(0.2f);
                }
            waitTimerIsRunning = false;
            noResourcesToGatherAtTheTime = true;
            StopCoroutine(waitTimer);
            waitTimer = WaitForResources();
            }

        private void MoveBackToOrigenBuilding()
            {
            noResourcesToGatherAtTheTime = false;
            targetReacht = false;
            currentMoveCoroutine = GoingToTargetBuilding(origenBuilding);
            StartCoroutine(currentMoveCoroutine);
            }

        private IEnumerator GoingToTargetBuilding(GameObject target)
            {
            while (!targetReacht)
                {
                Vector3 targetPosition = target.transform.position;
                gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
                }
            }

        public void SetBuildingData(GameObject[] SubBuildingPosition)
            {
            GetTargetBuilding(SubBuildingPosition[0]);
            GetOrigenBuilding(SubBuildingPosition[1]);
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

