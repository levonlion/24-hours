using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Node")]
public class Node : ScriptableObject
{
    public int Time;
    public int Karma;
    public int Health;

    public Sprite Background;
    public Color BackgroundColor;
    public string Text;
    public string LeftText;
    public string RightText;
    public string DownText;
    
    public Node LeftNode;
    public Node RightNode;
    public Node DownNode;
}
