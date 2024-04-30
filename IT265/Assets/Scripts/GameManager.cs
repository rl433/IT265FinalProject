using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }
    [Header("Panels")] [SerializeField] private GameObject pregamePanel;
    [SerializeField] private GameObject piecesInventory;
    [SerializeField] private CurrentTurnIndicator turnIndicator;
    [SerializeField] private GameObject gameUI;

    [Header("Misc")] [SerializeField] private ObjectSelector objectSelector;

    private int numberOfPlayers;
    private int currentPlayer = -1;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        if (objectSelector != null)
        {
            objectSelector.allowMove = false;
            objectSelector.enabled = false;
        }

        if (piecesInventory != null)
        {
            piecesInventory.SetActive(false);
        }

        if (turnIndicator != null)
        {
            turnIndicator.gameObject.SetActive(false);
        }

        if (pregamePanel != null)
        {
            pregamePanel.SetActive(true);
        }

        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }
    }

    public void SetNumberOfPlayers(string players)
    {
        try
        {
            numberOfPlayers = int.Parse(players);
        }
        catch (UnityException e)
        {
            numberOfPlayers = 0;
        }
        Debug.Log($"Number of Players set to {numberOfPlayers}");
    }

    public void PrepareGame()
    {
        //TODO
        if (numberOfPlayers > 0)
        {
            //TODO 2
            pregamePanel.SetActive(false);
            piecesInventory.SetActive(true);
            objectSelector.enabled = true;
            objectSelector.allowMove = false;
            //if you don't want to allow delete
            objectSelector.rightClickToDelete = false;
        }
    }

    public void StartGame()
    {
        //TODO
        piecesInventory.SetActive(false);
        objectSelector.enabled = false;
        currentPlayer = Random.Range(0, numberOfPlayers);
        turnIndicator.SetText($"It's Player {currentPlayer+1}'s Turn");
        turnIndicator.gameObject.SetActive(true);
    }

    public void EndTurn()
    {
        objectSelector.enabled = false;
        currentPlayer++;
        if (currentPlayer >= numberOfPlayers)
        {
            currentPlayer = 0;
        }
        turnIndicator.SetText($"It's Player {currentPlayer+1}'s Turn");
        turnIndicator.gameObject.SetActive(true);
        gameUI.SetActive(false);
    }

    public void HandleTurn()
    {
        objectSelector.enabled = true;
        objectSelector.allowMove = true;
        turnIndicator.gameObject.SetActive(false);
        gameUI.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}