using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Farm : MonoBehaviour, IResourcBuilding
        {
        private readonly List<string> resouces = new List<string>();
        private Color color = new Color();
        public List<string> ResouceToGather { get => resouces; set => SetResouces(resouces); }
        public Color BuildingColor { get => color; }

        private Color GetBuildingsColor()
            {
            Color color = GetComponent<Renderer>().material.color;
            return color;
            }
        private void Start()
            {
            SetResouces(resouces);
            color = GetBuildingsColor();
            }
        private List<string> SetResouces(List<string> resoucesToGather)
            {
            resoucesToGather.Add("Wheat");
            return resouces;
            }
        }
    }
