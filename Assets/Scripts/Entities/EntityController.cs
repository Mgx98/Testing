using System.Collections;
using UnityEngine;

namespace Entities
{
    /// <summary>
    ///     This script defines the base for all entities.
    /// </summary>
    [RequireComponent(typeof(EntityStats))]
    public abstract class EntityController : MonoBehaviour
    {
        private bool _attackCooldown;

        /// <summary>
        ///     Current entity stats
        /// </summary>
        public EntityStats EntityStats { get; private set; }

        protected virtual void Awake()
        {
            EntityStats = gameObject.GetComponent<EntityStats>();
        }

        #region Helper Methods

        /// <summary>
        ///     Checks whether the other object can be seen, hit, from where the base object is standing.
        /// </summary>
        /// <param name="self"> The transform of the shooting object. </param>
        /// <param name="other"> The transform of the receiving object. </param>
        /// <param name="range"> The lenght of the ray being shot. </param>
        /// <typeparam name="T"> The type of object it is looking for. </typeparam>
        /// <returns> Returns true when other object being hit is same as the type defined. </returns>
        public static bool HitOtherTypeDefined<T>(Transform self, Transform other, float range)
        {
            var selfPosition = self.localPosition;

            var rayStart = new Vector3(selfPosition.x, selfPosition.y + 1f, selfPosition.z);
            var rayDirection = other.transform.localPosition - selfPosition;

            var ray = new Ray(rayStart, rayDirection);

            Debug.DrawRay(rayStart, rayDirection);

            Physics.Raycast(ray, out var raycastHit, range);

            return raycastHit.transform.gameObject.GetComponentInParent(typeof(T)) ||
                   raycastHit.transform.gameObject.GetComponent(typeof(T));
        }

        #endregion

        #region Combat Behaviours

        /// <summary>
        ///     Let the entity die and kill off its behaviour.
        /// </summary>
        protected abstract void Die();

        /// <summary>
        ///     Let the entity dodge an attack.
        /// </summary>
        protected abstract void Dodge();

        /// <summary>
        ///     Let the entity take damage.
        /// </summary>
        /// <param name="attackDamage"> The amount of damage </param>
        public abstract void TakeDamage(int attackDamage);

        /// <summary>
        ///     Pre-written method for a simple damage over time.
        /// </summary>
        /// <param name="entityController"> What entity to do damage on. </param>
        protected internal void TimedAttack(EntityController entityController)
        {
            if (_attackCooldown) return;

            _attackCooldown = true;

            if (HitOtherTypeDefined<EntityController>(transform, entityController.transform, EntityStats.AttackRange))
                entityController.TakeDamage(EntityStats.AttackDamage);

            StartCoroutine(AttackCooldown());
        }

        private IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(EntityStats.AttackCooldown);

            _attackCooldown = false;
        }

        #endregion
    }
}