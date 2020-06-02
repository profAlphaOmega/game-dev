using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
    public class GrabAttack : MonoBehaviour {
        public LayerMask HitLayer;
        public Collider2D hitBox;
        static int numColliders = 1;
        public Collider2D grabbedCol;
        public Main self;
        public ContactFilter2D contactFilter = new ContactFilter2D();
        private bool _inertiaThrow = false;
        private bool _godPress = false;
        public GameEvent Grabbed;
        

        void Start()
        {
            hitBox.enabled = false;
            self = this.transform.parent.GetComponent<Main>();
            contactFilter.SetLayerMask(HitLayer);
        }

        public void Check()
        {
            // Grab
            if(self.InputManager.LastInputDown("GRAB"))
            {
                Grab();
            }
            
            // Grabbing
            if(self.state.grabbing)
            {
                Grabbing();
            }

            // Release
            if(self.state.grabbing && self.InputManager.LastInputUp("GRAB") && self.state.grounded)
            {
                if(self.InputManager.LastInputHold("UP","DOWN","LEFT","RIGHT"))
                {
                    InertiaThrow();
                }
                else
                {
                    Release();
                }
            }
            
            // AirRelease
            if(self.state.grabbing && self.InputManager.LastInputUp("GRAB") && !self.state.grounded)
            {
                if(self.InputManager.LastInputHold("UP","DOWN","LEFT","RIGHT"))
                {
                    InertiaThrow();
                }
                else
                {
                    Release();
                }
            }
        }


        public void Grab()
        {
            hitBox.enabled = true;
            
            if(!self.state.grabbing)
            {
                // Check Collisions
                Collider2D[] cols = new Collider2D[numColliders]; // throttle number of colliders
                int colliderCount = hitBox.OverlapCollider(contactFilter, cols);
                if (cols != null)
                {
                    foreach (Collider2D c in cols)
                    {
                        if (c != null)
                        {
                            grabbedCol = c;

                            switch (grabbedCol.tag)
                            {
                                case "Enemy":
                                    self.state.grabbing = true;
                                    grabbedCol.GetComponent<Enemy.Grab>().Grabbed();
                                    c.transform.position = this.transform.position;
                                    break;
                                case "Wall":
                                    self.state.grabbing = true;
                                    self.state.canMove = false;
                                    self.state.wallGrabbing = true;
                                    break;
                            }
                        }
                        else
                        {
                            hitBox.enabled = false;
                            self.state.wallGrabbing = false;
                            self.state.grabbing = false;
                        }
                    }
                }
            }
        }

        public void Grabbing()
        {
            // Grabbing...
            if(grabbedCol != null)
            {
                switch (grabbedCol.tag)
                {
                    case "Enemy":
                        grabbedCol.GetComponent<Enemy.Grab>().Grabbed();
                        grabbedCol.transform.position = this.transform.position;
                        break;
                }
            }
        }

        public void Release()
        {
            switch(grabbedCol.tag)
            {
                case "Enemy":
                    grabbedCol.GetComponent<Enemy.Grab>().Throw(self.velocity);
                    self.state.grabbing = false;
                    break;
                case "Wall":
                    self.state.wallGrabbing = false;
                    self.state.canMove = true;
                    break;
            }
            hitBox.enabled = false;
            self.state.grabbing = false;
            grabbedCol = null;
        }

        public void InertiaThrow()
        {
            if(!_inertiaThrow) StartCoroutine(IInertiaThrow());
        }

        IEnumerator IInertiaThrow()
        {
            _inertiaThrow = true;
            self.state.canMove = false;
            yield return StartCoroutine(UTILS.WaitForFrames(1));

            float _velPlayerMoveX = 0;
            float _velPlayerMoveY = 0;
            float _velThrowX = 0;
            float _velThrowY = 0;

            if(self.InputManager.LastInputHold("RIGHT"))
            {
                _velThrowX = 20;
                _velThrowY = 5;
                _velPlayerMoveX = 2;
            }   
            if(self.InputManager.LastInputHold("LEFT"))
            {
                _velThrowX = 20;
                _velThrowY = 5;
                _velPlayerMoveX = 2;
            }   
            if(self.InputManager.LastInputHold("UP"))
            {
                _velThrowY = 15;
                _velPlayerMoveX = 0;
            }   
            if(self.InputManager.LastInputHold("DOWN"))
            {
                _velThrowY = 10;
                _velPlayerMoveX = 0;
            }   

            Vector2 _velPlayerMove = new Vector2(_velPlayerMoveX * -self.state.dirX, _velPlayerMoveY);
            Vector2 _velThrow = new Vector2(_velThrowX * self.state.dirX, _velThrowY * self.state.dirY);

            switch(grabbedCol.tag)
                {
                    case "Enemy":
                        grabbedCol.GetComponent<Enemy.Grab>().Throw(_velThrow);
                        // _velPlayerMove.x = self.ProcessVelocityX(_velPlayerMove.x, 0, .05f);
                        // self.controller.Move(_velPlayerMove);
                        self.MoveY(targetVelocityY: 0,velocity: ref self.velocity,timebeta: .05f);
                        self.state.grabbing = false;
                        break;
                    case "Wall":
                        self.state.wallGrabbing = false;
                        break;
                }

            hitBox.enabled = false;
            self.state.canMove = true;
            _inertiaThrow = false;
        }

        // put in logic for direction and multiplier or additive value
        public void GodPress()
        {
            if(!_godPress) StartCoroutine(IGodPress());
        }
        IEnumerator IGodPress()
        {
            _godPress = true;
            self.state.canMove = false;

            float _velPlayerMoveX = 5;
            Vector2 _velPlayerMove = new Vector2(_velPlayerMoveX * self.state.dirX, self.velocity.y);
            
            yield return StartCoroutine(UTILS.WaitForFrames(10));
            while (Mathf.Abs(_velPlayerMove.x) > 0.75f)
            {
                // _velPlayerMove.x = self.ProcessVelocityX(_velPlayerMove.x, 0, .05f);
                // self.controller.Move(_velPlayerMove);
                self.MoveY(targetVelocityY: 0,velocity: ref self.velocity,timebeta: .05f);
            }
            yield return StartCoroutine(UTILS.WaitForFrames(10));
            
            switch(grabbedCol.tag)
            {
                case "Enemy":
                    grabbedCol.GetComponent<Enemy.Grab>().Throw(new Vector2(self.state.dirX * 10,0));
                    self.state.grabbing = false;
                    break;
                default:
                    break;
            }
            
            hitBox.enabled = false;
            grabbedCol = null;
            self.state.grabbing = false;
            self.state.canMove = true;
            _godPress = false;
        }
    }
}
