using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using Northwind.Model;

namespace Northwind.UI.Models
{
    public interface IAdapter
    {
        TTarget Adapt<TSouce, TTarget>(TSouce souce);
    }

    public class Adapter : IAdapter
    {
        static Adapter()
        {
            Init();
        }

        /// <summary>
        /// Initialize mapping combinations
        /// </summary>
        public static void Init()
        {
            Mapper.CreateMap<Product, ProductDto>()
                  .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(p => p.Category.CategoryName));
            Mapper.CreateMap<ProductDto, Product>()
                  .ForMember(p => p.Category, opt => opt.Ignore())
                  .ForMember(p => p.Supplier, opt => opt.Ignore())
                  .ForMember(p => p.OrderDetails, opt => opt.Ignore());

            Mapper.CreateMap<Category, CategoryDto>()
                  .ForMember(dto => dto.Picture, opt => opt.Ignore())
                  .ForMember(dto => dto.IsDeletable, opt => opt.MapFrom(c => c.Products.Any()))
                  .ForMember(dto => dto.TempPictureId, opt => opt.Ignore());
            Mapper.CreateMap<CategoryDto, Category>()
                  .ForMember(c => c.Products, opt => opt.Ignore())
                  .ForMember(c => c.PictureId, opt => opt.MapFrom(dto => dto.TempPictureId ?? dto.PictureId))
                  .ForMember(c => c.Picture, opt => opt.Condition(dto => dto.Picture != null));

            Mapper.CreateMap<Supplier, SupplierDto>();
            Mapper.CreateMap<SupplierDto, Supplier>()
                  .ForMember(s => s.Products, opt => opt.Ignore());

            Mapper.CreateMap<Order, OrderDto>()
                  .ForMember(dto => dto.EmployeeName, opt => opt.MapFrom(
                      o => string.Format("{0}, {1}", o.Employee.LastName, o.Employee.FirstName)))
                  .ForMember(dto => dto.CustomerCompanyName, opt => opt.MapFrom(o => o.Customer.CompanyName))
                  .ForMember(dto => dto.ShipperCompanyName, opt => opt.MapFrom(o => o.Shipper.CompanyName));
            Mapper.CreateMap<OrderDto, Order>()
                  .ForMember(o => o.Employee, opt => opt.Ignore())
                  .ForMember(o => o.Customer, opt => opt.Ignore())
                  .ForMember(o => o.Shipper, opt => opt.Ignore())
                  .ForMember(o => o.ShipAddress, opt => opt.Ignore())
                  .ForMember(o => o.OrderDetails, opt => opt.Ignore());
        }

        public TResult Adapt<TSource, TResult>(TSource source)
        {
            return Mapper.Map<TResult>(source);
        }
    }
}