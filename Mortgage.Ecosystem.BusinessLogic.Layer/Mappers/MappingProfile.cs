using AutoMapper;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Contact, ContactDto>();
            //CreateMap<Company, CompanyDto>()
            //    .ForMember(c => c.FullAddress,
            //        opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            //CreateMap<Employee, EmployeeDto>();

            //CreateMap<CompanyForCreationDto, Company>();

            //CreateMap<EmployeeForCreationDto, Employee>();

            //CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();

            //CreateMap<CompanyForUpdateDto, Company>();

            //CreateMap<UserForRegistrationDto, User>();
        }
    }

    public class MapperHelper : Profile
    {
        public static TDestination Merge<TSource, TDestination>(TSource entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>().ReverseMap();
            });
            var _mapper = config.CreateMapper();
            return _mapper.Map<TDestination>(entity);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>().ReverseMap();
            });
            var _mapper = config.CreateMapper();
            destination = _mapper.Map<TDestination>(source);
            return destination;
        }

        public static TDestination Merge<TSource, TDestination>(TSource source, TDestination destination)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            }).CreateMapper();
            return config.Map(source, destination);
        }
		
        public static List<TDestination> MapList<TDestination, TSource>(List<TSource> entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>().ReverseMap();
            });
            var _mapper = config.CreateMapper();
            return _mapper.Map<List<TDestination>>(entity);
        }
    }
}
