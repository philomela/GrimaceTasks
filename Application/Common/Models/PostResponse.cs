using Application.Common.Mappings;
using AutoMapper;
using Domain.Core;

namespace Application.Common.Models;

public record PostResponse : IMapWith<Post>
{
    public long Id { get; set; }

    public int Points { get; set; }

    public string Url { get; set; }

    public DateTime Updated_at { get; set; }

    public DateTime Created_at { get; set; }

    public DateTime Expires { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PostResponse, Post>()
            .ForMember(p => p.Id,
                opt => opt.MapFrom(pr => pr.Id))
            .ForMember(p => p.Points,
                opt => opt.MapFrom(pr => pr.Points))
            .ForMember(p => p.Url,
                opt => opt.MapFrom(pr => pr.Url))
            .ForMember(p => p.UpdatedAt,
                opt => opt.MapFrom(pr => pr.Updated_at))
            .ForMember(p => p.CreateDate,
                opt => opt.MapFrom(pr => pr.Created_at))
            .ForMember(p => p.Expires,
                opt => opt.MapFrom(pr => pr.Expires));
    }
}
