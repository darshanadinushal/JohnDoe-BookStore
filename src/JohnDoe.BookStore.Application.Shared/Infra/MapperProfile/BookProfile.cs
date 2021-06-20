using AutoMapper;
using JohnDoe.BookStore.Application.Shared.DomainModel;
using JohnDoe.BookStore.Application.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JohnDoe.BookStore.Application.Shared.Infra.MapperProfile
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            this.CreateMap<BookInformation, XmlBook>()
                .ReverseMap();
        }
    }
}
