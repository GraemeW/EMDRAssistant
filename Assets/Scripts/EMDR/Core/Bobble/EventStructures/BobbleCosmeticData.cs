using UnityEngine;

namespace EMDR.Core
{
    public struct BobbleCosmeticData
    {
        public BobbleCosmeticDataType type { get; private set; }
        public BobbleShape bobbleShape { get; private set; }
        public float bobbleSize { get; private set; }
        public Color bobbleColor { get; private set; }

        public BobbleCosmeticData(BobbleShape bobbleShape)
        {
            type = BobbleCosmeticDataType.Shape;
            this.bobbleShape = bobbleShape;
            
            // Dummies
            bobbleSize = EMDRBobble.defaultBobbleScale;
            bobbleColor = EMDRBobble.defaultBobbleColor;
        }

        public BobbleCosmeticData(float bobbleSize)
        {
            type = BobbleCosmeticDataType.Size;
            this.bobbleSize = bobbleSize;
            
            // Dummies
            bobbleShape = EMDRBobble.defaultBobbleShape;
            bobbleColor = EMDRBobble.defaultBobbleColor;
        }

        public BobbleCosmeticData(Color bobbleColor)
        {
            type = BobbleCosmeticDataType.Color;
            this.bobbleColor = bobbleColor;
            
            // Dummies
            bobbleShape = EMDRBobble.defaultBobbleShape;
            bobbleSize = EMDRBobble.defaultBobbleScale;
        }
    }
}
