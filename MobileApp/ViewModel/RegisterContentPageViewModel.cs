﻿using MobileApp.Handler.AppConfig;
using MobileApp.Model;
using MobileApp.Service;
using MobileApp.Validation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Shared.DataTransferObject;
using Shared.Infrastructure.Backend.Api;

namespace MobileApp.ViewModel
{
    public class RegisterContentPageViewModel : BaseViewModel
    {
        private CancellationTokenSource _webApiActionCancelationToken = new CancellationTokenSource();
        private ObservableCollection<LanguageModel> _languages = new ObservableCollection<LanguageModel>();
        public ObservableCollection<LanguageModel> Languages
        {
            get
            {
                return _languages;
            }
            set
            {
                _languages = value;
            }
        }


        public ValidatableObject<string> ActivationCodePart1 { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> ActivationCodePart2 { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> ActivationCodePart3 { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> ActivationCodePart4 { get; private set; } = new ValidatableObject<string>();
        public string FullActivationCode => ActivationCodePart1.Value + ActivationCodePart2.Value + ActivationCodePart3.Value + ActivationCodePart4.Value;
        private string ActivationToken { get; set; }

        public ValidatableObject<string> Firstname { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Lastname { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Email { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<DateTime> DateOfBirth { get; private set; } = new ValidatableObject<DateTime>() { Value = DateTime.Now };
        public ValidatableObject<string> Password { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> PasswordRepeat { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> PhonePrefix { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Phone { get; private set; } = new ValidatableObject<string>();
        private bool _isLoading;
        private bool _showPassword;
        private bool _isNowScuccessfullyRegistered;
        private bool _isActivated;
        private bool _isActivationError;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public bool ShowPassword
        {
            get
            {
                return _showPassword;
            }
            set
            {
                _showPassword = value;
                OnPropertyChanged(nameof(ShowPassword));
            }
        }
        public bool IsNowScuccessfullyRegistered
        {
            get
            {
                return _isNowScuccessfullyRegistered;
            }
            set
            {
                _isNowScuccessfullyRegistered = value;
                OnPropertyChanged(nameof(IsNowScuccessfullyRegistered));
                OnPropertyChanged(nameof(IsNotActivatedButRegistered));
            }
        }
        public bool IsActivated
        {
            get
            {
                return _isActivated;
            }
            set
            {
                _isActivated = value;
                OnPropertyChanged(nameof(IsActivated));
                OnPropertyChanged(nameof(IsNotActivatedButRegistered));
            }
        }
        public bool IsActivationError
        {
            get
            {
                return _isActivationError;
            }
            set
            {
                _isActivationError = value;
                OnPropertyChanged(nameof(IsActivationError));
            }
        }
        public bool IsNotActivatedButRegistered
        {
            get
            {
                return IsNowScuccessfullyRegistered&&!IsActivated;
            }
        }
        private readonly ApplicationConfigHandler _applicationConfigHandler;
        private readonly NavigationService _navigationService;
        public ICommand SubmitCommand { get; private set; }
        public ICommand ReturnToLoginCommand { get; private set; }
        public ICommand ValidateByValueChangeCommand { get; private set; }
        public ICommand ActivateAccountCommand { get; private set; }
        public ICommand ResendActivationMailCommand { get; private set; }
        public ICommand SendNewActivationCodeCommand { get; private set; }
        public RegisterContentPageViewModel(
            ApplicationConfigHandler applicationConfigHandler,
            NavigationService navigationService)
        {
            _applicationConfigHandler = applicationConfigHandler;
            _navigationService  = navigationService;    
            _languages.Add(new LanguageModel { Country = "Deutschland", PhonePrefix = "+49" });
            _languages.Add(new LanguageModel { Country = "Österrreich", PhonePrefix = "+48" });
            AddValidations();
            ValidateByValueChangeCommand = new RelayCommand<string>(ValidateUiVoid);
            SubmitCommand = new RelayCommand<bool>(SubmitAction);
            ReturnToLoginCommand = new RelayCommand(ReturnToLoginAction);
            ResendActivationMailCommand = new RelayCommand(ResendActivationMailAction);
            ActivateAccountCommand = new RelayCommand(ActivateAccountAction);
            SendNewActivationCodeCommand = new RelayCommand(SendNewActivationCodeAction);
            ActivationToken = _applicationConfigHandler.ApplicationConfig.AccountConfig.RegisterBase64Token;
            IsNowScuccessfullyRegistered = !String.IsNullOrEmpty(ActivationToken);
        }
        private void SendNewActivationCodeAction()
        {

            SubmitAction(true);
        }
        private async void ActivateAccountAction()
        {
            bool validateCode = ValidateCodes();
            if (!validateCode)
                return;


        }
        private void ResendActivationMailAction()
        {
            SubmitAction(true);
        }
        private void ReturnToLoginAction()
        {
            if(IsActivated)
            {
                _navigationService.CloseCurrentPage();
                IsActivated = false;
                IsNowScuccessfullyRegistered = false;
                ActivationCodePart1.Value = null;
                ActivationCodePart2.Value = null;
                ActivationCodePart3.Value = null;
                ActivationCodePart4.Value = null;
                IsActivationError = false;
                Firstname.Value = null;
                Lastname.Value = null;
                Email.Value = null;
                DateOfBirth.Value = DateTime.Now;
                Password.Value = null;
                PasswordRepeat.Value = null;
                PhonePrefix.Value = null;
                Phone.Value = null;

    }
        }
        private async void SubmitAction(bool resend=false)
        {
            IsLoading = true;
            bool validEntries = ValidateUi();
            if (validEntries)
            {
                RegisterUserDTO registerDataTransferModel = new RegisterUserDTO();
                registerDataTransferModel.FirstName = Firstname.Value;
                registerDataTransferModel.LastName =Lastname.Value;
                registerDataTransferModel.Email =Email.Value;
                registerDataTransferModel.DateOfBirth =DateOfBirth.Value;
                registerDataTransferModel.Phone =PhonePrefix.Value+Phone.Value;
                registerDataTransferModel.Password =Password.Value;

            }
            IsLoading = false;
        }
        private bool ValidateUi(int checkUiElemt = -1)
        {
            bool isValidEmail = false;
            bool isValidPassword = false;
            bool isValidPasswordRepeat = false;
            bool isValidFirstname = false;  
            bool isValidLastname = false;
            bool isValidPhonenumber = false;
            bool isDateValid = false;
            switch (checkUiElemt)
            {
                case 1:
                    isValidEmail = ValidateEmail();
                    break;
                case 2:
                    isValidPassword = ValidatePassword();
                    isValidPasswordRepeat = ValidatePasswordRepeat();
                    break;
                case 3:
                    isValidPasswordRepeat = ValidatePasswordRepeat();
                    isValidPassword = ValidatePassword();
                    break;
                case 4:
                    isValidFirstname = ValidateFirstname();
                    break;
                case 5:
                    isValidLastname = ValidateLastname();
                    break;
                case 6:
                    isValidPhonenumber = ValidatePhone();
                    break;
                default:
                    isValidEmail = ValidateEmail();
                    isValidPassword = ValidatePassword();
                    isValidPasswordRepeat = ValidatePasswordRepeat();
                    isDateValid = ValidateDate();
                    isValidFirstname = ValidateFirstname();
                    isValidLastname = ValidateLastname();
                    isValidPhonenumber = ValidatePhone();
                    break;
            }
            return isValidEmail && isValidPassword && isValidPasswordRepeat && isValidFirstname && isValidLastname && isValidPhonenumber&& isDateValid;
        }
        private void ValidateUiVoid(string checkUiElemt)
        {
            if (int.TryParse(checkUiElemt, out int value))
            {
                ValidateUi(value);

            }

        }
        private bool ValidateDate()
        {
            return DateOfBirth.Validate();
        }
        private bool ValidateEmail()
        {
            return Email.Validate();
        }

        private bool ValidatePassword()
        {
            return Password.Validate();
        }
        private bool ValidatePasswordRepeat()
        {
            return PasswordRepeat.Validate();
        }
        private bool ValidateFirstname()
        {
            return Firstname.Validate();
        }
        private bool ValidateLastname()
        {
            return Lastname.Validate();
        }
        private bool ValidatePhone()
        {
            return PhonePrefix.Validate() && Phone.Validate();
        }
        private bool ValidateCodes()
        {
            return ActivationCodePart1.Validate() && ActivationCodePart2.Validate() && ActivationCodePart3.Validate() && ActivationCodePart4.Validate();
        }
        private void AddValidations()
        {
            Firstname.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Firstname entry is required."
            }); 
            Lastname.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Lastname entry is required."
            });
            Email.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Email entry is required."
            });
            Email.Validations.Add(new EmailRule
            {
                ValidationMessage = "Email is invalid."
            });
            DateOfBirth.Validations.Add(new IsValidDate { ValidationMessage="You must select a birth date."});

            Phone.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Phonenumber entry is required."
            });
            PhonePrefix.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Phoneprefix entry is required."
            });

            Password.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Password entry is required."
            });
            Password.Validations.Add(new ValueMustBeEqualRule<string>(PasswordRepeat)
            {
                ValidationMessage = "Password entry is not equal than Password repeat."
            });
            PasswordRepeat.Validations.Add(new IsNotNullOrEmptyRule()
            {
                ValidationMessage = "Password entry is required."
            });
            PasswordRepeat.Validations.Add(new ValueMustBeEqualRule<string>(Password)
            {
                ValidationMessage = "Password repeat entry is not equal than Password."
            });
            ActivationCodePart1.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Code is required."
            });
            ActivationCodePart2.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Code is required."
            });
            ActivationCodePart3.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Code is required."
            });
            ActivationCodePart4.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Code is required."
            });
        }
    }
}
