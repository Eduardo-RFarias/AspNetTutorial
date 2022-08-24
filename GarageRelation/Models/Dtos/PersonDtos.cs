using System.ComponentModel.DataAnnotations;

namespace GarageRelation.Dtos
{
    public record PersonCreateDto
    {
        [Required]
        [MaxLength(40)]
        [MinLength(4)]
        public string Name { get; init; } = default!;

        [Required]
        [Range(0, 255)]
        public int Age { get; init; } = default!;

        [Required]
        [MaxLength(40)]
        public string MainAddress { get; set; } = default!;

        [Required]
        [MaxLength(10)]
        public string Number { get; set; } = default!;

        [MaxLength(255)]
        public string? Complement { get; set; }
    }

    public record PersonUpdateDto
    {
        [Required]
        [MaxLength(40)]
        [MinLength(4)]
        public string Name { get; init; } = default!;

        [Required]
        [Range(0, 255)]
        public int Age { get; init; } = default!;

        [Required]
        [MaxLength(40)]
        public string MainAddress { get; set; } = default!;

        [Required]
        [MaxLength(10)]
        public string Number { get; set; } = default!;

        [MaxLength(255)]
        public string? Complement { get; set; }
    }

    public record PersonPartialUpdateDto
    {
        [MaxLength(40)]
        [MinLength(4)]
        public string? Name { get; init; }

        [Range(0, 255)]
        public int? Age { get; init; }

        [MaxLength(40)]
        public string? MainAddress { get; set; }

        [MaxLength(10)]
        public string? Number { get; set; }

        [MaxLength(255)]
        public string? Complement { get; set; }
    }

    public record PersonResponseDto
    {
        [Required]
        public int Id { get; set; } = default!;

        [Required]
        [MaxLength(40)]
        [MinLength(4)]
        public string Name { get; init; } = default!;

        [Required]
        [Range(0, 255)]
        public int Age { get; init; } = default!;

        [Required]
        [MaxLength(40)]
        public string MainAddress { get; set; } = default!;

        [Required]
        [MaxLength(10)]
        public string Number { get; set; } = default!;

        [MaxLength(255)]
        public string? Complement { get; set; } = default!;
    }
}
