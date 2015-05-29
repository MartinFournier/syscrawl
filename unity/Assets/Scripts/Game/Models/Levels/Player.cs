using syscrawl.Models.Levels;
using syscrawl.Signals;

namespace syscrawl.Models
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
        public Node CurrentNode { get; private set; }

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

