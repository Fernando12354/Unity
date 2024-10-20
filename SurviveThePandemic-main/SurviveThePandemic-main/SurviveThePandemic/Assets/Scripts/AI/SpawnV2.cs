using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AUTHOR:Garth de Wet (Corrupted Heart)
// CONTACT:mydeathofme[at]gmail[dot]com
// FILENAME:Spawner.cs
// PURPOSE:To allow spawning of different enemy types and different ways to spawn them.

public class SpawnV2 : MonoBehaviour
{
    //----------------------------------
    // All the Enums
    //----------------------------------
    // Spawn types
    public enum SpawnTypes
    {
        Normal,
        Once,
        Wave,
        TimedWave
    }
    // The different Enemy levels
    public enum EnemyLevels
    {
        Easy,
        Medium,
        Hard,
        Boss
    }
    //---------------------------------
    // End of the Enums
    //---------------------------------
   
    // Enemy level to be spawnedEnemy
    public EnemyLevels enemyLevel = EnemyLevels.Easy;
   
    //----------------------------------
    // Enemy Prefabs
    //----------------------------------
    public GameObject EasyEnemy;
    public GameObject MediumEnemy;
    public GameObject HardEnemy;
    public GameObject BossEnemy;
    //----------------------------------
    // End of Enemy Prefabs
    //----------------------------------
 
    //----------------------------------
    // Enemies and how many have been created and how many are to be created
    //----------------------------------
    public int totalEnemy = 10;
    private int numEnemy = 0;
    private int spawnedEnemy = 0;
    //----------------------------------
    // End of Enemy Settings
    //----------------------------------
 
   
    // The ID of the spawner
    private int SpawnID;
   
    //----------------------------------
    // Different Spawn states and ways of doing them
    //----------------------------------
    private bool waveSpawn = false;
    public bool Spawn = true;
    public SpawnTypes spawnType = SpawnTypes.Normal;
    // timed wave controls
    public float waveTimer = 30.0f;
    private float timeTillWave = 0.0f;
    //Wave controls
    public int totalWaves = 5;
    private int numWaves = 0;
    //----------------------------------
    // End of Different Spawn states and ways of doing them
    //----------------------------------
   
    void Start()
    {
        // sets a random number for the id of the spawner
        SpawnID = Random.Range(1, 500);
    }
   
    // Draws a cube to show where the spawn point is... Useful if you don't have a object that show the spawn point
    void OnDrawGizmos()
    {
        // Sets the color to red
        Gizmos.color = Color.red;
        //draws a small cube at the location of the gam object that the script is attached to
        Gizmos.DrawCube(transform.position, new Vector3 (0.5f,0.5f,0.5f));
    }
   
