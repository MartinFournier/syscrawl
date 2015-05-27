using syscrawl.Models.Levels;
using syscrawl.Services.Levels;

namespace syscrawl.Models.Levels
{
    public interface ILevel
    {
        LevelGraph GetGraph();
    }

    public class Level : ILevel
    {
        readonly ILevelGenerator levelGenerator;

        public LevelGraph Graph;
        //        public Positioning Positioning;

        public Level(ILevelGenerator levelGenerator)
        {
            this.levelGenerator = levelGenerator;
            Graph = this.levelGenerator.Generate();
        }

        public LevelGraph GetGraph()
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