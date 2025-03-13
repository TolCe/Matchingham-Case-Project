public class GameSignals
{
    public class CallInputStart { }
    public class CallInputRelease { }

    public class CallLevelLoaded { }

    public class CallLevelEnd
    {
        public CallLevelEnd(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
    }
}