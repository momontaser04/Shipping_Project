using AutoMapper;
using BL.Dtos;
using Domains;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<TbCarriers, CarrierDto>().ReverseMap();
            CreateMap<TbCities, CityDto>().ReverseMap();
            CreateMap<VwCities, CityDto>().ReverseMap();
            CreateMap<TbCountries, CountryDto>().ReverseMap();
            CreateMap<TbRefreshTokens, RefreshTokenDto>().ReverseMap();
            CreateMap<TbPaymentMethods, PaymentMethodDto>().ReverseMap();
            CreateMap<TbSetting, SettingDto>().ReverseMap();
            CreateMap<TbShippingTypes, ShippingTypeDto>().ReverseMap();
            CreateMap<TbShippments, ShippmentDto>().ReverseMap();
            CreateMap<TbShippmentStatus, ShippmentStatusDto>().ReverseMap();
            CreateMap<TbSubscriptionPackages, SubscriptionPackageDto>().ReverseMap();
            CreateMap<TbUserSenders, UserSenderDto>().ReverseMap();
            CreateMap<TbUserReceivers, UserReceiverDto>().ReverseMap();
            CreateMap<TbUserSubscriptions, UserSubscriptionDto>().ReverseMap();
            CreateMap<TbShipingPackging, ShippingPackgingDto>().ReverseMap();

        }
    }
}
