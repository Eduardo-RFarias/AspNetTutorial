using System.ComponentModel.DataAnnotations;


namespace GarageRelation.API.Models.Dtos
{
	public record CarCreateDto
	(
		[Required]
		[MaxLength(8)]
		[MinLength(8)]
		string Plate,

		[Required]
		[MaxLength(40)]
		[MinLength(1)]
		string Brand,

		[Required]
		[MaxLength(40)]
		[MinLength(1)]
		string Model,

		[Required]
		[Range(1800, 3000)]
		int Year,

		[Required]
		int PersonId
	);

	public record CarUpdateDto
	(
		[Required]
		[MaxLength(8)]
		[MinLength(8)]
		string Plate,

		[Required]
		[MaxLength(40)]
		[MinLength(1)]
		string Brand,
		[Required]
		[MaxLength(40)]
		[MinLength(1)]
		string Model,

		[Required]
		[Range(1800, 3000)]
		int Year,

		[Required]
		int PersonId
	);

	public record CarPartialUpdateDto
	(
		[MaxLength(8)]
		[MinLength(8)]
		string? Plate = null,

		[MaxLength(40)]
		[MinLength(1)]
		string? Brand = null,

		[MaxLength(40)]
		[MinLength(1)]
		string? Model = null,

		[Range(1800, 3000)]
		int? Year = null,

		int? PersonId = null
	);

	public record CarResponseDto
	(
		[Required]
		int Id,

		[Required]
		[MaxLength(8)]
		[MinLength(8)]
		string Plate,

		[Required]
		[MaxLength(40)]
		[MinLength(1)]
		string Brand,

		[Required]
		[MaxLength(40)]
		[MinLength(1)]
		string Model,

		[Required]
		[Range(1800, 3000)]
		int Year,

		[Required]
		int PersonId
	);
}
