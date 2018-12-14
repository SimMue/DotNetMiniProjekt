using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService<TDto>
    {
	    [OperationContract]
	    [FaultContract(typeof(UnknownFault))]
		List<TDto> GetAll();

	    [OperationContract]
	    [FaultContract(typeof(UnknownFault))]
		TDto GetById(int id);

		[OperationContract]
	    [FaultContract(typeof(InvalidDateRangeFault))]
	    [FaultContract(typeof(AutoUnavailableFault))]
	    [FaultContract(typeof(UnknownFault))]
		void Insert(TDto dto);

	    [OperationContract]
	    [FaultContract(typeof(InvalidDateRangeFault))]
	    [FaultContract(typeof(AutoUnavailableFault))]
	    [FaultContract(typeof(OptimisticConcurrencyFault))]
	    [FaultContract(typeof(UnknownFault))]
		void Update(TDto dto);

	    [OperationContract]
	    [FaultContract(typeof(UnknownFault))]
		void Delete(TDto dto);
    }
}
