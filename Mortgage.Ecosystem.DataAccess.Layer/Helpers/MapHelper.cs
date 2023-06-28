using AutoMapper;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Map Helper
    public class MapHelper : Profile
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