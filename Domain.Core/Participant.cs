using Domain.Core.Dicitionarys;

namespace Domain.Core;

public record Participant
{
    public long Id { get; set; }

    public SocialNetworks SocialNetworks { get; set; }

    public string UserName { get; set; }
}
