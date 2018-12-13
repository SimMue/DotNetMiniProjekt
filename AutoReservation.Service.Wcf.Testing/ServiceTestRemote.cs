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
        private readonly ServiceTestRemoteFixture serviceTestRemoteFixture;

        public ServiceTestRemote(ServiceTestRemoteFixture serviceTestRemoteFixture)
        {
            this.serviceTestRemoteFixture = serviceTestRemoteFixture;
        }

        private IAutoReservationService<AutoDto> autoTarget;
        protected override IAutoReservationService<AutoDto> AutoTarget
        {
            get
            {
                if (autoTarget == null)
                {
                    ChannelFactory<IAutoReservationService<AutoDto>> channelFactory = new ChannelFactory<IAutoReservationService<AutoDto>>("AutoReservationService");
                    autoTarget = channelFactory.CreateChannel();
                }
                return autoTarget;
            }
        }

        protected override IAutoReservationService<KundeDto> KundeTarget { get; }
        protected override IAutoReservationService<ReservationDto> ReservationTarget { get; }
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