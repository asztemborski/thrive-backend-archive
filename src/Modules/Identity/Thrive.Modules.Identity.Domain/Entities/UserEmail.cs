using Thrive.Modules.Identity.Domain.Exceptions;
using Thrive.Modules.Identity.Domain.ValueObjects;

namespace Thrive.Modules.Identity.Domain.Entities;

public sealed class UserEmail
{
    public Email Address { get; private set; } = null!;
    public bool IsConfirmed { get; private set; }

    private UserEmail()
    {
    }

    public UserEmail(Email address)
    {
        Address = address;
    }

    public void ChangeAddress(string newAddress)
    {
        if (Address == newAddress)
        {
            throw new SameEmailException();
        }

        Address = newAddress;
    }

    public void Confirm()
    {
        if (IsConfirmed)
        {
            throw new EmailAlreadyConfirmedException();
        }

        IsConfirmed = true;
    }
}