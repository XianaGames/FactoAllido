namespace NavigatorTool
{
    public static class FrameNavigatorProvider
    {
        private static FrameNavigator _frameNavigator;

        public static FrameNavigator FrameNavigator => _frameNavigator;

        public static void Reset() => 
            _frameNavigator = new FrameNavigator();
    }
}