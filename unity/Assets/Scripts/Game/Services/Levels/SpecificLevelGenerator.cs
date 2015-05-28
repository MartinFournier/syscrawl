using UnityEngine;
using syscrawl.Levels.Nodes;
using syscrawl.Models.Levels;
using syscrawl.Services;

namespace syscrawl.Services.Levels
{
    public class SpecificLevelGenerator : ILevelGenerator
    {
        public LevelGraph Generate()
        {
            var graph = new LevelGraph();

            var entranceNode =
                graph.CreateNode<EntranceNode>(
                    "Entrance 0", null);

            var fw1 =
                graph.CreateNode<FirewallNode>(
                    "fw1", entranceNode);

            var c1 =
                graph.CreateNode<ConnectorNode>(
                    "c1", fw1);

            var c11 =
                graph.CreateNode<ConnectorNode>(
                    "c11", fw1);

            graph.CreateNode<FilesystemNode>(
                "system1", c11);
            
            graph.CreateNode<FilesystemNode>(
                "system2", c11);

            var c12 =
                graph.CreateNode<ConnectorNode>(
                    "c12", fw1);

            var fw2c1 =
                graph.CreateNode<FirewallNode>(
                    "fw2c1", c1);

            var fw2c =
                graph.CreateNode<ConnectorNode>(
                    "fwc1", fw2c1);

            graph.CreateNode<FilesystemNode>(
                "fs4", fw2c);

            var fwc1 =
                graph.CreateNode<FirewallNode>(
                    "fwc1", c1);

            graph.CreateNode<FilesystemNode>(
                "fs1", fwc1);

            graph.CreateNode<FilesystemNode>(
                "fs2", fwc1);

            graph.CreateNode<FilesystemNode>(
                "fs3", fwc1);

            graph.CreateNode<ConnectorNode>(
                "c2", fw1);

            graph.CreateNode<ConnectorNode>(
                "c3", fw1);

            return graph;
        
        }
    }
}