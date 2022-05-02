using JosephHungerman.Identity.Models;
using JosephHungerman.Identity.Repositories;
using Microsoft.AspNetCore.Identity;

namespace JosephHungerman.Identity.Services;

public class ApplicationUserValidator : IUserValidator<ApplicationUser>
{
    private readonly IUnitOfWork _unitOfWork;

    public ApplicationUserValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
    {
        IList<Email>? whitelist = await _unitOfWork.WhitelistRepository.GetAsync(wl => wl.Address == user.Email);

        var validatedUser = !whitelist.Any() ? null : whitelist.First();

        if (validatedUser != null)
        {
            return IdentityResult.Success;
        }

        return IdentityResult.Failed(new IdentityError
        {
            Code = "UnknownMember",
            Description = "Provided email is not a valid admin user"
        });
    }
}