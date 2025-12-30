using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class UserRoleService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<UserRoleService> _logger;

    public UserRoleService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<UserRoleService> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    // Seed baseline roles (Admin, User, Guest)
    public async Task SeedDefaultRolesAsync()
    {
        var roles = new[] { "Admin", "User", "Guest" };
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                {
                    _logger.LogError("Failed creating role {Role}: {Errors}", role, string.Join(", ", result.Errors.Select(e => e.Description)));
                    throw new InvalidOperationException($"Failed to create role {role}");
                }
            }
        }
    }

    // Assign a role to a user
    public async Task AssignRoleAsync(string username, string role)
    {
        var user = await _userManager.FindByNameAsync(username)
            ?? throw new InvalidOperationException($"User '{username}' not found.");

        if (!await _roleManager.RoleExistsAsync(role))
            throw new InvalidOperationException($"Role '{role}' does not exist.");

        if (await _userManager.IsInRoleAsync(user, role)) return;

        var result = await _userManager.AddToRoleAsync(user, role);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Failed assigning role {Role} to {User}: {Errors}", role, username, string.Join(", ", result.Errors.Select(e => e.Description)));
            throw new InvalidOperationException($"Failed to assign role '{role}' to '{username}'.");
        }
        _logger.LogInformation("Assigned role {Role} to {User}", role, username);
    }

    // Remove a role from a user
    public async Task RemoveRoleAsync(string username, string role)
    {
        var user = await _userManager.FindByNameAsync(username)
            ?? throw new InvalidOperationException($"User '{username}' not found.");

        if (!await _userManager.IsInRoleAsync(user, role)) return;

        var result = await _userManager.RemoveFromRoleAsync(user, role);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Failed removing role {Role} from {User}: {Errors}", role, username, string.Join(", ", result.Errors.Select(e => e.Description)));
            throw new InvalidOperationException($"Failed to remove role '{role}' from '{username}'.");
        }
        _logger.LogInformation("Removed role {Role} from {User}", role, username);
    }

    // Replace all roles for a user (idempotent)
    public async Task SetRolesAsync(string username, IEnumerable<string> roles)
    {
        var user = await _userManager.FindByNameAsync(username)
            ?? throw new InvalidOperationException($"User '{username}' not found.");

        // Ensure roles exist
        foreach (var role in roles.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            if (!await _roleManager.RoleExistsAsync(role))
                throw new InvalidOperationException($"Role '{role}' does not exist.");
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        var toAdd = roles.Except(currentRoles, StringComparer.OrdinalIgnoreCase).ToArray();
        var toRemove = currentRoles.Except(roles, StringComparer.OrdinalIgnoreCase).ToArray();

        if (toRemove.Length > 0)
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, toRemove);
            if (!removeResult.Succeeded)
                throw new InvalidOperationException($"Failed to remove roles: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");
        }

        if (toAdd.Length > 0)
        {
            var addResult = await _userManager.AddToRolesAsync(user, toAdd);
            if (!addResult.Succeeded)
                throw new InvalidOperationException($"Failed to add roles: {string.Join(", ", addResult.Errors.Select(e => e.Description))}");
        }

        _logger.LogInformation("Set roles for {User}: {Roles}", username, string.Join(", ", roles));
    }

    // Query helpers
    public async Task<IReadOnlyList<string>> GetUserRolesAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username)
            ?? throw new InvalidOperationException($"User '{username}' not found.");
        var roles = await _userManager.GetRolesAsync(user);
        return roles;
    }

    public async Task<bool> IsInRoleAsync(string username, string role)
    {
        var user = await _userManager.FindByNameAsync(username)
            ?? throw new InvalidOperationException($"User '{username}' not found.");
        return await _userManager.IsInRoleAsync(user, role);
    }

    // Ensure user has at least one role (assign Guest by default)
    public async Task EnsureDefaultUserRoleAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username)
            ?? throw new InvalidOperationException($"User '{username}' not found.");

        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Count == 0)
        {
            await AssignRoleAsync(username, "Guest");
        }
    }
}
