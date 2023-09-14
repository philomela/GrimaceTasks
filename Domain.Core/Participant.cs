using Domain.Core.Dicitionarys;

namespace Domain.Core;

public partial record Participant
{
    public long Id { get; set; } //Id в телеграмме

    //public string SocialNetworks { get; set; } //Использовать

    public string UserName { get; set; } //Имя пользователя в соцсети.

    public long ParentId { get; set; }

    //public UserParticipant User { get; set; }

    //public record UserParticipant
    //{
    //    public long Id { get; set; }
    //}
}