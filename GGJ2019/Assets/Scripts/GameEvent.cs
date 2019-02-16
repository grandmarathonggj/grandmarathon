namespace DefaultNamespace
{
    public class GameEvent
    {
        public const string LEVEL_TIMER_END = "LEVEL_TIMER_END";
        public const string LEVEL_COMPLETED = "LEVEL_COMPLETED";
        public const string PREVIOUS_LEVEL = "PREVIOUS_LEVEL";
        public const string NEXT_LEVEL = "NEXT_LEVEL";
        public const string RETRY_LEVEL = "RETRY_LEVEL";
        public const string PICKUP = "PICKUP";
        public const string PAUSE_BUTTON_CLICKED = "PAUSE_BUTTON_CLICKED";
        public const string PAUSE = "PAUSE";
        public const string UNPAUSE = "UNPAUSE";
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