using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Node")]
public class Node : ScriptableObject
{
    public Stats NegativeStats;
    public Stats PositiveStats;
    public List<RandomEvent> RandomEvents;

    public Sprite Background;
    public Color BackgroundColor;
    public Color TextColor = new Color(0,0,0,1);
    public string Text;
    public string LeftText;
    public string RightText;
    public string WriteCondition;
    public string ReadCondition;
    
    public Node LeftNode;
    public Node RightNode;
}
