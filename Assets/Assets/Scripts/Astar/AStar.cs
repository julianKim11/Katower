using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public static Stack<Node> GetFixedPath()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        CreateNodes();
        //if (nodes == null)
        //{
            
        //}

        List<Point> fixedPath = new List<Point>();

        switch (sceneNumber)
        {
            case 1:
                fixedPath.AddRange(new[]
                {
                    new Point(18, 6),
                    new Point(17, 6),
                    new Point(16, 6),
                    new Point(15, 6),
                    new Point(14, 6),
                    new Point(13, 6),
                    new Point(12, 6),
                    new Point(11, 6),
                    new Point(10, 6),
                    new Point(9, 6),
                    new Point(9, 5),
                    new Point(8, 5),
                    new Point(7, 5),
                    new Point(6, 5),
                    new Point(5, 5),
                    new Point(4, 5),
                    new Point(3, 5),
                    new Point(2, 5),
                });
                break;
            case 2:
                fixedPath.AddRange(new[]
                {
                    new Point(17, 17),
                    new Point(16, 17),
                    new Point(15, 17),
                    new Point(14, 17),
                    new Point(13, 17),
                    new Point(12, 17),
                    new Point(11, 17),
                    new Point(10, 17),
                    new Point(9, 17),
                    new Point(8, 17),
                    new Point(7, 17),
                    new Point(6, 17),
                    new Point(5, 17),
                    new Point(4, 17),
                    new Point(3, 17),
                    new Point(2, 17),
                    new Point(2, 16),
                    new Point(2, 15),
                    new Point(2, 14),
                    new Point(2, 13),
                    new Point(3, 13),
                    new Point(4, 13),
                    new Point(5, 13),
                    new Point(6, 13),
                    new Point(7, 13),
                    new Point(8, 13),
                    new Point(9, 13),
                    new Point(10, 13),
                    new Point(11, 13),
                    new Point(12, 13),
                    new Point(13, 13),
                    new Point(14, 13),
                    new Point(15, 13),
                    new Point(16, 13),
                    new Point(17, 13),
                    new Point(17, 12),
                    new Point(17, 11),
                    new Point(17, 10),
                    new Point(17, 9),
                    new Point(16, 9),
                    new Point(15, 9),
                    new Point(14, 9),
                    new Point(13, 9),
                    new Point(12, 9),
                    new Point(11, 9),
                    new Point(10, 9),
                    new Point(9, 9),
                    new Point(8, 9),
                    new Point(7, 9),
                    new Point(6, 9),
                    new Point(5, 9),
                    new Point(4, 9),
                    new Point(3, 9),
                    new Point(2, 9),
                    new Point(2, 8),
                    new Point(2, 7),
                    new Point(2, 6),
                    new Point(2, 5),
                    new Point(3, 5),
                    new Point(4, 5),
                    new Point(5, 5),
                    new Point(6, 5),
                    new Point(7, 5),
                    new Point(8, 5),
                    new Point(9, 5),
                    new Point(10, 5),
                    new Point(11, 5),
                    new Point(12, 5),
                    new Point(13, 5),
                    new Point(14, 5),
                    new Point(15, 5),
                    new Point(16, 5),
                    new Point(17, 5),
                    new Point(17, 4),
                    new Point(17, 3),
                    new Point(17, 2),
                    new Point(17, 1),
                    new Point(16, 1),
                    new Point(15, 1),
                    new Point(14, 1),
                    new Point(13, 1),
                    new Point(12, 1),
                    new Point(11, 1),
                    new Point(10, 1),
                    new Point(9, 1),
                    new Point(8, 1),
                    new Point(7, 1),
                    new Point(6, 1),
                    new Point(5, 1),
                    new Point(4, 1),
                    new Point(3, 1),
                    new Point(2, 1),
                });
                break;
            case 3:
                fixedPath.AddRange(new[] {
                    new Point(18, 14),
                    new Point(18, 13),
                    new Point(18, 12),
                    new Point(18, 11),
                    new Point(17, 11),
                    new Point(16, 11),
                    new Point(15, 11),
                    new Point(15, 12),
                    new Point(15, 13),
                    new Point(15, 14),
                    new Point(14, 14),
                    new Point(13, 14),
                    new Point(12, 14),
                    new Point(11, 14),
                    new Point(10, 14),
                    new Point(10, 13),
                    new Point(10, 12),
                    new Point(10, 11),
                    new Point(9, 11),
                    new Point(8, 11),
                    new Point(7, 11),
                    new Point(6, 11),
                    new Point(6, 12),
                    new Point(6, 13),
                    new Point(6, 14),
                    new Point(5, 14),
                    new Point(4, 14),
                    new Point(3, 14),
                    new Point(2, 14),
                    new Point(2, 13),
                    new Point(2, 12),
                    new Point(2, 11),
                    new Point(2, 10),
                    new Point(2, 9),
                    new Point(3, 9),
                    new Point(4, 9),
                    new Point(5, 9),
                    new Point(6, 9),
                    new Point(7, 9),
                    new Point(8, 9),
                    new Point(9, 9),
                    new Point(10, 9),
                    new Point(11, 9),
                    new Point(12, 9),
                    new Point(13, 9),
                    new Point(14, 9),
                    new Point(15, 9),
                    new Point(16, 9),
                    new Point(17, 9),
                    new Point(17, 8),
                    new Point(17, 7),
                    new Point(17, 6),
                    new Point(17, 5),
                    new Point(17, 4),
                    new Point(17, 3),
                    new Point(17, 2),
                    new Point(17, 1),
                    new Point(16, 1),
                    new Point(15, 1),
                    new Point(14, 1),
                    new Point(13, 1),
                    new Point(12, 1),
                    new Point(11, 1),
                    new Point(10, 1),
                    new Point(9, 1),
                    new Point(8, 1),
                    new Point(7, 1),
                    new Point(6, 1),
                    new Point(5, 1),
                    new Point(4, 1),
                    new Point(3, 1),
                    new Point(2, 1),
                    new Point(2, 2),
                    new Point(2, 3),
                    new Point(3, 3),
                    new Point(4, 3),
                    new Point(5, 3),
                    new Point(6, 3),
                    new Point(7, 3),
                    new Point(8, 3),
                    new Point(9, 3),
                    new Point(10, 3),
                    new Point(11, 3),
                    new Point(12, 3),
                    new Point(13, 3),
                    new Point(14, 3),
                    new Point(14, 4),
                    new Point(14, 5),
                    new Point(14, 6),
                    new Point(13, 6),
                    new Point(12, 6),
                    new Point(11, 6),
                    new Point(10, 6),
                    new Point(9, 6),
                    new Point(8, 6),
                    new Point(7, 6),
                    new Point(6, 6),
                    new Point(5, 6),
                    new Point(4, 6),
                    new Point(3, 6),
                    new Point(2, 6)
                });
                break;
            case 4:
                fixedPath.AddRange(new[]
                {
                    new Point(19, 4),
                    new Point(18, 4),
                    new Point(17, 4),
                    new Point(16, 4),
                    new Point(15, 4),
                    new Point(14, 4),
                    new Point(13, 4),
                    new Point(12, 4),
                    new Point(11, 4),
                    new Point(10, 4),
                    new Point(9, 4),
                    new Point(8, 4),
                    new Point(7, 4),
                    new Point(6, 4),
                    new Point(5, 4),
                    new Point(4, 4),
                    new Point(3, 4),
                    new Point(2, 4),
                    new Point(1, 4),
                });
                break;
        }

        Stack<Node> finalPath = new Stack<Node>();

        foreach (Point point in fixedPath)
        {
            Node node = nodes[point];
            TileScript tile = LevelManager.Instance.Tiles[point];
            tile.IsEmpty = false;

            finalPath.Push(node);
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