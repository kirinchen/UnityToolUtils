using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class FlexibleDraggableObject : MonoBehaviour {
    public RectTransform Target;
    private EventTrigger _eventTrigger;
    // private CanvasScaler _canvasScaler;

    ////void Awake() {
    ////    _canvasScaler = GetComponentInParent<CanvasScaler>();//anyone know if ?? is redundant here?
    ////}

    void Start() {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
    }

    void OnDrag(BaseEventData data) {
        PointerEventData ped = (PointerEventData)data;

        // Target.transform.localPosition= Target.transform.localPosition + (ped.delta);
        Vector3 v = Target.anchoredPosition;
        Target.anchoredPosition = new Vector3(v.x + ped.delta.x, v.y + ped.delta.y, v.z);
        //   Target.transform.localPosition = v + unscaleEventDelta(ped.delta);
    }

    /*  public Vector3 unscaleEventDelta(Vector3 vec) {
          Vector2 referenceResolution = _canvasScaler.referenceResolution;
          Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

          float widthRatio = currentResolution.x / referenceResolution.x;
          float heightRatio = currentResolution.y / referenceResolution.y;
          float ratio = Mathf.Lerp(widthRatio, heightRatio, _canvasScaler.matchWidthOrHeight);

          return vec / ratio;
      } */
}