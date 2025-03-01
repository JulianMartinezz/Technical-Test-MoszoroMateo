using AutoMapper;
using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;

namespace HR_Medical_Records_Management_System.Mapper
{
    public class AutoMapper : Profile
    {
        /// <summary>
        /// Configures the AutoMapper mappings for various DTOs to the TMedicalRecord entity.
        /// - Maps DeleteMedicalRecordDto to TMedicalRecord, setting DeletionDate to the current date and StatusId to 2.
        /// - Maps PostMedicalRecordDto to TMedicalRecord, setting CreationDate to the current date and StatusId to 1.
        /// - Maps UpdateMedicalRecordDto to TMedicalRecord, setting ModificationDate to the current date.
        /// </summary>
        public AutoMapper()
        {
            /// <summary>
            /// Maps DeleteMedicalRecordDto to TMedicalRecord.
            /// Sets the DeletionDate to the current date and the StatusId to 2.
            /// Ignores the MedicalRecordId field.
            /// </summary>
            CreateMap<DeleteMedicalRecordDto, TMedicalRecord>()
                .ForMember(dest=>dest.MedicalRecordId, opt=>opt.Ignore())
                .ForMember(dest=>dest.DeletionDate,opt=>opt.MapFrom(src=>DateOnly.FromDateTime(DateTime.UtcNow)))
                .ForMember(dest=> dest.StatusId, opt => opt.MapFrom(src => 2));

            /// <summary>
            /// Maps PostMedicalRecordDto to TMedicalRecord.
            /// Sets the CreationDate to the current date and the StatusId to 1.
            /// Ensures that only non-null source members are mapped.
            /// </summary>
            CreateMap<PostMedicalRecordDto, TMedicalRecord>()
                .ForMember(dest=>dest.CreationDate, opt=>opt.MapFrom(src=>DateOnly.FromDateTime(DateTime.UtcNow)))
                .ForMember(dest=>dest.StatusId, opt=>opt.MapFrom(src=>1))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            /// <summary>
            /// Maps UpdateMedicalRecordDto to TMedicalRecord.
            /// Sets the ModificationDate to the current date.
            /// Ignores the MedicalRecordId field.
            /// Ensures that only non-null source members are mapped.
            /// </summary>
            CreateMap<UpdateMedicalRecordDto, TMedicalRecord>()
                .ForMember(dest=>dest.ModificationDate, opt=>opt.MapFrom(src=>DateOnly.FromDateTime(DateTime.UtcNow)))
                .ForMember(dest => dest.MedicalRecordId, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
