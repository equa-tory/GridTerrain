using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject frontWall;
    public GameObject backWall;

    public void UpdateVisibility(bool hasLeftNeighbor, bool hasRightNeighbor, bool hasFrontNeighbor, bool hasBackNeighbor)
    {
        leftWall.SetActive(!hasLeftNeighbor);
        rightWall.SetActive(!hasRightNeighbor);
        frontWall.SetActive(!hasFrontNeighbor);
        backWall.SetActive(!hasBackNeighbor);
    }
}
