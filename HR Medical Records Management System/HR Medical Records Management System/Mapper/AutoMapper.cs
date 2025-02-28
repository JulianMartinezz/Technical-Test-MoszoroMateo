using AutoMapper;
using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;

namespace HR_Medical_Records_Management_System.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //Map that will be used to map the DeleteMedicalRecordDto to the TMedicalRecord
            //setting the DeletionDate to the current date and the StatusId to 2
            //ignoring the MedicalRecordId
            CreateMap<DeleteMedicalRecordDto, TMedicalRecord>()
                .ForMember(dest=>dest.MedicalRecordId, opt=>opt.Ignore())
                .ForMember(dest=>dest.DeletionDate,opt=>opt.MapFrom(src=>DateOnly.FromDateTime(DateTime.UtcNow)))
                .ForMember(dest=> dest.StatusId, opt => opt.MapFrom(src => 2));

            //Map that will be used to map the PostMedicalRecordDto to the TMedicalRecord
            //setting the CreationDate to the current date and the StatusId to 1
            CreateMap<PostMedicalRecordDto, TMedicalRecord>()
                .ForMember(dest=>dest.CreationDate, opt=>opt.MapFrom(src=>DateOnly.FromDateTime(DateTime.UtcNow)))
                .ForMember(dest=>dest.StatusId, opt=>opt.MapFrom(src=>1))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            //Map that will be used to map the UpdateMedicalRecordDto to the TMedicalRecord
            //setting the ModificationDate to the current date
            //ignoring the MedicalRecordId
            CreateMap<UpdateMedicalRecordDto, TMedicalRecord>()
                .ForMember(dest=>dest.ModificationDate, opt=>opt.MapFrom(src=>DateOnly.FromDateTime(DateTime.UtcNow)))
                .ForMember(dest => dest.MedicalRecordId, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
