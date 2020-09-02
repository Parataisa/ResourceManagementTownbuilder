using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen
    {
    public class ResourceBase
        {
        public int sizeOfTheResource;
        public int quantityOfTheResource;
        public string nameOfTheResource;
        public Vector2 positionOfThePatch;
        public GameObject ResourcePrefab;
        public Mesh mesh;

        public ResourceBase(string nameOfTheResource)
            {
            this.nameOfTheResource = nameOfTheResource;
            this.quantityOfTheResource = GetQuantityOfTheResource();
            this.sizeOfTheResource = GetSizeOfTheResource();
            Vector2 positonOnTheMesh = GetPositionOfResource(mesh);
            this.positionOfThePatch = positonOnTheMesh;

            }
        public int GetQuantityOfTheResource()
            {
            //Some random calc

            return 0;
            }
        public int GetSizeOfTheResource()
            {
            //Some random calc

            return 0;
            }
        public Vector2 GetPositionOfResource(Mesh mesh)
            {
            Vector2 position = new Vector2();

            //Some random calc
            return position;
            }
        }

    }
