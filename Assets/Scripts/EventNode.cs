using UnityEngine;

public class EventNode : MonoBehaviour
{
    private enum NodeState
    {
        unvisited = 0,
        active = 1,
        completed = 2,
        failed = 3
    }
    private NodeState _nodeState;
    
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private LineRenderer _lineRenderer;
    
    private Color[] _nodeColors;
    public Color unvisitedColor;
    public Color activeColor;
    public Color completedColor;
    public Color failedColor;

    private void Awake()
    {
        _nodeState = NodeState.unvisited;
        _nodeColors = new[]{ unvisitedColor, activeColor, completedColor, failedColor };
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = .01f;
        _lineRenderer.endWidth = .01f;
        _lineRenderer.enabled = false;
    }

    private void OnMouseEnter()
    {
        if(_nodeState != NodeState.unvisited)
            return;
        
        _spriteRenderer.color = Color.black;
        
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, PlayerState.currentNode.GetComponent<EventNode>()._transform.position);
        _lineRenderer.SetPosition(1, _transform.position);
    }
    
    private void OnMouseExit()
    {
        UpdateColor();
        _lineRenderer.enabled = false;
    }

    private void OnMouseDown()
    {
        Select();
        _lineRenderer.enabled = false;
    }

    public void Select()
    {
        if(_nodeState != NodeState.unvisited)
            return;
        
        //TODO: this should be done when the player wins/loses
        PlayerState.currentNode.GetComponent<EventNode>()._nodeState = NodeState.completed;
        PlayerState.currentNode.GetComponent<EventNode>().UpdateColor();

        PlayerState.currentNode = gameObject;
        _nodeState = NodeState.active;
        UpdateColor();
    }
    
    private void UpdateColor()
    {
        _spriteRenderer.color = _nodeColors[(int)_nodeState];
    }
}
