using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPositionCalculate : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;

    public void CalculatePosition(Vector3 collisionPoint)
    {
        collisionPoint += (tilemap.cellSize * 0.5f);
        var cellPos = tilemap.WorldToCell(collisionPoint);
        var cell = tilemap.GetTile(cellPos);
        
        Debug.Log("Tilemap cell position : " + cellPos);
        Debug.Log(tilemap.HasTile(cellPos));

        var newPos = cellPos + Vector3Int.up;

        if (tilemap.HasTile(cellPos))
        {
            /*tilemap.SetTile(newPos, cell);
            tilemap.SetTile(cellPos, null);*/

            Vector3 itemPos = cellPos;
            //itemPos.x += 2;
            itemPos.y += 2;
            itemPos.x += (tilemap.cellSize * 0.5f).x; //매직넘버 전환 등 수정예정

            ItemManager.Instance.ShowItem(itemPos);
        }
    }
}
