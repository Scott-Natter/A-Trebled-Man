using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public GridTile parent;
    public int distance;
    public bool visited;
    public bool walkable;
    public bool current;
    public bool target;
    public bool selectable;
    public List<GridTile> adjacentTiles;

    private Material defaultMaterial;
    private Material visionMaterial;
    private Material enemyVisionMaterial;
    private Material maestroMoveMaterial;
    private Material gruntMoveMaterial;

    public LayerMask layerMask;

    public bool inVision = false;
    public bool inEnemyVision = false;
    public bool maestroMoving = false;
    public bool gruntMoving = false;

    public enum TileMaterial { Default, MaestroVision, EnemyVision, MaestroMove, GruntMove };

    void Start()
    {
        defaultMaterial = gameObject.GetComponent<Renderer>().material;
        visionMaterial = Resources.Load<Material>("Green Floor");
        enemyVisionMaterial = Resources.Load<Material>("Red Tile");
        gruntMoveMaterial = Resources.Load<Material>("Move Red");
        maestroMoveMaterial = Resources.Load<Material>("Move Green");

        adjacentTiles = new List<GridTile>();
        Reset();
    }

    // Sets tile to default state
    public void Reset()
    {
        parent = null;
        distance = 0;
        visited = false;
        walkable = true;
        current = false;
        target = false;
        selectable = false;
        adjacentTiles.Clear();
    }

    public void Update()
    {
        if (maestroMoving)
        {
            SetColor(TileMaterial.MaestroMove);
        }
        else if (gruntMoving)
        {
            SetColor(TileMaterial.GruntMove);
        }
        else if (inEnemyVision)
        {
            SetColor(TileMaterial.EnemyVision);
        } else if (inVision)
        {
            SetColor(TileMaterial.MaestroVision);
        } else
        {
            SetColor(TileMaterial.Default);
        }
    }

    // determine adjacent tiles in each cardinal direction to the current tile
    public void GetAdjacentTiles(int jumpHeight)
    {
        Reset();
        CheckTile(Vector3.forward, jumpHeight);
        CheckTile(Vector3.back, jumpHeight);
        CheckTile(Vector3.left, jumpHeight);
        CheckTile(Vector3.right, jumpHeight);
    }

    // determine if an adjacent tile exists in the given direction given the provided jump height
    private void CheckTile(Vector3 direction, int jumpHeight)
    {
        // for each object that collides with the given box
        // (create a box with origin at the center of the supposed tile adjacent to the current tile's position in the direction specified.
        // box dimensions should not exceed the dimensions of a given tile, and should account for the jump height provided

        foreach (Collider collider in Physics.OverlapBox(transform.position + direction, new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f)))
        {
            if (collider.gameObject.tag == "GridTile")
            {
                // get the grid tile
                GridTile gridTile = collider.GetComponent<GridTile>();
                // if one was found, it is walkable, and the tile is unoccupied, add it to the adjacent tiles
                // layermask is needed because vision cone will register as occupying tiles above unless it is masked out
                if (gridTile != null && gridTile.walkable && (!Physics.Raycast(gridTile.transform.position, Vector3.up, out RaycastHit hit, 1, layerMask)))
                {
                    adjacentTiles.Add(gridTile);
                }
            }
        }
    }

    // Reference Update for use
    // Allow other classes to change color via static enum call
    public void SetColor(TileMaterial tileMaterial)
    {
        Material newTileMat = null;

        switch (tileMaterial)
        {
            case TileMaterial.EnemyVision:
                newTileMat = enemyVisionMaterial;
                break;
            case TileMaterial.MaestroVision:
                newTileMat = visionMaterial;
                break;
            case TileMaterial.MaestroMove:
                newTileMat = maestroMoveMaterial;
                break;
            case TileMaterial.GruntMove:
                newTileMat = gruntMoveMaterial;
                break;
            case TileMaterial.Default:
                newTileMat = defaultMaterial;
                break;
        }

        gameObject.GetComponent<Renderer>().material = newTileMat;
    }
}
