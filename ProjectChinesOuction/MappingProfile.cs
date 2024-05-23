using AutoMapper;
using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

namespace ProjectChinesOuction
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Gift, PresentDTO>();
            CreateMap<PresentDTO, Gift>();

            CreateMap<Donor, DonorDTO>();
            CreateMap<DonorDTO, Donor>();

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();

            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<OrderItemDTO, OrderItem>();

            CreateMap<Raffle, RaffleDTO>();
            CreateMap<RaffleDTO, Raffle>();

            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<Winner, WinnerDTO>().ReverseMap();

        }
    }
}
