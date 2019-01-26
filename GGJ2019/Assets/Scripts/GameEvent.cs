namespace DefaultNamespace
{
    public class GameEvent
    {
        public static string LEVEL_TIMER_END = "LEVEL_TIMER_END";
        public static string LEVEL_COMPLETED = "LEVEL_COMPLETED";
        public static string NEXT_LEVEL = "NEXT_LEVEL";
        public static string RETRY_LEVEL = "RETRY_LEVEL";
        public const string START_LEVEL_TIMER = "START_LEVEL_TIMER";
        public const string LEVEL_TIMER_TICK = "LEVEL_TIMER_TICK";
        public const string ANIMATE_SCORE = "ANIMATE_SCORE";
		public const string PLAYER_DEATH = "PLAYER_DEATH";
	}
}

class LevelCompletedParams : EventParam
{
    public bool success;
    public int score;
    public int star;

    public LevelCompletedParams(bool success, int score, int star)
    {
        this.success = success;
        this.score = score;
        this.star = star;
    }
}