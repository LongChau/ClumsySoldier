using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gamesystem
/// singleton
/// </summary>
public class GameSystem : MonoBehaviour
{
    public int levelID = 1;

    public bool isWin;
    public bool isLoose;
    public bool isLoose50;

    public string lastLevel;

    public static GameSystem instance;

    // singleton 
    public void Awake()
    {
        Debug.Log("GameSystem.Awake()");

        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            Debug.Log("GameSystem instance create");
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Debug.Log("GameSystem: destroy this game object when duplicate");
            Destroy(gameObject);
        }

        Debug.LogFormat("GameSystem: DontDestroyOnLoad({0})", gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        lastLevel = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
