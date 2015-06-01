using strange.extensions.signal.impl;
using syscrawl.Game.Views.Levels;

namespace syscrawl.Game
{
    public class GameStartSignal : Signal
    {
    }

    public class GenerateLevelSignal : Signal
    {
    }

    public class LevelGeneratedSignal : Signal
    {
    }

    public class PlayerMovedSignal : Signal
    {
    }

    public class PositionNodesSignal : Signal<LevelMediator>
    {
    }
}