    void Update ()
    {
        if(Spawn)
        {
            // Spawns enemies everytime one dies
            if (spawnType == SpawnTypes.Normal)
            {
                // checks to see if the number of spawned enemies is less than the max num of enemies
                if(numEnemy < totalEnemy)
                {
                    // spawns an enemy
                    spawnEnemy();
                }
            }
            // Spawns enemies only once
            else if (spawnType == SpawnTypes.Once)
            {
                // checks to see if the overall spawned num of enemies is more or equal to the total to be spawned
                if(spawnedEnemy >= totalEnemy)
                {
                    //sets the spawner to false
                    Spawn = false;
                }
                else
                {
                    // spawns an enemy
                    spawnEnemy();
                }
            }
            //spawns enemies in waves, so once all are dead, spawns more
            else if (spawnType == SpawnTypes.Wave)
            {
                if(numWaves < totalWaves + 1)
                {
                    if (waveSpawn)
                    {
                        //spawns an enemy
                        spawnEnemy();
                    }
                    if (numEnemy == 0)
                    {
                        // enables the wave spawner
                        waveSpawn = true;
                        //increase the number of waves
                        numWaves++;
                    }
                    if(numEnemy == totalEnemy)
                    {
                        // disables the wave spawner
                        waveSpawn = false;
                    }
                }
            }
            // Spawns enemies in waves but based on time.
            else if(spawnType == SpawnTypes.TimedWave)
            {
                // checks if the number of waves is bigger than the total waves
                if(numWaves <= totalWaves)
                {
                    // Increases the timer to allow the timed waves to work
                    timeTillWave += Time.deltaTime;
                    if (waveSpawn)
                    {
                        //spawns an enemy
                        spawnEnemy();
                    }
                    // checks if the time is equal to the time required for a new wave
                    if (timeTillWave >= waveTimer)
                    {
                        // enables the wave spawner
                        waveSpawn = true;
                        // sets the time back to zero
                        timeTillWave = 0.0f;
                        // increases the number of waves
                        numWaves++;
                        // A hack to get it to spawn the same number of enemies regardless of how many have been killed
                        numEnemy = 0;
                    }
                    if(numEnemy >= totalEnemy)
                    {
                        // diables the wave spawner
                        waveSpawn = false;
                    }
                }
                else
                {
                    Spawn = false;
                }
            }
        }
    }
    // spawns an enemy based on the enemy level that you selected
    private void spawnEnemy()
    {
        // To check which enemy prefab to instantiate
        if (enemyLevel == EnemyLevels.Easy)
        {
            // Checks to see if there is a gameobject in the easy enemy var
            if (EasyEnemy != null)
            {
                // spawns the enemy
                GameObject Enemy = (GameObject) Instantiate(EasyEnemy, gameObject.transform.position, Quaternion.identity);
                // calls a function on the enemy that applies the spawner's ID to the enemy
                Enemy.SendMessage("setName", SpawnID);
            }
            else
            {
                //Shows a debug message if there is no prefab
                Debug.Log("ERROR: No easy enemy Prefab loaded");
            }
        }
        else if (enemyLevel == EnemyLevels.Medium)
        {
            // Checks to see if there is a gameobject in the medium enemy var
            if (MediumEnemy != null)
            {
                // spawns the enemy
                GameObject Enemy = (GameObject) Instantiate(MediumEnemy, gameObject.transform.position, Quaternion.identity);
                // calls a function on the enemy that applies the spawner's ID to the enemy
                Enemy.SendMessage("setName", SpawnID);
            }
            else
            {
                //Shows a debug message if there is no prefab
                Debug.Log("ERROR: No medium enemy Prefab loaded");
            }
        }
        else if (enemyLevel == EnemyLevels.Hard)
        {
            // Checks to see if there is a gameobject in the hard enemy var
            if (HardEnemy != null)
            {
                // spawns the enemy
                GameObject Enemy = (GameObject) Instantiate(HardEnemy, gameObject.transform.position, Quaternion.identity);
                // calls a function on the enemy that applies the spawner's ID to the enemy
                Enemy.SendMessage("setName", SpawnID);
            }
            else
            {
                //Shows a debug message if there is no prefab
                Debug.Log("ERROR: No hard enemy Prefab loaded");
            }
        }
        else if (enemyLevel == EnemyLevels.Boss)
        {
            // Checks to see if there is a gameobject in the boss enemy var
            if (BossEnemy != null)
            {
                // spawns the enemy
                GameObject Enemy = (GameObject) Instantiate(BossEnemy, gameObject.transform.position, Quaternion.identity);
                // calls a function on the enemy that applies the spawner's ID to the enemy
                Enemy.SendMessage("setName", SpawnID);
            }
            else
            {
                //Shows a debug message if there is no prefab
                Debug.Log("ERROR: No boss enemy Prefab loaded");
            }
        }
        // Increase the total number of enemies spawned and the number of spawned enemies
        numEnemy++;
        spawnedEnemy++;
    }
    // Call this function from the enemy when it "dies" to remove an enemy count
    public void killEnemy(int sID)
    {
        // if the enemy's spawnId is equal to this spawnersID then remove an enemy count
        if (SpawnID == sID)
        {
            numEnemy--;
        }
    }
    //enable the spawner based on spawnerID
    public void enableSpawner(int sID)
    {
        if (SpawnID == sID)
        {
            Spawn = true;
        }
    }
    //disable the spawner based on spawnerID
    public void disableSpawner(int sID)
    {
        if(SpawnID == sID)
        {
            Spawn = false;
        }
    }
    // returns the Time Till the Next Wave, for a interface, ect.
    public float TimeTillWave
    {
        get
        {
            return timeTillWave;
        }
    }
    // Enable the spawner, useful for trigger events because you don't know the spawner's ID.
    public void enableTrigger()
    {
        Spawn = true;
    }
}

