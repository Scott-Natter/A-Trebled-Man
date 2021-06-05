using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles all vision colliders, including cover, and their interactions with GridTiles and Grunts
public class MaestroVision : MonoBehaviour
{
    private List<Collider> colliders;
    private List<GridTile> tiles;

    private void Start()
    {
        colliders = new List<Collider>();
        tiles = new List<GridTile>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Enemy layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && other.gameObject.tag == "Grunt") {
            colliders.Add(other);
            SetLayerRecursively(other.gameObject, LayerMask.NameToLayer("Maestro"));
        }

        if (other.gameObject.tag == "GridTile")
        {
            tiles.Add(other.GetComponent<GridTile>());
            other.GetComponent<GridTile>().inVision = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Maestro") && other.gameObject.tag == "Grunt") {
            colliders.Remove(other);
            SetLayerRecursively(other.gameObject, LayerMask.NameToLayer("Enemy"));
        }

        if (other.gameObject.tag == "GridTile")
        {
            tiles.Remove(other.GetComponent<GridTile>());
            other.GetComponent<GridTile>().inVision = false;
            other.GetComponent<GridTile>().inEnemyVision = false;
        }
    }

    void OnDisable() {

        foreach (Collider grunt in colliders) {
            SetLayerRecursively(grunt.gameObject, LayerMask.NameToLayer("Enemy"));
        }

        foreach (GridTile tile in tiles)
        {
            tile.inVision = false;
            tile.inEnemyVision = false;
        }

        colliders.Clear();
        tiles.Clear();
    }

    void SetLayerRecursively(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;

        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.layer != 9)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
           
        }
    }
}
