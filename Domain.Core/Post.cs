using Domain.Core.Dicitionarys;

namespace Domain.Core
{
    public record Post
    {
        public long Id { get; set; }

        public int Points { get; set; }

        public string Url { get; set; }

        public SocialNetworks SocialNetwork { get; set; }
    }
}