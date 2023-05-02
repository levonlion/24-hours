using UnityEngine;

public class NodeController : MonoBehaviour
{
    public Node Model { get; protected set; }
    
    [SerializeField] protected NodeView View;

    public virtual void Initialize(Node model)
    {
        Model = model;
        View.Initialize(Model);
    }

    public void SwipeLeft()
    {
        GameManager.Instance.SwipeLeft();
    }
    
    public void SwipeRight()
    {
        GameManager.Instance.SwipeRight();
    }
}
