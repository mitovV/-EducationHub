namespace EducationHub.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Data.Models;
    using Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using static Data.Common.Validations.DataValidation.User;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public string Username { get; set; }

        public string PictureUrl { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            // TODO: Implement uploading
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            var username = user.UserName;
            var pictureUrl = this.Input.PictureUrl;

            if (this.Input.PhoneNumber != phoneNumber || username != this.Input.Username || pictureUrl != user.PictureUrl)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                var setUsernameResult = await this.userManager.SetUserNameAsync(user, this.Input.Username);
                user.PictureUrl = pictureUrl;

                await this.userManager.UpdateAsync(user);
                if (!setPhoneResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set phone number.";
                    return this.RedirectToPage();
                }

                if (!setUsernameResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set Username. Try with different one.";
                    return this.RedirectToPage();
                }
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";
            return this.RedirectToPage();
        }

        private async Task LoadAsync(User user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            this.Username = userName;
            this.PictureUrl = user.PictureUrl;

            this.Input = new InputModel
            {
                PhoneNumber = phoneNumber,
            };
        }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
            public string Username { get; set; }

            [MaxLength(PictureUrlMaxLength)]
            public string PictureUrl { get; set; }

            [ImageSizeInMBValidation(3)]
            public IFormFile Image { get; set; }
        }
    }
}
