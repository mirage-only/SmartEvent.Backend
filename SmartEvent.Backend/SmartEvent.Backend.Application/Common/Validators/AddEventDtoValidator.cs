using FluentValidation;
using SmartEvent.Backend.Application.DTOs.EventDTOs.Requests;

namespace SmartEvent.Backend.Application.Common.Validators;

public class AddEventDtoValidator: AbstractValidator<AddEventDto>
{
    public  AddEventDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(dto => dto.Name).NotNull().NotEmpty();
        RuleFor(dto => dto.Description).NotNull().NotEmpty();
        RuleFor(dto => dto.ImageUrl).NotNull();
        RuleFor(dto => dto.StartTime).NotNull();
        RuleFor(dto => dto.Latitude).NotNull();
        RuleFor(dto => dto.Longitude).NotNull();
        RuleFor(dto => dto.Address).NotNull().NotEmpty();
        RuleFor(dto => dto.Room).NotNull();
        RuleFor(dto => dto.QrCodeExpirationTime).NotNull().NotEmpty();
    }
}