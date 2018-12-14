using System;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;
using Xunit;

namespace AutoReservation.Service.Wcf.Testing
{
    public class ServiceTestRemote
        : ServiceTestBase
        , IClassFixture<ServiceTestRemoteFixture>
    {
        private readonly ServiceTestRemoteFixture _serviceTestRemoteFixture;

        public ServiceTestRemote(ServiceTestRemoteFixture serviceTestRemoteFixture)
        {
            this._serviceTestRemoteFixture = serviceTestRemoteFixture;
        }

        private IAutoReservationService<AutoDto> _autoTarget;
        private IAutoReservationService<KundeDto> _kundeTarget;
        private IAutoReservationService<ReservationDto> _reservationTarget;


        protected override IAutoReservationService<AutoDto> AutoTarget
        {
            get
            {
                if (_autoTarget == null)
                {
                    ChannelFactory<IAutoReservationService<AutoDto>> channelFactory = new ChannelFactory<IAutoReservationService<AutoDto>>("AutoReservationService");
                    _autoTarget = channelFactory.CreateChannel();
                }
                return _autoTarget;
            }
        }

        protected override IAutoReservationService<KundeDto> KundeTarget
        {
            get
            {
                if (_kundeTarget == null)
                {
                    ChannelFactory<IAutoReservationService<KundeDto>> channelFactory = new ChannelFactory<IAutoReservationService<KundeDto>>("AutoReservationService");
                    _kundeTarget = channelFactory.CreateChannel();
                }
                return _kundeTarget;
            }
        }

        protected override IAutoReservationService<ReservationDto> ReservationTarget
        {
            get
            {
                if (_reservationTarget == null)
                {
                    ChannelFactory<IAutoReservationService<ReservationDto>> channelFactory = new ChannelFactory<IAutoReservationService<ReservationDto>>("AutoReservationService");
                    _reservationTarget = channelFactory.CreateChannel();
                }
                return _reservationTarget;
            }
        }
    }

    public class ServiceTestRemoteFixture
        : IDisposable
    {
        public ServiceTestRemoteFixture()
        {
            ServiceHost = new ServiceHost(typeof(AutoReservationService<AutoDto, Auto>));
            ServiceHost.Open();
        }

        public void Dispose()
        {
            if (ServiceHost.State != CommunicationState.Closed)
            {
                ServiceHost.Close();
            }
        }

        public ServiceHost ServiceHost { get; }
    }
}