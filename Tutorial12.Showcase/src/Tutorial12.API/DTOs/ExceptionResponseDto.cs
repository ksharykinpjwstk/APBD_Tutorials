using System.Net;

namespace Tutorial12.API.DTOs;

public record ExceptionResponseDto(HttpStatusCode StatusCode, string Description);