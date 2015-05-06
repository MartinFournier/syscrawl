using UnityEngine;
using syscrawl.Levels.Nodes;

namespace syscrawl.Levels.Graph.Generators
{
    public static class SpecificGraph
    {
        public static LevelGraph Generate(Level level, LevelSettings settings)
        {
            var graph = new LevelGraph(level, settings);

            var entranceNode =
                graph.CreateNode(
                    NodeType.Entrance,
                    "Entrance 0",
                    null);

            var fw1 =
                graph.CreateNode(
                    NodeType.Firewall,
                    "fw1",
                    entranceNode);

            var c1 =
                graph.CreateNode(
                    NodeType.Connector,
                    "c1",
                    fw1);

            var fw2c1 =
                graph.CreateNode(
                    NodeType.Firewall,
                    "fw2c1",
                    c1);

            var fwc1 =
                graph.CreateNode(
                    NodeType.Firewall,
                    "fwc1",
                    c1);

            var fs1 =
                graph.CreateNode(
                    NodeType.Filesystem,
                    "fs1",
                    fwc1);

            var fs2 =
                graph.CreateNode(
                    NodeType.Filesystem,
                    "fs2",
                    fwc1);

            var fs3 =
                graph.CreateNode(
                    NodeType.Filesystem,
                    "fs3",
                    fwc1);

            var c2 =
                graph.CreateNode(
                    NodeType.Connector,
                    "c2",
                    fw1);

            var c3 =
                graph.CreateNode(
                    NodeType.Connector,
                    "c3",
                    fw1);

            return graph;
        }
    }
}