using AutoMapper;
using Chat.Application.Dto;
using Chat.Application.Dto.Chat;
using Chat.Core.Helper;
using Chat.Domain.Entities;

namespace Chat.API.Config
{
    /// <summary>
    /// AutoMapper
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<User, UserItem>().ReverseMap();
            CreateMap<Dictionary, DictionaryDto>().ReverseMap();
            CreateMap<ChatMes, ChatMesDto>().ReverseMap();
            CreateMap<OnlineUser, OnlineUserDto>().ReverseMap();
            CreateMap<ChatMes, ChatMesItem>().ReverseMap();
            //CreateMap<Log, LogDto>().ReverseMap();

            //CreateMap<string, int>().ConvertUsing(new IntTypeStringReverseConverter());
            //CreateMap<int, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            //CreateMap<DateTime, int>().ConvertUsing(new DateTimeTypeReverseConverter());
            //CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeStringConverter());
            //CreateMap<DateTime, string>().ConvertUsing(new DateTimeTypeStringReverseConverter());

        }
        /// <summary>
        /// DateTimeTypeConverter
        /// </summary>
        public class DateTimeTypeConverter : ITypeConverter<int, DateTime>
        {
            /// <summary>
            /// DateTimeTypeConverter
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            public DateTime Convert(int source, DateTime destination, ResolutionContext context)
            {
                return UnixTime.FromTimeStamp(source);
            }
        }

        /// <summary>
        /// DateTimeTypeReverseConverter
        /// </summary>
        public class DateTimeTypeReverseConverter : ITypeConverter<DateTime, int>
        {
            /// <summary>
            /// Convert
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            public int Convert(DateTime source, int destination, ResolutionContext context)
            {
                return UnixTime.ToTimeStamp(source);
            }
        }

        public class DateTimeTypeStringConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(string source, DateTime destination, ResolutionContext context)
            {
                return UnixTime.FromTimeString(source);
            }
        }

        public class DateTimeTypeStringReverseConverter : ITypeConverter<DateTime, string>
        {
            public string Convert(DateTime source, string destination, ResolutionContext context)
            {
                return UnixTime.ToTimeString(source);
            }
        }

        public class IntTypeStringConverter : ITypeConverter<int, string>
        {
            public string Convert(int source, string destination, ResolutionContext context)
            {
                string result = "";
                if (source < 1000000000)
                {
                    result = source.ToString();
                }
                else
                {
                    result = UnixTime.FromTimeStamp(source).ToString();
                }
                return result;
            }
        }

        public class IntTypeStringReverseConverter : ITypeConverter<string, int>
        {
            public int Convert(string source, int destination, ResolutionContext context)
            {
                int result = 0;
                if (int.Parse(source) < 1000000000)
                {
                    result = int.Parse(source);
                }
                else
                {
                    DateTime date = UnixTime.FromTimeString(source);
                    result = UnixTime.ToTimeStamp(date);
                }
                return result;

            }
        }
    }
}
