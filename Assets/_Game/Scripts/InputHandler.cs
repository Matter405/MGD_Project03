using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private InputSystem_Actions _inputSystemActions;
    public event Action<Vector2> TouchStarted;
    public event Action<Vector2> TouchEnded;

    public Vector2 TouchStartPosition { get; private set; }

    public Vector2 TouchCurrentPosition { get; private set; }

    public bool TouchHeld { get; private set; } = false;

    private void Awake()
    {
        _inputSystemActions = new InputSystem_Actions();
    }

    private void Update()
    {
        if(TouchHeld)
        {
            TouchCurrentPosition =
                _inputSystemActions.Player.TouchPoint.ReadValue<Vector2>();
        }
    }

    private void OnEnable()
    {
        _inputSystemActions.Enable();
        _inputSystemActions.Player.TouchPoint.performed += OnTouchPerformed;
        _inputSystemActions.Player.TouchPoint.canceled += OnTouchCancelled;
    }

    private void OnDisable()
    {
        _inputSystemActions.Player.TouchPoint.performed -= OnTouchPerformed;
        _inputSystemActions.Player.TouchPoint.canceled -= OnTouchCancelled;
        _inputSystemActions.Disable();
    }

    /*This syntax is called ‘subscribing’ and ‘unsubscribing’ to an event.
     * This allows us to wait for some specific thing to happen 
     * and run a method when it does (subscribe a function to run in response)
     * Our InputHandler script is hooking into the InputActions we set up earlier. 
     * When we receive an input, this class will get the notification 
     * and respond by exposing what’s needed in this script.
     */

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("Touch");
        //Change our public bool
        TouchHeld = true;
        //Read position from our input action
        Vector2 TouchPosition = context.ReadValue<Vector2>();
        //save start position
        TouchStartPosition = TouchPosition;
        //update current position - here it's our start
        TouchCurrentPosition = TouchPosition;
        //send event notification for event listeners
        TouchStarted?.Invoke(TouchPosition);
        //Debug.Log("Touch Start Pos: " + TouchStartPosition);
    }

    /* When a touch is performed, we get information about the position
     * of the touch and store everything.
     * We’re also calling events. 
     * This way other classes aren’t messing with the new Input System and all its
     * complexities, they’re just looking at variable changes or event calls 
     * from this InputHandler script.
     */

    private void OnTouchCancelled(InputAction.CallbackContext context)
    {
        //Debug.Log("Released");
        //revert our public bool
        TouchHeld = false;
        //send notification for listeners of last known location
        TouchEnded?.Invoke(TouchCurrentPosition);
        //Debug.Log("Touch End Pos: " + TouchCurrentPosition);
        //clear out touch positions when there's no input
        TouchStartPosition = Vector2.zero;
        TouchCurrentPosition = Vector2.zero;
    }



}