using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerMove : GridMove
{

    private bool enteredCover = false;
    public GridTile titleTargetTile;
    public GameObject reference;

    private IEnumerator WaitToMove() {
        yield return new WaitForSeconds(0);
        MoveToTile(titleTargetTile);
        StartCoroutine(WaitForAnimation(anime));
    }

    private IEnumerator WaitForAnimation ( Animator anime )
    {
        yield return new WaitForSeconds(.9f);

        if (!enteredCover) {
            anime.Play("Enter Cover");
            enteredCover = true;
        } else {
            anime.Play("Cover Idle");
        }
        StartCoroutine(WaitForAnimation(anime));
    }

    public bool useMouse = false;
    public Text speedSlide;

    public PlayerController playerController;

    public GameObject resetGameUI;

    private CoverVisionManager coverVisionManager;

    public Cover nearbyCover;

    public AudioSource maestroMotif;
    public AudioSource gruntMotif;
    public AudioSource gruntTakeDown;

    public bool inCover;
    private float lastInputTime;
    private Vector2 leftStickInput;
    private Vector2 rightStickInput;

    //private bool beatOccurring = false;

    public float enemyDistanceDetectionRadius;

    private GameObject closestGrunt;

    public GameObject note, brokenNote, damagedNote;

    public bool playTitleScreenAnim;

    private void Awake()
    {
        //ConfigureInputDetection();
    }

    private void OnEnable()
    {
        if (playerController != null)
        {
            playerController.Enable();
        }
    }

    private void Start()
    {
        inCover = false;
        coverVisionManager = GetComponent<CoverVisionManager>();
        
        lastInputTime = 0f;
        Init();

        if (playTitleScreenAnim) {
            nextTile = titleTargetTile;
            PlayTitleScreenAnim();
            return;
        }
    }

    // Gross result of rushed code/builds
    // Should add State with Enums so we can define a single State the player is in
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeCover();
        }
        ConfigureKeyboardDetection();
        ToggleAudio();
        if(Input.GetKeyDown("o")){
            anime.Play("Dab");
            anime.SetBool("dab", true);

        }

        if (!moving)
        {
            FindSelectableGridTiles();
            if (useMouse)
            {
                CheckMouse();
            }
            else
            {
                if (!inCover)
                {
                    CheckInputs();
                }
            }
        }
        else
        {
            if(transform.position == new Vector3(nextTile.transform.position.x, transform.position.y, nextTile.transform.position.z))
            {
                GetCurrentGridTile();
                CancelMovement();
            }
            else
            {
                Move();
            }
            
        }
        CheckForDeath();
        SetNoteRotation();
    }

    private void PlayTitleScreenAnim() {
        StartCoroutine(WaitToMove());
    }

    private void SetNoteRotation()
    {
        if (note != null)
        {
            note.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (brokenNote != null)
        {
            brokenNote.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (damagedNote != null)
        {
            damagedNote.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void ClearNotes()
    {
        if (note != null)
        {
            note.SetActive(false);
        }
        if (brokenNote != null)
        {
            brokenNote.SetActive(false);
        }
        if (damagedNote != null)
        {
            damagedNote.SetActive(false);
        }
    }

    // demo function to show how enemies could pathfind
    private void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0)
            && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)
            && hit.collider.tag == "GridTile"
            && hit.collider.GetComponent<GridTile>().selectable
           )
        {
            MoveToTile(hit.collider.GetComponent<GridTile>());
        }
    }

    private void ConfigureInputDetection()
    {
        playerController = new PlayerController();

        playerController.Gameplay.Move.performed += ctx => leftStickInput = GetNearestCardinalDirection(ctx.ReadValue<Vector2>());
        playerController.Gameplay.Move.performed += ctx => Timestamp();
        playerController.Gameplay.Move.canceled += ctx => leftStickInput = Vector2.zero;

        playerController.Gameplay.Rotate.performed += ctx => rightStickInput = GetNearestCardinalDirection(ctx.ReadValue<Vector2>());
        playerController.Gameplay.Rotate.canceled += ctx => rightStickInput = Vector2.zero;

        playerController.Gameplay.TakeDown.performed += ctx => TakeDown();
        // playerController.Gameplay.Cover.performed += ctx => TakeCover();  // Enable when controllers work + bind cover in Input Controller asset

    }

    private void Timestamp()
    {
        lastInputTime = Time.realtimeSinceStartup;
    }

    public float GetLastInputTime()
    {
        return lastInputTime;
    }

    private void HandleNote()
    {
        if (IsOnBeat())
        {
            DisplayGreenNote();
        }
        else
        {
            DisplayRedNote();
        }
    }

    private bool IsOnBeat()
    {
        return transform.parent.gameObject.GetComponent<TimeTicker>().beatOccurring;
    }

    private void DisplayGreenNote()
    {
        note.SetActive(true);
        damagedNote.SetActive(false);
    }

    private void DisplayRedNote()
    {
        note.SetActive(false);
        damagedNote.SetActive(true);
    }

    private void ConfigureKeyboardDetection()
    {
        // Timestamp only on player move inputs
        if (Input.GetKeyDown(KeyCode.W))
        {
            leftStickInput = Vector2.up;
            Timestamp();
            HandleNote();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (inCover) {
                coverVisionManager.EnableLeftVision();
            }

            else {
                leftStickInput = Vector2.left;
                Timestamp();
                HandleNote();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            leftStickInput = Vector2.down;
            Timestamp();
            HandleNote();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (inCover)
            {
                coverVisionManager.EnableRightVision();
            }
            else
            {
                leftStickInput = Vector3.right;
                Timestamp();
                HandleNote();
            }
        }
        else
        {
            leftStickInput = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDown();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeCover();
        }

    }

    private void CheckInputs()
    {
        // if the current grid tile exists,
        // and the tile in the cardinal direction nearest the control stick's current direction exists,
        // and it is selectable
        // move to that tile
        if (currentGridTile != null
            && Physics.Raycast(currentGridTile.transform.position,
                               new Vector3(leftStickInput.x, 0, leftStickInput.y),
                               out RaycastHit hit,
                               1
                              )
            && hit.collider.tag == "GridTile"
            && hit.collider.GetComponent<GridTile>().selectable
           )
        {
            nextTile = hit.collider.GetComponent<GridTile>();
            t = 0;
            if (HeadingMatches(nextTile))
            {
                MoveToTile(nextTile);
            }
            else
            {
                SetHeading(nextTile);
            }
        }
    }

    private Vector2 GetNearestCardinalDirection(Vector2 stickInput)
    {
        if (Mathf.Abs(stickInput.x) == Mathf.Abs(stickInput.y))
        {
            return Vector2.zero;
        }
        else if (Mathf.Abs(stickInput.x) > Mathf.Abs(stickInput.y))
        {
            return new Vector2(stickInput.x < 0 ? -1 : 1, 0);
        }
        else
        {
            return new Vector2(0, stickInput.y < 0 ? -1 : 1);
        }
    }

    private void TakeDown()
    {
        // If a raycast hits a grunt
        // and the grunt and the player are both facing the same direction
        // take down
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1)
            && hit.collider.tag == "Grunt"
           
           )
        {
            if (maestroMotif != null)
            {
                maestroMotif.mute = false;
            }
            if (gruntMotif != null)
            {
                gruntMotif.mute = true;                
            }
            if (hit.collider.gameObject.GetComponent<EnemyMove>().tensionSound != null)
            {
                hit.collider.gameObject.GetComponent<EnemyMove>().tensionSound.mute = true;
            }
            if (gruntTakeDown != null)
            {
                gruntTakeDown.PlayScheduled(2.0f);
            }

            hit.collider.GetComponent<EnemyMove>().PlayDeathAnimation(); 
        }
    }

    private void TakeCover()
    {
        // if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1) && hit.collider.tag == "Cover")
        // {
        Debug.Log("at cover");
        if (!inCover)
        {
            transform.position += new Vector3(0f,0f,-1f);
            EnterCover();
            coverVisionManager.DisableNormalVision();
            transform.position += new Vector3(0f,0f,-1f);
        }

        else
        {
            LeaveCover();
            coverVisionManager.EnableNormalVision();
            transform.position += new Vector3(0f,0f,1f);
        }
        // }
    }

    private void CheckForDeath()
    {
        foreach (Collider collider in Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity))
        {
            if (collider.gameObject.tag == "Grunt")
            {
                resetGameUI.SetActive(true);
                Debug.Log("Game Over");
                gameObject.SetActive(false);
            }
        }
    }

    public void AdjustSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        speedSlide.text = newSpeed.ToString();
    }

    private void EnterCover()
    {
        Debug.Log("Entering Cover");
        inCover = true;
        anime.Play("crouch");
    }

    private void LeaveCover()
    {
        Debug.Log("Leaving Cover");
        inCover = false;
        anime.Play("Idle");
    }

    private void ToggleAudio()
    {
        if (PlayerIsCloseToEnemy())
        {
            if (maestroMotif != null)
            {
                maestroMotif.mute = true;
            }
            if (gruntMotif != null)
            {
                gruntMotif.mute = false;
            }
        }
        else
        {
            if (maestroMotif != null)
            {
                maestroMotif.mute = false;
            }
            if (gruntMotif != null)
            {
                gruntMotif.mute = true;
            }
        }
    }

    private bool PlayerIsCloseToEnemy()
    {

        if (closestGrunt != null && Vector3.Distance(transform.position, closestGrunt.transform.position) > enemyDistanceDetectionRadius)
        {
            closestGrunt = null;
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Grunt"))
        {
            EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
            if (Vector3.Distance(transform.position, enemyMove.transform.position) < enemyDistanceDetectionRadius)
            {
                if (closestGrunt == null ||
                    Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, closestGrunt.transform.position))
                    {
                        closestGrunt = enemy;
                    }
            }
        }
        return closestGrunt != null;
    }

    private void OnDestroy() {
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void RecordAnimation()
    {
        reference.GetComponent<SpawnRecord>().Record();
        anime.SetBool("RecPickup", true);
       
    }
}
