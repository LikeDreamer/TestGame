using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AltarOfSword
{
    public class SpinePhysical : MonoBehaviour
    {
        public new SkeletonAnimation animation;
        public Skeleton skeleton ;
        public BoxCollider2D BoxCollider;
        public float[] vertexBuffer;
        void Start()
        {
            animation=this.GetComponent<SkeletonAnimation>();
            skeleton=animation.skeleton;
            BoxCollider = this.gameObject.AddComponent<BoxCollider2D>();

            //Physics2D
            //Physics.
            //Rigidbody
            //Rigidbody2D
            
        }

        void Update()
        {
            
            Vector2 site = new Vector2();
            Vector2 size = new Vector2();

            skeleton.GetBounds(out site.x, out site.y, out size.x, out size.y, ref vertexBuffer);
            BoxCollider.size = size;
            site = site + size / 2.0f;
            site.y = size.y / 2.0f;
            BoxCollider.offset = new Vector2(skeleton.RootBone.WorldX, skeleton.RootBone.WorldY);
            site = (Vector2)this.transform.position + site;
        }
    }
}
