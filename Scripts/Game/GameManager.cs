using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    //1
    public static GameManager Instance;
    //2
    public int money;
    //3
    public int waveNumber;
    //4
    public int escapedEnemies;
    //5
    public int maxAllowedEscapedEnemies = 10;

    public int waves = 0;
    public int kills = 0;

    [SerializeField]
    private Text wavesText;
    [SerializeField]
    private Text killsText;
    [SerializeField]
    private GameObject menu;

    //6
    public bool enemySpawningOver;
    //7
    public AudioClip gameWinSound;
    public AudioClip gameLoseSound;
    //8
    public bool gameOver;
    private bool isPaused = false;

    //1
    private void Awake()
    {
        Instance = this;

        Pause();
    }

    public void Pause()
    {
        menu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Unpause()
    {
        menu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        isPaused = false;
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }

    void Update()
    {
        //2
        if (!gameOver && enemySpawningOver)
        {
            // Check if no enemies left, if so win game
            //3
            if (EnemyManager.Instance.Enemies.Count == 0)
            {
                OnGameWin();
            }
        }

        // When ESC is pressed, quit to the title screen
        //4
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    QuitToTitleScreen();
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        // 3
        waves = 0;
        kills = 0;
        wavesText.text = "Waves Survived: " + waves;
        killsText.text = "Mutants Killed: " + kills;

        //ClearRobots();
        //ClearBullets();
        Debug.Log("Game Saved");
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        int i = 0;
        //foreach (GameObject targetGameObject in targets)
        //{
        //    Target target = targetGameObject.GetComponent<Target>();
        //    if (target.activeRobot != null)
        //    {
        //        save.livingTargetPositions.Add(target.position);
        //        save.livingTargetsTypes.Add((int)target.activeRobot.GetComponent<Robot>().type);
        //        i++;
        //    }
        //}

        save.waves = waves;
        save.kills = kills;

        return save;
    }

    public void NewGame()
    {
        waves = 0;
        kills = 0;
        wavesText.text = "Waves: " + waves;
        killsText.text = "Kills: " + kills;

        //ClearRobots();
        //ClearBullets();
        //RefreshRobots();

        Unpause();
    }

    public void AddWave()
    {
        waves++;
        wavesText.text = "Waves: " + waves;
    }

    public void AddKill()
    {
        kills++;
        killsText.text = "Kills: " + kills;
    }

    //public void LoadGame()
    //{
    //    // 1
    //    if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
    //    {
    //        //ClearBullets();
    //        //ClearRobots();
    //        //RefreshRobots();

    //        // 2
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
    //        Save save = (Save)bf.Deserialize(file);
    //        file.Close();

    //        // 3
    //        for (int i = 0; i < save.livingTargetPositions.Count; i++)
    //        {
    //            int position = save.livingTargetPositions[i];
    //            Target target = targets[position].GetComponent<Target>();
    //            target.ActivateRobot((RobotTypes)save.livingTargetsTypes[i]);
    //            target.GetComponent<Target>().ResetDeathTimer();
    //        }

    //        // 4
    //        wavesText.text = "Waves: " + save.waves;
    //        killsText.text = "Hits: " + save.kills;
    //        waves = save.waves;
    //        kills = save.kills;

    //        Debug.Log("Game Loaded");

    //        Unpause();
    //    }
    //    else
    //    {
    //        Debug.Log("No game saved!");
    //    }
    //}

    //5
    private void OnGameWin()
    {
        AudioSource.PlayClipAtPoint(gameWinSound, Camera.main.transform.position);
        gameOver = true;
        UIManager.Instance.ShowWinScreen();
    }
    //6
    public void QuitToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    //1
    public void OnEnemyEscape()
    {
        escapedEnemies++;
        UIManager.Instance.ShowDamage();

        if (escapedEnemies == maxAllowedEscapedEnemies)
        {
            // Too many enemies escaped, you lose the game
            OnGameLose();
        }
    }

    //2
    private void OnGameLose()
    {
        gameOver = true;

        AudioSource.PlayClipAtPoint(gameLoseSound, Camera.main.transform.position);
        EnemyManager.Instance.DestroyAllEnemies();
        WaveManager.Instance.StopSpawning();

        UIManager.Instance.ShowLoseScreen();
    }

    //3
    public void RetryLevel()
    {
        SceneManager.LoadScene("Game");
    }
}
