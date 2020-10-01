using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingBase : MonoBehaviour, IResourcBuilding
        {
        internal readonly List<string> resouces = new List<string>();
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
        protected virtual void Start()
            {
            SetResouces(resouces);
            color = GetBuildingsColor();
            }
        virtual internal List<string> SetResouces(List<string> resoucesToGather)
            {
            return resouces;
            }
        }
    }
