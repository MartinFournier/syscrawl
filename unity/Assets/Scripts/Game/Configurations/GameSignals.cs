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

    public class PlayerMoveToSignal : Signal<Node>
    {
    }

    public class PositionNodesSignal : Signal<LevelSceneMediator>
    {
    }

    public class CreateNodeSignal : Signal<Node, GameObject, Vector3, SceneNodeType>
    {
    }

    public class CreateNodeTypeSignal : Signal<NodeType, GameObject>
    {
    }

    public class CreateNodeConnectionSignal : Signal<CreateNodeConnection>
    {
    }
}
