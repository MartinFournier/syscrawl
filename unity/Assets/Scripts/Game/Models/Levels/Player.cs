using syscrawl.Models.Levels;
using syscrawl.Signals;

namespace syscrawl.Models
{
    public interface IPlayer
    {
        void MoveTo(Node node);

        string Name { get; set; }
    }

    public class Player : IPlayer
    {
        Node currentNode;
        Node previousNode;

        [Inject]
        public PlayerMovedSignal playerMovedSignal { get; set; }

        public string Name { get; set; }

        public void MoveTo(Node node)
        {
            previousNode = currentNode;
            currentNode = node;
            playerMovedSignal.Dispatch();
        }
    }
}

