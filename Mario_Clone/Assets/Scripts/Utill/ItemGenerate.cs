using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemGenerate : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    List<Vector3Int> cellGeneratedItemAlready = new List<Vector3Int>();
    Vector3 newItemPos = Vector3.zero;

    public bool CalculatePositionAndCheckItHasTile(Vector3 collisionPoint)
    {
        collisionPoint += tilemap.cellSize * 0.5f;
        var cellPos = tilemap.WorldToCell(collisionPoint);
        var cell = tilemap.GetTile(cellPos);
        
        if (tilemap.HasTile(cellPos) && !cellGeneratedItemAlready.Contains(cellPos))
        {
            newItemPos = cellPos;
            newItemPos.y += 2f;
            newItemPos.x += (tilemap.cellSize * 0.5f).x; //매직넘버 전환 등 수정예정
            
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
