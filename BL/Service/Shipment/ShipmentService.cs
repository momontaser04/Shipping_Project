using AutoMapper;
using BL.Contract;
using BL.Contract.Shipment;
using BL.Dtos;
using BL.Service;
using BL.Service.Shipment;
using DAL.Contract;
using DAL.Models;
using Domains;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ShipmentService : BaseService<TbShippments, ShippmentDto>, IShipment
    {
        private readonly IUserSender _userSender;
        private readonly IUserReceiver _userReceiver;
        private readonly IRateCalculator _rateCalculator;
        private readonly ITrackingNumberCreator _trackingNumberCreator;
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<TbShippments> _repo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        IShipmentStatus _ShipmentStatus;
        public ShipmentService(IGenericRepository<TbShippments> repo,IMapper mapper,IUserService userService,IUserReceiver userReceiver,IUserSender userSender,
           ITrackingNumberCreator trackingNumberCreator,IRateCalculator rateCalculator,IUnitOfWork unitOfWork,IShipmentStatus shipmentStatus): base(unitOfWork, mapper,userService)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _userSender = userSender;
            _userReceiver = userReceiver;
            _trackingNumberCreator = trackingNumberCreator;
            _rateCalculator=rateCalculator;
            _mapper = mapper;
            _userService= userService;
            _ShipmentStatus= shipmentStatus;

        }
        public async Task Create(ShippmentDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                dto.TrackingNumber = _trackingNumberCreator.Create(dto);
                dto.ShippingRate = _rateCalculator.Calculate(dto);
                // save sender
                if (dto.SenderId == Guid.Empty)
                {
                    Guid gSenderId = Guid.Empty;
                    _userSender.Add(dto.UserSender, out gSenderId);
                    dto.SenderId = gSenderId;
                }
                // save receiver
                if (dto.ReceiverId == Guid.Empty)
                {
                    Guid gReciverId = Guid.Empty;
                    _userReceiver.Add(dto.UserReceiver, out gReciverId);
                    dto.ReceiverId = gReciverId;
                }
                Guid gShipmentId = Guid.Empty;
                this.Add(dto, out gShipmentId);
                // add shipment status
                ShippmentStatusDto status = new ShippmentStatusDto();
                status.ShippmentId = gShipmentId;
                status.CurrentState = (int)ShipmentStatusEnum.Created;
              await  _ShipmentStatus.AddAsync(status);
                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
            }
          

        }

        public async Task Edit(ShippmentDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                dto.ShippingRate = _rateCalculator.Calculate(dto);

                dto.UserSender.Id = dto.SenderId;
                await _userSender.UpdateAsync(dto.UserSender);

                dto.UserReceiver.Id = dto.ReceiverId;
                await _userReceiver.UpdateAsync(dto.UserReceiver);

                await this.UpdateAsync(dto);

                // تأكد إنك تحفظ التغييرات فعلاً قبل الـ Commit
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }



        public async Task<ShippmentDto> GetShipment(Guid id)
        {
            try
            {
                var userId = _userService.GetLoggedInUser();

                var shipments = await _repo.GetList(
                    filter: a => a.Id == id && a.CreatedBy == userId,
                    selector: a => new ShippmentDto
                    {
                        Id = a.Id,
                        ShipingDate = a.ShipingDate,
                        DelivryDate = a.DelivryDate,
                        SenderId = a.SenderId,
                        ReceiverId = a.ReceiverId,
                        ShippingTypeId = a.ShippingTypeId,
                        ShipingPackgingId = a.ShipingPackgingId,
                        Width = a.Width,
                        Height = a.Height,
                        Weight = a.Weight,
                        Length = a.Length,
                        PackageValue = a.PackageValue,
                        ShippingRate = a.ShippingRate,
                        PaymentMethodId = a.PaymentMethodId,
                        UserSubscriptionId = a.UserSubscriptionId,
                        TrackingNumber = a.TrackingNumber,
                        ReferenceId = a.ReferenceId,

                        UserSender = new UserSenderDto
                        {
                            Id = a.Sender.Id,
                            SenderName = a.Sender.SenderName,
                            Email = a.Sender.Email,
                            Phone = a.Sender.Phone,
                            Address = a.Sender.Address,
                            Contact = a.Sender.Contact,
                            PostalCode = a.Sender.PostalCode,
                            OtherAddress = a.Sender.OtherAddress,
                            CityId = a.Sender.CityId,
                            CountryId = a.Sender.City.CountryId
                        },
                        UserReceiver = new UserReceiverDto
                        {
                            Id = a.Receiver.Id,
                            ReceiverName = a.Receiver.ReceiverName,
                            Email = a.Receiver.Email,
                            Phone = a.Receiver.Phone,
                            Address = a.Receiver.Address,
                            Contact = a.Receiver.Contact,
                            PostalCode = a.Receiver.PostalCode,
                            OtherAddress = a.Receiver.OtherAddress,
                            CityId = a.Receiver.CityId,
                            CountryId = a.Receiver.City.CountryId
                        }
                    },
                    orderBy: a => a.CreatedDate,
                    isDescending: true,
                    a => a.Sender,
                    a => a.Sender.City,
                    a => a.Sender.City.Country,
                    a => a.Receiver,
                    a => a.Receiver.City,
                    a => a.Receiver.City.Country
                );

                return  shipments.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting shipment", ex);
            }
        }

        public async Task<List<ShippmentDto>> GetShipments()
        {
            try
            {
                var userId = _userService.GetLoggedInUser();

                var shipments = await _repo.GetList(
                    filter: a => a.CreatedBy == userId,
                    selector: a => new ShippmentDto
                    {
                        Id = a.Id,
                        ShipingDate = a.ShipingDate,
                        DelivryDate = a.DelivryDate,
                        SenderId = a.SenderId,
                        ReceiverId = a.ReceiverId,
                        ShippingTypeId = a.ShippingTypeId,
                        ShipingPackgingId = a.ShipingPackgingId,
                        Width = a.Width,
                        Height = a.Height,
                        Weight = a.Weight,
                        Length = a.Length,
                        PackageValue = a.PackageValue,
                        ShippingRate = a.ShippingRate,
                        PaymentMethodId = a.PaymentMethodId,
                        UserSubscriptionId = a.UserSubscriptionId,
                        TrackingNumber = a.TrackingNumber,
                        ReferenceId = a.ReferenceId,

                        UserSender = new UserSenderDto
                        {
                            Id = a.Sender.Id,
                            SenderName = a.Sender.SenderName,
                            Email = a.Sender.Email,
                            Phone = a.Sender.Phone
                        },
                        UserReceiver = new UserReceiverDto
                        {
                            Id = a.Receiver.Id,
                            ReceiverName = a.Receiver.ReceiverName,
                            Email = a.Receiver.Email,
                            Phone = a.Receiver.Phone
                        }
                    },
                    orderBy: a => a.CreatedDate,
                    isDescending: true,
                    a => a.Sender, a => a.Receiver
                );

                return shipments;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting shipments", ex);
            }
        }

        public async Task<PagedResult<ShippmentDto>> GetShipments(int pageNumber, int pageSize)
        {
            try
            {
                var userId = _userService.GetLoggedInUser();

                var result = await _repo.GetPagedList(
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    filter: a => a.CreatedBy == userId && a.CurrentState > 0,
                    selector: a => new ShippmentDto
                    {
                        Id = a.Id,
                        ShipingDate = a.ShipingDate,
                        DelivryDate = a.DelivryDate,
                        SenderId = a.SenderId,
                        ReceiverId = a.ReceiverId,
                        ShippingTypeId = a.ShippingTypeId,
                        ShipingPackgingId = a.ShipingPackgingId,
                        Width = a.Width,
                        Height = a.Height,
                        Weight = a.Weight,
                        Length = a.Length,
                        PackageValue = a.PackageValue,
                        ShippingRate = a.ShippingRate,
                        PaymentMethodId = a.PaymentMethodId,
                        UserSubscriptionId = a.UserSubscriptionId,
                        TrackingNumber = a.TrackingNumber,
                        ReferenceId = a.ReferenceId,
                        UserSender = new UserSenderDto
                        {
                            Id = a.Sender.Id,
                            SenderName = a.Sender.SenderName,
                            Email = a.Sender.Email,
                            Phone = a.Sender.Phone
                        },
                        UserReceiver = new UserReceiverDto
                        {
                            Id = a.Receiver.Id,
                            ReceiverName = a.Receiver.ReceiverName,
                            Email = a.Receiver.Email,
                            Phone = a.Receiver.Phone
                        }
                    },
                    orderBy: a => a.CreatedDate,
                    isDescending: true,
                    a => a.Sender, a => a.Receiver
                );

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting shipments", ex);
            }
        }
    }
}
