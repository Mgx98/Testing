using UnityEngine;

namespace Entities.Enemies
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] protected float debugDrawRadius = 1f;

        public virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

        }
    }
}