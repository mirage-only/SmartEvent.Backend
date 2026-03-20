using AutoMapper;
using SmartEvent.Backend.Application.DTOs.QrCodeDTOs.Responses;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Common.Mapping;

public class QrCodeMappingProfile: Profile
{
    public QrCodeMappingProfile()
    {
        CreateMap<QrCode, CurrentQrCodeDto>();
    }
}