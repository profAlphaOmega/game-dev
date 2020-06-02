using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
    public class Attack : MonoBehaviour
    {

        [System.Serializable]
        public class hbx
        {
            public BoxCollider2D hitBox;
            public int frames = 0;
        }


        private static int numColliders = 2;
        private string[] matchtags;
        private List<Collider2D> results = new List<Collider2D>(); 
        public LayerMask HitLayer;
        public hbx[] hitboxes;


        void Start()
        {
            foreach (hbx h in hitboxes)
            {
				h.hitBox.enabled = false;
            }
        }

        public List<Collider2D> Check(params string[] matchTags)
        {
            /// <summary>
            /// Checks to see if the hitboxes of the gameObject collided with any other objects and returns an array of the collided.
            /// </summary>
            /// <para>Give it an array of matchable tags to collide with. Also specify how many Colliders there are and how long each lasts</para>
            
            results = new List<Collider2D>();
            matchtags = matchTags;
            StartCoroutine(IRun());
            return results;
        }


        public IEnumerator IRun()
        {
            /// <summary>
            /// Takes array of hitboxes cycles through each for their specified frame length. Returns an array of colliders that represented the collided
            /// </summary>
            /// <para>Take an array of tags that the collider can match with. There is also a throttling of how many can collide at once</para>
            
            foreach(hbx h in hitboxes)
            {
                Collider2D[] cols = new Collider2D[numColliders]; // throttle number of colliders
                ContactFilter2D contactFilter = new ContactFilter2D();
                contactFilter.SetLayerMask(HitLayer);
                h.hitBox.enabled = true;
                int colliderCount = h.hitBox.OverlapCollider(contactFilter, cols);

                for (var i=1; i <= h.frames; i++)
                {
                    if (cols != null)
                    {
                        foreach (Collider2D c in cols)
                        {
                            if (c != null)
                            {
                                foreach (string s in matchtags)
                                {
                                    if(c.CompareTag(s))
                                    {
                                        h.hitBox.enabled = false;
                                        i = h.frames + 1;
                                        results.Add(c);
                                        goto EXIT; // potential comment this out and let the thing that is being collided with handle its frequency of collision
                                    }
                                }
                            }
                        }
                    }
                yield return null;

                }
            h.hitBox.enabled = false;
            }
        EXIT:
            Debug.Log("EXIT : " + results.Count);
        }
    }
}