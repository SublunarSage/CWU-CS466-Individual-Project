using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;
using Google.XR.Cardboard;
using UnityEngine.EventSystems;


namespace VRPuzzleBall.Scripts.Runtime
{
    public class PuzzleBoardController : MonoBehaviour
    {

        public static event Action<GameStateEvent> OnGameStateEventSent;

        [SerializeField] private Transform puzzleBoardTransform;
        [SerializeField] private double rotationSpeed = 0.15;
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private float gyroSensitivity;

        private InputAction mobileGyro;
        private InputAction mobileAttitude;
        private InputAction touchPosition;
        
        private Quaternion initialRotation;
        private Quaternion gyroRotation;

        private AttitudeSensor _attitudeSensor;
        private bool _isTouching = false;

        private void Awake()
        {
            mobileGyro = inputActions.FindActionMap("Gameplay").FindAction("Gyro", true);
            inputActions.FindActionMap("Gameplay").FindAction("TouchScreenTap").performed += OnTouchscreenTapped;
            inputActions.FindActionMap("Gameplay").FindAction("TouchScreenTap").canceled += OnTouchscreenTapped;
            mobileAttitude = inputActions.FindActionMap("Gameplay").FindAction("Att", true);
            touchPosition = inputActions.FindActionMap("Gameplay").FindAction("TouchPosition");


        }

        // Start is called before the first frame update
        void Start()
        {
            puzzleBoardTransform = GetComponent<Transform>();
            initialRotation = transform.rotation;
            //InputSystem.AddDevice<Gyroscope>();

        }

        // Update is called once per frame
        void Update()
        {
            //_isTouching = Api.IsTriggerHeldPressed;
            UpdateGyro();
            
        }

        private void FixedUpdate()
        {
            
        }

        private void UpdateGyro()
        {
            if (_isTouching || Api.IsTriggerHeldPressed)
            {
                InputSystem.EnableDevice(AttitudeSensor.current); // Getting Attitude Sensor only works by constantly enabling the device in the update...Why?
                InputSystem.EnableDevice(Gyroscope.current);

                Quaternion landscapeCorrection = Quaternion.Euler(0, 0, 0);
                Vector3 correctedInput = landscapeCorrection * mobileGyro.ReadValue<Vector3>();

                if (Gyroscope.current.remote) puzzleBoardTransform.Rotate(correctedInput* gyroSensitivity);
                else puzzleBoardTransform.Rotate(mobileGyro.ReadValue<Vector3>()* gyroSensitivity);
                
            }
            else
            {
                puzzleBoardTransform.rotation = Quaternion.Slerp(puzzleBoardTransform.rotation, initialRotation,
                    (float) rotationSpeed * Time.deltaTime);
            }
            
        }

        private void OnTouchscreenTapped(InputAction.CallbackContext context)
        {

            Vector2 touchPositionValue = touchPosition.ReadValue<Vector2>();
            
            if (context.performed)
            {
                _isTouching = true;
                
                // Check if the touch position is over a 3D/World-Space Button
                Ray ray = Camera.main.ScreenPointToRay(touchPositionValue);
                RaycastHit hit;
                
                Debug.Log(touchPosition.ReadValue<Vector2>());
                if (Physics.Raycast(ray, out hit))
                {
                    
                }
                
                // Check if the touch position is over a UI element
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    position = touchPositionValue
                };
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (var result in results)
                {
                    if (result.gameObject.name == "TryAgainButton")
                    {
                        OnGameStateEventSent?.Invoke(GameStateEvent.Restart);
                    }
                    else if (result.gameObject.name == "QuitButton")
                    {
                        OnGameStateEventSent?.Invoke(GameStateEvent.Quit);
                    }
                }


            }
            else if (context.canceled)
            {
                _isTouching = false;
            }
        }

        private void OnEnable()
        {
            inputActions.FindActionMap("Gameplay").Enable();
        }

        private void OnDisable()
        {
            inputActions.FindActionMap("Gameplay").Disable();
        }
        
        private static void EnableDeviceIfNeeded(InputDevice device)
        {
            if (device != null && !device.enabled) InputSystem.EnableDevice(device);
        }
        
        private static TDevice GetRemoteDevice<TDevice>() where TDevice : InputDevice
        {
            foreach (var device in InputSystem.devices)
            {
                if (device.remote && device is TDevice deviceOfType) return deviceOfType;
            }

            return default;
        }
    }

}
