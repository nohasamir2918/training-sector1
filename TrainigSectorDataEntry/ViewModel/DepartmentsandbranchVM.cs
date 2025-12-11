using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TrainigSectorDataEntry.Models;

namespace TrainigSectorDataEntry.ViewModel
{
    public class DepartmentsandbranchVM
    {
        public int Id { get; set; }
        [ValidateNever]
        public int SpecializationId { get; set; }
        [ValidateNever]
        public int EducationalFacilitiesId { get; set; }

        public int? DepatmentTypeID { get; set; }

        public string NameAr { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public bool IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public int? UserCreationId { get; set; }

        public DateOnly? UserCreationDate { get; set; }

        public int? UserUpdationId { get; set; }

        public DateOnly? UserUpdationDate { get; set; }

        public int? UserDeletionId { get; set; }

        public DateOnly? UserDeletionDate { get; set; }

        public virtual ICollection<DepartmentsandBranchesDetail> DepartmentsandBranchesDetails { get; set; } = new List<DepartmentsandBranchesDetail>();
        public virtual DepartmentType? DepatmentType { get; set; }
        [ValidateNever]
        public virtual EducationalFacility EducationalFacilities { get; set; } = null!;
        [ValidateNever]
        public virtual Specialization Specialization { get; set; } = null!;
    }
}
