using Yetibyte.IgdbSharp.Data.Images;

namespace Yetibyte.IgdbSharp.Data.GameEngines
{
    /// <summary>
    /// The logo of a <see cref="GameEngine"/>.
    /// </summary>
    public class GameEngineLogo : Entity, IImage
    {
        public bool AlphaChannel { get; set; }
        public bool Animated { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }

        public string ImageId { get; set; }
        public string Url { get; set; }

        /// <summary>
        /// Converts the given <see cref="Int64"/> value to a new instance of <see cref="GameEngineLogo"/> by using the value
        /// for its Id property. All other properties will be set to their respective default value.
        /// </summary>
        /// <param name="id"></param>
        public static explicit operator GameEngineLogo(long id) => new GameEngineLogo { Id = id };
    }
}
