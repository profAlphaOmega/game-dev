
using UnityEngine;
using System.Collections;

namespace Player1 
{    
    public class Controller2D : Player.RaycastController
    {
        float maxClimbAngle = 80;
        float maxDescendAngle = 75;

        private float velocityXSmoothing;
        private float velocityYSmoothing;

        public CollisionInfo collisions;
        // public PhysicsOld physicsOld;
        private Vector2 velocityOld;
        public bool collisiondetect = true;
        public float climbMultiplier = 1.0f;
        public float descendMultiplier = 1.0f;

        public override void Start()
        {
            base.Start();
            collisions.faceDir = 1;
        }

        public void Move(Vector2 velocity, bool collision=true, bool vel=true)
        {
            // Get Collider Dimensions
            UpdateRaycastOrigins();

            // Reset Collision Value
            collisions.Reset();

            // Set current to old velocity
            velocityOld = velocity;
            
            // Detect Collisions and Set Velocity and Collision Values
            if(vel)
            {
                velocity = HorizontalCollisions(velocity);
                velocity = VerticalCollisions(velocity);
            }
            
            // Switch Detect Collision
            if(!collision) 
            {
                velocity = velocityOld;
                collisions.Reset();
            }
            // Motion 
            transform.Translate(velocity);
        }



        public Vector2 HorizontalCollisions(Vector2 velocity)
        {
            // Find what direction you are facing, have this depend on input velocity and not another variable outside of this script
            if (velocity.x != 0)
            {
                collisions.faceDir = (int)Mathf.Sign(velocity.x);
            }
            float directionX = collisions.faceDir;
            
            // Raylength
            // float rayLength = 0.05f; // change this to make it static length

            float rayLength = Mathf.Abs(velocity.x) + skinWidth; // change this to make it static length
            // float rayLength = 10; // change this to make it static length


            if (Mathf.Abs(velocity.x) < skinWidth)
            {
                rayLength = 2 * skinWidth;
            }
            Debug.Log(rayLength);

            // Cast Rays
            for (int i = 0; i < horizontalRayCount; i++)
            {
                // RaySpace
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                
                // Raycast
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
                Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.blue);
                
                // Collision
                if (hit)
                {
                    // Find Angle of collision hit
                    float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                    
                    // Climbing
                    if (i == 0 && slopeAngle <= maxClimbAngle) 
                    {
                        // In Between Slopes
                        if (collisions.descendingSlope)
                        {
                            collisions.descendingSlope = false;
                            velocity = velocityOld;
                        }


                        float distanceToSlopeStart = 0;
                        if(slopeAngle != collisions.slopeAngleOld)
                        {
                            distanceToSlopeStart = hit.distance - skinWidth;
                            velocity.x -= distanceToSlopeStart * directionX;
                        }

                        ClimbSlope(ref velocity, slopeAngle);
                        velocity.x += distanceToSlopeStart * directionX;
                    }

                    // Not Climbing or Slope angle is too high
                    if(!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                    {
                        velocity.x = (hit.distance - skinWidth) * directionX;
                        rayLength = hit.distance;

                        // Find Y component if climbing slope otherwise Y component is determined elsewhere
                        if (collisions.climbingSlope)
                        {
                            velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad * Mathf.Abs(velocity.x));
                        }
                        

                        // Determine collision direction from directionX (velocity.x)
                        collisions.left = directionX == -1;
                        collisions.right = directionX == 1;
                    }  
                }
            }
            return velocity;
        }

        public Vector2 VerticalCollisions(Vector2 velocity)
        {
                    
            // if (velocity.y < 0) // old implementation
            if (Mathf.Sign(velocity.y) == -1)
            {
                DescendSlope(ref velocity); // Handles flat ground as well [revisted: not sure what this is, will you have a negative YV on flat ground?]
            }

            float directionY = Mathf.Sign(velocity.y);
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;
            

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

                if (hit)
                {
                    velocity.y = (hit.distance - skinWidth) * directionY;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                    }

                    collisions.below = directionY == -1;
                    collisions.above = directionY == 1;
                }
            }

            if (collisions.climbingSlope)
            {
                float directionX = Mathf.Sign(velocity.x);
                rayLength = Mathf.Abs(velocity.x) + skinWidth;
                Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * velocity.y;
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

                if (hit)
                {
                    float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                    if(slopeAngle != collisions.slopeAngle)
                    {
                        velocity.x = (hit.distance - skinWidth) * directionX;
                        collisions.slopeAngle = slopeAngle;
                    }
                }
            }

            return velocity;
        }

        void ClimbSlope(ref Vector2 velocity, float slopeAngle)
        {
            // Finds your new Y and X component from current X velocity
            float moveDistance = Mathf.Abs(velocity.x) * climbMultiplier;
            // You can set climbing speed here
            float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

            // Max Climb Speed
            if(velocity.y <= climbVelocityY)
            {
                velocity.y = climbVelocityY;
                velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                collisions.below = true;
                collisions.climbingSlope = true;
                collisions.slopeAngle = slopeAngle;
            }
        }

        void DescendSlope(ref Vector2 velocity)
        {
            // Find Direction
            float directionX = Mathf.Sign(velocity.x);
            
            // Cast Rays
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

            // Collision Detection
            if (hit)
            {
                // Hit Angle
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                
                // If you are and can descend
                if(slopeAngle != 0 && slopeAngle <= maxDescendAngle)
                {
                    // Descending the way you are facing
                    if(Mathf.Sign(hit.normal.x) == directionX)
                    {
                        // find new X and Y velocity from current Y
                        if(hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                        {
                            float moveDistance = Mathf.Abs(velocity.x) * descendMultiplier;
                            float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                            velocity.y -= descendVelocityY;

                            collisions.slopeAngle = slopeAngle;
                            collisions.descendingSlope = true;
                            collisions.below = true;    

                        }
                    }
                }
            }
        }


        public struct CollisionInfo
        {
            public bool above, below;
            public bool left, right;

            public bool climbingSlope;
            public bool descendingSlope;
            public float slopeAngle, slopeAngleOld;
            public int faceDir;
            

            public void Reset()
            {
                above = below = false;
                left = right = false;
                climbingSlope = false;
                descendingSlope = false;

                slopeAngleOld = slopeAngle;
                slopeAngle = 0;
            }
        }
    }
}