using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using TrainigSectorDataEntry.Models;

namespace TrainigSectorDataEntry.ViewModel
{
    public class ComplaintsAndSuggestionVM
    {
        public int Id { get; set; }

        public int TrainigSectorId { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "رقم التليفون مطلوب")]
        [RegularExpression(@"^[0-9]+$",
    ErrorMessage = "رقم التليفون يجب أن يحتوي على أرقام فقط")]
        public int Telephone { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "نص الشكوى / المقترح مطلوب")]
        public string ComplaintText { get; set; } = null!;

        [Required(ErrorMessage = "يرجى إرفاق ملف")]
        public IFormFile UploadedFile { get; set; } = null!;

        [ValidateNever]
        public string FilePath { get; set; } = null!;

        public bool IsActive { get; set; } = true;
        public bool? IsDeleted { get; set; } = false;

        public int? UserCreationId { get; set; }
        public DateOnly? UserCreationDate { get; set; }

        [ValidateNever]
        public virtual TrainingSector TrainigSector { get; set; } = null!;
    }
}
