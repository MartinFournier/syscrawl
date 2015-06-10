using strange.extensions.signal.impl;
using syscrawl.Game.Views.Levels;
using UnityEngine;
using syscrawl.Game.Models.Levels;

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

    public class CreateNodeSignal : Signal<Node, GameObject, Vector3>
    {
    }

    public class CreateNodeTypeSignal : Signal<NodeType, GameObject>
    {
    }
}
