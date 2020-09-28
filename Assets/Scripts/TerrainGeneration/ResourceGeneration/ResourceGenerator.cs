using ResourceGeneration.ResourceVariationen;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.RecourceGeneration
    {
    class ResourceGenerator : MonoBehaviour
        {
        public static List<GameObject> ResourcePrefabs = new List<GameObject>();
        private static readonly Dictionary<Vector2, Vector2> spawnedPoints = new Dictionary<Vector2, Vector2>();
        private readonly Dictionary<Vector2, Vector2> points = new Dictionary<Vector2, Vector2>();
        public int numberOfIterationen = 20;
        public int NumberOfResources = 10;
        public Mesh terrainMesh;
        public event System.Action<ResourceBase> ResourceSuccessfullyGenerated;
        public static Object[] subListObjects;

        public void Start()
            {
            if (subListObjects == null)
                {
                subListObjects = Resources.LoadAll("GameObjects/GatherableResources/ResourceVariationen", typeof(GameObject));
                }
            foreach (GameObject ResourcePrefab in subListObjects)
                {
                ResourcePrefabs.Add(ResourcePrefab);
                }
            for (int i = 1; i <= NumberOfResources; i++)
                {
                GenerateResource(ResourcePrefabs[Random.Range(0, ResourcePrefabs.Count)]);
                }
            }

        private void GenerateResource(GameObject resourceType)
            {
            var resourceToSpawn = Instantiate<GameObject>(resourceType, transform.parent.transform);
            var scriptOfTheResource = resourceToSpawn.GetComponent<ResourceBase>();
            scriptOfTheResource.GetComponent<ResourceBase>().SizeOfTheModel = resourceType.gameObject.transform.localScale;
            FindObjectOfType<ResourceBase>(resourceToSpawn).ChooseLocationEvent += SetLocationOfTheResource;
            FindObjectOfType<ResourceBase>(scriptOfTheResource).ResourceGenerated += GeneratResources;
            }

        private void SetLocationOfTheResource(GameObject resourceToSpawn)
            {
            var spawnPopintArea = resourceToSpawn.GetComponent<ResourceBase>().AreaOfTheResource;
            var PositonOnTheMap = resourceToSpawn.GetComponent<ResourceBase>().PositionOnTheMap;
            bool pointValid = SpawnLocationPointIteration(resourceToSpawn, spawnPopintArea, PositonOnTheMap);
            if (pointValid)
                {
                Debug.Log(resourceToSpawn.name + " created");
                ResourceSuccessfullyGenerated?.Invoke(resourceToSpawn.GetComponent<ResourceBase>());
                }
            else
                {
                Destroy(resourceToSpawn);
                Debug.Log(resourceToSpawn.name + " destroyed");
                }
            }

        private bool SpawnLocationPointIteration(GameObject resourceToSpawn, Vector2 spawnPointArea, Vector3 PositonOnTheMap)
            {
            if (spawnedPoints.Count == 0)
                {
                resourceToSpawn.GetComponent<Transform>().transform.localPosition = PositonOnTheMap;
                spawnedPoints.Add(new Vector2(PositonOnTheMap.x, PositonOnTheMap.z), spawnPointArea);
                resourceToSpawn.transform.parent = transform.parent;
                return true;
                }
            else
                {
                points.Add(PositonOnTheMap, spawnPointArea);
                int numberOfInnerLoops = 0;
                while (!(numberOfIterationen <= numberOfInnerLoops))
                    {
                    int loopcount = 0;
                    foreach (var savedPoint in spawnedPoints)
                        {
                        Vector2 savedPointKey = new Vector2(savedPoint.Key.x, savedPoint.Key.y);
                        Vector3 savedPointValue = new Vector3(savedPoint.Value.x, 0, savedPoint.Value.y);
                        Rectangle savedArea = GetRectangle(savedPointValue, savedPointKey);
                        Rectangle newArea = GetRectangle(new Vector3(spawnPointArea.x, 0, spawnPointArea.y), new Vector2(PositonOnTheMap.x, PositonOnTheMap.z));
                        if (!newArea.IntersectsWith(savedArea) && PositonOnTheMap.x < 256 - spawnPointArea.x / 2 && PositonOnTheMap.z < 256 - spawnPointArea.y / 2)
                            {
                            loopcount++;
                            if (loopcount == spawnedPoints.Count)
                                {
                                resourceToSpawn.transform.parent = transform.parent;
                                resourceToSpawn.GetComponent<Transform>().transform.localPosition = PositonOnTheMap;
                                resourceToSpawn.GetComponent<ResourceBase>().PositionOnTheMap = PositonOnTheMap;
                                spawnedPoints.Add(new Vector2(PositonOnTheMap.x, PositonOnTheMap.z), spawnPointArea);
                                points.Remove(PositonOnTheMap);
                                return true;
                                }
                            }
                        else
                            {
                            points.Remove(PositonOnTheMap);
                            var newPickedPoint = ResourceBase.GetPositionOfResource(spawnPointArea);
                            PositonOnTheMap = newPickedPoint;
                            numberOfInnerLoops++;
                            loopcount = 0;
                            points.Add(PositonOnTheMap, spawnPointArea);
                            break;
                            }
                        }
                    }
                points.Remove(PositonOnTheMap);
                }
            return false;
            }

        private void GeneratResources(ResourceBase scriptOfTheResource)
            {
            int size = scriptOfTheResource.SizeOfTheResource;
            Vector3 sizeOfTheModel = scriptOfTheResource.SizeOfTheModel;
            Vector2 area = scriptOfTheResource.AreaOfTheResource;
            GameObject child = scriptOfTheResource.transform.GetChild(0).gameObject;
            List<Rectangle> AreasOfChildObjects = new List<Rectangle>();

            for (int i = 0; i < size; i++)
                {
                GameObject _ = Instantiate<GameObject>(child, scriptOfTheResource.transform);
                for (int x = 0; x <= numberOfIterationen; x++)
                    {
                    int innerLoop = 0;
                    var localYTransform = _.GetComponent<Transform>().localScale = scriptOfTheResource.SizeOfTheModel;
                    Vector2 localPositon = GetlocalPositon(area);
                    Rectangle RecArea = GetRectangle(sizeOfTheModel, localPositon);

                    if (AreasOfChildObjects.Count == 0)
                        {
                        Vector3 newChildObjectPosition = new Vector3(localPositon.x, localYTransform.y / 2, localPositon.y);
                        _.GetComponent<Transform>().localPosition = newChildObjectPosition;
                        AreasOfChildObjects.Add(RecArea);
                        break;
                        }

                    foreach (var savedChild in AreasOfChildObjects)
                        {
                        Rectangle NewRecArea = GetRectangle(sizeOfTheModel, localPositon);
                        if (!NewRecArea.IntersectsWith(savedChild))
                            {
                            innerLoop++;
                            if (innerLoop == AreasOfChildObjects.Count)
                                {
                                Vector3 newChildObjectPosition = new Vector3(localPositon.x, localYTransform.y / 2, localPositon.y);
                                _.GetComponent<Transform>().localPosition = newChildObjectPosition;
                                AreasOfChildObjects.Add(RecArea);
                                x = numberOfIterationen;
                                break;
                                }
                            }
                        else
                            {
                            localPositon = GetlocalPositon(area);
                            break;
                            }
                        }
                    }
                }
            
            Destroy(child);
            scriptOfTheResource.SizeOfTheResource = AreasOfChildObjects.Count;
            }

        private static Rectangle GetRectangle(Vector3 areaOfTheModel, Vector2 localPositon)
            {
            Rectangle RecArea;
            RecArea.X = Mathf.CeilToInt((localPositon.x - areaOfTheModel.x / 2));
            RecArea.Y = Mathf.CeilToInt((localPositon.y - areaOfTheModel.z / 2));
            RecArea.Width = Mathf.CeilToInt(areaOfTheModel.x);
            RecArea.Height = Mathf.CeilToInt(areaOfTheModel.z);
            return RecArea;
            }

        private Vector2 GetlocalPositon(Vector2 area)
            {
            Vector2 position;
            position.x = UnityEngine.Random.Range(-area.x / 2, area.x / 2);
            position.y = UnityEngine.Random.Range(-area.y / 2, area.y / 2);
            return position;
            }
        }
    }
