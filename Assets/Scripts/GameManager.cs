using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Node _startingNode;
    [SerializeField] private Stats _startingStats;
    [SerializeField] private Stats _maxStats;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private NodeControllerBase _prefab;

    [SerializeField] private StatController _karmaController;
    [SerializeField] private StatController _healthController;
    [SerializeField] private StatController _timeController;
    
    private NodeControllerBase _currentNodeController;
    private List<string> _conditions = new List<string>();

    private void Awake()
    {
        Instance = this;
        UpdateCurrentNode(_startingNode);
        _karmaController.Initialize(_startingStats.Karma, _maxStats.Karma, "Karma");
        _healthController.Initialize(_startingStats.Health, _maxStats.Health, "Health");
        _timeController.Initialize(_startingStats.Time, _maxStats.Time, "Time");
    }

    public void SwipeLeft()
    {
        ApplyStats(_currentNodeController.Model.NegativeStats);
        UpdateCurrentNode(_currentNodeController.Model.LeftNode);
    }
    
    public void SwipeRight()
    {
        if(!ApplyRandomEvents(_currentNodeController.Model))
        {
            ApplyStats(_currentNodeController.Model.PositiveStats);
            UpdateCurrentNode(_currentNodeController.Model.RightNode);
        }
    }

    private bool ApplyRandomEvents(Node node)
    {
        var events = node.RandomEvents;
        
        foreach (var randomEvent in events)
        {
            if(Random.Range(0f, 100f) < randomEvent.ActionChance)
            {
                UpdateCurrentNode(randomEvent.ActionNode);
                ApplyStats(randomEvent.ActionNode.PositiveStats);
                return true;
            }
        }

        return false;
    }

    private void ApplyStats(Stats stats)
    {
        _karmaController.Add(stats.Karma);
        _healthController.Add(stats.Health);
        _timeController.Add(-stats.Time);
    }

    private void UpdateCurrentNode(Node model)
    {
        if(_currentNodeController != null)
            Destroy(_currentNodeController.gameObject);

        if(model.WriteCondition != string.Empty && !_conditions.Contains(model.WriteCondition))
        {
            _conditions.Add(model.WriteCondition);
            Debug.Log(model.WriteCondition);
        }
        
        if (model.ReadCondition != string.Empty)
        {
            UpdateCurrentNode(_conditions.Contains(model.ReadCondition) ? model.LeftNode : model.RightNode);
            return;
        }

        _currentNodeController = Instantiate(model);
    }

    private NodeControllerBase Instantiate(Node node)
    {
        var controller = Instantiate(_prefab, _spawnPos);
        controller.Initialize(node);
        return controller;
    }
}
