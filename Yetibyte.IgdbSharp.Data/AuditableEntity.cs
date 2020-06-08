using System;

namespace Yetibyte.IgdbSharp.Data
{
    /// <summary>
    /// The base type for any uniquely identifyable object retrievable from IGDB, that has properties for keeping track
    /// of when it was last modified and when it was created.
    /// </summary>
    public abstract class AuditableEntity : Entity
    {
        #region Props

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        #endregion

        #region Ctors

        protected AuditableEntity() { }

        #endregion

    }

}
