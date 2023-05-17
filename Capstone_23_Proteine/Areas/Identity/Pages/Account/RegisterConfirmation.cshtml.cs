// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using Capstone_23_Proteine.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Capstone_23_Proteine.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _sender;

        public RegisterConfirmationModel(UserManager<IdentityUser> userManager, IEmailSender sender)
        {
            _userManager = userManager;
            _sender = sender;
        }

        public string Email { get; set; }
        public bool DisplayConfirmAccountLink { get; set; }
        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null, string sendGridApiKey = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;

            // Send email confirmation
            string emailSubject = "Contact Confirmation";
            string emailMessage = "Dear " + email + "\n" +
                "We received your message. Thank you for contacting us. \n" +
                "Our team will be in contact with you very soon. \n" +
                "Best Regards\n\n" +
                "https://localhost:7116/Identity/Account/ConfirmEmail";

            if (sendGridApiKey != null)
            {
                // Create an instance of EmailSender with the provided SendGrid API key
                var emailSender = new EmailSender(sendGridApiKey);
                await emailSender.SendEmailAsync(email, emailSubject, emailMessage);
            }
           /* else
            {
                // Handle the case when the SendGrid API key is null
                // You can choose to log an error, display a message, or take any other appropriate action
                // For example, throw an exception:
                throw new ArgumentNullException(nameof(sendGridApiKey), "SendGrid API key is null.");
            }*/

            return Page();
        }
    }
}
