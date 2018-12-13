using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Service.Wcf
{
    [ServiceContract]
    public interface IReservableService<TDto>
    {
        [OperationContract]
        [FaultContract(typeof(UnknownFault))]
        bool CheckAvailability(TDto dto);
    }
}
