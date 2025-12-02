namespace Moveo_backend.IAM.Domain.Model.Commands;

public record RefreshTokenCommand(
    string AccessToken,
    string RefreshToken
);
