using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private NodeController _prefab;

    [SerializeField] private StatController _karmaController;
    [SerializeField] private StatController _healthController;
    [SerializeField] private StatController _timeController;
    [SerializeField] private GameObject _startMenuPanel;
    [SerializeField] private Image _dimming;

    [SerializeField] private GameObject _endscreenPanel;
    [SerializeField] private TMP_Text _endScreenText;

    private NodeController _currentNodeController;
    private List<string> _conditions = new List<string>();

    private void Awake()
    {
        Instance = this;
        UpdateCurrentNode(_startingNode);
        _karmaController.Initialize(_startingStats.Karma, _maxStats.Karma, "Karma", false);
        _healthController.Initialize(_startingStats.Health, _maxStats.Health, "Health", false);
        _timeController.Initialize(_startingStats.Time, _maxStats.Time, "Time", true);
        _karmaController.OnValueChanged += () => _endScreenText.text = _karmaController.Value >= 50 ? "Вы попали в рай" : "Вы попали в ад";
    }   

    public void StartGame()
    {
        _dimming.gameObject.SetActive(true);
        StartCoroutine(Dim());

        IEnumerator Dim()
        {
            
            float t = 0;

            do
            {
                var col = Color.black;
                col.a = t;
                _dimming.color = col;
                t += Time.deltaTime;
                yield return null;
            } while (t < 1);

            _startMenuPanel.SetActive(false);
            yield return new WaitForSeconds(1);
            
            do
            {
                var col = Color.black;
                col.a = t;
                _dimming.color = col;
                t -= Time.deltaTime;
                yield return null;
            } while (t > 0);
            
            _dimming.gameObject.SetActive(false);
        }
    }

    public void Lose()
    {
        StartCoroutine(Dim());

        IEnumerator Dim()
        {
            _dimming.gameObject.SetActive(true);
            float t = 0;

            do
            {
                var col = Color.black;
                col.a = t;
                _dimming.color = col;
                t += Time.deltaTime;
                yield return null;
            } while (t < 1);

            _endscreenPanel.SetActive(true);
            
            yield return new WaitForSeconds(1);
            
            do
            {
                var col = Color.black;
                col.a = t;
                _dimming.color = col;
                t -= Time.deltaTime;
                yield return null;
            } while (t > 0);
            
            _dimming.gameObject.SetActive(false);
        }
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

    private NodeController Instantiate(Node node)
    {
        var controller = Instantiate(_prefab, _spawnPos);
        controller.Initialize(node);
        return controller;
    }
}
