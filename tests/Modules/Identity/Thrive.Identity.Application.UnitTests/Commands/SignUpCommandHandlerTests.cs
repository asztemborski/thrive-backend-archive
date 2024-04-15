using MediatR;
using Microsoft.Extensions.Options;
using Moq;
using Thrive.Modules.Identity.Application.Commands.SignUpCommand;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.Exceptions;
using Thrive.Modules.Identity.Application.Options;
using Thrive.Modules.Identity.Domain.Repositories;

namespace Thrive.Identity.Application.UnitTests.Commands;

public sealed class SignUpCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IValueHasher> _valueHasherMock = new();
    private readonly Mock<IPublisher> _publisherMock = new();

    [Fact]
    public async Task Handle_ShouldThrowInvalidEmailProviderException_WhenEmailProviderIsBanned()
    {
        var command = new SignUpCommand("email@banned-domain.com", "username", "password", "password");
        
        var emailOptions = Options.Create(new EmailOptions
        {
            BannedEmailProviders = ["banned-domain"],
            EmailConfirmationBaseUri = "confirmation/uri/test",
            EmailConfirmationTokenExpirationTime = 20
        });
        
        var handler = new SignUpCommandHandler(_userRepositoryMock.Object, _valueHasherMock.Object,
            _publisherMock.Object, emailOptions);
        
        // Act & Assert
        await Assert.ThrowsAsync<InvalidEmailProviderException>(() => handler.Handle(command, new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_ShouldThrowEmailAlreadyUsedException_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new SignUpCommand("email@test.com", "username", "password", "password");

        var emailOptions = Options.Create(new EmailOptions
        {
            BannedEmailProviders = [],
            EmailConfirmationBaseUri = "confirmation/uri/test",
            EmailConfirmationTokenExpirationTime = 20
        });
        
        _userRepositoryMock.Setup(x => x.IsUnique(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((false, true));
        
        var handler = new SignUpCommandHandler(_userRepositoryMock.Object, _valueHasherMock.Object,
            _publisherMock.Object, emailOptions);

        // Act & Assert
        await Assert.ThrowsAsync<EmailAlreadyUsedException>(() => handler.Handle(command, new CancellationToken()));
    }

    [Fact]
    public async Task Handle_ShouldThrowUsernameAlreadyUsedException_WhenUsernameIsNotUnique()
    {
        // Arrange
        var command = new SignUpCommand("email@test.com", "username", "password", "password");

        var emailOptions = Options.Create(new EmailOptions
        {
            BannedEmailProviders = [],
            EmailConfirmationBaseUri = "confirmation/uri/test",
            EmailConfirmationTokenExpirationTime = 20
        });
        
        _userRepositoryMock.Setup(
                x => x.IsUnique(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((true, false));
        
        var handler = new SignUpCommandHandler(_userRepositoryMock.Object, _valueHasherMock.Object,
            _publisherMock.Object, emailOptions);

        // Act & Assert
        await Assert.ThrowsAsync<UsernameAlreadyUsedException>(() => handler.Handle(command, new CancellationToken()));
    }
}