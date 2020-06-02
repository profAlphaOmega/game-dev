using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Detection : MonoBehaviour {

        public Transform trackedTransform;
        public Transform detectedTransform;
        public LayerMask detectMask;
        public LayerMask trackMask;
        public float trackMaxSight = 10;
        int trackWaitForFrames = 10; 
        Main self;
        public string playerTag = "Player";
        public int arcWaitForFrames = 10; 
        public int arcSightAngle = 90;
        public int arcRayCount = 2;
        public float arcRotation = 1;
        public float arcMaxSight = 10;
        // public float upAngle = 1;
        // public float downAngle = 1;
        // public int _sightAngle = 90;
        // public float _maxSight = 10;
        

        void Start () 
        {
            self = transform.parent.GetComponent<Main>();
        }

        void Update() {
            if(self.state.canDetectCheck()) StartCoroutine(Detect());
            if(self.state.canTrackCheck() && self.state.hasDetected && detectedTransform) StartCoroutine(ITrack(detectedTransform)); 
            if(self.state.isTracking && trackedTransform) // && !self.state.busy
            {
                if(Mathf.Abs(self.transform.position.x - trackedTransform.position.x) < self.combatDistance) return; 

                self.smoothTime = (self.controller.collisions.below) ? self.accelerationTimeGrounded : self.accelerationTimeAirborne;
                if(self.transform.position.x  < trackedTransform.position.x)
                {
                    self.Move(velocity: ref self.velocity,finalVelocity: self.chaseSpeed,smoothTime: self.smoothTime);
                    if(!self.facingRight) self.Flip();
                    self.state.busy = true;
                }
                if(self.transform.position.x  > trackedTransform.position.x)
                {
                    self.Move(velocity: ref self.velocity,finalVelocity: -self.chaseSpeed,smoothTime: self.smoothTime);
                    if(self.facingRight) self.Flip();
                    self.state.busy = true;
                }
            }

        }
        
        public IEnumerator Detect()
        {
            self.state.setDetect(true);
            Vector3 detectOrigin = transform.position;
            for (int i = 0; i <= arcRayCount; i++)
            {
                Vector2 arcAngleVector = new Vector2(Mathf.Cos(((arcSightAngle/2) - ((arcSightAngle/(arcRayCount-1)) * i) + arcRotation)*Mathf.Deg2Rad), Mathf.Sin(((arcSightAngle/2)-( (arcSightAngle/(arcRayCount-1)) * i) + arcRotation)*Mathf.Deg2Rad));  
                if(!self.facingRight) arcAngleVector.x *= -1;
                RaycastHit2D hit = Physics2D.Raycast(detectOrigin, arcAngleVector, arcMaxSight, detectMask);
                Debug.DrawRay(detectOrigin, arcAngleVector*arcMaxSight, Color.green);
                if (hit && hit.collider.CompareTag(playerTag))
                {
                    detectedTransform = hit.transform; 
                    self.state.hasDetected = true;
                    self.state.setDetect(false);
                    yield break;
                }
            }
            yield return StartCoroutine(UTILS.WaitForFrames(arcWaitForFrames));
            self.state.setDetect(false);
        }

        public IEnumerator ITrack(Transform tt)
        {
            self.state.setTrack(true);
            while(self.state.isTracking)
            {
                self.state.setTrack(true);
                
                // Get Transforms
                Vector2 detectOrigin = transform.position;
                Vector2 trackedOrigin = tt.position;
                // Find Magnitude to Player, Set Magnitude to Max
                float distanceToPlayer = Vector2.Distance(detectOrigin, trackedOrigin) / trackMaxSight;
                // Find Direction to Player
                Vector2 trackedPosition = trackedOrigin - detectOrigin;
                
                // Vector Direction to Player and Max Magnitude
                trackedPosition /= distanceToPlayer;
                RaycastHit2D hit = Physics2D.Raycast(detectOrigin, trackedPosition, trackMaxSight, trackMask);
                
                //Debug Rays
                Debug.DrawRay(detectOrigin, trackedPosition, Color.green);
                
                
                //Alert
                if (hit && hit.collider.CompareTag(playerTag))
                {
                    trackedTransform = hit.transform;
                }
                else 
                // if the tracked falls out of range stop tracking
                // this is also where logic will go for tracking if the player when around a corner, right now it would stop tracking even if the tracked is lost sight of for only a frame
                {
                    self.state.setTrack(false);
                    yield break;
                }
                yield return null;
            }
        }

        // public IEnumerator Angle()
        // {
        //     isDetecting = true;
        //     Vector2 detectOrigin = transform.position;
        //     Vector2 upAngleVector = new Vector2(Mathf.Cos(_sightAngle/2*Mathf.Deg2Rad), Mathf.Sin(_sightAngle/2*Mathf.Deg2Rad));
        //     Vector2 downAngleVector = new Vector2(Mathf.Cos(_sightAngle/2*Mathf.Deg2Rad), Mathf.Sin(-_sightAngle/2*Mathf.Deg2Rad));  
        //     // Roatate
        //     upAngleVector.x *= upAngle;
        //     downAngleVector.x *= downAngle;
        //     // Flip
        //     if(!self.facingRight)
        //     {
        //         upAngleVector.x *= -1;
        //         downAngleVector.x *= -1;
        //     }
        //     // RayCast  
        //     RaycastHit2D hit = Physics2D.Raycast(detectOrigin, upAngleVector, _maxSight, layerMask);
        //     RaycastHit2D hit2 = Physics2D.Raycast(detectOrigin, downAngleVector, _maxSight, layerMask);
        //     Debug.DrawRay(detectOrigin, upAngleVector*_maxSight, Color.green);
        //     Debug.DrawRay(detectOrigin, downAngleVector*_maxSight, Color.green);
        //     // Alert
        //     if (hit && hit.collider.CompareTag(playerTag))
        //     {
        //         // Debug.Log("ALERT");
        //     }
        //     if (hit2 && hit2.collider.CompareTag(playerTag))
        //     {
        //         // Debug.Log("ALERT");
        //     }
        //     // NEXT
        //     yield return StartCoroutine(UTILS.WaitForFrames(10));
        //     isDetecting = false;
        // }
    }
    
}    