using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Brand;
using Application.DTO.BrandDtoF;
using Application.DTO.CategoryDtoF;
using Application.DTO.ProductDto;
using Application.DTO.SubCategory;
using AutoMapper;
using Domain.Entity;

namespace Application.Common
{
    public  class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CSubCategoryDto, SubCategory>();
            CreateMap<USubcategoryDto, SubCategory>().ReverseMap();
            CreateMap<SubCategory, SubcategoryDto>()
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(s=>s.Category.Name)).ReverseMap();
              

            CreateMap<CcategoryDto, Category>().ReverseMap();
            CreateMap<UcategoryDto, Category>().ReverseMap();
            CreateMap<Category, CategoryDto>().ForMember(x=>x.CategoryName,opt=>opt.MapFrom(s=>s.Name))
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(s=>s.CategoryId));

            CreateMap<BrandDto, Brand>().ReverseMap();
            CreateMap<UBrandDto, Brand>().ReverseMap();

            CreateMap<Product, GProductDto>()
                .ForMember(dest=>dest.BrandName,opt=>opt.MapFrom(s=>s.Brand.Name))
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(s=>s.Category.Name))
                .ForMember(dest=>dest.SubCategoryName,opt=>opt.MapFrom(s=>s.SubCategory.Name));
            
        }
    }
}