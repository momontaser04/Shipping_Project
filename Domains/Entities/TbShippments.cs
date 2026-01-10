using System;
using System.Collections.Generic;
using System.Threading;

namespace Domains.Entities;

public partial class TbShippments:BaseTable
{


    public DateTime ShipingDate { get; set; }
    public DateTime DelivryDate { get; set; }
  
    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public Guid ShippingTypeId { get; set; }

    public Guid? ShipingPackgingId { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }

    public double Weight { get; set; }

    public double Length { get; set; }

    public decimal PackageValue { get; set; }

    public decimal ShippingRate { get; set; }

    public Guid? PaymentMethodId { get; set; }

    public Guid? UserSubscriptionId { get; set; }

    public double? TrackingNumber { get; set; }

    public Guid? ReferenceId { get; set; }

    public Guid? CarrierId { get; set; }
    public virtual TbCarriers Carrier { get; set; } = null!;
    public virtual TbPaymentMethods? PaymentMethod { get; set; }

    public virtual TbUserReceivers Receiver { get; set; } = null!;

    public virtual TbUserSenders Sender { get; set; } = null!;

    public virtual TbShippingTypes ShippingType { get; set; } = null!;
    public virtual TbShipingPackging ShipingPackging { get; set; } = null!;

    public virtual ICollection<TbShippmentStatus> TbShippmentStatus { get; set; } = new List<TbShippmentStatus>();
}
