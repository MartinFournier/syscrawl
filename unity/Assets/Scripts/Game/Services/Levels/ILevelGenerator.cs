using syscrawl.Models.Levels;

namespace syscrawl.Services.Levels
{
    public interface ILevelGenerator
    {
        LevelGraph Generate();
    }
}
