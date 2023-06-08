using System.ComponentModel.DataAnnotations;

namespace TZPU.Web.Utility
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if(file != null)
            {
                if(file.Length > (_maxFileSize * 1024 * 1024 * 4))
                {
                    return new ValidationResult($"Maksimalna dozvoljena velicina fajla je: {_maxFileSize}MB!");
                }
            }

            return ValidationResult.Success;
        }
    }
}
