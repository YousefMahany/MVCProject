namespace RouteG04.PL.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Please Enter Password!!")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password Is Required!!")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
