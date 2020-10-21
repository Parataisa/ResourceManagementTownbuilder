using Assets.Scripts.Buildings.BuildingSystemHelper;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings
    {
    class BuildingBase : MonoBehaviour, IBuildings
        {
        private Color color = new Color();
        public Color BuildingColor { get => color; }
        public string BuildingTyp => this.GetType().Name;
        public Vector3 BuildingPosition => this.gameObject.transform.position;
        public Vector3 BuildingSize => this.gameObject.transform.lossyScale;

        protected virtual void Start()
            {
            color = GetBuildingsColor();
            }
        internal Color GetBuildingsColor()
            {
            Color color = this.GetComponent<Renderer>().material.color;
            return color;
            }
        }
    }
