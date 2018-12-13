using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
	public abstract class BaseFault
	{
		[DataMember]
		public string Operation { get; set; }

		[DataMember]
		public string ProblemType { get; set; }
	}
}
