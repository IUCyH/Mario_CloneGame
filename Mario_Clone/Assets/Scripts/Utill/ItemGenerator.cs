using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemGenerator : SingletonMonoBehaviour<ItemGenerator>
{
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    GridLayout grid;
    [SerializeField]
    List<Vector3Int> cellGeneratedItemAlready = new List<Vector3Int>();
    Vector3 newItemPos = Vector3.zero;

    public bool CalculatePositionAndCheckItHasTile(Vector3 collisionPoint)
    {
        //collisionPoint += grid.cellSize * 0.5f;
        Debug.Log("collision Point : " + collisionPoint);
        var cellPos = grid.WorldToCell(collisionPoint);
        var debugCellPos = cellPos;
        debugCellPos.x += (int)(grid.cellSize * 0.5f).x;
        debugCellPos.y += (int)(grid.cellSize * 0.5f).y;
        Debug.Log("cell Position : " + debugCellPos);

        bool hasTile = tilemap.HasTile(debugCellPos);
        //var cell = tilemap.GetTile(cellPos);
        
        //Debug.Log(cellPos);
        //if (!hasTile) cellPos.x--;*/
        
        if (tilemap.HasTile(cellPos) && !cellGeneratedItemAlready.Contains(cellPos))
        {
            newItemPos = cellPos;
            newItemPos.x += (grid.cellSize * 0.5f).x; //매직넘버 전환 등 수정예정
            
            cellGeneratedItemAlready.Add(cellPos);
            return true;
        }

        return false;
    }

    public void GenerateItem()
    {
        ItemManager.Instance.ShowItem(newItemPos);
    }
}
