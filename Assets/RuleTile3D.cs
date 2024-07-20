using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New 3D Rule Tile", menuName = "Tiles/3D Rule Tile")]
public class RuleTile3D : TileBase
{
    public GameObject prefab;

    private static readonly Vector3Int[] neighborOffsets = new Vector3Int[]
    {
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(0, 0, 1),
        new Vector3Int(0, 0, -1)
    };

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);

        Tilemap localTilemap = tilemap.GetComponent<Tilemap>();
        if (localTilemap != null)
        {
            foreach (Vector3Int offset in neighborOffsets)
            {
                Vector3Int neighborPosition = position + offset;
                localTilemap.RefreshTile(neighborPosition);
            }

            Vector3 tileWorldPosition = localTilemap.CellToWorld(position);
            InstantiatePrefab(tileWorldPosition, localTilemap, position);
        }
    }

    private void InstantiatePrefab(Vector3 position, Tilemap tilemap, Vector3Int tilePosition)
    {
        GameObject existingObject = FindExistingTile(tilePosition, tilemap);
        if (existingObject == null)
        {
            GameObject go = Instantiate(prefab, position, Quaternion.identity);
            go.transform.SetParent(tilemap.transform);
            go.name = $"Tile_{tilePosition.x}_{tilePosition.y}_{tilePosition.z}";
            AdjustSides(go, tilemap, tilePosition);
        }
        else
        {
            AdjustSides(existingObject, tilemap, tilePosition);
        }
    }

    private GameObject FindExistingTile(Vector3Int position, Tilemap tilemap)
    {
        string tileName = $"Tile_{position.x}_{position.y}_{position.z}";
        Transform existingTransform = tilemap.transform.Find(tileName);
        return existingTransform != null ? existingTransform.gameObject : null;
    }

    private void AdjustSides(GameObject tileObject, Tilemap tilemap, Vector3Int position)
    {
        foreach (Transform side in tileObject.transform)
        {
            side.gameObject.SetActive(true);
        }

        foreach (Vector3Int offset in neighborOffsets)
        {
            Vector3Int neighborPosition = position + offset;
            if (tilemap.GetTile(neighborPosition) != null)
            {
                string sideName = GetSideName(offset);
                Transform sideTransform = tileObject.transform.Find(sideName);
                if (sideTransform != null)
                {
                    sideTransform.gameObject.SetActive(false);
                }
            }
        }
    }

    private string GetSideName(Vector3Int offset)
    {
        if (offset == new Vector3Int(1, 0, 0)) return "Right";
        if (offset == new Vector3Int(-1, 0, 0)) return "Left";
        if (offset == new Vector3Int(0, 1, 0)) return "Top";
        if (offset == new Vector3Int(0, -1, 0)) return "Bottom";
        if (offset == new Vector3Int(0, 0, 1)) return "Front";
        if (offset == new Vector3Int(0, 0, -1)) return "Back";
        return string.Empty;
    }
}
