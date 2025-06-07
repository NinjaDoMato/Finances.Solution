using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Finances.Database.Context;
using Finances.Database.Entities;
using Finances.APP.Models.Account;
using Microsoft.AspNetCore.Authorization;

namespace Finances.APP.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Email e senha são obrigatórios.");
                return View();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
                return View();
            }

            // Atualiza o último login
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize]
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    LastLoginAt = u.LastLoginAt,
                    DateCreated = u.DateCreated
                })
                .ToListAsync();

            return View(users);
        }

        [Authorize]
        public IActionResult CreateUser()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Este email já está em uso.");
                    return View(model);
                }

                var user = new User
                {
                    Email = model.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                TempData["success"] = "Usuário criado com sucesso.";
                return RedirectToAction(nameof(Users));
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> EditUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                if (await _context.Users.AnyAsync(u => u.Email == model.Email && u.Id != model.Id))
                {
                    ModelState.AddModelError("Email", "Este email já está em uso.");
                    return View(model);
                }

                user.Email = model.Email;
                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                }

                await _context.SaveChangesAsync();

                TempData["success"] = "Usuário atualizado com sucesso.";
                return RedirectToAction(nameof(Users));
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            TempData["success"] = "Usuário excluído com sucesso.";
            return RedirectToAction(nameof(Users));
        }
    }
}