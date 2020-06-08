using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.IgdbSharp.Data
{
    /// <summary>
    /// The base type for any uniquely identifyable object retrievable from IGDB.
    /// </summary>
    public abstract class Entity : IEquatable<Entity>
    {
        #region Constants

        private const int ARBITRARY_PRIME = 37;

        #endregion

        #region Props

        public long Id { get; set; }

        #endregion

        #region Ctors

        protected Entity() { }

        #endregion

        #region Methods

        public bool ReferenceEquals(Entity other) => ReferenceEquals(this, other);

        public virtual bool Equals(Entity other) => other != null && other.GetType() == GetType() && other.Id == Id;

        public override bool Equals(object obj) => obj is Entity otherEntity && otherEntity.Equals(this);

        public override int GetHashCode()
        {
            unchecked
            {

                return (GetType().GetHashCode() * ARBITRARY_PRIME) ^ Id.GetHashCode();

            }
        }

        #endregion

        #region Operators

        public static bool operator ==(Entity a, Entity b)
        {

            if (a is null && b is null)
                return true;

            return !(a is null) && a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        #endregion

    }

}
