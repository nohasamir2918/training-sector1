using System;
using System.Collections.Generic;

namespace TrainigSectorDataEntry.Models;

public partial class Specialization
{
    public int Id { get; set; }

    public int EducationalFacilitiesId { get; set; }

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

    public virtual ICollection<DepartmentType> DepartmentTypes { get; set; } = new List<DepartmentType>();

    public virtual ICollection<Departmentsandbranch> Departmentsandbranches { get; set; } = new List<Departmentsandbranch>();

    public virtual EducationalFacility EducationalFacilities { get; set; } = null!;
}
