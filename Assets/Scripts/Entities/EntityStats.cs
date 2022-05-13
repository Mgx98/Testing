using UnityEngine;

namespace Entities
{
    /// <summary>
    ///     This script holds all the stats of entities.
    /// </summary>
    public class EntityStats : MonoBehaviour
    {
        #region Fields

        [Range(0f, 1f)] [SerializeField] private float armorShielding;
        [Range(0, 999)] [SerializeField] private int health;
        [Range(0, 999)] [SerializeField] private int bodyHealth;
        [Range(0, 999)] [SerializeField] private int attackDamage;
        [Range(0f, 999f)] [SerializeField] private float maxSpeed;
        [Range(0f, 999f)] [SerializeField] private float minSpeed;
        [Range(0f, 999f)] [SerializeField] private float attackRange;
        [Range(0f, 999f)] [SerializeField] private float sight;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float dodgeCooldown;

        #endregion

        #region Properties

        /// <summary>
        ///     The current state of the body defined in a number.
        /// </summary>
        public int BodyHealth
        {
            get => bodyHealth;
            set
            {
                // Lock body health to zero
                if (value >= 0) bodyHealth = value;
            }
        }

        /// <summary>
        ///     The current health.
        /// </summary>
        public int Health
        {
            get => health;
            set
            {
                // Lock body health to zero
                if (value >= 0) health = value;
            }
        }

        /// <summary>
        ///     The cooldown after an attack in seconds.
        /// </summary>
        public float AttackCooldown => attackCooldown;

        /// <summary>
        ///     The damage on attacking another entity.
        /// </summary>
        public int AttackDamage => attackDamage;

        /// <summary>
        ///     Percentage of damage being withheld.
        /// </summary>
        public float ArmorShielding => armorShielding;

        /// <summary>
        ///     Maximum speed the entity can go.
        /// </summary>
        public float MaxSpeed => maxSpeed;

        /// <summary>
        ///     Minimum speed of the entity.
        /// </summary>
        public float MinSpeed => minSpeed;

        /// <summary>
        ///     The cooldown after a dodge in seconds.
        /// </summary>
        public float DodgeCooldown => dodgeCooldown;

        /// <summary>
        ///     The range within an entity can do damage.
        /// </summary>
        public float AttackRange => attackRange;

        /// <summary>
        ///     The range within and entity can notice things.
        /// </summary>
        public float Sight => sight;

        #endregion
    }
}