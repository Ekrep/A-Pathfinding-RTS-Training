using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public static PathFinding Instance;
    private void Awake()
    {
        Instance = this;
    }


    public List<Tile> FindPath(Tile startTile, Tile endTile)
    {
        Tile start = startTile;//baslangic Tile'i
        Tile end = endTile;//hedef Tile

        List<Tile> openList = new List<Tile>();//gidilebilecek tile'i listeye aktarma
        List<Tile> closedList = new List<Tile>();//kullanilmis tile'i listeye aktarma

        openList.Add(start);
        while (openList.Count > 0)//kullanilabilir tile sayisi bitene kadar
        {
            Tile currentTile = openList[0];//mevcut tile ilk tile

            for (int i = 0; i < openList.Count; i++)//kullanilabilir tile sayisi kadar bir sonraki tile value'si hesaplayip bir adim sonraki tile'i bulma
            {
                if (currentTile.fValue > openList[i].fValue)
                {
                    currentTile = openList[i];
                }
                if (currentTile.fValue == openList[i].fValue && currentTile.hValue > openList[i].hValue)
                {
                    currentTile = openList[i];
                }
            }
            openList.Remove(currentTile);
            closedList.Add(currentTile);
            if (currentTile == end)//mevcut tile hedef tile ise path'i olusturma
            {
                
                    return TracePath(start, end);
               
            }
                

              
            for (int i = 0; i < currentTile.neighbourTiles.Count; i++)//mevcut tile'in komsu tile sayisi kadar
            {
                
                if (closedList.Contains(currentTile.neighbourTiles[i]))//kullanilmissa atla
                {
                    continue;
                   
                }
                if (currentTile.neighbourTiles[i].isBlocked)//blokluysa atla
                {
                    continue;
                }
                int movementCost = currentTile.gValue + CalculateDistance(currentTile, currentTile.neighbourTiles[i]);//hedef tile'a gidis maliyeti
               
                if (openList.Contains(currentTile.neighbourTiles[i]) == false || movementCost < currentTile.neighbourTiles[i].gValue)
                {
                    currentTile.neighbourTiles[i].gValue = movementCost;
                    currentTile.neighbourTiles[i].hValue = CalculateDistance(currentTile.neighbourTiles[i], end);
                    currentTile.neighbourTiles[i].parentNode = currentTile;

                    if (openList.Contains(currentTile.neighbourTiles[i]) == false)
                    {
                        openList.Add(currentTile.neighbourTiles[i]);
                    }
                }
            }




            
            
        }
        return null;//gidilemiyorsa null cevir

    }

    private int CalculateDistance(Tile current, Tile target)//mevcut ve hedef tile arasýndaki mesafeyi ve cost'u hesaplama
    {
        int distanceX = Mathf.Abs(current.positionOnX - target.positionOnX);
        int distaceY = Mathf.Abs(current.positionOnY - target.positionOnY);

        if (distanceX > distaceY)
        {
            return 14 * distaceY + 10 * (distanceX - distaceY);
        }
        return 14 * distanceX + 10 * (distaceY - distanceX);
    }
    private List<Tile> TracePath(Tile startNode, Tile endNode)//gidilebiliyorsa path'i olusturup reverse ile sondan basa cevirme
    {
        List<Tile> path = new List<Tile>();
        Tile currentNode = endNode;
        

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;

        }
        path.Reverse();
        return path;
    }

  


}
