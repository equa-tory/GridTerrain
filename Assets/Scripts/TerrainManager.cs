using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private Vector2 size = new Vector2(10, 10);
    [SerializeField] private TerrainTile tile;
    [SerializeField] private Grid grid;
    [HideInInspector] public List<TerrainTile> tiles = new List<TerrainTile>();


    private void Start() {
        
        for(int x = 0; x < size.x; x++) {
            for(int y = 0; y < size.y; y++) {

                Vector3Int pos = grid.WorldToCell(new Vector3(x, 0, y));
                var t = Instantiate(tile, grid.CellToWorld(pos), Quaternion.identity, grid.transform);
                t.tm = this;
                tiles.Add(t);
            }
        }

    }
}
