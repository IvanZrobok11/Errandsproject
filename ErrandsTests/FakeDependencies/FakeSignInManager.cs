using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Errands.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ErrandsTests.FakeDependencies
{
    public class FakeSignInManager : SignInManager<User>
    {
        internal const string ValidUser = "Valid@valid.com";
        internal const string TwoFactorRequired = "TwoFactor@invalid.com";
        internal const string LockedOutUser = "Locked@invalid.com";
        public FakeSignInManager()
            : base(
                new FakeUserManager(),
                new Mock<IHttpContextAccessor >().Object,
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object)
        {
        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            if (userName == ValidUser && password == ValidUser)
            {
                return Task.FromResult(SignInResult.Success);
            }

            if (userName == TwoFactorRequired && password == TwoFactorRequired)
            {
                return Task.FromResult(SignInResult.TwoFactorRequired);
            }

            if (userName == LockedOutUser && password == LockedOutUser)
            {
                return Task.FromResult(SignInResult.LockedOut);
            }

            return Task.FromResult(SignInResult.Failed);
        }
    }
}
