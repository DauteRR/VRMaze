using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Abstract class representing an interactive gazable
 * object
 */
[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(Collider))]
public abstract class Gazable : MonoBehaviour {
    /* Tells if the player is gazing the object */
    protected bool gazedAt;
    /* Name of the action button */
    public string actionButtonName = "square";

    /* Initialization method, event trigger entries are
     * added to the event trigger component
     */
    private void Awake() {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry {
            eventID = EventTriggerType.PointerClick
        };
        pointerClickEntry.callback.AddListener((data) => { OnPointerClick(); });

        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry {
            eventID = EventTriggerType.PointerEnter
        };
        pointerEnterEntry.callback.AddListener((data) => { OnPointerEnter(); });

        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry {
            eventID = EventTriggerType.PointerExit
        };
        pointerExitEntry.callback.AddListener((data) => { OnPointerExit(); });

        eventTrigger.triggers.Add(pointerClickEntry);
        eventTrigger.triggers.Add(pointerEnterEntry);
        eventTrigger.triggers.Add(pointerExitEntry);
    }

    /*
     * If the player is gazing the object and press the action
     * button, execute pointer click handler
     */
    public virtual void Update() {
        if (gazedAt && InputController.GetButtonDown(InputController.GetPS4ButtonName(actionButtonName))) {
            ExecuteEvents.Execute(
                gameObject, 
                new PointerEventData(EventSystem.current), 
                ExecuteEvents.pointerClickHandler
            );
        }
    }

    /*
     * Listener for the PointerEnter entry of the
     * event trigger
     */
    private void OnPointerEnter() {
        gazedAt = true;
    }

    /*
     * Listener for the PointerExit entry of the
     * event trigger
     */
    private void OnPointerExit() {
        gazedAt = false;
    }

    /*
     * Listener for the PointerClick entry of the
     * event trigger. In this method, the child classes
     * should define the behaviour of the interaction
     * with the object
     */
    public abstract void OnPointerClick();
}
