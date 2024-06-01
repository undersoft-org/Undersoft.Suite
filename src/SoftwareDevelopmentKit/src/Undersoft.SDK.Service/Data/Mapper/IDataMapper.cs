using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;

namespace Undersoft.SDK.Service.Data.Mapper
{
    public interface IDataMapper : IMapper
    {
        MapperConfigurationExpression MapperExtension { get; }

        IDataMapper Build();

        bool TryCreateMap<TSource, TDestination>(bool reverse = true);
        bool TryCreateMap<TDestination>(object source, bool reverse = true);
        bool TryCreateMap(Type source, Type destination, bool reverse = true);
        bool TryCreateMap<TDestination>(IQueryable source, bool reverse = true);

        bool MapExist<TSource, TDestination>();
        bool MapNotExist<TSource, TDestination>();
        bool MapNotExist<TDestination>(object source);
        bool MapExist<TDestination>(object source);
        bool MapExist(Type source, Type destination);
        bool MapExist(IQueryable source, Type destination);
        bool MapExist<TDestination>(IQueryable source);
        bool MapNotExist(Type source, Type destination);
    }
}