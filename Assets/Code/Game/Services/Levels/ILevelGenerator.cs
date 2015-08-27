using syscrawl.Game.Models.Levels;

namespace syscrawl.Game.Services.Levels
{
    public interface ILevelGenerator
    {
        LevelGraph Generate();
    }
}
