using syscrawl.Models.Levels;
using syscrawl.Services.Levels;

namespace syscrawl.Models.Levels
{
    public interface ILevel
    {
        GameLevelGraph GetGraph();
    }

    public class Level : ILevel
    {
        readonly ILevelGenerator levelGenerator;

        public GameLevelGraph Graph;
        //        public Positioning Positioning;

        public Level(ILevelGenerator levelGenerator)
        {
            this.levelGenerator = levelGenerator;
            Graph = this.levelGenerator.Generate();
        }

        public GameLevelGraph GetGraph()
        {
            return Graph;
        }

        //            Positioning =
        //                new Positioning(
        //                Graph,
        //                configurations.NodeAngle,
        //                configurations.NodeDistance);
        //
        //            Positioning.Position();
   
   
        //        void Start()
        //        {
        //            Positioning.ToggleVisibility();
        //        }
    }
}