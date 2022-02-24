using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum NeighboorFlags
{
    None = 0,
    West = 1,
    North = 2,
    Est = 4,
    South = 8
}

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject floor;
    [SerializeField] float tileSize = 4.0f;
    [SerializeField] LayerMask autoTileLayerMask;
    [SerializeField] List<Mesh> wallMeshes;

    public bool isWall = false;

    private Dictionary<NeighboorFlags, int> bitMaskTable = new Dictionary<NeighboorFlags, int>() {
        { NeighboorFlags.None, 0 },

        { NeighboorFlags.West, 1 },
        { NeighboorFlags.North, 2 },
        { NeighboorFlags.Est, 3 },
        { NeighboorFlags.South, 4 },

        { NeighboorFlags.West | NeighboorFlags.Est, 5},
        { NeighboorFlags.North | NeighboorFlags.South , 6 },

        { NeighboorFlags.West | NeighboorFlags.North , 7 },
        { NeighboorFlags.North | NeighboorFlags.Est , 8 },
        { NeighboorFlags.Est | NeighboorFlags.South , 9 },
        { NeighboorFlags.South | NeighboorFlags.West , 10 },

        { NeighboorFlags.West | NeighboorFlags.North | NeighboorFlags.Est, 11 },
        { NeighboorFlags.North | NeighboorFlags.Est | NeighboorFlags.South, 12 },
        { NeighboorFlags.Est | NeighboorFlags.South | NeighboorFlags.West, 13 },
        { NeighboorFlags.South | NeighboorFlags.West | NeighboorFlags.North, 14 },

        { NeighboorFlags.West | NeighboorFlags.North | NeighboorFlags.Est | NeighboorFlags.South , 15 },
    };

    private void Start()
    {
        Autotile();
    }

    public void Autotile()
    {
        if (isWall)
        {
            NeighboorFlags bitMask = CalculateBitMask();
            if (bitMaskTable.ContainsKey(bitMask))
            {
                setWall(bitMaskTable[bitMask]);
                return;
            }
        }

        setWall();
    }

    private NeighboorFlags CalculateBitMask()
    {
        NeighboorFlags bitMask = NeighboorFlags.None;

        if (CheckNeighboor(Vector3.forward)) bitMask |= NeighboorFlags.North;
        if (CheckNeighboor(Vector3.right)) bitMask |= NeighboorFlags.Est;
        if (CheckNeighboor(Vector3.back)) bitMask |= NeighboorFlags.South;
        if (CheckNeighboor(Vector3.left)) bitMask |= NeighboorFlags.West;

        return bitMask;
    }

    private bool CheckNeighboor(Vector3 direction)
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + direction * tileSize - new Vector3(0, 0.5f, 0), new Vector3(1, 0.1f, 1), Quaternion.identity, autoTileLayerMask);
        if (hitColliders.Length == 1)
        {
            Tile tile = hitColliders[0].gameObject.GetComponentInParent<Tile>();
            return tile.isWall;
        }
        return false;
    }

    private void setWall(int wallID = -1)
    {
        if (wallID < 0 || wallID >= wallMeshes.Count)
        {
            wall.GetComponent<MeshFilter>().mesh = null;
            wall.GetComponent<MeshCollider>().sharedMesh = null;
        }
        else
        {
            wall.GetComponent<MeshFilter>().mesh = wallMeshes[wallID];
            wall.GetComponent<MeshCollider>().sharedMesh = wallMeshes[wallID];
        }   
    }
}
