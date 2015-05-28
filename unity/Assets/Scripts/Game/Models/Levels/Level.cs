using syscrawl.Models.Levels;
using syscrawl.Services.Levels;

namespace syscrawl.Models.Levels
{
    public interface ILevel
    {
        LevelGraph GetGraph();

        void Generate(string levelName);

        string GetName();
    }

    public class Level : ILevel
    {
        readonly ILevelGenerator levelGenerator;

        public LevelGraph Graph;
        //        public Positioning Positioning;

        string LevelName { get; set; }

        public Level(ILevelGenerator levelGenerator)
        {
            this.levelGenerator = levelGenerator;
        }

        public LevelGraph GetGraph()
        {
            return Graph;
        }

        public void Generate(string levelName)
        {
            Graph = levelGenerator.Generate();
            LevelName = levelName;
        }

        public string GetName()
        {
            return LevelName;
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

        public override string ToString()
        {
            return string.Format("Level: " + LevelName);
        }
    }
}