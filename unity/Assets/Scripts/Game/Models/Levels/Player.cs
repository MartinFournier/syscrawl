using syscrawl.Models.Levels;

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

        public string Name { get; set; }

        public void MoveTo(Node node)
        {
            previousNode = currentNode;
            currentNode = node;
        }
    }
}

