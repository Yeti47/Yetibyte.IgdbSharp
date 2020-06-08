using System;
using System.Collections.Generic;
using System.Text;
using Yetibyte.IgdbSharp.Data.Companies;

namespace Yetibyte.IgdbSharp.Data.GameEngines
{
    /// <summary>
    /// Describes a video game engine.
    /// </summary>
    public class GameEngine : Entity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public List<Company> Companies { get; } = new List<Company>();
       
        public string Url { get; set; }

        public GameEngineLogo Logo { get; set; }

        /// <summary>
        /// Converts the given <see cref="Int64"/> value to a new instance of <see cref="GameEngine"/> by using the value
        /// for its Id property. All other properties will be set to their respective default value.
        /// </summary>
        /// <param name="id"></param>
        public static explicit operator GameEngine(long id) => new GameEngine { Id = id };


    }
}
