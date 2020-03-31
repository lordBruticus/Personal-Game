using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //1
    public static UIManager Instance;
    //2
    public GameObject addTowerWindow;
    public GameObject addPlotWindow;
    public GameObject towerInfoWindow;
    public GameObject winGameWindow;
    public GameObject loseGameWindow;
    public GameObject blackBackground;
    public GameObject centerWindow;
    public GameObject damageCanvas;
    public Transform enemyHealthBars;
    public GameObject enemyHealthBarPrefab;

    public GameObject MenuWindow;
    //3
    public Text txtMoney;
    public Text txtWave;
    public Text txtEscapedEnemies;

    public static float vrUiScaleDivider = 12;

    //1
    void Awake()
    {
        Instance = this;
    }
    //2
    private void UpdateTopBar()
    {
        txtMoney.text = GameManager.Instance.money.ToString();
        txtWave.text = "Wave " + GameManager.Instance.waveNumber + " / " + WaveManager.Instance.enemyWaves.Count;
        txtEscapedEnemies.text = "Escaped Enemies " + GameManager.Instance.escapedEnemies + " / " +
                                 GameManager.Instance.maxAllowedEscapedEnemies;
    }
    //3
    //4
    public void ShowAddTowerWindow(GameObject towerSlot)
    {
        if (GameManager.Instance.gameOver)
        {
            return;
        }

        addTowerWindow.SetActive(true);
        addTowerWindow.GetComponent<AddTowerWindow>().towerSlotToAddTowerTo = towerSlot;

        UtilityMethods.MoveUiElementToWorldPosition(addTowerWindow.GetComponent<RectTransform>(), towerSlot.transform.position);
    }

    public void ShowAddPlotWindow(GameObject plotSlot)
    {
        if (GameManager.Instance.gameOver)
        {
            return;
        }

        addPlotWindow.SetActive(true);
        addPlotWindow.GetComponent<AddPlotWindow>().plotSlotToAddPlotTo = plotSlot;

        UtilityMethods.MoveUiElementToWorldPosition(addPlotWindow.GetComponent<RectTransform>(), plotSlot.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTopBar();
    }

    public void ShowTowerInfoWindow(Tower tower)
    {
        if (GameManager.Instance.gameOver)
        {
            return;
        }
        towerInfoWindow.GetComponent<TowerInfoWindow>().tower = tower;
        towerInfoWindow.SetActive(true);

        UtilityMethods.MoveUiElementToWorldPosition(towerInfoWindow.GetComponent<RectTransform>(), tower.transform.position);
    }

    public void ShowWinScreen()
    {
        winGameWindow.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        loseGameWindow.SetActive(true);
    }

    //1
    public void CreateHealthBarForEnemy(Enemy enemy)
    {
        //2
        GameObject healthBar = Instantiate(enemyHealthBarPrefab);
        //3
        healthBar.transform.SetParent(enemyHealthBars, false);
        //4
        healthBar.GetComponent<EnemyHealthBar>().enemy = enemy;
    }

    //1
    public void ShowCenterWindow(string text)
    {
        centerWindow.transform.Find("TxtWave").GetComponent<Text>().text = text;
        StartCoroutine(EnableAndDisableCenterWindow());
    }

    //2
    private IEnumerator EnableAndDisableCenterWindow()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.4f);
            centerWindow.SetActive(true);

            yield return new WaitForSeconds(.4f);
            centerWindow.SetActive(false);
        }
    }

    //1
    public void ShowDamage()
    {
        StartCoroutine(DoDamageAnimation());
    }
    //2
    private IEnumerator DoDamageAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.1f);
            damageCanvas.SetActive(true);

            yield return new WaitForSeconds(.1f);
            damageCanvas.SetActive(false);
        }
    }


}
