using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.IgdbSharp.Data.Companies
{
    public class Company : Entity
    {
        public string Description { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Converts the given <see cref="Int64"/> value to a new instance of <see cref="Company"/> by using the value
        /// for its Id property. All other properties will be set to their respective default value.
        /// </summary>
        /// <param name="id"></param>
        public static explicit operator Company(long id) => new Company { Id = id };

    }
}
