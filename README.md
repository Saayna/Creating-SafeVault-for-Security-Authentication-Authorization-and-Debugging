# Creating-SafeVault---for-Security-Authentication-Authorization-and-Debugging-
For Coursera Webdev Course

SafeVault/
├── Program.cs
├── Startup.cs (if using .NET 5 style)
├── appsettings.json
│
├── Models/
│   ├── User.cs
│   ├── UserInput.cs
│   ├── LoginViewModel.cs
│   ├── LoginRequest.cs
│   ├── RefreshToken.cs
│
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── IUserRepository.cs
│   ├── UserRepository.cs
│   ├── MySqlUserRepository.cs
│
├── Services/
│   ├── AuthService.cs
│   ├── InputSanitizer.cs
│   ├── PasswordHasher.cs
│   ├── JwtService.cs
│   ├── RefreshTokenService.cs
│   ├── UserRoleService.cs
│
├── Controllers/
│   ├── AccountController.cs
│   ├── AuthController.cs
│   ├── UserController.cs
│   ├── AdminController.cs
│   ├── DashboardController.cs
│   ├── SecureController.cs
│
├── Middleware/
│   ├── SecurityHeadersExtensions.cs
│
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml
│   │   ├── Register.cshtml
│   ├── Shared/
│       ├── _Layout.cshtml
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   ├── webform.html
│
├── Tests/
│   ├── TestInputValidation.cs
│   ├── TestSqlInjection.cs
│   ├── AuthServiceTests.cs
│   ├── AuthorizationPolicyTests.cs
│   ├── SecurityVulnerabilityTests.cs
│   ├── SecurityIntegrationTests.cs
│   ├── FakeUserRepository.cs
│   ├── Tokens.cs
│   ├── JwtTestHelper.cs
│   ├── SecurityMessages.cs
│
├── Migrations/
│   ├── CreateIdentitySchema.cs
│
└── Properties/
    ├── launchSettings.json
