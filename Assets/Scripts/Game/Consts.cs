namespace RubiksCubeEgg.Game
{
    public class Consts
    {
        
        public const int CameraLayerMask = 1 << 3;
        public const int BallLayerMask = 1 << 6;
        public const int UpLayerMask = 1 << 7;
        public const int MiddleLayerMask = 1 << 8;
        public const int BottomLayerMask = 1 << 9;
        public const int CameraLayer = 3;
        public const int BallLayer = 6;
        public const int UpLayer = 7; 
        public const int MiddleLayer = 8;   
        public const int BottomLayer = 9;          
        public const float SegmentRotStep = 90f;
        public const float SideRotStep = 0.375f;
        public const int SideBallCount = 8;
        public const float SegmentRotSpeed = 300f;
        public const float SegmentRotAligningSpeed = 50f;
        public const float SideRotSpeed = 1f;
        public const string ForwardTag = "Forward";
        public const string BackTag = "Back";
        public const string LeftTag = "Left";
        public const string RightTag = "Right";
    }
}
