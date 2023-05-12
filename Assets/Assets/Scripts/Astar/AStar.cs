using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Astar
{
    private static Dictionary<Point, Node> nodes;
    private static TileScript tileScript;

    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();

        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }
    //public static Stack<Node> GetPath(Point start, Point goal)
    //{
    //    if(nodes == null)
    //    {
    //        CreateNodes();
    //    }

    //    HashSet<Node> openList = new HashSet<Node>();

    //    HashSet<Node> closedList = new HashSet<Node>();

    //    Stack<Node> finalPath = new Stack<Node>();

    //    Node currentNode = nodes[start];

    //    openList.Add(currentNode);

    //    while (openList.Count > 0)
    //    {
    //        for (int x = -1; x <= 1; x++)
    //        {
    //            for (int y = -1; y <= 1; y++)
    //            {
    //                Point neighbourPos = new Point(currentNode.GridPosition.x - x, currentNode.GridPosition.y - y);

    //                if (LevelManager.Instance.InBounds(neighbourPos) && LevelManager.Instance.Tiles[neighbourPos].WalkAble && neighbourPos != currentNode.GridPosition)
    //                {
    //                    int gCost = 0;
    //                    if (Math.Abs(x - y) == 1)
    //                    {
    //                        gCost = 10;
    //                    }
    //                    else
    //                    {
    //                        if (!ConnectedDiagonally(currentNode, nodes[neighbourPos]))
    //                        {
    //                            continue;
    //                        }
    //                        gCost = 14;
    //                    }

    //                    Node neighbour = nodes[neighbourPos];

    //                    if (openList.Contains(neighbour))
    //                    {
    //                        if (currentNode.G + gCost < neighbour.G)
    //                        {
    //                            neighbour.CalcValues(currentNode, nodes[goal], gCost);
    //                        }
    //                    }
    //                    else if (!closedList.Contains(neighbour))
    //                    {
    //                        openList.Add(neighbour);
    //                        neighbour.CalcValues(currentNode, nodes[goal], gCost);
    //                    }
    //                }
    //            }
    //        }

    //        openList.Remove(currentNode);
    //        closedList.Add(currentNode);

    //        if (openList.Count > 0)
    //        {
    //            currentNode = openList.OrderBy(n => n.F).First();
    //        }
    //        if(currentNode == nodes[goal])
    //        {
    //            while(currentNode.GridPosition != start)
    //            {
    //                finalPath.Push(currentNode);
    //                currentNode = currentNode.Parent;
    //            }
    //            break;
    //        }
    //    }

    //    return finalPath;

    //    //solo para el debugging hay q remover dps
    //    //GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList, closedList, finalPath);
    //}
    public static Stack<Node> GetFixedPath()
    {
        if (nodes == null)
        {
            CreateNodes();
        }

        List<Point> fixedPath = new List<Point>()
        {
            new Point(18, 2),
            new Point(18, 3),
            new Point(18, 4),
            new Point(18, 5),
            new Point(18, 6),
            new Point(18, 7),
            new Point(18, 8),
            new Point(18, 9),
            new Point(18, 11),
            new Point(18, 12),
            new Point(17, 12),
            new Point(16, 12),
            new Point(15, 12),
            new Point(14, 12),
            new Point(13, 12),
            new Point(12, 12),
            new Point(11, 12),
            new Point(11, 11),
            new Point(11, 10),
            new Point(11, 9),
            new Point(11, 8),
            new Point(11, 7),
            new Point(11, 6),
            new Point(12, 6),
            new Point(13, 6),
            new Point(14, 6),
            new Point(14, 5),
            new Point(14, 4),
            new Point(14, 3),
            new Point(13, 3),
            new Point(12, 3),
            new Point(11, 3),
            new Point(10, 3),
            new Point(9, 3),
            new Point(8, 3),
            new Point(7, 3),
            new Point(7, 4),
            new Point(7, 5),
            new Point(7, 6),
            new Point(7, 7),
            new Point(7, 8),
            new Point(7, 9),
            new Point(7, 10),
            new Point(7, 11),
            new Point(7, 12),
            new Point(6, 12),
            new Point(5, 12),
            new Point(4, 12),
            new Point(3, 12),
            new Point(2, 12),
            new Point(2, 11),
            new Point(2, 10),
            new Point(2, 9),
            new Point(2, 8),
            new Point(2, 7),
            new Point(2, 6),
            new Point(2, 5),
            new Point(2, 4),
            new Point(2, 3),
            new Point(2, 2),
        };

        Stack<Node> finalPath = new Stack<Node>();

        foreach (Point point in fixedPath)
        {
            Node node = nodes[point];
            TileScript tile = LevelManager.Instance.Tiles[point];
            tile.IsEmpty = false;

            finalPath.Push(node);
            //finalPath.Push(nodes[point]);
        }

        return finalPath;
    }
    private static bool ConnectedDiagonally(Node currentNode, Node neighbor)
    {
        Point direction = neighbor.GridPosition - currentNode.GridPosition;

        Point first = new Point(currentNode.GridPosition.x + direction.x, currentNode.GridPosition.y);

        Point second = new Point(currentNode.GridPosition.x, currentNode.GridPosition.y + direction.y);

        if(LevelManager.Instance.InBounds(first) && !LevelManager.Instance.Tiles[first].WalkAble)
        {
            return false;
        }
        if(LevelManager.Instance.InBounds(second) && !LevelManager.Instance.Tiles[second].WalkAble)
        {
            return false;
        }
        return true;
    }
}