using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : GridMove
{
    // TODO: Look for RayCast alternative? Possibly use a Physics Overlap Box or use empty GameObject that matches the light cone

    public GridTile[] moveTargets;
    public float visionLength = 1;

    // Components should be greater than 0, less than 1
    public Vector3 coneWidth = new Vector3(0, 0, 0.5f);

    // UI Stuff
    public Text sightSlide;
    public Text speedSlide;

    private Stack<GridTile> moveTargetsStack;
    private RaycastHit frontLeft;
    private RaycastHit front;
    private RaycastHit frontRight;
    public GridTile lastKnownPosition;

    public GameObject player;
    public ParticleSystem poof;
    public bool isStanding = false;

    public AudioSource tensionSound;

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(0.5f);
        
        gameObject.SetActive(false);
    }


    void Start()
    {
        GetComponent<CapsuleCollider>().enabled = true;
        
        moveTargetsStack = new Stack<GridTile>();
        Init();
        ResetMoveTargets();

        TimeTicker.OnTick += delegate (object sender, TimeTicker.OnTickEventArgs e)
        {
            stepNext();
        };
       

        if (tensionSound != null)
        {
            tensionSound.mute = true;
        }
    }

    void Update()
    {
        DetermineNextAction();
        MoveBetweenTiles();
    }
    public void MoveBetweenTiles()
    {
        if(nextTile != null && transform.position != new Vector3(nextTile.transform.position.x, transform.position.y, nextTile.transform.position.z))
        {
            Move();
        }
    }
    public void AdjustVision(float newVision)
    {
        visionLength = newVision;
        sightSlide.text = visionLength.ToString();
    }

    public void AdjustSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        speedSlide.text = newSpeed.ToString();
    }

    private void DetermineNextAction()
    {                           
        bool seeMaestro = transform.GetChild(1).GetComponent<GruntVision>().seeMaestro;

        if (seeMaestro)
        {
            if (Vector3.Distance(transform.position, GetTargetGridTile(player).transform.GetChild(0).transform.position) <= 1f && path.Peek() != GetTargetGridTile(player))
            {
                print("test");
                path.Clear();
                path.Push(GetTargetGridTile(player));
            }
            else
                SetAlertState();
        }

        if (GetTargetGridTile(gameObject) == lastKnownPosition && !seeMaestro)
        {
            print("At last known and don't see maestro");
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1) && hit.collider != null && hit.collider.tag == "Player")
            {
                print("Kills");
                player.gameObject.SetActive(false);
            }
            else
            {
                print("Else no kill");
                if (tensionSound != null)
                {
                    tensionSound.mute = true;
                }
                ClearAlertState();
            }
        }

        if (!moving && !isStanding)
        {
            FindSelectableGridTiles();
            if (moveTargetsStack.Count == 0)
            {
                ResetMoveTargets();
            }
            else
            {
                MoveToTile(moveTargetsStack.Pop());
                nextTile = null;
            }
        } else
        {
            /*
            if (transform.parent.parent.gameObject.GetComponent<TimeTicker>().beatOccurring)
            {
                Move();
            }
            */

        }
    }

    public void stepNext()
    {
        //GetCurrentGridTile();
        if(!isStanding)
        {
            if (Vector3.Distance(transform.position, GetTargetGridTile(player).transform.GetChild(0).transform.position) <= 1.2f && transform.GetChild(1).GetComponent<GruntVision>().seeMaestro)
            {
                print("why must this be stupid");
                transform.position = GetTargetGridTile(player).transform.GetChild(0).position;
            }
            if (nextTile == null && path.Peek() != null)
            {
                //print("nexttileNullCase");
                nextTile = path.Pop();
            }
            else if (transform.position == new Vector3(nextTile.transform.position.x, transform.position.y, nextTile.transform.position.z))
            {
                GetCurrentGridTile();
                print("arrived");
                if (path.Peek() != null)
                {
                    nextTile = path.Pop();
                    t = 0;
                }
                else
                {
                    print("Movement Cancelled");
                    CancelMovement();
                }

            }
        }
        
    }
    public void SetAlertState()
    {
        //bool seeMaestro = transform.GetChild(1).GetComponent<GruntVision>().seeMaestro;
        //CancelMovement();
        //path.Clear();
        //moving = false;
        isStanding = false;
        if (lastKnownPosition != GetTargetGridTile(player))
        {
            moveTargetsStack.Clear();
            lastKnownPosition = GetTargetGridTile(player);
            moveTargetsStack.Push(lastKnownPosition);
            moving = false;
            nextTile = null;
            //moveTargetsStack.Push(GetTargetGridTile(player));

        }
        //MoveToTile(GetTargetGridTile(player));
        //moveTargetsStack.Push(GetTargetGridTile(player));

        if (tensionSound != null)
        {
            tensionSound.mute = false;
        }
    }

    public void ClearAlertState()
    {
        Debug.Log("EnemyMove - ClearAlertState");
        lastKnownPosition = null;
        
    }

    void ResetMoveTargets()
    {
        foreach (GridTile moveTarget in moveTargets)
        {
            moveTargetsStack.Push(moveTarget);
        }
    }

    private void OnDestroy()
    {
        TimeTicker.OnTick -= delegate (object sender, TimeTicker.OnTickEventArgs e)
        {
            Move();
        };

    }

    public void PlayDeathAnimation()
    {
        
        anime.Play("fallover");
        poof.Emit(20);
        GetComponent<CapsuleCollider>().enabled = false;
        
        StartCoroutine(StartCountdown());
        
    }
}
