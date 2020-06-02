using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1 
{
	public class Punch : MonoBehaviour 
	{
		Main self;
        public LayerMask HitLayer;
        public BoxCollider2D hitBox1;
        public BoxCollider2D hitBox2;
        public int dmg = 2;
        static int numColliders = 2;
        Animator Animator;
        public int _attackFrames = 4;
        public float _colliderSizeX = 1;
        


        void Start()
        {
            hitBox1.enabled = false;
            self = this.transform.parent.GetComponent<Main>();
            Animator = this.transform.parent.GetComponent<Animator>();
            // this.transform.localScale = new Vector3(0,1,1);
        }
        public void Check()
        {
            if(self.InputManager.LastInputDown("PUNCH") && !self.state.isAttackLagging)
            {
				Debug.Log("punch");
                Attack();
            }
        }

        public void Attack()
        {
        	StartCoroutine(IPunch());
        }


        public IEnumerator IPunch()
        {
            Animator.Play("SwordAttack",-1,0f);
            hitBox1.enabled = true;
            // this.transform.localScale = new Vector3(0,1,1);
            Collider2D[] cols = new Collider2D[numColliders]; // throttle number of colliders
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(HitLayer);
            
            int i = 1;
			while (i <= _attackFrames)
            {
                int colliderCount = hitBox1.OverlapCollider(contactFilter, cols);
                // this.transform.localScale = new Vector3((i*_colliderSizeX/_attackFrames),1f,1f);
                if (cols != null)
                {
                    foreach (Collider2D c in cols)
                    {
                        if (c != null)
                        {
                            Debug.Log(c.name);
                            switch(c.tag)
							{
								case "Enemy":
									c.GetComponent<Enemy.Main>().Attacked(dmg);
									break;
							}
                        }
                    }
                }
                i++;
                yield return null;
            }
            hitBox1.enabled = false;
        }
	}
}
