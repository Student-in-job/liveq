using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EQueueVidly.Dtos;
using EQueueVidly.Models;
using EQueueVidly.ViewModels;

namespace EQueueVidly.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Appointment, AppintmentDto>()
                .ForMember(
                d=>d.Total, 
                m=>m.MapFrom(src=>src.Capacity)
                )/*.ForMember(
                d=>d.FirstName, m=>m.MapFrom(src=>src.User.FirstName)
                ).ForMember(
                d=>d.LastName, m=>m.MapFrom(src=>src.User.LastName))*/;
            Mapper.CreateMap<ApplicationUser, TeacherViewModel>()
                .ForMember(d=>d.TeacherId, m=>m.MapFrom(s=>s.Id));
            Mapper.CreateMap<Attendee, QueueItemDto>();
            Mapper.CreateMap<Appointment, ScheduleEventVM>()/*
                .ForMember(d => d.TeacherId, m => m.MapFrom(s => s.User.Id))
                .ForMember(d => d.TeacherName, m => m.MapFrom(s => s.User.FirstName))*/
                .ForMember(d => d.TimeLimit, m => m.MapFrom(s => s.TimeLimit))
                .ForMember(d => d.QueueItemsCount, m => m.MapFrom(s => s.Attendees.Count));

            /*Mapper.CreateMap<QueueItemDto, QueueItemDto>();*/

        }

    }
}