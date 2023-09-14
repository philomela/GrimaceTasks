using Application.Common.Mappings;
using AutoMapper;
using Domain.Core;

namespace Application.Common.Models;

public class ParticipantResponse : IMapWith<Participant>
{
    public long Id { get; set; }

    //public string SocialNetworks { get; set; } //Использовать enum и везеде по проекту в дто и моделях

    public string UserName { get; set; }

    public long UserId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ParticipantResponse, Participant>()
            .ForMember(p => p.Id,
                opt => opt.MapFrom(pr => pr.Id))
            .ForMember(p => p.UserName,
                opt => opt.MapFrom(pr => pr.UserName))
            .ForMember(p => p.ParentId,
                opt => opt.MapFrom(pr => pr.UserId));
    }
}
