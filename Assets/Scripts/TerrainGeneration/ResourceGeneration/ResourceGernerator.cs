using Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.RecourceGeneration
    {
    class ResourceGernerator : MonoBehaviour
        {
        public static List<GameObject> ResourcePrefabs = new List<GameObject>();
        private static Dictionary<Vector2, Vector2> spawnedPoints = new Dictionary<Vector2, Vector2>();
        private static Dictionary<Vector2, Vector2> points = new Dictionary<Vector2, Vector2>();
        public int numberOfIterationen = 20;
        public int NumberOfResources;
        public Mesh terrainMesh;
        public void Start()
            {
            NumberOfResources = 10 - 1;//UnityEngine.Random.Range(1, 20);
            UnityEngine.Object[] subListObjects = Resources.LoadAll("ResourceVariationen", typeof(GameObject));
            foreach (GameObject gameObject in subListObjects)
                {
                GameObject lo = (GameObject)gameObject;
                ResourcePrefabs.Add(lo);
                }
            for (int i = 0; i <= NumberOfResources; i++)
                {
                int randomResouce = UnityEngine.Random.Range(0, ResourcePrefabs.Count);
                ChooseResouceToGenerate(ResourcePrefabs[randomResouce]);
                }


            }

        private void ChooseResouceToGenerate(GameObject resourceType)
            {
            var resourceToSpawn = Instantiate<GameObject>(resourceType);
            var scriptOfTheResource = resourceToSpawn.GetComponent<ResourceBase>();
            scriptOfTheResource.GetComponent<ResourceBase>().sizeOfTheModel = resourceType.gameObject.transform.localScale;
            FindObjectOfType<ResourceBase>(resourceToSpawn).ChooseLocationEvent += SetLocationOfTheResouurce;
            FindObjectOfType<ResourceBase>(scriptOfTheResource).ResourceGenerated += GeneratResources;
            }

        private void SetLocationOfTheResouurce(GameObject resourceToSpawn)
            {
            var spawnPopintArea = resourceToSpawn.GetComponent<ResourceBase>().areaOfTheResource;
            var PositonOnTheMap = resourceToSpawn.GetComponent<ResourceBase>().positionOnTheMap;
            bool pointValid = PointIteration(resourceToSpawn, spawnPopintArea, PositonOnTheMap);
            if (pointValid)
                {
                Debug.Log(resourceToSpawn.name + " created");
                }
            else
                {
                Destroy(resourceToSpawn);
                Debug.Log(resourceToSpawn.name + " destroyed");
                }
            }

        private bool PointIteration(GameObject resourceToSpawn, Vector2 spawnPopintArea, Vector3 PositonOnTheMap)
            {
            for (int i = 0; i <= numberOfIterationen; i++)
                {
                if (spawnedPoints.Count == 0)
                    {
                    resourceToSpawn.GetComponent<Transform>().transform.position = PositonOnTheMap;
                    spawnedPoints.Add(new Vector2(PositonOnTheMap.x, PositonOnTheMap.z), spawnPopintArea);
                    return true;
                    }
                else
                    {
                    points.Add(PositonOnTheMap, spawnPopintArea);
                    int numberOfInnerLoops = 0;
                    while (points.Count > 0 || numberOfIterationen <= numberOfInnerLoops)
                        {
                        int loopcount = 0;
                        foreach (var savedPoint in spawnedPoints)
                            {
                            Rectangle savedArea = new Rectangle();
                            savedArea.Width = (int)savedPoint.Value.x;
                            savedArea.Height = (int)savedPoint.Value.y;
                            savedArea.X = (int)(savedPoint.Key.x - savedPoint.Value.x / 2);
                            savedArea.Y = (int)(savedPoint.Key.y - savedPoint.Value.y / 2);

                            Rectangle newArea = new Rectangle();
                            newArea.Width = (int)spawnPopintArea.x;
                            newArea.Height = (int)spawnPopintArea.y;
                            newArea.X = (int)(PositonOnTheMap.x - spawnPopintArea.x / 2);
                            newArea.Y = (int)(PositonOnTheMap.z - spawnPopintArea.y / 2);
                            if (!newArea.IntersectsWith(savedArea) && PositonOnTheMap.x < 256 - spawnPopintArea.x / 2 && PositonOnTheMap.z < 256 - spawnPopintArea.y / 2)
                                {
                                loopcount++;
                                if (loopcount == spawnedPoints.Count)
                                    {
                                    resourceToSpawn.GetComponent<Transform>().transform.position = PositonOnTheMap;
                                    resourceToSpawn.GetComponent<ResourceBase>().positionOnTheMap = PositonOnTheMap;
                                    spawnedPoints.Add(new Vector2(PositonOnTheMap.x, PositonOnTheMap.z), spawnPopintArea);
                                    points.Remove(PositonOnTheMap);
                                    return true;
                                    }
                                }
                            else
                                {
                                points.Remove(PositonOnTheMap);
                                var newPickedPoint = ResourceBase.GetPositionOfResource(spawnPopintArea);
                                PositonOnTheMap = newPickedPoint;
                                numberOfInnerLoops++;
                                loopcount = 0;
                                points.Add(PositonOnTheMap, spawnPopintArea);
                                break;
                                }
                            }
                        }
                    points.Remove(PositonOnTheMap);
                    }
                }

            return false;
            }

        private void GeneratResources(ResourceBase scriptOfTheResource)
            {
            int size = scriptOfTheResource.sizeOfTheResource;
            Vector2 area = scriptOfTheResource.areaOfTheResource;
            GameObject child = scriptOfTheResource.transform.GetChild(0).gameObject;
            for (int i = 0; i < size; i++)
                {
                GameObject _ = Instantiate<GameObject>(child, scriptOfTheResource.transform);
                var localYTransform = _.GetComponent<Transform>().localScale = scriptOfTheResource.sizeOfTheModel;
                Vector2 localPositon = GetlocalPositon(area);
                Vector3 vector3 = new Vector3(localPositon.x, localYTransform.y / 2, localPositon.y);
                _.GetComponent<Transform>().localPosition = vector3;
                }
            Destroy(child);
            }

        private Vector2 GetlocalPositon(Vector2 area)
            {
            Vector2 position;
            position.x = UnityEngine.Random.Range(-area.x / 2 , area.x / 2);
            position.y = UnityEngine.Random.Range(-area.y / 2, area.y / 2);
            return position;
            }
        }
    }
