using System.ComponentModel.DataAnnotations;

namespace GarageRelation.API.Models.Dtos
{
	public record PersonCreateDto
	(
		[Required]
		[MaxLength(40)]
		[MinLength(4)]
		string Name,

		[Required]
		[Range(0, 255)]
		int Age,

		[Required]
		[MaxLength(40)]
		string MainAddress,

		[Required]
		[MaxLength(10)]
		string Number,

		[MaxLength(255)]
		string? Complement = null
	);

	public record PersonUpdateDto
	(
		[Required]
		[MaxLength(40)]
		[MinLength(4)]
		string Name,

		[Required]
		[Range(0, 255)]
		int Age,

		[Required]
		[MaxLength(40)]
		string MainAddress,

		[Required]
		[MaxLength(10)]
		string Number,

		[MaxLength(255)]
		string? Complement = null
	);

	public record PersonPartialUpdateDto
	(
		[MaxLength(40)]
		[MinLength(4)]
		string? Name = null,

		[Range(0, 255)]
		int? Age = null,

		[MaxLength(40)]
		string? MainAddress = null,

		[MaxLength(10)]
		string? Number = null,

		[MaxLength(255)]
		string? Complement = null
	);

	public record PersonResponseDto
	(
		[Required]
		int Id,

		[Required]
		[MaxLength(40)]
		[MinLength(4)]
		string Name,

		[Required]
		[Range(0, 255)]
		int Age,

		[Required]
		[MaxLength(40)]
		string MainAddress,

		[Required]
		[MaxLength(10)]
		string Number,

		[MaxLength(255)]
		string? Complement = null
	);
}
