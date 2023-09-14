using Domain.Core.Dicitionarys;
namespace Domain.Core;

public record ApiLog
{
    public long Id { get; set; }

    public string Request { get; set; }

    public string Response { get; set; }

    public string MethodType { get; set; }

    public string NameMethod { get; set; }

    public DateTime CreateDate { get; set; }
}
