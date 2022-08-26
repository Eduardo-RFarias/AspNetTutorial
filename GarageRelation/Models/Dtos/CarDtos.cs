using System.ComponentModel.DataAnnotations;

namespace GarageRelation.Models.Dtos
{
    namespace GarageRelation.Dtos
    {
        public record CarCreateDto
        {
            [Required]
            [MaxLength(8)]
            [MinLength(8)]
            public string Plate { get; set; } = default!;

            [Required]
            [MaxLength(40)]
            [MinLength(1)]
            public string Brand { get; set; } = default!;

            [Required]
            [MaxLength(40)]
            [MinLength(1)]
            public string Model { get; set; } = default!;

            [Required]
            [Range(1800, 3000)]
            public int Year { get; set; } = default!;

            [Required]
            public int PersonId { get; set; } = default!;
        }

        public record CarUpdateDto
        {
            [Required]
            [MaxLength(8)]
            [MinLength(8)]
            public string Plate { get; set; } = default!;

            [Required]
            [MaxLength(40)]
            [MinLength(1)]
            public string Brand { get; set; } = default!;

            [Required]
            [MaxLength(40)]
            [MinLength(1)]
            public string Model { get; set; } = default!;

            [Required]
            [Range(1800, 3000)]
            public int Year { get; set; } = default!;

            [Required]
            public int PersonId { get; set; } = default!;
        }

        public record CarPartialUpdateDto
        {
            [MaxLength(8)]
            [MinLength(8)]
            public string? Plate { get; set; } = default!;

            [MaxLength(40)]
            [MinLength(1)]
            public string? Brand { get; set; } = default!;

            [MaxLength(40)]
            [MinLength(1)]
            public string? Model { get; set; } = default!;

            [Range(1800, 3000)]
            public int? Year { get; set; } = default!;

            public int? PersonId { get; set; } = default!;
        }

        public record CarResponseDto
        {
            [Required]
            public int Id { get; set; } = default!;

            [Required]
            [MaxLength(8)]
            [MinLength(8)]
            public string Plate { get; set; } = default!;

            [Required]
            [MaxLength(40)]
            [MinLength(1)]
            public string Brand { get; set; } = default!;

            [Required]
            [MaxLength(40)]
            [MinLength(1)]
            public string Model { get; set; } = default!;

            [Required]
            [Range(1800, 3000)]
            public int Year { get; set; } = default!;

            [Required]
            public int PersonId { get; set; } = default!;
        }
    }
}
