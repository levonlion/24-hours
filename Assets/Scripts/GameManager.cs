using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private Node _startingNode;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private NodeControllerBase _prefab;
    
    private NodeControllerBase _currentNodeController;

    private void Awake()
    {
        Instance = this;
        UpdateCurrentNode(_startingNode);
    }

    public void SwipeLeft()
    {
        UpdateCurrentNode(_currentNodeController.Model.LeftNode);
    }
    
    public void SwipeRight()
    {
        UpdateCurrentNode(_currentNodeController.Model.RightNode);
    }

    public void SwipeDown()
    {
        UpdateCurrentNode(_currentNodeController.Model.DownNode);
    }

    private void UpdateCurrentNode(Node model)
    {
        if(_currentNodeController != null)
            Destroy(_currentNodeController.gameObject);
        
        _currentNodeController = Instantiate(model);
    }

    private NodeControllerBase Instantiate(Node node)
    {
        var controller = Instantiate(_prefab, _spawnPos);
        controller.Initialize(node);
        return controller;
    }
}
