namespace EducationHub.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services;
    using Web.Infrastructure.ValidationAttributes;

    using static Data.Common.Validations.DataValidation.User;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ICloudinaryService cloudinaryService;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ICloudinaryService cloudinaryService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.cloudinaryService = cloudinaryService;
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

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            var username = user.UserName;
            var pictureUrl = this.Input.PictureUrl;

            if (this.Input.PhoneNumber != phoneNumber
                || username != this.Input.Username
                || pictureUrl != user.PictureUrl
                || this.Input.Image != null)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                var setUsernameResult = await this.userManager.SetUserNameAsync(user, this.Input.Username);

                if (this.Input.Image != null)
                {
                    pictureUrl = await this.cloudinaryService.ImageUploadAsync(this.Input.Image);
                }

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
            [StringLength(UsernameMaxLenght, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = UsernameMinLenght)]
            public string Username { get; set; }

            [MaxLength(PictureUrlMaxLength)]
            [Display(Name ="Picture URL")]
            public string PictureUrl { get; set; }

            [ImageSizeInMBValidation(3)]
            public IFormFile Image { get; set; }
        }
    }
}
