# Creating-SafeVault---for-Security-Authentication-Authorization-and-Debugging-
For Coursera Webdev Course

Summary of what was done :

1. Secure Code for Input Validation & SQL Injection Prevention
Implemented DataAnnotations and regex allowlists for usernames, emails, and passwords.
Replaced unsafe string concatenation in queries with parameterized SQL statements (SqlCommand / MySqlCommand).
Added InputSanitizer service to enforce validation rules consistently across controllers.

2. Authentication & Authorization (RBAC)
Integrated ASP.NET Core Identity with secure password hashing (PBKDF2).
Configured JWT tokens with short lifetimes and refresh token rotation.
Defined role-based policies (Admin, User, Guest) and applied [Authorize(Roles="...")] attributes to endpoints.
Added UserRoleService to seed roles and assign/remove them safely.

3. Debug & Resolve Security Vulnerabilities
Identified insecure patterns:
SQL injection via string concatenation.
XSS via unencoded user-generated content.

Fixed by:
Using parameterized queries everywhere.
Enforcing output encoding in Razor views (@Html.Encode).
Returning generic error messages to prevent user enumeration.
Adding lockout policies to mitigate brute-force attacks.


4. Generate & Execute Security Tests
Unit tests for password hashing, input validation, and authentication logic.
Integration tests simulating:
SQL injection attempts (' OR '1'='1).
XSS payloads (<script>alert('XSS')</script>).
Role-based access (Admin vs User vs Guest).

Verified that malicious inputs are blocked and valid inputs succeed.

5. Code Optimizations & Outputs
Enforced HTTPS redirection + HSTS with secure headers (CSP, X-Frame-Options, X-Content-Type-Options).
Added structured logging for login attempts and access events.
Optimized JWT handling with short-lived access tokens and refresh token rotation.
Clean project structure with separation of Models, Data, Services, Controllers, Middleware, Views, Tests.
Output: A secure, tested, and maintainable ASP.NET Core application resilient against common web vulnerabilities.
