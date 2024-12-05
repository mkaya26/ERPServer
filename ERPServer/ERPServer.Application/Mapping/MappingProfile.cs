using AutoMapper;
using ERPServer.Application.Features.Customers.CreateCustomer;
using ERPServer.Application.Features.Customers.UpdateCustomer;
using ERPServer.Application.Features.Depots.CreateDepot;
using ERPServer.Application.Features.Depots.UpdateDepot;
using ERPServer.Application.Features.Invoices.CreateInvoice;
using ERPServer.Application.Features.Orders.CreateOrder;
using ERPServer.Application.Features.Orders.UpdateOrder;
using ERPServer.Application.Features.Products.CreateProduct;
using ERPServer.Application.Features.Products.UpdateProduct;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace ERPServer.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public MappingProfile()
        {
            //_httpContextAccessor = httpContextAccessor;
            //var userId = _httpContextAccessor.HttpContext.User?.Claims.Select(f => f.Value).FirstOrDefault();
            //
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();
            //
            CreateMap<CreateDepotCommand, Depot>();
            CreateMap<UpdateDepotCommand, Depot>();
            //
            CreateMap<CreateProductCommand, Product>()
                .ForMember(
                    m => m.Type,
                    o => o.MapFrom(
                    v => ProductTypeEnum.FromValue(v.TypeValue)));
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(
                    m => m.Type,
                    o => o.MapFrom(
                    v => ProductTypeEnum.FromValue(v.TypeValue)));
            //
            CreateMap<CreateOrderCommand, Order>()
                .ForMember(m => m.OrderDetails,
                o => o.MapFrom(p => p.OrderDetails.Select(
                   s => new OrderDetail
                   {
                       Price = s.Price,
                       ProductId = s.ProductId,
                       Quantity = s.Quantity,
                       //CreateBy = userId ?? "",
                       CreateDate = DateTime.Now
                   }).ToList()));
            //
            CreateMap<UpdateOrderCommand, Order>().ForMember(
                member => member.OrderDetails, 
                options => options.Ignore());
            //
            CreateMap<CreateInvoiceCommand, Invoice>()
                .ForMember(m => m.InvoiceType,
                o => o.MapFrom(p => InvoiceTypeEnum.FromValue(p.InvoiceType)))
                .ForMember(m => m.InvoiceDetails,
                o => o.MapFrom(p => p.InvoiceDetails.Select(
                   s => new InvoiceDetail
                   {
                       Price = s.Price,
                       ProductId = s.ProductId,
                       Quantity = s.Quantity,
                       CreateDate = DateTime.Now
                   }).ToList()));
        }
    }
}
