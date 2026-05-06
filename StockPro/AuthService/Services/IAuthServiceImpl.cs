using AuthService.Entities;
using AuthService.Repositories;
using BCrypt.Net;
using AuthService.DTOs;
using System.Security.Claims;

public class AuthServiceImpl : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthServiceImpl(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string> RegisterAsync(RegisterDTO dto)
    {
        var exists = await _userRepository.ExistsByEmailAsync(dto.Email);

        if (exists)
            throw new Exception("User already exists");

        var user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "STAFF",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        return "User registered successfully";
    }

    public async Task<string> LoginAsync(LoginDTO dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
            throw new Exception("User not found");

        if (!user.IsActive)
            throw new Exception("User is deactivated");

        bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!isValid)
            throw new Exception("Invalid credentials");

        // Update last login
        user.LastLoginAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        // Generate JWT
        var token = _jwtService.GenerateToken(user);

        return token;
    }

    public string Logout(string token)
    {
        return "Logged out successfully";
    }

    public async Task<string> ChangePasswordAsync(Guid userId, ChangePasswordDTO dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        bool isValid = BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.PasswordHash);

        if (!isValid)
            throw new Exception("Old password incorrect");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

        await _userRepository.UpdateAsync(user);

        return "Password updated successfully";
    }

    public async Task<string> UpdateProfileAsync(Guid userId, UpdateUserDTO dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        user.FullName = dto.FullName;
        user.Phone = dto.Phone;

        await _userRepository.UpdateAsync(user);

        return "Profile updated successfully";
    }

    public async Task<UserDTO> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        return new UserDTO
        {
            UserId = user.UserId,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
        };
    }

    // ================= GET ALL USERS =================
    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(u => new UserDTO
        {
            UserId = u.UserId,
            Email = u.Email,
            FullName = u.FullName,
            Role = u.Role,
            Phone = u.Phone
        }).ToList();
    }

    public async Task<string> DeactivateUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        user.IsActive = false;

        await _userRepository.UpdateAsync(user);

        return "User deactivated successfully";
    }

    public async Task<string> RefreshTokenAsync(string token)
    {
        // Clean the token first
        if (string.IsNullOrEmpty(token))
            throw new Exception("Token is required");

        // Remove "Bearer " prefix if present
        token = token.Replace("Bearer ", "").Trim();

        var principal = _jwtService.GetPrincipalFromExpiredToken(token);

        // Get email from claims - FIXED: Use ClaimTypes.Email instead of Name
        var email = principal.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
            throw new Exception("Invalid token: email claim not found");

        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            throw new Exception("User not found");

        if (!user.IsActive)
            throw new Exception("User is deactivated");

        return _jwtService.GenerateToken(user);
    }
}