using Finanzas_Personales_App.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;



namespace Finanzas_Personales_App.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{

		private readonly FinanzasDbContext _DbContext;

		public AccountController(FinanzasDbContext context)
		{
			_DbContext = context;
		}
		public IActionResult Login()
		{
			var redirectUrl = Url.Action("GoogleResponse", "Account");
			var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
			return Challenge(properties, GoogleDefaults.AuthenticationScheme);
		}

		public async Task<IActionResult> GoogleResponse()
		{
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// Extraer datos del perfil de Google
			var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
			var nombre = result.Principal.FindFirst(ClaimTypes.Name)?.Value;
			var sub = result.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(email))
			{
				return Unauthorized(); // Fallback defensivo
			}

			// Buscar usuario por email
			var usuario = _DbContext.Usuarios.FirstOrDefault(u => u.Email == email);
			if (usuario == null)
			{
				usuario = new Usuario
				{
					IdUsuario = Guid.NewGuid(), // GUID propio del sistema
					Email = email,
					Nombre = nombre
				};
				_DbContext.Usuarios.Add(usuario);
				_DbContext.SaveChanges();
			}

			// Crear lista de claims personalizados
			var claims = new List<Claim>
	{
		new Claim(ClaimTypes.Name, nombre ?? ""),
		new Claim(ClaimTypes.Email, email),
		new Claim("UsuarioId", usuario.IdUsuario.ToString()) // ← lo importante
    };

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			// Guardar en cookie de autenticación
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

			return RedirectToAction("Index", "Home");
		}


		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}

}
