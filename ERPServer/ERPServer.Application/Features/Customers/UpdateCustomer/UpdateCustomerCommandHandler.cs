using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace ERPServer.Application.Features.Customers.UpdateCustomer
{
    internal sealed class UpdateCustomerCommandHandler(
      ICustomerRepository customerRepository,
      IUnitOfWork unitOfWork,
      IMapper mapper,
       IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateCustomerCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.User?.Claims.Select(f => f.Value).FirstOrDefault();
            //
            Customer customer = await customerRepository.GetByExpressionWithTrackingAsync(f => f.Id == request.Id);
            //
            if (customer is null)
            {
                return Result<string>.Failure("Müşteri bulunamadı.");
            }
            //
            if (customer.TaxNumber != request.TaxNumber)
            {
                bool isTaxNumberExists = await customerRepository.AnyAsync(f => f.TaxNumber == request.TaxNumber, cancellationToken);
                //
                if (isTaxNumberExists)
                {
                    return Result<string>.Failure("Vergi numarası daha önceden başka bir müşteri için kaydedilmiş.");
                }
            }
            //
            mapper.Map(request, customer);
            //
            customer.UpdateBy = userId?.ToUpper() ?? "";
            customer.UpdateDate = DateTime.Now;
            //
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Müşteri bilgileri başarıyla güncellendi.";
        }
    }
}