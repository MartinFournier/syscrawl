using syscrawl.Models.Levels;
using syscrawl.Services.Levels;

namespace syscrawl.Models.Levels
{
    public interface ILevel
    {
        
    }

    public class Level : ILevel
    {
        readonly ILevelGenerator levelGenerator;

        public GameLevelGraph Graph;
        //        public Positioning Positioning;

        public Level(ILevelGenerator levelGenerator)
        {
            this.levelGenerator = levelGenerator;
            this.levelGenerator.Generate();
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