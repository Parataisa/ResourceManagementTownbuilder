using UnityEngine;

namespace Assets.Scripts.Buildings.SocialBuildings
    {
    class SocialBuildingBase : MonoBehaviour, ISocialBuildings
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
        private Color GetBuildingsColor()
            {
            Color color = GetComponent<Renderer>().material.color;
            return color;
            }
        }
    }
