using AutoMapper;
using AutoMapper;
using MovieBooking.API.DTOs.Auth;
using MovieBooking.API.DTOs.Phim;
using MovieBooking.API.Entities;

namespace MovieBooking.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Auth Mappings
            CreateMap<RegisterRequestDto, NguoiDung>();
            CreateMap<NguoiDung, LoginResponseDto>();

            // Phim Mappings
            CreateMap<Phim, PhimDto>();
            CreateMap<PhimCreateDto, Phim>();
            CreateMap<PhimUpdateDto, Phim>();

            // Banner Mappings
            CreateMap<Banner, BannerDto>()
                .ForMember(dest => dest.TenPhim, opt => opt.MapFrom(src => src.Phim != null ? src.Phim.TenPhim : null));
        }
    }
}
