using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRPuzzleBall.Scripts.Runtime
{
    public class PuzzleBoardController : MonoBehaviour
    {

        [SerializeField] private Transform puzzleBoardTransform;
        [SerializeField] private double maxTiltAngle = 5.0;
        
        // Start is called before the first frame update
        void Start()
        {
            puzzleBoardTransform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            // Keyboard Controls: Pressing a key will tilt the board in the specified direction.
            
        }
    }

}
