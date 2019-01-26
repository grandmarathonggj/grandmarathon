namespace DefaultNamespace
{
    public class GameEvent
    {
        public static string LEVEL_TIMER_END="LEVEL_TIMER_END";
        public static string LEVEL_COMPLETED = "LEVEL_COMPLETED";
        public const string START_LEVEL_TIMER = "START_LEVEL_TIMER";
        public const string LEVEL_TIMER_TICK = "LEVEL_TIMER_TICK";
        public const string ANIMATE_SCORE = "ANIMATE_SCORE";
    }
}

class LevelCompletedParams : EventParam
{
    public bool completed;
    public int score;

    public LevelCompletedParams(bool completed, int score)
    {
        this.completed = completed;
        this.score = score;
    }
}