using AutoMapper;
using Yummy.WebAPI.Dtos.AboutDto;
using Yummy.WebAPI.Dtos.CategoryDto;
using Yummy.WebAPI.Dtos.ChefDto;
using Yummy.WebAPI.Dtos.ContactDto;
using Yummy.WebAPI.Dtos.DashboardDto;
using Yummy.WebAPI.Dtos.EmployeeTaskDto;
using Yummy.WebAPI.Dtos.FeatureDto;
using Yummy.WebAPI.Dtos.ForProfileInTheAdminPageDto;
using Yummy.WebAPI.Dtos.GalleryDto;
using Yummy.WebAPI.Dtos.HeroDto;
using Yummy.WebAPI.Dtos.LoginDto;
using Yummy.WebAPI.Dtos.MessageDto;
using Yummy.WebAPI.Dtos.MessageDto.MessageDtoForAdminThema.MessageListForNavbarSection;
using Yummy.WebAPI.Dtos.ProductDto;
using Yummy.WebAPI.Dtos.ProfileDto;
using Yummy.WebAPI.Dtos.ReservationDto;
using Yummy.WebAPI.Dtos.TestimonialDto;
using Yummy.WebAPI.Dtos.YummyEventsDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebApi.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Hero, ResultHeroDto>().ReverseMap();
            CreateMap<Hero, CreateHeroDto>().ReverseMap();
            CreateMap<Hero, GetHeroByIdDto>().ReverseMap();
            CreateMap<Hero, UpdateHeroDto>().ReverseMap();

            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, GetAboutByIdDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();

            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, GetFeatureByIdDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();

            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<Product, ResultProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.DataTarget, opt => opt.MapFrom(src => src.Category.DataTarget))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.CategoryId))
                .ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, GetProductByIdDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();

            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetTestimonialByIdDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();

            CreateMap<Chef, ResultChefDto>().ReverseMap();
            CreateMap<Chef, CreateChefDto>().ReverseMap();
            CreateMap<Chef, GetChefByIdDto>().ReverseMap();
            CreateMap<Chef, UpdateChefDto>().ReverseMap();
            CreateMap<Chef, ResultGetChef>().ForMember(desc => desc.TaskCount, opt => opt.MapFrom(src => src.ChefTasks.Count)).ReverseMap();

            CreateMap<Reservation, ResultReservationDto>().ReverseMap();
            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();

            CreateMap<Gallery, ResultGalleryDto>().ReverseMap();
            CreateMap<Gallery, CreateGalleryDto>().ReverseMap();
            CreateMap<Gallery, GetGalleryByIdDto>().ReverseMap();
            CreateMap<Gallery, UpdateGalleryDto>().ReverseMap();

            CreateMap<Contact, ResultContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, GetContactByIdDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();

            CreateMap<Message, ResultMessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, GetMessageByIdDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            CreateMap<Message, MessageListByIsReadFalseDto>().ReverseMap();
            CreateMap<Message, ResultGetMessage>().ReverseMap();

            CreateMap<YummyEvents, ResultYummyEventDto>().ReverseMap();
            CreateMap<YummyEvents, CreateYummyEventDto>().ReverseMap();
            CreateMap<YummyEvents, GetYummyEventByIdDto>().ReverseMap();
            CreateMap<YummyEvents, UpdateYummyEventDto>().ReverseMap();

            CreateMap<EmployeeTask, ResultEmployeeTaskDto>().ForMember(dest => dest.ChefImageUrls, opt => opt.MapFrom(src => src.ChefTasks.Select(ct => ct.Chef.ImageUrl).ToList()));
            CreateMap<EmployeeTask, GetEmployeeTaskByIdDto>().ForMember(dest => dest.ChefIds, opt => opt.MapFrom(src => src.ChefTasks.Select(ct => ct.ChefId).ToList()));
            CreateMap<EmployeeTask, CreateEmployeeTaskDto>();
            CreateMap<EmployeeTask, UpdateEmployeeTaskDto>();

            CreateMap<AppUser, RegisterDto>()
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore());
            CreateMap<AppUser, ProfileDto>();
            CreateMap<AppUser, UpdateProfileDto>()
                .ForMember(dest => dest.CurrentUsername, opt => opt.MapFrom(src => src.UserName));

            CreateMap<UpdateProfileDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        }
    }
}
