namespace EMDR.Core
{
    public struct BobbleMotionData
    {
        public BobbleMotionDataType type { get; private set; }
        public float relativeSpeed { get; private set; }
        public float xRange { get; private set; }

        public BobbleMotionData(BobbleMotionDataType type, float input)
        {
            this.type = type;
            relativeSpeed = 0.0f;
            xRange = 1.0f;
            
            switch (type)
            {
                case BobbleMotionDataType.Speed:
                    relativeSpeed = input;
                    break;
                case BobbleMotionDataType.Range:
                    xRange = input;
                    break;
            }
        }
    }
}
