using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomExtensions;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    private int _itemsCollected = 0;
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            if(_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
            Debug.LogFormat("Items: {0}", _itemsCollected);
        }
    }
    private int _playerHP = 3;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if(_playerHP <= 0)
            {
                labelText = "That looked like it hurt. Try again?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "The point is to not get hit.";
            }
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        _state = "Manager initialized.";
        _state.FancyDebug();
        Debug.Log(_state);
    }
    void OnGUI ()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" +_playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected:" + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel(0);
            }
        }
        if (showLossScreen)
        {
            if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                Utilities.RestartLevel();
            }
        }
    }
}