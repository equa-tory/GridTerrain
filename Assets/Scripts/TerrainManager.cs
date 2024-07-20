using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab;
    private Dictionary<Vector3Int, GameObject> tiles = new Dictionary<Vector3Int, GameObject>();
    public Camera cam;
    public Grid grid;

    void Start()
    {
        // Пример начальной генерации карты
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                AddTile(new Vector3Int(x, 0, y));
            }
        }
    }

    public void AddTile(Vector3Int position)
    {
        if (!tiles.ContainsKey(position))
        {
            GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity);
            tiles.Add(position, newTile);
            UpdateTileVisibility(position);
        }
    }

    public void RemoveTile(Vector3Int position)
    {
        if (tiles.ContainsKey(position))
        {
            Destroy(tiles[position]);
            tiles.Remove(position);
            UpdateAdjacentTilesVisibility(position);
        }
    }

    private void UpdateTileVisibility(Vector3Int position)
    {
        GameObject tile;
        if (tiles.TryGetValue(position, out tile))
        {
            TerrainTile tileScript = tile.GetComponent<TerrainTile>();
            tileScript.UpdateVisibility(
                tiles.ContainsKey(position + Vector3Int.left),
                tiles.ContainsKey(position + Vector3Int.right),
                tiles.ContainsKey(position + new Vector3Int(0, 0, 1)),
                tiles.ContainsKey(position + new Vector3Int(0, 0, -1))
            );
        }
    }

    private void UpdateAdjacentTilesVisibility(Vector3Int position)
    {
        UpdateTileVisibility(position + Vector3Int.left);
        UpdateTileVisibility(position + Vector3Int.right);
        UpdateTileVisibility(position + new Vector3Int(0, 0, 1));
        UpdateTileVisibility(position + new Vector3Int(0, 0, -1));
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Vector3 pos = GetMousePos();
            Vector3Int gridPos = grid.WorldToCell(pos);

            AddTile(gridPos);
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)){
            Vector3 pos = GetMousePos();
            Vector3Int gridPos = grid.WorldToCell(pos);

            RemoveTile(gridPos);
        }
    }

    private Vector3 GetMousePos(){
        Vector3 pos;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;
        Ray ray = cam.ScreenPointToRay(mousePos);
        if(Physics.Raycast(ray, out RaycastHit hit, 100)) pos = hit.point;
        else pos = Vector3.zero;
        return pos;
    }
}
