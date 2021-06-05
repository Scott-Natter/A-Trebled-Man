using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour
{

    public int jumpHeight;
    public int moveDistance;
    public float moveSpeed;

    protected bool moving = false;
    public GridTile currentGridTile;
    public GridTile nextTile;

    private GameObject[] gridTiles;
    private List<GridTile> selectableTiles = new List<GridTile>();
    public Stack<GridTile> path = new Stack<GridTile>();
    private Vector3 velocity;
    private Vector3 heading;
    private Vector3 smoothDamp = Vector3.zero;

    public Animator anime;

    public float t;

    public LayerMask layerMask;

    private void Start()
    {
        anime = GetComponent<Animator>();
    }

    public void FindSelectableGridTiles()
    {
        FindAdjacentGridTiles();
        GetCurrentGridTile();
        if (currentGridTile != null) {
            Queue<GridTile> process = new Queue<GridTile>();
            process.Enqueue(currentGridTile);
            currentGridTile.visited = true;

            while (process.Count > 0)
            {
                GridTile t = process.Dequeue();
                selectableTiles.Add(t);
                t.selectable = true;
                if (t.distance < moveDistance)
                {
                    foreach (GridTile gridTile in t.adjacentTiles)
                    {
                        if (!gridTile.visited)
                        {
                            gridTile.parent = t;
                            gridTile.visited = true;
                            gridTile.distance = t.distance + 1;
                            process.Enqueue(gridTile);
                        }
                    }
                }
            }
        }
    }

    // build path to the specified tile via Breadth First Search
    public void MoveToTile(GridTile gridTile)
    {
        if (gridTile == null) { return; }
        // clear any existing path before navigating
        
        path.Clear();
        gridTile.target = true;
        moving = true;

        // starting at the provided tile, while the tile has another parent, add that tile to the path
        for (GridTile nextGridTile = gridTile; nextGridTile != null; nextGridTile = nextGridTile.parent)
        {
            
            path.Push(nextGridTile);
        }
    }

    // navigate to the specified tile, following the path of tiles
    public virtual void Move()
    {
        // if there is still a tile in the path, navigate
        if (path.Count > 0)
        {
            anime.Play("walk");
            
            // get the tile at the top of the stack (the next one in the path)
            Vector3 targetPosition = nextTile.transform.GetChild(0).transform.position;
            targetPosition.y = transform.position.y;

            if (GetComponent<PlayerMove>() != null)
            {
                print("Player moving!");
                nextTile.maestroMoving = true;
            }
            if (GetComponent<EnemyMove>() != null)
            {
                //print("Enemy moving!");
                nextTile.gruntMoving = true;
            }

            if(transform.position != new Vector3(nextTile.transform.position.x, transform.position.y, nextTile.transform.position.z))
            {
                SetHeading(nextTile);
            }
            

            // set velocity
            // velocity = heading * moveSpeed;
            // transform.position += velocity * Time.deltaTime;
            // transform.position = target

            t += Time.deltaTime / moveSpeed;
            transform.position = Vector3.Lerp(transform.position, targetPosition, t);

            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothDamp, moveSpeed);

            // remove the tile from the path
            if (GetComponent<PlayerMove>() != null)
            {
                print("Player done!");
                nextTile.maestroMoving = false;
            }
            if (GetComponent<EnemyMove>() != null)
            {
                //print("Enemy done!");
                nextTile.gruntMoving = false;
            }
        }
        // if there are no more tiles in the path, stop moving
        else
        {
            CancelMovement();
        }
    }

    protected void SetHeading(GridTile nextTile)
    {
        Vector3 target = new Vector3(nextTile.transform.position.x,
                                     transform.position.y,
                                     nextTile.transform.position.z
                                    );

        // set heading
        heading = target - transform.position;
        heading.Normalize();
        transform.forward = heading;
    }

    public bool HeadingMatches(GridTile nextTile)
    {
        Vector3 target = new Vector3(nextTile.transform.position.x,
                                     transform.position.y,
                                     nextTile.transform.position.z
                                    );

        // set heading
        Vector3 heading = target - transform.position;
        heading.Normalize();
        return transform.forward == heading;
    }

    protected void Init()
    {
        gridTiles = GameObject.FindGameObjectsWithTag("GridTile");
    }

    protected void GetCurrentGridTile()
    {
        currentGridTile = GetTargetGridTile(gameObject);
    }

    // returns the tile a given target is atop
    protected GridTile GetTargetGridTile(GameObject target)
    {
        // raycast from the target position one meter down.
        // if a target was found, return the gridTile component, otherwise null.
        // layer mask is needed because if raycast down hits vision cone, vision cone obviously does not count as a grid tile
        return Physics.Raycast(target.transform.position, Vector3.down * 2, out RaycastHit raycastHit, 2, layerMask) && raycastHit.collider.tag == "GridTile"
            ? raycastHit.collider.GetComponent<GridTile>()
            : null;
    }

    // for every grid tile in the scene, each one must find its adjacent tiles
    private void FindAdjacentGridTiles()
    {
        foreach (GameObject gridTile in gridTiles)
        {
            gridTile.GetComponent<GridTile>().GetAdjacentTiles(jumpHeight);
        }
    }

    public void RemoveSelectableTiles()
    {
        if (currentGridTile != null)
        {
            currentGridTile.current = false;
            currentGridTile = null;
        }
        foreach (GridTile gridTile in selectableTiles)
        {
            gridTile.Reset();
        }

        selectableTiles.Clear();
    }

    public void CancelMovement()
    {
            path.Clear();
            RemoveSelectableTiles();
            moving = false;
            anime.Play("Idle");
    }
}
