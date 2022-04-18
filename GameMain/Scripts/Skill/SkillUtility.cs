using UnityEngine;

namespace AltarOfSword
{
    public static class SkillUtility
    {
        public static void Set(this BoxCollider2D collider, int type, float value)
        {
            if (collider == null) return;
            Vector2 site = collider.offset;
            Vector2 size = collider.size;
            switch (type)
            {
                case 0: site.x = value; break;
                case 1: site.y = value; break;
                case 2: size.x = value; break;
                case 3: size.y = value; break;
                default: break;
            }
            collider.offset = site;
            collider.size = size;
        }

        public static void Set(this CapsuleCollider2D collider, int type, float value)
        {
            if (collider == null) return;
            Vector2 site = collider.offset;
            Vector2 size = collider.size;
            switch (type)
            {
                case 0: site.x = value; break;
                case 1: site.y = value; break;
                case 2: size.x = value; break;
                case 3: size.y = value; break;
                default: break;
            }
            collider.offset = site;
            collider.size = size;
        }

        public static float Get(this BoxCollider2D collider, int type)
        {
            if (collider == null) return 0.0f;
            switch (type)
            {
                case 0: return collider.offset.x;
                case 1: return collider.offset.y;
                case 2: return collider.size.x;
                case 3: return collider.size.y;
                default: return 0.0f;
            }
        }

        public static float Get(this CapsuleCollider2D collider, int type)
        {
            if (collider == null) return 0.0f;
            switch (type)
            {
                case 0: return collider.offset.x;
                case 1: return collider.offset.y;
                case 2: return collider.size.x;
                case 3: return collider.size.y;
                default: return 0.0f;
            }
        }

    }
}
