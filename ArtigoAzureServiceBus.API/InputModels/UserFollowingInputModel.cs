using System;

namespace ArtigoAzureServiceBus.API.InputModels
{
    public class UserFollowingInputModel
    {
        public int IdUserFollower { get; set; }
        public int IdUserFollowee { get; set; }
        public DateTime FollowedAt { get; set; } = DateTime.Now;
        public string Email { get; set; } = "umemail@email.com";
    }
}
