using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Lumberjack : MonoBehaviour, IResourcBuilding
        {
        private readonly List<string> resouces = new List<string>();
        private Color color = new Color();
        public List<string> ResourceToGather { get => resouces; set => SetResouces(resouces); }
        public Color BuildingColor { get => color; }
        public string BuildingTyp => this.GetType().Name;
        public Vector3 BuildingPosition => this.gameObject.transform.position;
        public Vector3 BuildingSize => this.gameObject.transform.lossyScale;
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
            resoucesToGather.Add("Tree");
            return resouces;
            }
        }
    }
