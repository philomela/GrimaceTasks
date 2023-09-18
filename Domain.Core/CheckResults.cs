using Domain.Core.Dicitionarys;
namespace Domain.Core;

public record CheckResults
{
    public long Id { get; set; }

    public long ParticipantId { get; set; }

    public long TaskId { get; set; }

    public int Points { get; set; }

    public DateTime DateChecks { get; set; }

    public DateTime CreateDate { get; set; }
}
