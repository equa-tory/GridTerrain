using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainTile : MonoBehaviour
{
    [HideInInspector] public TerrainManager tm;
    [SerializeField] private GameObject topFace;
    [SerializeField] private GameObject backFace;
    [SerializeField] private GameObject frontFace;
    [SerializeField] private GameObject leftFace;
    [SerializeField] private GameObject rightFace;

    private void Start() {
        foreach(var t in tm.tiles){
            if(t.transform.position.x == transform.position.x + 1) rightFace.SetActive(false);
            if(t.transform.position.x == transform.position.x - 1) leftFace.SetActive(false);
            if(t.transform.position.z == transform.position.z + 1) frontFace.SetActive(false);
            if(t.transform.position.z == transform.position.z - 1) backFace.SetActive(false);
            if(t.transform.position.y == transform.position.y + 1) topFace.SetActive(false);

        }
    }
}
