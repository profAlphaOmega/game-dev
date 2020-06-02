using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
    public class HighKick : MonoBehaviour
    {

        Main self;
        // public GameObject alucard;
        public LayerMask HitLayer;
        public BoxCollider2D hitBox1;
        public BoxCollider2D hitBox2;
        public int dmg = 2;
        static int numColliders = 2;
        Animator Animator;
        public int _kickframes = 4;
        // public float _colliderSizeX = 1;
        // GameEvent SwordAttackHit;


        void Start()
        {
            hitBox1.enabled = false;
            self = this.transform.parent.GetComponent<Main>();
            Animator = this.transform.parent.GetComponent<Animator>();
            // this.transform.localScale = new Vector3(0,1,1);
        }
        public void Check()
        {
            if (self.InputManager.LastInputDown("HIGHKICK") && !self.state.isAttackLagging)
            {
                Attack();
            }
        }

        public void Attack()
        {
            // Animator.Play("SwordAttack",-1,0f);
            // hitBox.enabled = true;
            StartCoroutine(IHighKick());
            // Collider2D[] cols = new Collider2D[numColliders]; // throttle number of colliders
            // ContactFilter2D contactFilter = new ContactFilter2D();
            // contactFilter.SetLayerMask(HitLayer);

            // int colliderCount = hitBox.OverlapCollider(contactFilter, cols);
            // Debug.Log("Number of Colliders Hit: " + cols.Length);
            // if (cols != null)
            // {
            //     foreach (Collider2D c in cols)
            //     {
            //         if (c != null)
            //         {
            //             Debug.Log(c.name);
            //             // if (c.CompareTag("Enemy"))
            //             switch(c.tag)
            //                 {
            //                     case "Enemy":
            //                         c.GetComponent<Enemy.Health>().ReduceHealth(dmg);
            //                         break;
            //                 }
            //         }
            //     }
            // }
            // hitBox.enabled = false;
        }


        public IEnumerator IHighKick()
        {
            Debug.Log("HighKick");
            Animator.Play("SwordAttack", -1, 0f);
            // this.transform.localScale = new Vector3(0,1,1);
            Collider2D[] cols = new Collider2D[numColliders]; // throttle number of colliders
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(HitLayer);


            int i = 1;
            while (i <= _kickframes)
            {
                if (i <= _kickframes / 2)
                {
                    hitBox1.enabled = true;
                    hitBox2.enabled = false;
                    // this.transform.localScale = new Vector3((i*_colliderSizeX/_kickframes),1f,1f);
                    int colliderCount1 = hitBox1.OverlapCollider(contactFilter, cols);
                    if (cols != null)
                    {
                        foreach (Collider2D c in cols)
                        {
                            if (c != null)
                            {
                                Debug.Log("1st collider hit " + c.name);
                                // if (c.CompareTag("Enemy"))
                                switch (c.tag)
                                {
                                    case "Enemy":
                                        c.GetComponent<Enemy.Main>().Attacked(dmg);
                                        i = _kickframes + 1; // this is to only record 1 frame collision and avoid both colliders from doing damage
                                        break;
                                }
                            }
                        }
                    }
                    i++;
                    yield return null;
                }
                else
                {
                    hitBox1.enabled = false;
                    hitBox2.enabled = true;
                    int colliderCount2 = hitBox2.OverlapCollider(contactFilter, cols);
                    if (cols != null)
                    {
                        foreach (Collider2D c in cols)
                        {
                            if (c != null)
                            {
                                Debug.Log("2nd collider hit " + c.name);
                                // if (c.CompareTag("Enemy"))
                                switch (c.tag)
                                {
                                    case "Enemy":
                                        c.GetComponent<Enemy.Main>().Attacked(dmg);
                                        i = _kickframes + 1; // this is to only record 1 frame collision and avoid both colliders from doing damage
                                        break;
                                }
                            }
                        }
                    }
                    i++;
                    yield return null;
                }

                // int colliderCount = hitBox1.OverlapCollider(contactFilter, cols);
                // this.transform.localScale = new Vector3((i*_colliderSizeX/_kickframes),1f,1f);
            }
            hitBox1.enabled = false;
            hitBox2.enabled = false;
        }
    }
}