﻿using System;
using System.ServiceModel;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Dal.Entities;
using AutoReservation.Service.Wcf.Converters;

namespace AutoReservation.Service.Wcf
{
	public class ReservationService : AutoReservationService<ReservationDto, Reservation>, IReservableService<ReservationDto>
	{
		public ReservationService(ReservationManager manager, DtoEntityConverter<ReservationDto, Reservation> converter) : base(manager, converter)
		{
		}
		public bool CheckAvailability(ReservationDto reservationDto)
		{
			try
			{
				WriteActualMethod();
				Reservation entity = Converter.ConvertToEntity(reservationDto);
				return ((ReservationManager)Manager).CheckAvailability(entity);
			}
			catch (Exception exception)
			{
				UnknownFault fault = new UnknownFault();
				fault.Operation = "checkAvailability";
				fault.ProblemType = exception.Message;
				throw new FaultException<UnknownFault>(fault);
			}
		}
	}
}
