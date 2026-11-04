using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StaySharp.Models;
using StaySharp.Security;

namespace StaySharp.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(StaySharpContext context, IPasswordHasher<Usuario> hasher)
        {
            const string email = "admin@staysharp.com";

            if (await context.Usuarios.AnyAsync(u => u.Email == email))
                return;

            var admin = new Usuario
            {
                Nome = "Administrador",
                Email = email,
                Perfil = Perfis.Administrador,
                Ativo = true
            };
            admin.SenhaHash = hasher.HashPassword(admin, "Admin@123");

            context.Usuarios.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}
