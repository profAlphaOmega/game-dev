using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
    public class SwordAttack : MonoBehaviour {

        Main self;
        // public GameObject alucard;
        public LayerMask HitLayer;
        public Collider2D hitBox;
        public int dmg = 2;
        static int numColliders = 2;
        Animator Animator;
        // GameEvent SwordAttackHit;

        void Start()
        {
            hitBox.enabled = false;
            self = this.transform.parent.GetComponent<Main>();
            Animator = this.transform.parent.GetComponent<Animator>();
        }
        public void Check()
        {
            if(self.InputManager.LastInputDown("ATTACK") && !self.state.isAttackLagging)
            {
                Attack();
            }
        }

        public void Attack()
        {
            Animator.Play("SwordAttack",-1,0f);
            hitBox.enabled = true;

            Collider2D[] cols = new Collider2D[numColliders]; // throttle number of colliders
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(HitLayer);

            int colliderCount = hitBox.OverlapCollider(contactFilter, cols);
            // Debug.Log("Number of Colliders Hit: " + cols.Length);
            if (cols != null)
            {
                foreach (Collider2D c in cols)
                {
                    if (c != null)
                    {
                        Debug.Log(c.name);
                        // if (c.CompareTag("Enemy"))
                        switch(c.tag)
                            {
                                case "Enemy":
                                    c.GetComponent<Enemy.Main>().Attacked(dmg);
                                    break;
                            }
                    }
                }
            }
            hitBox.enabled = false;
        }
    }
}