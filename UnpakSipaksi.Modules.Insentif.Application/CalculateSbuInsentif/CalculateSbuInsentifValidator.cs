using FluentValidation;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;

namespace UnpakSipaksi.Modules.Insentif.Application.CalculateSbuInsentif
{
    public sealed class CalculateSbuInsentifCommandValidator : AbstractValidator<CalculateSbuInsentifCommand>
    {
        public static bool IsValidEnumMember<T>(string input) where T : Enum
        {
            return EnumExtensions.GetAllEnumStrings<T>().Contains(input, StringComparer.OrdinalIgnoreCase);
        }
        public static bool IsValidType(string input)
        {
            return new[] { "fakultas", "lppm" }.Contains(input, StringComparer.OrdinalIgnoreCase);
        }

        public CalculateSbuInsentifCommandValidator()
        {
            RuleFor(c => c.StatusJurnal)
                .NotEmpty().WithMessage("'StatusJurnal' tidak boleh kosong.");
                //.Must(Helper.BeValidGuidV4).WithMessage("'StatusJurnal' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.PeranPenulis)
                .NotEmpty().WithMessage("'PeranPenulis' tidak boleh kosong.")
                .Must(value => IsValidEnumMember<Peran>(value)).WithMessage("'PeranPenulis' tidak valid.");

            RuleFor(c => c.JumlahPenulis)
                .NotEmpty().WithMessage("'JumlahPenulis' tidak boleh kosong.")
                .GreaterThan(0).WithMessage("'JumlahPenulis' tidak boleh nol");

            RuleFor(c => c.JenisJurnal)
                .NotEmpty().WithMessage("'JenisJurnal' tidak boleh kosong.")
                .Must(value => IsValidEnumMember<JenisJurnal>(value)).WithMessage("'JenisJurnal' tidak valid.");

            RuleFor(c => c.LibatkanMahasiswa)
                .NotEmpty().WithMessage("'LibatkanMahasiswa' tidak boleh kosong.")
                .Must(value => IsValidEnumMember<LibatkanMahasiswa>(value)).WithMessage("'LibatkanMahasiswa' tidak valid.");

            RuleFor(c => c.StatusPengajuan)
                .NotEmpty().WithMessage("'StatusPengajuan' tidak boleh kosong.")
                .Must(value => IsValidEnumMember<StatusPengajuan>(value)).WithMessage("'StatusPengajuan' tidak valid.");
        }
    }
}
