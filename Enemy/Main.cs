using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enemy
{
    [RequireComponent(typeof(Controller2D))]
    public class Main : MonoBehaviour {

        public Controller2D controller;
        public EnemyState state;
        public Vector2 velocity;
        public Health health;

        public float moveSpeed = 1; // this is a multiplier
        public float chaseSpeed = 6f; // this is a multiplier
        
        public float combatDistance = 1f; // ScriptableObject???
        
        public float jumpHeight = 2.4f;
        public float timeToJumpApex = .50f;
        
        public bool facingRight = false;
       
        private float gravity = -10;
        private float velocityXSmoothing;
        public float smoothTime;
        public float accelerationTimeAirborne = .05f;
        public float accelerationTimeGrounded = .01f;
        public IFrames iframes;

        void Start () 
        {
            controller = GetComponent<Controller2D>();
            health = GetComponent<Health>();
            iframes = GetComponent<IFrames>();
            gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2); // Calculate gravity
            state = new EnemyState();
            // FloatVariable newSO = ScriptableObject.CreateInstance<FloatVariable>();
            // AssetDatabase.CreateAsset(newSO, "Assets/Resources/SO/test.asset");

        }
        
        void Update () 
        {




            if(!state.busy)
            {
                smoothTime = (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne;
                Move(velocity: ref velocity, finalVelocity: 0, smoothTime: smoothTime);
            }
        }


        public void Attacked(float dmg)
        {
            if (!iframes._isInvincible)
			{
				StartCoroutine(iframes.StartIFrames());
                health.ReduceHealth(dmg);
			}


        }

        public void Move(ref Vector2 velocity, float finalVelocity, float smoothTime)
        {    
            CollisionCheck(ref velocity);
            velocity.y += gravity * Time.deltaTime;
            velocity.x = Mathf.SmoothDamp(velocity.x, finalVelocity, ref velocityXSmoothing, smoothTime);
            controller.Move(velocity * Time.deltaTime);
        }

        public void Flip()
        {
            facingRight = !facingRight;
            Vector2 theScale = this.transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void CollisionCheck(ref Vector2 velocity)
        {
            if (controller.collisions.above)
            {
                velocity.y = 0;
            } 
            if (controller.collisions.below)
            {
                velocity.y = 0;
                state.grounded = true;
                state.falling = false;
            }
            if (!controller.collisions.below)
            {
                state.grounded = false;
                state.canPatrol = false;
                state.falling = (velocity.y < 0) ?  true :  false;    
            }
        }

    }
}
