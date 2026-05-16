using AutoMapper;
using MovieBooking.API.DTOs.Auth;
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

            // Phim Mappings (s? thêm sau)
        }
    }
}
