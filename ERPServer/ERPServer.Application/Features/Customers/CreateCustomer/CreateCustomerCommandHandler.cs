using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TS.Result;

namespace ERPServer.Application.Features.Customers.CreateCustomer
{
    internal sealed class CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateCustomerCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.User?.Claims.Select(f => f.Value).FirstOrDefault();
            //
            bool isTaxNumberExists = await customerRepository.AnyAsync(f => f.TaxNumber == request.TaxNumber, cancellationToken);
            //
            if (isTaxNumberExists) 
            {
                return Result<string>.Failure("Vergi numarası daha önceden başka bir müşteri için kaydedilmiş.");
            }
            //
            Customer customer = mapper.Map<Customer>(request);
            //
            customer.CreateBy = userId?.ToUpper() ?? "";
            customer.CreateDate = DateTime.Now;
            //
            await customerRepository.AddAsync(customer, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Müşteri kaydı başarıyla tamamlandı.";
        }
    }
}
