// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Game.Online.API;

namespace osu.Game.Users.Drawables
{
    [LongRunningLoad]
    public class DrawableAvatar : Sprite
    {
        private readonly User user;

        [Resolved(CanBeNull = true)]
        private IAPIProvider api { get; set; }

        /// <summary>
        /// A simple, non-interactable avatar sprite for the specified user.
        /// </summary>
        /// <param name="user">The user. A null value will get a placeholder avatar.</param>
        public DrawableAvatar(User user = null)
        {
            this.user = user;

            RelativeSizeAxes = Axes.Both;
            FillMode = FillMode.Fit;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(LargeTextureStore textures)
        {
            string avatarUrl = user?.AvatarUrl;
            if (api != null && avatarUrl != null)
                Texture = textures.Get(avatarUrl);
            else if (user != null && user.Id > 1)
                Texture = textures.Get($@"https://a.ppy.sh/{user.Id}");

            Texture ??= textures.Get(@"Online/avatar-guest");
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.FadeInFromZero(300, Easing.OutQuint);
        }
    }
}
