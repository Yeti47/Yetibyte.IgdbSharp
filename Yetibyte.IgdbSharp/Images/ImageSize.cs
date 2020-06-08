using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.IgdbSharp.Images
{
    public sealed class ImageSize : IEquatable<ImageSize>
    {

        #region Static Fields

        #endregion

        #region Static Props

        public static ImageSize CoverSmall { get; } = new ImageSize("cover_small", "Cover Small", 90, 128, "Fit");
        public static ImageSize ScreenshotMed { get; } = new ImageSize("screenshot_med", "Screenshot Medium", 569, 320, "Lfill, Center gravity");
        public static ImageSize CoverBig { get; } = new ImageSize("cover_big", "Cover Big", 264, 374, "Fit");
        public static ImageSize LogoMed { get; } = new ImageSize("logo_med", "Logo Medium", 284, 160, "Fit");
        public static ImageSize ScreenshotBig { get; } = new ImageSize("screenshot_big", "Screenshot Big", 889, 500, "Lfill, Center gravity");
        public static ImageSize ScreenshotHuge { get; } = new ImageSize("screenshot_huge", "Screenshot Huge", 1280, 720, "Lfill, Center gravity");
        public static ImageSize Thumb { get; } = new ImageSize("thumb", "Thumbnail", 90, 90, "Thumb, Center gravity");
        public static ImageSize Micro { get; } = new ImageSize("micro", "Micro", 35, 35, "Thumb, Center gravity");
        public static ImageSize P720 { get; } = new ImageSize("720p", "720p", 1280, 720, "Fit, Center gravity");
        public static ImageSize P1080 { get; } = new ImageSize("1080p", "1080p", 1920, 1080, "Fit, Center gravity");

        #endregion

        #region Props

        public string Name { get; }
        public string FriendlyName { get; }

        public int Width { get; }
        public int Height { get; }

        public int Size => Width * Height;

        public string Description { get; }

        #endregion

        #region Ctors

        private ImageSize(string name, string friendlyName, int width, int height, string description)
        {
            Name = name;
            FriendlyName = friendlyName;
            Width = width;
            Height = height;
            Description = description;
        }

        #endregion

        #region Operators

        public static bool operator ==(ImageSize a, ImageSize b)
        {
            if (ReferenceEquals(a, b))
                return true;

            return !(a is null) && a.Equals(b);
        }

        public static bool operator !=(ImageSize a, ImageSize b) => !(a == b);

        #endregion

        #region Methods

        public override bool Equals(object obj) => obj is ImageSize imageSize && imageSize.Equals(this);

        public bool Equals(ImageSize other) => other?.Name == Name;

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public override string ToString() => $"{Name} ({Width} x {Height})";

        #endregion
    }
}
