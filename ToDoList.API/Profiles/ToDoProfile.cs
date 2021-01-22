using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Models;

namespace ToDoList.API.Profiles
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDoItem, ToDoItemDto>();
            CreateMap<ToDoItemDto, ToDoItem>();
        }
    }
}
