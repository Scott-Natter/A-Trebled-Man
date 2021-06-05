using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages + calculates distance between the parent Enemy and Maestro
// Not sure how cohesive this class is, kind of all over the place

public class ProximityMonitor : MonoBehaviour
{
    private PlayerMove player;
    private EnemyMove enemy;

    public float distanceThreshold = 4f;
    public float onBeatThreshold = .5f;    // Second

    public AudioSource maestroSong;
    public AudioSource gruntIsNear;

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        enemy = GetComponent<EnemyMove>();

        TimeTicker.OnTick += delegate (object sender, TimeTicker.OnTickEventArgs e)
        {
            // Stamp current time, always on the beat
            currentTime = Time.realtimeSinceStartup;
        };
    }

    private void Update()
    {
        MuteAudio();
        CheckPlayerStealth();
    }

    // If player is stealthy, do nothing.
    // If not, turn enemy around
    private void CheckPlayerStealth()
    {
        if (!PlayerIsStealthy())
        {
            enemy.SetAlertState();
        }

        else
        {
           //print("didn't hear anything");
        }
    }

    // Is player close to enemy and inputting close to the beat times?
    private bool PlayerIsStealthy()
    {
        // Player is close to Grunt
        if (PlayerIsCloseToEnemy())
        {
            // Determine the space by which input is stealthy
            float upperBound = currentTime + onBeatThreshold;
            // Would like a lower bound, but we cannot go back in time to allow an input before we started tracking ...

            // if any moveInput comes in before upperBound, return true
            if (player.GetLastInputTime() < upperBound)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        // If not close to Grunt, Maestro is stealthy
        else
        {
            return true;
        }
    }

    private void MuteAudio()
    {
        if (PlayerIsCloseToEnemy())
        {
            maestroSong.mute = true;
            gruntIsNear.mute = false;
        }
        else
        {
            maestroSong.mute = false;
            gruntIsNear.mute = true;
        }
    }

    private bool PlayerIsCloseToEnemy()
    {
        Vector3 enemyPos = enemy.transform.position;
        Vector3 playerPos = player.transform.position;
        return Vector3.Distance(enemyPos, playerPos) < distanceThreshold;
    }
}
