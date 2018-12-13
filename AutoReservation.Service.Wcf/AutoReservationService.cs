using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using AutoReservation.BusinessLayer;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf.Converters;

namespace AutoReservation.Service.Wcf
{
    public abstract class AutoReservationService<TDto, TEntity> : IAutoReservationService<TDto>
    {
        protected readonly ManagerBase<TEntity> _manager;
        protected readonly DtoEntityConverter<TDto, TEntity> _converter;

        public AutoReservationService(ManagerBase<TEntity> manager, DtoEntityConverter<TDto, TEntity> converter)
        {
            _manager = manager;
            _converter = converter;
        }

        protected static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}-{typeof(TDto).Namespace}");

        public List<TDto> GetAll()
        {
           try
	        {
				WriteActualMethod();
		        List<TEntity> entities = _manager.GetAll();
		        return _converter.ConvertToDtos(entities);
			}
	        catch (Exception exception)
	        {
		        UnknownFault fault = new UnknownFault();
		        fault.Operation = "getAll";
		        fault.ProblemType = exception.Message;
		        throw new FaultException<UnknownFault>(fault);
	        }
		}

        public TDto GetById(int id)
        {
	        try
	        {
				WriteActualMethod();
		        TEntity entity = _manager.GetById(id);
		        return _converter.ConvertToDto(entity);
			}
	        catch (Exception exception)
	        {
		        UnknownFault fault = new UnknownFault();
		        fault.Operation = "getById";
		        fault.ProblemType = exception.Message;
		        throw new FaultException<UnknownFault>(fault);
	        }
		}

	    public abstract bool CheckAvailability(TDto dto);
		
		public void Insert(TDto dto)
        {
	        try
	        {
		        WriteActualMethod();
		        TEntity entity = _converter.ConvertToEntity(dto);
				_manager.Insert(entity);
	        }
	        catch (InvalidDateRangeException exception)
	        {
		        InvalidDateRangeFault fault = new InvalidDateRangeFault();
		        fault.Operation = "insert";
		        fault.ProblemType = exception.Message;
		        throw new FaultException<InvalidDateRangeFault>(fault);
			}
	        catch (AutoUnavailableException exception)
	        {
		        AutoUnavailableFault fault = new AutoUnavailableFault();
		        fault.Operation = "insert";
		        fault.ProblemType = exception.Message;
		        throw new FaultException<AutoUnavailableFault>(fault);
	        }
			catch (Exception exception)
	        {
				UnknownFault fault = new UnknownFault();
		        fault.Operation = "insert";
		        fault.ProblemType = exception.Message;
		        throw new FaultException<UnknownFault>(fault);
			}
        }

        public void Update(TDto dto)
        {
	        try
	        {
		        WriteActualMethod();
		        TEntity entity = _converter.ConvertToEntity(dto);
				_manager.Update(entity);
	        }
	        catch (InvalidDateRangeException e)
	        {
		        InvalidDateRangeFault fault = new InvalidDateRangeFault();
		        fault.Operation = "update";
		        fault.ProblemType = e.Message;
		        throw new FaultException<InvalidDateRangeFault>(fault);
	        }
	        catch (AutoUnavailableException e)
	        {
		        AutoUnavailableFault fault = new AutoUnavailableFault();
		        fault.Operation = "update";
		        fault.ProblemType = e.Message;
		        throw new FaultException<AutoUnavailableFault>(fault);
	        }
	        catch (OptimisticConcurrencyException<TEntity> e)
	        {
		        OptimisticConcurrencyFault fault = new OptimisticConcurrencyFault();
		        fault.Operation = "update";
		        fault.ProblemType = e.Message;
		        throw new FaultException<OptimisticConcurrencyFault>(fault);
			}
	        catch (Exception e)
	        {
		        UnknownFault fault = new UnknownFault();
		        fault.Operation = "update";
		        fault.ProblemType = e.Message;
		        throw new FaultException<UnknownFault>(fault);
	        }
        }

        public void Delete(TDto dto)
        {
	        try
	        {
		        WriteActualMethod();
		        TEntity entity = _converter.ConvertToEntity(dto);
				_manager.Delete(entity);
			}
		    catch (Exception exception)
	        {
		        UnknownFault fault = new UnknownFault();
		        fault.Operation = "delete";
		        fault.ProblemType = exception.Message;
		        throw new FaultException<UnknownFault>(fault);
	        }
		}
    }
}