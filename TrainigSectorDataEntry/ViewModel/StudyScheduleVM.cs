namespace TrainigSectorDataEntry.ViewModel
{
    public class StudyScheduleVM
    {
        public int EducationalLevelId { get; set; }
        public int TermId { get; set; }
        public int IdType { get; set; }
        public int DepartmentsandbranchesId { get; set; }   // ✅ أساسي

        public int SpecializationId { get; set; }            // ✅ للمعاهد فقط
        public int EducationalFacilitiesId { get; set; }     // Id اللي داخل
    }


}
