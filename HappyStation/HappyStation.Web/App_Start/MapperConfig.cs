using Autofac;

using AutoMapper;

using HappyStation.Core.Entities;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.App_Start
{
    public class MapperConfig
    {
        public static void Register(IContainer container)
        {
            Mapper.Initialize(map =>
                              {
                                  map.ConstructServicesUsing(container.Resolve);

                                  map.CreateMap<Service, ServiceViewModel>();
                                  map.CreateMap<ServiceViewModel, Service>();
                                  map.CreateMap<News, NewsViewModel>();
                                  map.CreateMap<NewsViewModel, News>();
                                  map.CreateMap<Comment, CommentVewModel>();
                                  map.CreateMap<CommentVewModel, Comment>();
                                  map.CreateMap<Photo, PhotoViewModel>();
                                  map.CreateMap<PhotoViewModel, Photo>();
                                  map.CreateMap<PhotoAlbum, PhotoAlbumViewModel>();
                                  map.CreateMap<PhotoAlbumViewModel, PhotoAlbum>();
                              });
        }
    }
}