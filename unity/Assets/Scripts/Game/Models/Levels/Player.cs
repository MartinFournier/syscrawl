using syscrawl.Game.Models.Levels;

namespace syscrawl.Game.Models
{
    public interface IPlayer
    {
        void MoveTo(Node node);

        string Name { get; set; }

        Node CurrentNode { get; }

        Node PreviousNode { get; }
    }

    public class Player : IPlayer
    {
        //TODO: Make set private again, used for debugging.
        public Node CurrentNode { get; set; }

        public Node PreviousNode { get; private set; }

        [Inject]
        public PlayerMovedSignal PlayerMovedSignal { get; set; }

        public string Name { get; set; }

        public void MoveTo(Node node)
        {
            PreviousNode = CurrentNode;
            CurrentNode = node;
            PlayerMovedSignal.Dispatch();
        }
    }
}

