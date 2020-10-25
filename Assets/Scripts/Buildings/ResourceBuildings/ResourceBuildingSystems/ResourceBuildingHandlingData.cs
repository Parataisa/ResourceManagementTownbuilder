namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingHandlingData
        {
        private float produktionSpeed;
        private int workingPeopleCapacity;
        private int workingPeople;

        public float ProduktionSpeed { get => produktionSpeed; set => produktionSpeed = value; }
        public int WorkingPeopleCapacity { get => workingPeopleCapacity; set => workingPeopleCapacity = value; }
        public int WorkingPeople { get => workingPeople; set => workingPeople = value; }
        }
    }
