using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector2 startPosition;
    public Vector2 currentPosition;

    public UnityEvent onDrag;
    public UnityEvent onBeginDrag;
    public UnityEvent onEndDrag;

    private bool dragging = false;
    private Plane plane;

    private void Start()
    {
        plane = new Plane(Vector3.forward, Vector3.zero);
    }

    private void Awake()
    {
        plane = new Plane(Vector3.forward, Vector3.zero);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Player player = hit.collider.gameObject.GetComponent<Player>();
            if (player && player.state == PlayerState.Idle)
            {
                startPosition = hit.collider.transform.position;
                dragging = true;
                onBeginDrag.Invoke();
            }
            else
            {
                dragging = false;
            }
        }
        else
        {
            dragging = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {        
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                currentPosition = ray.GetPoint(distance);
                onDrag.Invoke();
            }
            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragging)
            onEndDrag.Invoke();
        dragging = false;
    }

    
}
